//01_Server
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
/*
단일 접속 1대 1통신 echo서버 (220.90.179.75)
 */
namespace _1012_Server
{
    class Server
    {
        const int PORT = 7000;
        private Socket server = null;

        //대기소켓 생성, 주소할당, 망연결
        public bool Start()
        {
            try
            {                
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, PORT);
                server.Bind(ipep);

                server.Listen(20);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false; 
            }
        }
        
        //클라이언트 접속 대기(연결 처리 수행) -> [recv, send]
        public void Run()
        {
            while (true)
            {
                //1. 연결처리
                Console.WriteLine("서버 시작... 클라이언트 접속 대기중...");
                IPEndPoint myip = (IPEndPoint)server.LocalEndPoint;
                Console.WriteLine("자신의 주소 => {0}:{1}", myip.Address, myip.Port);

                Socket client = server.Accept();  // 클라이언트 접속 대기                                       
                IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                Console.WriteLine("클라이언트 주소 => {0}:{1}", ip.Address, ip.Port);

                while (true)
                {
                    try
                    {
                        //2. 데이터 수신
                        byte[] data = new byte[1024];

                        int ret = client.Receive(data);
                        if (ret == 0)
                            throw new Exception("오류");
                        string recvmessage = Encoding.Default.GetString(data, 0, ret).TrimEnd('\0'); ;
                        Console.WriteLine("수신 메시지: {0}byte", ret);            
                        Console.WriteLine("수신 메시지: " + recvmessage);

                        //3. 데이터 송신(echo)
                        ret = client.Send(data, ret, SocketFlags.None); // 문자열 전송
                        Console.WriteLine("송신 메시지: {0}byte\n", ret);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        //소켓 종료, 반복문 종료
                        client.Close();
                        break;
                    }
                }
            }
        }       
    
        public void Stop()
        {
            server.Close();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();
            server.Run();
            server.Stop();
        }
    }
}
