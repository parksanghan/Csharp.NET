using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TCPServerSkleton.MSocket;

namespace ConsoleApp3.MSocket
{
   
    public class MSocketServer
    {
        private readonly   Socket serverSocket;
        private CancellationTokenSource? cts; // 비동기 관리 객체 
        private readonly List<Socket> clients;
        public delegate void LogDelegate(Socket clientSocket , MSocketProperty mSocketProperty ); //  일반로그 대리자
        public delegate void ErrorDelegate(Socket clientSocket, Exception ex, MSocketProperty mSocketProperty); // 에러로그 대리자
                                                                                
        // 대리자 인스턴스 선언
        public LogDelegate logDelegate;
        public ErrorDelegate errorDelegate;

        // 함수포인터 대리자 외부 초기화
        public void SetDelegate(Action<Socket, MSocketProperty> logAction, Action<Socket, Exception , MSocketProperty> errAction)
        {
            this.logDelegate = (sock,state) => logAction(sock, state);
            this.errorDelegate = (sock, ex, state) => errAction(sock, ex, state);
        }
        // 함수포인터 대리자 내부 초기화 
        private  void SetDelegate()
        {   

        }
        public MSocketServer(Socket serverSocket)
        {
            this.serverSocket = serverSocket ?? throw new ArgumentNullException(nameof(serverSocket));

            clients = new List<Socket>();   
        }
        public void RemoveClient(Socket socket)
        {
            lock (clients)
            {
                if (clients.Contains(socket))
                {
                    clients.Remove(socket);
                    socket.Close();
                }
            }
        }
        public void AddClient(Socket socket)
        {
            lock (clients)
            {
                if (!clients.Contains(socket))
                {
                    clients.Add(socket);
                }
            }

        }   
        public static MSocketServer Create()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            return new MSocketServer(serverSocket);
        }
        public void SocketInit(string address)
        {
            serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            serverSocket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            serverSocket.Bind(System.Net.IPEndPoint.Parse(address));
        }
        public void StartListening(int backlog=100                  )
        {
            serverSocket.Listen(backlog);
            Console.WriteLine("Server is listening...");
            cts = new CancellationTokenSource();// 비동기 관리 객체 초기화

            Task.Run(() =>AcceptLoopClient(cts.Token));
        }
        public void AcceptLoopClient(CancellationToken token)
        {

            while (!token.IsCancellationRequested)
            {
                try
                {
                    Socket clientSocket = serverSocket.Accept();
                    AddClient(clientSocket);
                    Console.WriteLine("Client connected.");
                    // 호출 뒤에 각 쓰레드에 할당
                    Thread thread = new Thread(() => HandleClient(clientSocket))
                    {
                        IsBackground = true
                    };
                    thread.Start();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Socket exception: {ex.Message}");

                }
                catch (ObjectDisposedException)
                {
                    // 서버 소켓이 닫혔을 때 발생할 수 있음
                    break;
                }
            }
          
        }
        private void HandleClient(Socket clientSocket)
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int received = clientSocket.Receive(buffer);
                    if (received == 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, received);
                    Console.WriteLine($"[Client {clientSocket.RemoteEndPoint}] {msg}");

                    // 에코 응답
                    byte[] data = Encoding.UTF8.GetBytes("Echo: " + msg);
                    clientSocket.Send(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WARN] Client error: {ex.Message}");
                RemoveClient(clientSocket);
            }
            finally
            {
                Console.WriteLine($"[INFO] Client disconnected: {clientSocket.RemoteEndPoint}");
                clientSocket.Close();
            }
        }

        public void Stop()
        {
            serverSocket.Close();
            Console.WriteLine("Server stopped.");
            clients.Clear();
        }
    }
}
