using NetworkGenerator.Attributes;
using System.Runtime.InteropServices;

namespace NetworkGenerator.MessageStructs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MESSAGEHEADER
    {
        [PacketField(1)]
        public ushort snyc;

        [PacketField(2)]
        public int messageid;

        [PacketField(3)]
        public int messagesize;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MESSAGETAIL
    {
        [PacketField(1)]
        public ushort snyc;

        [PacketField(2)]
        public bool isresolutioned;
    }
}
