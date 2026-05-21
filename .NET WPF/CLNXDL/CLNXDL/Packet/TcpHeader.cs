using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLNXDL.Packet
{
    public sealed class TcpHeader
    {
        public const int MinHeaderSize = 20;

        public ushort SourcePort { get; set; }
        public ushort DestinationPort { get; set; }

        public uint SequenceNumber { get; set; }
        public uint AcknowledgmentNumber { get; set; }

        // TCP header length / 4.
        // 기본 TCP 헤더 20바이트면 5.
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

        public byte[] Options { get; set; } = new byte[0];

        public int HeaderLength
        {
            get { return DataOffset * 4; }
        }

        public byte[] ToBytes()
        {
            int headerLength = HeaderLength;

            if (headerLength < MinHeaderSize)
                throw new InvalidOperationException("TCP header length must be at least 20 bytes.");

            if (headerLength > 60)
                throw new InvalidOperationException("TCP header length must not exceed 60 bytes.");

            int optionLength = Options == null ? 0 : Options.Length;

            if (MinHeaderSize + optionLength != headerLength)
                throw new InvalidOperationException("TCP options length does not match DataOffset.");

            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.WriteUInt16BE(SourcePort);
                writer.WriteUInt16BE(DestinationPort);

                writer.WriteUInt32BE(SequenceNumber);
                writer.WriteUInt32BE(AcknowledgmentNumber);

                // 상위 4비트: DataOffset
                // 하위 4비트: Reserved, 여기서는 0
                writer.Write((byte)(DataOffset << 4));

                byte flags = 0;

                if (Fin) flags |= 0x01;
                if (Syn) flags |= 0x02;
                if (Rst) flags |= 0x04;
                if (Psh) flags |= 0x08;
                if (Ack) flags |= 0x10;
                if (Urg) flags |= 0x20;
                if (Ece) flags |= 0x40;
                if (Cwr) flags |= 0x80;

                writer.Write(flags);

                writer.WriteUInt16BE(WindowSize);
                writer.WriteUInt16BE(Checksum);
                writer.WriteUInt16BE(UrgentPointer);

                if (Options != null && Options.Length > 0)
                {
                    writer.Write(Options);
                }

                return stream.ToArray();
            }
        }

        public static TcpHeader FromBytes(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (buffer.Length < MinHeaderSize)
                throw new ArgumentException("TCP header must be at least 20 bytes.");

            using (MemoryStream stream = new MemoryStream(buffer))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                TcpHeader header = new TcpHeader();

                header.SourcePort = reader.ReadUInt16BE();
                header.DestinationPort = reader.ReadUInt16BE();

                header.SequenceNumber = reader.ReadUInt32BE();
                header.AcknowledgmentNumber = reader.ReadUInt32BE();

                byte offsetAndReserved = reader.ReadByte();
                header.DataOffset = (byte)(offsetAndReserved >> 4);

                int headerLength = header.HeaderLength;

                if (headerLength < MinHeaderSize)
                    throw new ArgumentException("Invalid TCP DataOffset.");

                if (headerLength > 60)
                    throw new ArgumentException("Invalid TCP DataOffset. Header length exceeds 60 bytes.");

                if (buffer.Length < headerLength)
                    throw new ArgumentException("Buffer is smaller than TCP header length.");

                byte flags = reader.ReadByte();

                header.Fin = (flags & 0x01) != 0;
                header.Syn = (flags & 0x02) != 0;
                header.Rst = (flags & 0x04) != 0;
                header.Psh = (flags & 0x08) != 0;
                header.Ack = (flags & 0x10) != 0;
                header.Urg = (flags & 0x20) != 0;
                header.Ece = (flags & 0x40) != 0;
                header.Cwr = (flags & 0x80) != 0;

                header.WindowSize = reader.ReadUInt16BE();
                header.Checksum = reader.ReadUInt16BE();
                header.UrgentPointer = reader.ReadUInt16BE();

                int optionLength = headerLength - MinHeaderSize;

                if (optionLength > 0)
                {
                    header.Options = reader.ReadBytes(optionLength);
                }
                else
                {
                    header.Options = new byte[0];
                }

                return header;
            }
        }
    }

 
}
