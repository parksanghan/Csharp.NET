using NetworkGenerator.MessageStructs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NetworkGenerator.Packets
{
    public class CntlCmdUdp
    {

        public const ushort MessageType = 65505;
        public const int MessageSize = 3; //데이터 송신에 사용할 패킷 헤더 
        public EMessageID MessageID; // 데이터 송신에 사용할 패킷 헤더

        // 실제 전송 데이터(구조체)
        public CntlCmdUdpData m_Data;// 단일 데이터
        public CntlCmdUdpData[] m_datalist; //복합 데이터
      
        // Resoultion 딕셔너리 
        public Dictionary<string,int> m_Resolutions = new Dictionary<string, int>(); 
   


        // 직렬화 부분 (공통)
        public byte[] Serialize() 
        {
            Validate();
            ApplyResolution();
            return new byte[]
            {
                m_Data.UvhfCommand,
                m_Data.PttStatus,
                m_Data.RadioRxVolStatus
            };
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
}
}
