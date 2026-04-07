using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace UdpServerLib
{
    /// <summary>
    /// 수신된 UDP 데이터 이벤트 인자
    /// </summary>
    public class UdpDataReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; }
        public IPEndPoint RemoteEndPoint { get; }
        public DateTime ReceivedAt { get; }

        public UdpDataReceivedEventArgs(byte[] data, IPEndPoint remoteEndPoint)
        {
            Data = data;
            RemoteEndPoint = remoteEndPoint;
            ReceivedAt = DateTime.Now;
        }
    }

    /// <summary>
    /// 에러 이벤트 인자
    /// </summary>
    public class UdpServerErrorEventArgs : EventArgs
    {
        public Exception Exception { get; }
        public string Message { get; }

        public UdpServerErrorEventArgs(Exception ex, string message = null)
        {
            Exception = ex;
            Message = message ?? ex.Message;
        }
    }

    /// <summary>
    /// 범용 UDP 서버 모듈
    /// </summary>
    public class UdpServer : IDisposable
    {
        // ─── 이벤트 ───────────────────────────────────────────
        public event EventHandler<UdpDataReceivedEventArgs> DataReceived;
        public event EventHandler<UdpServerErrorEventArgs> ErrorOccurred;
        public event EventHandler Started;
        public event EventHandler Stopped;

        // ─── 속성 ─────────────────────────────────────────────
        public int Port { get; private set; }
        public bool IsRunning { get; private set; }
        public int BufferSize { get; set; } = 65535;
        public string LocalAddress { get; private set; } = "0.0.0.0";

        // ─── 내부 필드 ────────────────────────────────────────
        private UdpClient _udpClient;
        private CancellationTokenSource _cts;
        private Task _receiveTask;
        private readonly object _lock = new object();
        private bool _disposed;

        // ─── 생성자 ───────────────────────────────────────────
        public UdpServer() { }

        public UdpServer(int port) { Port = port; }

        public UdpServer(string localAddress, int port)
        {
            LocalAddress = localAddress;
            Port = port;
        }

        // ─── 서버 시작 ────────────────────────────────────────
        /// <summary>포트를 지정해서 서버 시작</summary>
        public void Start(int port)
        {
            Port = port;
            Start();
        }

        /// <summary>서버 시작 (Port 프로퍼티 사전 설정 필요)</summary>
        public void Start()
        {
            lock (_lock)
            {
                if (IsRunning)
                    throw new InvalidOperationException("서버가 이미 실행 중입니다.");

                if (Port <= 0 || Port > 65535)
                    throw new ArgumentOutOfRangeException(nameof(Port), "포트 번호는 1~65535 사이여야 합니다.");

                try
                {
                    var endpoint = new IPEndPoint(IPAddress.Parse(LocalAddress), Port);
                    _udpClient = new UdpClient(endpoint);
                    _udpClient.Client.ReceiveBufferSize = BufferSize;

                    _cts = new CancellationTokenSource();
                    IsRunning = true;

                    _receiveTask = Task.Run(() => ReceiveLoopAsync(_cts.Token), _cts.Token);

                    Started?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    IsRunning = false;
                    _udpClient?.Close();
                    throw new InvalidOperationException($"서버 시작 실패: {ex.Message}", ex);
                }
            }
        }

        // ─── 서버 중지 ────────────────────────────────────────
        public void Stop()
        {
            lock (_lock)
            {
                if (!IsRunning) return;

                IsRunning = false;
                _cts?.Cancel();
                _udpClient?.Close();

                try { _receiveTask?.Wait(3000); } catch { /* ignore */ }

                _cts?.Dispose();
                _cts = null;

                Stopped?.Invoke(this, EventArgs.Empty);
            }
        }

        // ─── 데이터 송신 ──────────────────────────────────────
        /// <summary>특정 클라이언트에게 데이터 전송</summary>
        public int Send(byte[] data, IPEndPoint targetEndPoint)
        {
            if (!IsRunning)
                throw new InvalidOperationException("서버가 실행 중이 아닙니다.");
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));

            return _udpClient.Send(data, data.Length, targetEndPoint);
        }

        /// <summary>특정 클라이언트에게 데이터 전송 (비동기)</summary>
        public async Task<int> SendAsync(byte[] data, IPEndPoint targetEndPoint)
        {
            if (!IsRunning)
                throw new InvalidOperationException("서버가 실행 중이 아닙니다.");
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));

            return await _udpClient.SendAsync(data, data.Length, targetEndPoint);
        }

        /// <summary>브로드캐스트 전송</summary>
        public int Broadcast(byte[] data, int port)
        {
            if (!IsRunning)
                throw new InvalidOperationException("서버가 실행 중이 아닙니다.");

            _udpClient.EnableBroadcast = true;
            var broadcastEP = new IPEndPoint(IPAddress.Broadcast, port);
            return _udpClient.Send(data, data.Length, broadcastEP);
        }

        // ─── 수신 루프 ────────────────────────────────────────
        private async Task ReceiveLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var result = await _udpClient.ReceiveAsync();
                    if (token.IsCancellationRequested) break;

                    // 이벤트 발생 (별도 스레드로 처리하여 수신 루프 블로킹 방지)
                    var args = new UdpDataReceivedEventArgs(result.Buffer, result.RemoteEndPoint);
                    _ = Task.Run(() => DataReceived?.Invoke(this, args), token);
                }
                catch (ObjectDisposedException)
                {
                    break; // 소켓이 닫힌 경우 정상 종료
                }
                catch (SocketException ex) when (token.IsCancellationRequested)
                {
                    break; // 취소로 인한 종료
                }
                catch (Exception ex)
                {
                    if (!token.IsCancellationRequested)
                        ErrorOccurred?.Invoke(this, new UdpServerErrorEventArgs(ex));
                }
            }
        }

        // ─── Dispose ─────────────────────────────────────────
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            Stop();
            _udpClient?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
