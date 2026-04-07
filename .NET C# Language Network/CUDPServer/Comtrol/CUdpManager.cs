using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CUDPServer.Comtrol
{
    internal class CUdpManager
    {
        public List<UdpClient> clients { get; set; } = new List<UdpClient>();
        public CUDPServer.Socket.CUDPServer server { get; set; }
        public CUdpManager()
        {
        }
        public void AddClient(UdpClient client)
        {
            clients.Add(client);
        }
        public void RemoveClient(UdpClient client) {
            clients.Remove(client);
        }
        public void SendToAll(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (var client in clients)
            {
                try
                {
                    server.SendMessage(message, client.Client.RemoteEndPoint.ToString().Split(':')[0], ((IPEndPoint)client.Client.RemoteEndPoint).Port);
                    client.Send(data, data.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending to client: {ex.Message}");
                }
            }
        }
        public void SendToClient(string message, UdpClient client)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            try
            {
                server.SendMessage(message, client.Client.RemoteEndPoint.ToString().Split(':')[0], ((IPEndPoint)client.Client.RemoteEndPoint).Port);
                client.Send(data, data.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending to client: {ex.Message}");
            }
        }
        public void Broadcast(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            foreach (var client in clients)
            {
                try
                {
                    server.SendMessage(message, client.Client.RemoteEndPoint.ToString().Split(':')[0], ((IPEndPoint)client.Client.RemoteEndPoint).Port);
                    client.Send(data, data.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error broadcasting to client: {ex.Message}");
                }
            }
        }
        public void DisconnectClient(UdpClient client)
        {
            try
            {
                client.Close();
                RemoveClient(client);
                Console.WriteLine($"Client {client.Client.RemoteEndPoint} disconnected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error disconnecting client: {ex.Message}");
            }
        }
        
    }
}
