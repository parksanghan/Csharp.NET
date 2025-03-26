//02_Client
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _1012_Server
{
    class Client
    {
        const string SERVER_IP  = "220.90.179.75";  //"127.0.0.1"
        const int SERVER_PORT   = 7000;

        private Socket server;

        public bool Start()
        {
            try
            {
                server = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
                server.Connect(ipep);

                Console.WriteLine("서버에 접속...");

                return true;
            }
            catch(Exception ex)
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
        static void Main(string[] args)
        {
            Client client = new Client();
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

                //recv
                string recvmsg;
                ret = client.RecvData(out recvmsg);
                Console.WriteLine("{0}byte 수신 {1}", ret, recvmsg);
            }

            client.Stop();
        }
    }
}
