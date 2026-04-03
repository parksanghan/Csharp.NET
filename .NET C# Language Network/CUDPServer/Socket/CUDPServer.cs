using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CUDPServer.Socket
{
    internal class UDPServer
    {
        private UdpClient _server;
        private IPEndPoint _endPoint;
        private int _port;


        public UDPServer(int port)
        {
            _port = port;
            _server = new UdpClient(port);          // 포트 바인딩
            _endPoint = new IPEndPoint(IPAddress.Any, 0); // 수신용 엔드포인트
        }
        public void Start()
        {
            Console.WriteLine($"UDP Server started on port {_port}");
            while (true)
            {
                try
                {
                    // 데이터 수신
                    byte[] data = _server.Receive(ref _endPoint);
                    string message = Encoding.UTF8.GetString(data);
                    Console.WriteLine($"Received from {_endPoint}: {message}");
                    // 응답 전송 (옵션)
                    string response = $"Echo: {message}";
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    _server.Send(responseData, responseData.Length, _endPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        public void Stop()
        {
            _server.Close();
            Console.WriteLine("UDP Server stopped.");
        }
        public UdpClient GetServer()
        {
            return _server;
        }

        public UdpClient ConnectClient(string ipAddress, int port)
        {
            UdpClient client = new UdpClient();
            client.Connect(ipAddress, port);
            return client;
        }
        public void SendMessage(string message, string ipAddress, int port)
        {
            UdpClient client = ConnectClient(ipAddress, port);
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length);
            client.Close();
        }
        public void BroadcastMessage(string message, List<IPEndPoint> clients)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (var client in clients)
            {
                _server.Send(data, data.Length, client);
            }
        }
    }

}