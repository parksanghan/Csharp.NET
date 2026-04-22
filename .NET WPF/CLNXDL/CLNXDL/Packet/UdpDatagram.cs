using System;

public sealed class UdpDatagram
{
    public UdpHeader Header { get; set; }
    public byte[] Payload { get; set; }

    public UdpDatagram()
    {
        Header = new UdpHeader();
        Payload = new byte[0];
    }

    public byte[] ToBytes()
    {
        int payloadLength = Payload == null ? 0 : Payload.Length;
        int totalLength = UdpHeader.HeaderSize + payloadLength;

        if (totalLength > ushort.MaxValue)
            throw new InvalidOperationException("UDP datagram is too large.");

        Header.Length = (ushort)totalLength;

        byte[] headerBytes = Header.ToBytes();
        byte[] result = new byte[totalLength];

        Buffer.BlockCopy(headerBytes, 0, result, 0, headerBytes.Length);

        if (payloadLength > 0)
        {
            Buffer.BlockCopy(Payload, 0, result, UdpHeader.HeaderSize, payloadLength);
        }

        return result;
    }

    public static UdpDatagram FromBytes(byte[] buffer)
    {
        if (buffer == null)
            throw new ArgumentNullException("buffer");

        if (buffer.Length < UdpHeader.HeaderSize)
            throw new ArgumentException("Invalid UDP datagram.");

        UdpHeader header = UdpHeader.FromBytes(buffer);

        if (buffer.Length < header.Length)
            throw new ArgumentException("Buffer is smaller than UDP datagram length.");

        int payloadLength = header.Length - UdpHeader.HeaderSize;
        byte[] payload = new byte[payloadLength];

        if (payloadLength > 0)
        {
            Buffer.BlockCopy(buffer, UdpHeader.HeaderSize, payload, 0, payloadLength);
        }

        return new UdpDatagram
        {
            Header = header,
            Payload = payload
        };
    }
}
}