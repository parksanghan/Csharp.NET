using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVVMServer.SocketNet
{
    internal class SocketServer
    {

        //readonly 란 생성자에서만 값을 할당할 수 있으며, 그 이후에는 값을 변경할 수 없는 필드를 선언할 때 사용됩니다.
        private readonly Socket _serverSocket;
        public List<Socket> Sockets = new List<Socket>();

        private static SocketServer Instance { get; }

        public static SocketServer GetInstance()
        {
            if (Instance == null)
            {
                return new SocketServer();
            }
            return Instance;
        }

        public SocketServer()
        {
            //interNetwork: IPv4 주소 체계를 사용합니다.
            //Stream: 연결 지향적이고 신뢰할 수 있는 바이트 스트림을 제공합니다. TCP 프로토콜에 적합합니다.
            //Tcp: 전송 제어 프로토콜(TCP)을 사용합니다.
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPAddress.Any: 모든 네트워크 인터페이스에서 들어오는 연결을 수신 대기합니다.
            _serverSocket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 5000));
            _serverSocket.Listen(10); //최대 10개의 대기 연결을 허용합니다.
        }
        public void Start()
        {
            Console.WriteLine("서버 시작됨, 클라이언트 연결 대기 중...");
            while (true)
            {
                Socket client = _serverSocket.Accept();
                Sockets.Add(client);
                Console.WriteLine("클라이언트 연결됨: " + client.RemoteEndPoint.ToString());

            }

        }
        public void Broadcast()
        {

        }
        public void HandleClient(Socket client)
        {
            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    int bufferSize = client.Receive(buffer);
                    if (bufferSize == 0) break;
                    string msg = Encoding.UTF8.GetString(buffer, 0, bufferSize);
                    Console.WriteLine("클라이언트로부터 받은 메시지: " + msg);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("클라이언트 처리 중 오류 발생: " + ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("클라이언트 연결 종료됨: " + client.RemoteEndPoint.ToString());
            }
        }
        public void Receive(Socket client)
        {
            byte[] buffer = new byte[1024];
            int receivedBytes = client.Receive(buffer);
            string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
            Console.WriteLine("클라이언트로부터 받은 메시지: " + message);
        }

        public void Send(Socket client, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data);

        }
        public void Stop()
        {
            _serverSocket.Close();
            Sockets.ForEach(s => s.Close());
            Console.WriteLine("서버 중지됨.");
        }
    }
}
