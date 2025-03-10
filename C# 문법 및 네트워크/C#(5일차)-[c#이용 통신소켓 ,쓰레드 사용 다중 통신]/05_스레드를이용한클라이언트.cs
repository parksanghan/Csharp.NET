//05_스레드를 이용한 클라이언트.cs

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace _1012_Server
{
    //1. 대리자 정의
    delegate void RecvDel(string msg);

    class Client
    {
        const string SERVER_IP = "220.90.179.75";  //"127.0.0.1"
        const int SERVER_PORT = 7000;

        private Socket server;

        //2. 대리자 속성(사용자쪽에서 시작시 메서드 대입처리 )
        public RecvDel RecvCallBack { get; set; } = null;

        public bool Start()
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
                server.Connect(ipep);

                Thread thread = new Thread(new ParameterizedThreadStart(RecvThread));
                thread.IsBackground = true;
                thread.Start(server);

                Console.WriteLine("서버에 접속...");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Stop()
        {
            server.Close();
        }

        public int SendData(string msg)
        {
            byte[] sendmessage = Encoding.Default.GetBytes(msg);
            int ret = server.Send(sendmessage);
            return ret;
        }

        //ThreadFun
        public void RecvThread(object obj)
        {
            try
            {
                while (true)
                {
                    string msg;
                    int ret = RecvData(out msg);
                    if (ret == 0)
                        throw new Exception("예외발생");

                    //3. Del을 사용한 동기화 호출
                    RecvCallBack.Invoke(msg);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int RecvData(out string msg)
        {
            byte[] data = new byte[1024];

            int ret = server.Receive(data);
            msg = Encoding.Default.GetString(data).TrimEnd('\0');

            return ret;
        }
    }

    internal class Program
    {
        Client client = null;

        public void Run()
        {
            client = new Client();
            //Del Callback등록!!***********************************
            client.RecvCallBack = RecvData;

            if (client.Start() == false)
                return;

            while (true)
            {
                Console.Write(">>");
                string msg = Console.ReadLine();
                if (msg == string.Empty)
                    break;

                //send
                int ret = client.SendData(msg);
                Console.WriteLine("{0}byte 전송", ret);
            }

            client.Stop();
        }

        public void RecvData(string msg)
        {
            Console.WriteLine("서버로 부터 온 데이터 : " + msg);
        }


        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
