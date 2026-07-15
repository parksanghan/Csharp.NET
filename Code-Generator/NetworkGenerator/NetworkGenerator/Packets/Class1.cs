using NetworkGenerator.MessageStructs;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetworkGenerator.Packets
{
    public class CntlCmdUdp1
    {
        public const ushort MessageType = 65505;

        public const int HeaderSize = 4;
        public const int PayloadSize = 3;
        public const int TotalSize = HeaderSize + PayloadSize;

        public EMessageID MessageID;

        public CntlCmdUdpData m_Data;

        public CntlCmdUdpData[] m_DataList;

        public Dictionary<string, double> m_Resolutions =
        new Dictionary<string, double>
        {
        { nameof(CntlCmdUdpData.UvhfCommand), 1.0 },
        { nameof(CntlCmdUdpData.PttStatus), 1.0 },
        { nameof(CntlCmdUdpData.RadioRxVolStatus), 0.1 }
        };

        public CntlCmdUdp1()
        {
            // 해당 필드는 명령·상태 코드이므로 일반적으로 Resolution 1
            m_Resolutions[nameof(CntlCmdUdpData.UvhfCommand)] = 1.0;
            m_Resolutions[nameof(CntlCmdUdpData.PttStatus)] = 1.0;
            m_Resolutions[nameof(CntlCmdUdpData.RadioRxVolStatus)] = 1.0;
        }

        public byte[] Serialize()
        {
            Validate();

            using (MemoryStream stream = new MemoryStream(TotalSize))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // 헤더
                WriteUInt16BigEndian(writer, MessageType);
                WriteUInt16BigEndian(writer, (ushort)MessageID);

                // Payload
                writer.Write(ToRawByte(
                    m_Data.UvhfCommand,
                    GetResolution(nameof(CntlCmdUdpData.UvhfCommand))));

                writer.Write(ToRawByte(
                    m_Data.PttStatus,
                    GetResolution(nameof(CntlCmdUdpData.PttStatus))));

                writer.Write(ToRawByte(
                    m_Data.RadioRxVolStatus,
                    GetResolution(nameof(CntlCmdUdpData.RadioRxVolStatus))));

                writer.Flush();

                return stream.ToArray();
            }
        }

        private double GetResolution(string fieldName)
        {
            double resolution;

            if (!m_Resolutions.TryGetValue(fieldName, out resolution))
            {
                // 설정되지 않은 필드는 기본값 1
                resolution = 1.0;
            }

            if (resolution <= 0)
            {
                throw new InvalidOperationException(
                    fieldName + "의 Resolution은 0보다 커야 합니다.");
            }

            return resolution;
        }

        private static byte ToRawByte(
            double actualValue,
            double resolution)
        {
            // 송신: 실제값 ÷ Resolution = 원시값
            double rawValue = Math.Round(
                actualValue / resolution,
                MidpointRounding.AwayFromZero);

            if (rawValue < byte.MinValue ||
                rawValue > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(actualValue),
                    "Resolution 적용 결과가 UINT8 범위를 벗어났습니다.");
            }

            return (byte)rawValue;
        }

        private static void WriteUInt16BigEndian(
            BinaryWriter writer,
            ushort value)
        {
            writer.Write((byte)(value >> 8));
            writer.Write((byte)(value & 0xFF));
        }

        private void Validate()
        {
            if (m_Data.UvhfCommand > 2)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(m_Data.UvhfCommand));
            }

            if (m_Data.PttStatus > 2)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(m_Data.PttStatus));
            }

            if (m_Data.RadioRxVolStatus > 99)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(m_Data.RadioRxVolStatus));
            }
        }
        
    }
}