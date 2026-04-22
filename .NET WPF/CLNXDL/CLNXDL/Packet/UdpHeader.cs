using CLNXDL.Packet;
using System;
using System.IO;

public sealed class UdpHeader
{
    public const int HeaderSize = 8;

    public ushort SourcePort { get; set; }
    public ushort DestinationPort { get; set; }

    // UDP header 8 bytes + payload length
    public ushort Length { get; set; }

    public ushort Checksum { get; set; }

    public byte[] ToBytes()
    {
        if (Length < HeaderSize)
            throw new InvalidOperationException("UDP length must be at least 8 bytes.");

        using (MemoryStream stream = new MemoryStream())
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            writer.WriteUInt16BE(SourcePort);
            writer.WriteUInt16BE(DestinationPort);
            writer.WriteUInt16BE(Length);
            writer.WriteUInt16BE(Checksum);

            return stream.ToArray();
        }
    }

    public static UdpHeader FromBytes(byte[] buffer)
    {
        if (buffer == null)
            throw new ArgumentNullException("buffer");

        if (buffer.Length < HeaderSize)
            throw new ArgumentException("UDP header must be 8 bytes.");

        using (MemoryStream stream = new MemoryStream(buffer))
        using (BinaryReader reader = new BinaryReader(stream))
        {
            UdpHeader header = new UdpHeader();

            header.SourcePort = reader.ReadUInt16BE();
            header.DestinationPort = reader.ReadUInt16BE();
            header.Length = reader.ReadUInt16BE();
            header.Checksum = reader.ReadUInt16BE();

            if (header.Length < HeaderSize)
                throw new ArgumentException("Invalid UDP length.");

            return header;
        }
    }
}
