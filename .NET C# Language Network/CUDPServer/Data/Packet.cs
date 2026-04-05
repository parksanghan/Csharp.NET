using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CUDPServer.Data
{
    internal class Packet
    {
        public ProtocolType Protocol { get; set; }
        public IPAddress SourceIP { get; set; }
        public IPAddress DestinationIP { get; set; }    
        public Packet()
        {
            
        }

    }
}
