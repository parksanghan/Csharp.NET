using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.MSocket
{
    public class MSocketServer
    {
        private readonly   Socket serverSocket;                     
        public MSocketServer(Socket serverSocket)
        {
            
            
        }   
        public void SocketInit(string address)
        {
            serverSocket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(address), 8080));
        }
        public void StartListening(int backlog)
        {
            serverSocket.Listen(backlog);
            Console.WriteLine("Server is listening...");
        }
        public Socket AcceptClient()
        {
            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("Client connected.");
            return clientSocket;
            // 호출 뒤에 각 쓰레드에 할당
        }
        public void Stop()
        {
            serverSocket.Close();
            Console.WriteLine("Server stopped.");
        }
    }
}
