using NetworkGenerator.MessageStructs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace NetworkGenerator.Packets
{
    public class CntlCmdUdp
    {

        public const ushort MessageType = 65505;
        public const int HeaderSize = 4;
        public const int MessageSize = 3; //데이터 송신에 사용할 패킷 헤더 
        public EMessageID MessageId 
        {
            get
            {
                {
                    return EMessageID.e_data_one;
                }
            }
        } // 데이터 송신에 사용할 패킷 헤더
        public const int TotalSize = 7;

        // 실제 전송 데이터(구조체)
        public CntlCmdUdpData m_Data;// 단일 데이터
        public CntlCmdUdpData[] m_datalist; //복합 데이터 여러개 데이터 일수도 있으니 리스톨 해야할듯?  어차피 foreach 로 송신하는구조니간 리스트 전체를보내는것이 아닌 
        private readonly Dictionary<string, double> m_Resolutions =
             new Dictionary<string, double>
             {
                {
                    nameof(CntlCmdUdpData.UvhfCommand),
                    1.0
                },
                {
                    nameof(CntlCmdUdpData.PttStatus),
                    1.0
                },
                {
                    nameof(CntlCmdUdpData.RadioRxVolStatus),
                    1.0
                }
             };
        // Resoultion 딕셔너리 
        public bool isResolutioned = false;
        private double GetResolution(string fieldName)
        {
            double resolution;

            if (!m_Resolutions.TryGetValue(
                    fieldName,
                    out resolution))
            {
                // Resolution이 정의되지 않은 필드는 변환하지 않음
                return 1.0;
            }

            if (resolution <= 0.0)
            {
                throw new InvalidOperationException(
                    fieldName +
                    "의 Resolution은 0보다 커야 합니다.");
            }

            return resolution;
        }


        private static void WriteUInt16BigEndian(
            BinaryWriter writer,
            ushort value)
        {
            writer.Write((byte)(value >> 8));
            writer.Write((byte)(value & 0xFF));
        }
        // 직렬화 부분 (공통)
        public byte[] Serialize() 
        {
            Validate();
            ApplyResolution();
            using (MemoryStream stream = new MemoryStream(TotalSize))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                // 헤더
                WriteUInt16BigEndian(writer, MessageType);
                WriteUInt16BigEndian(writer, (ushort)MessageId);

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
            //return new byte[]
            //{
            //    m_Data.UvhfCommand,
            //    m_Data.PttStatus,
            //    m_Data.RadioRxVolStatus
            //};
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

        // 역직렬화 부분(공통)
        public static CntlCmdUdp Deserialize(byte[] bytes) // 아마 이부분도 bytes 는 
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            if (bytes.Length != MessageSize)
                throw new ArgumentException(
                    $"메시지 크기가 올바르지 않습니다. 예상: {MessageSize}, 실제: {bytes.Length}");
            int offset = 0;
                byte command = bytes[offset];
                offset += sizeof(byte);
                 
            return new CntlCmdUdp
            {
                m_Data = new CntlCmdUdpData
                {
                    UvhfCommand = bytes[0],
                    PttStatus = bytes[1],
                    RadioRxVolStatus = bytes[2] // #해당부분은 모든 필드값이 다 1바이트에 할당되는 것이 아니기에 2바이트 4바이트 이런것도 역직렬화 하는 과정에서 추가 역직렬화 추가 
                }
            };
        }


        /// <summary>
        /// 데이터 검증 함수 : 내부 ICD에 Range 사항있으면 반영할수 있도록 한다.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Validate()
        {
            if (m_Data.UvhfCommand > 2) 
                throw new ArgumentOutOfRangeException(nameof(m_Data.UvhfCommand));

            if (m_Data.PttStatus > 2)
                throw new ArgumentOutOfRangeException(nameof(m_Data.PttStatus));

            if (m_Data.RadioRxVolStatus > 99)
                throw new ArgumentOutOfRangeException(nameof(m_Data.RadioRxVolStatus));
        }
        /// <summary>
        ///  각 필드마다 Resoultion 적용
        /// </summary>
        private void ApplyResolution()
        {
                FieldInfo[] fieldInfos  =  typeof(CntlCmdUdpData).GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (FieldInfo fieldInfo in fieldInfos) 
                {
                    var value = fieldInfo.GetValue(m_Data);
                    double res_value = m_Resolutions[fieldInfo.Name] * Convert.ToDouble(value);
                    fieldInfo.SetValue(m_Data, res_value);
                    }
             
        }
        // 메세지 헤더 가져오기 Serialize 하기전에  헤더랑 
        private MESSAGEHEADER GetMessageHeader(int idx)
        {
            return new MESSAGEHEADER()
            {
                messageid = (int)MessageId,
                snyc = MessageType,
                messagesize = Marshal.SizeOf(m_datalist[idx])
            };
            
        }
        private MESSAGETAIL GetMessageTail(int idx)
        {
            return new MESSAGETAIL()
            {
                snyc = MessageType,
                isresolutioned = isResolutioned
            };
        }
}
}
