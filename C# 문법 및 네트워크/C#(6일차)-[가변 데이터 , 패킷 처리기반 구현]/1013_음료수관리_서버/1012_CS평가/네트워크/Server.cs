//server.cs
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace 서버
{
    #region delegate정의 및 열거형 정의
    //서버시작/서버종료/클라이언트접속/클라이언트접속해제/메시지수신/메시지전송
    public enum Log
    {
        Server_Start, Server_Stop,
        Client_Connect, Client_DisConnect,
        Message_Recv, Message_Send
    };

    public delegate void RecvDel(Socket s, string msg);
    public delegate void LogDel(Socket s, Log log, string msg);

    #endregion

    class Server
    {
        //소켓 맴버필드
                                     private Socket server = null;
        private List<Socket> sockets = new List<Socket>();
        private readonly int port;

        //thread 
        Thread run_thread = null;

        //대리자 맴버필드(생성자 초기화)
        private RecvDel del_recv;
        private LogDel del_log;

        #region 생성자
        public Server(int _port, RecvDel _recvdel, LogDel _logdel)
        {
            port = _port;
            del_recv = _recvdel;
            del_log = _logdel;
        }
        #endregion

        #region 외부 접근 메서드

        public bool Start()
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, port);
                server.Bind(ipep);
                server.Listen(20);

                run_thread = new Thread(new ThreadStart(ThreadRun));
                run_thread.Start();

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
            try
            {
                server.Close();
                run_thread.Abort();
                del_log(server, Log.Server_Stop, "");
            }
            catch (Exception ex)
            {
                del_log(server, Log.Server_Stop, "에러 :" + ex.Message);
            }
        }

        public bool SendData(Socket client, string msg)
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            return SendData(client, data);
        }

        public bool SendAllData(Socket client, string msg, bool isuser)
        {
            if (isuser == true)
            {
                foreach (Socket socket in sockets)
                {
                    SendData(socket, msg);
                }
            }
            else
            {
                foreach (Socket socket in sockets)
                {
                    if (socket != client)
                        SendData(socket, msg);
                }
            }
            return true;
        }

        #endregion

        #region 내부 사용 메서드(thread)

        //클라이언트 접속 대기(연결 처리 수행)
        public void ThreadRun()
        {
            while (true)
            {
                try
                {
                    //1. 연결처리
                    IPEndPoint myip = (IPEndPoint)server.LocalEndPoint;
                    string temp = string.Format("{0}:{1}", myip.Address, myip.Port);
                    del_log(server, Log.Server_Start, temp);    //********************                    

                    Socket client = server.Accept();  // 클라이언트 접속 대기                                       
                    IPEndPoint ip = (IPEndPoint)client.RemoteEndPoint;
                    temp = string.Format("{0}:{1}", ip.Address, ip.Port);
                    del_log(server, Log.Client_Connect, temp);    //******************** 

                    sockets.Add(client);

                    Thread thread = new Thread(new ParameterizedThreadStart(ThreadWork));
                    thread.IsBackground = true;
                    thread.Start(client);
                }
                catch (Exception ex)
                {
                    del_log(server, Log.Client_Connect, "에러" + ex.Message); //*****
                }
            }
        }

        //클라이언트 메시지 수신
        public void ThreadWork(object obj)
        {
            Socket client = (Socket)obj;

            while (true)
            {
                try
                {
                    //2. 데이터 수신
                    byte[] data = null;
                    if (ReceiveData(client, ref data) == false)
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

            //제거
            Socket sock = sockets.Find(s => s == client);
            sockets.Remove(sock);
            client.Close();
        }
        #endregion

        #region 내부 사용 메서드

        private bool SendData(Socket client, byte[] data)
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
                send_data = client.Send(data_size);

                // [data] 전체데이터(size)가 전송될때까지 반복 전송
                while (total < size)
                {
                    send_data = client.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
                del_log(client, Log.Message_Send, Encoding.UTF8.GetString(data));
                return true;
            }
            catch (Exception ex)
            {
                del_log(client, Log.Message_Send, "에러 : " + ex.Message);
                return false;
            }
        }

        private bool ReceiveData(Socket client, ref byte[] data)
        {
            try
            {
                int total = 0;
                int size = 0;    //실제 받을 데이터 크기
                int left_data = 0;    //남은 데이터 
                int recv_data = 0;    //수신한 바이트 크기

                //[header] 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                recv_data = client.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                data = new byte[size];

                //[data] size만큼 반복해서 데이터 수신
                while (total < size)
                {
                    recv_data = client.Receive(data, total, left_data, 0);
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
    }
}
