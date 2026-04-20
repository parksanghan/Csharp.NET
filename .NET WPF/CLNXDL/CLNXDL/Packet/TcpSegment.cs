using CLNXDL.Packet;
using System;

public sealed class TcpSegment
{
    public TcpHeader Header { get; set; }
    public byte[] Payload { get; set; }

    public TcpSegment()
    {
        Header = new TcpHeader();
        Payload = new byte[0];
    }

    public byte[] ToBytes()
    {
        byte[] headerBytes = Header.ToBytes();

        int payloadLength = Payload == null ? 0 : Payload.Length;
        byte[] result = new byte[headerBytes.Length + payloadLength];

        Buffer.BlockCopy(headerBytes, 0, result, 0, headerBytes.Length);

        if (payloadLength > 0)
        {
            Buffer.BlockCopy(Payload, 0, result, headerBytes.Length, payloadLength);
        }

        return result;
    }

    public static TcpSegment FromBytes(byte[] buffer)
    {
        if (buffer == null)
            throw new ArgumentNullException("buffer");

        if (buffer.Length < TcpHeader.MinHeaderSize)
            throw new ArgumentException("Invalid TCP segment.");

        TcpHeader header = TcpHeader.FromBytes(buffer);

        int headerLength = header.HeaderLength;
        int payloadLength = buffer.Length - headerLength;

        byte[] payload = new byte[payloadLength];

        if (payloadLength > 0)
        {
            Buffer.BlockCopy(buffer, headerLength, payload, 0, payloadLength);
        }

        return new TcpSegment
        {
            Header = header,
            Payload = payload
        };
    }
}