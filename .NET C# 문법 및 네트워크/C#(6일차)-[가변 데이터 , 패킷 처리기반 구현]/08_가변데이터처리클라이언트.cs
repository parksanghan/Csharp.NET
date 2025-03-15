//08_가변데이터처리클라이언트.cs

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace _1012_Server
{
    #region delegate정의 및 열거형 정의

    //서버연결/서버연결종료/메시지수신/메시지전송
    public enum Log
    {
        Server_Connect, Server_DisConnect,
        Message_Recv, Message_Send
    };

    public delegate void RecvDel(Socket s, string msg);
    public delegate void LogDel(Socket s, Log log, string msg);

    #endregion

    class Client
    {
        private readonly string server_ip; // = "220.90.179.75";  //"127.0.0.1"
        private readonly int server_port;
        private Socket server;

        //대리자 맴버필드(생성자 초기화)
        private RecvDel del_recv;
        private LogDel del_log;

        #region 생성자
        public Client(string _ip, int _port, RecvDel _recvdel, LogDel _logdel)
        {
            server_ip   = _ip;
            server_port = _port;
            del_recv    = _recvdel;
            del_log     = _logdel;
        }
        #endregion

        #region 외부 공개 메서드
        public bool Start()
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(server_ip), server_port);
                server.Connect(ipep);

                Thread thread = new Thread(new ParameterizedThreadStart(RecvThread));
                thread.IsBackground = true;
                thread.Start(server);

                string temp = string.Format("연결 성공 {0}:{1}", ipep.Address, ipep.Port);
                del_log(server, Log.Server_Connect, temp);  //**************

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
            IPEndPoint ipep = (IPEndPoint)server.RemoteEndPoint;
            string temp = string.Format("연결종료 {0}:{1}", ipep.Address, ipep.Port);
            del_log(server, Log.Server_DisConnect, temp);  //**************

            server.Close();
        }

        public bool SendData(string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            return SendData(data);
        }

        #endregion

        #region 내부 사용 메서드

        private bool SendData(byte[] data)
        {
            try
            {
                int total = 0;
                int size = data.Length;  //보낼 바이트 크기
                int left_data = size;
                int send_data = 0;            //보낸 바이트 크기

                // [header]전송할 데이터의 크기 전달
                byte[] data_size = new byte[4];
                data_size = BitConverter.GetBytes(size);
                send_data = server.Send(data_size);

                // [data] 전체데이터(size)가 전송될때까지 반복 전송
                while (total < size)
                {
                    send_data = server.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
                del_log(server, Log.Message_Send, Encoding.UTF8.GetString(data));
                return true;
            }
            catch (Exception ex)
            {
                del_log(server, Log.Message_Send, "에러 : " + ex.Message);
                return false;
            }
        }

        private bool ReceiveData(ref byte[] data)
        {
            try
            {
                int total = 0;
                int size = 0;    //실제 받을 데이터 크기
                int left_data = 0;    //남은 데이터 
                int recv_data = 0;    //수신한 바이트 크기

                //[header] 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                recv_data = server.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                data = new byte[size];

                //[data] size만큼 반복해서 데이터 수신
                while (total < size)
                {
                    recv_data = server.Receive(data, total, left_data, 0);
                    if (recv_data == 0) break;
                    total += recv_data;
                    left_data -= recv_data;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        #endregion

        #region 내부 사용(Thread)
        public void RecvThread(object obj)
        {
            Socket client = (Socket)obj;

            while (true)
            {
                try
                {
                    //2. 데이터 수신
                    byte[] data = null;
                    if (ReceiveData(ref data) == false)
                        throw new Exception("오류");

                    string recvmessage = Encoding.UTF8.GetString(data);
                    del_recv(client, recvmessage);  //callback......................
                    del_log(client, Log.Message_Recv, recvmessage);
                }
                catch (Exception ex)
                {
                    del_log(client, Log.Message_Recv, "에러 : " + ex.Message);
                    break;
                }
            }
        }
        #endregion 
    }

    /// <summary>
    /// Clisent 사용법
    /// 1. Callback함수 구현(2개)
    /// 2. Client객체 생성시 전달(serverip, serverPort, Callback함수 2개)
    /// 3. Client 실행 / 종료
    ///    Start() / Stop()
    /// 4. 전송처리
    ///    SendData()
    /// 5. 수신처리
    ///    1번에서 등록한 Callback함수에서 처리    
    /// </summary>
    internal class Program
    {
        Client client = null;

        #region Callback
        public void RecvDel(Socket s, string msg)
        {
            Console.WriteLine("[데이터 수신] " + msg);
        }

        public void LogDel(Socket s, Log log, string msg)
        {
            string temp = string.Format(log.ToString() + "\t" + msg);
            Console.WriteLine(temp);
        }
        #endregion 

        public void Run()
        {
            client = new Client("220.90.179.75", 7000, RecvDel, LogDel);
            if (client.Start() == false)
                return;

            while (true)
            {
                string msg = Console.ReadLine();
                if (msg == string.Empty)
                    break;

                //send
                bool b = client.SendData(msg);
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
