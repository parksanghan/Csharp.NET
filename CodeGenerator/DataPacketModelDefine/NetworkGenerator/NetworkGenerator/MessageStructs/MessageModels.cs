using NetworkGenerator.Attributes;
using System.Runtime.InteropServices;

namespace NetworkGenerator.MessageStructs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CntlCmdUdpData
    {
        [PacketField(1)]
        public byte UvhfCommand { get; set; }

        [PacketField(2)]
        public byte PttStatus { get; set; }

        [PacketField(3)]
        public byte RadioRxVolStatus { get; set; }
    }
}
