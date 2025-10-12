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
        private CancellationTokenSource? cts; // 비동기 관리 객체 
        public MSocketServer(Socket serverSocket)
        {
            this.serverSocket =  serverSocket ?? throw new ArgumentNullException(nameof(serverSocket)); 
            
            
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
