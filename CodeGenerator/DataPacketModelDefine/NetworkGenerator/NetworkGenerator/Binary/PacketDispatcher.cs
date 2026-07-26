using NetworkGenerator.Packets;
using System;
using System.IO;

namespace NetworkGenerator.Binary
{
    public static class PacketDispatcher
    {
        public const int HeaderSize = 10;
        public const int MaxPayloadSize = 16 * 1024 * 1024;

        public static int Process(
            byte[] data,
            int length,
            out IDataPacketObject updatedPacket)
        {
            updatedPacket = null;

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (length < 0 || length > data.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (length < HeaderSize)
            {
                return 0;
            }

            ushort sync = BitConverter.ToUInt16(data, 0);
            int rawMessageId = BitConverter.ToInt32(data, 2);
            int payloadSize = BitConverter.ToInt32(data, 6);

            if (payloadSize < 0 || payloadSize > MaxPayloadSize)
            {
                throw new InvalidDataException(
                    "Invalid payload size: " + payloadSize + ".");
            }

            int totalSize = HeaderSize + payloadSize;
            if (length < totalSize)
            {
                return 0;
            }

            int messageId = rawMessageId;
            updatedPacket = DataObjectRegistry.GetInstance(messageId);

            if (updatedPacket.MessageSync != sync)
            {
                throw new InvalidDataException(
                    "Sync mismatch for " + messageId +
                    ": expected=" + updatedPacket.MessageSync +
                    ", actual=" + sync + ".");
            }

            byte[] payload = new byte[payloadSize];
            Buffer.BlockCopy(
                data,
                HeaderSize,
                payload,
                0,
                payloadSize);

            updatedPacket.UpdateValue(payload);
            return totalSize;
        }

        public static IDataPacketObject ProcessPayload(
            int messageId,
            byte[] payload)
        {
            IDataPacketObject packet =
                DataObjectRegistry.GetInstance(messageId);

            packet.UpdateValue(payload);
            return packet;
        }
    }
}
