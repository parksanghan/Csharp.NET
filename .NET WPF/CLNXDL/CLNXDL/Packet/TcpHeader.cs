using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNXDL.Packet
{
    public class TcpHeader
    {
        public const int MinHeaderSize = 20;
        public ushort SourcePort { get; set; }
        public ushort DestinationPort { get; set; }
        public uint SequenceNumber { get; set; }
        public uint AcknowledgmentNumber { get; set; }
        /// <summary>
        /// 4 - bit 값   => TCP Header Length
        /// 기본 20바이트 = 5
        /// </summary>
        public byte DataOffset { get; set; } = 5;
        public bool Fin { get; set; }
        public bool Syn { get; set; }
        public bool Rst { get; set; }
        public bool Psh { get; set; }
        public bool Ack { get; set; }
        public bool Urg { get; set; }
        public bool Ece { get; set; }
        public bool Cwr { get; set; }

        public ushort WindowSize { get; set; }
        public ushort Checksum { get; set; }
        public ushort UrgentPointer { get; set; }

        public byte[] Options { get; set; } = Array.Empty<byte>();
        public int HeaderLength => DataOffset * 4;

        //public byte[] Tobytes()
        //{
        //    int  headerlength =  HeaderLength;
        //    if (headerlength < MinHeaderSize) throw new InvalidOperationException("Tcp headersize must be satisfied with 20 bytes..");
        //    if(headerlength > 60) throw new InvalidOperationException($"Invalid header length: {headerlength}..");
        //    int optionlength = Options == null ? 0 : Options.Length;
        //    if(MinHeaderSize + optionlength != headerlength)throw new InvalidOperationException($"Invalid header length: {headerlength}..");
        //    byte[] buffer = new byte[headerlength];
             
        //}
    }
}
