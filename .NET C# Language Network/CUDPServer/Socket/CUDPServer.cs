using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CUDPServer.Socket
{
    internal class CUDPServer
    {

        public UdpClient udpServer;

        public CUDPServer(int port)
        {
            udpServer = new UdpClient(port);
            IPEndPoint serverEp = new IPEndPoint(IPAddress.Any, port);
            udpServer.
        }
    }
}
