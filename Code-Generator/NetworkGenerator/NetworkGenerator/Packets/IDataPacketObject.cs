using NetworkGenerator.Binary;
using NetworkGenerator.MessageStructs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NetworkGenerator.Packets
{
 
    public abstract class DataPacketObject<TData>

    where TData : struct
    {
        public   abstract EMessageID MessageID { get; }
        protected const int HEADER_SYNC = 5555; // HEADER_SYNC 값은 필요가 없을 수도 있음 .

        /// <summary>
        /// 데이터 구조체 -> 필드애서 각 구조체 데이터 선언
        /// </summary>
        public abstract TData m_Data { get; set; } // 해당 데이터도 무조건 List로 하거나  단일 데이터 / 복합 데이터로 나누던가 해야할듯 
        public bool IsResolutioned = false;
        // 상속 Class에서 정의될 Resoultions 항목
        protected abstract Dictionary<string, double> m_Resolutions { get; }
        // 상속 Class에서 정의될 Max 값들
        protected abstract Dictionary<string, double> m_MaxValues { get; }
        // 상속 Class에서 정의될 Minx  값들
        protected abstract Dictionary<string, double> m_MinValues { get; }

       public int GetPayloadLegnth(EMessageID id)
        {

        }
        // 자식 클래스에서 정의한 구조체 데이터 필드를 순회하여 Min,MAX 값을 검증 
        public void Validate()
        {
            var fields = typeof(TData).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var field in fields)
            {
                object raw = field.GetValue(m_Data);
                double value = Convert.ToDouble(raw);

                if (m_MaxValues.TryGetValue(field.Name, out double max))
                {
                    if (value > max)
                        throw new ArgumentOutOfRangeException(
                            field.Name,
                            $"{field.Name} value {value} > max {max}");
                }

                if (m_MinValues.TryGetValue(field.Name, out double min))
                {
                    if (value < min)
                        throw new ArgumentOutOfRangeException(
                            field.Name,
                            $"{field.Name} value {value} < min {min}");
                }
            }
        }
        public void ApplyResolution()
        {
            if (IsResolutioned == true) return;

            FieldInfo[] fieldInfos = typeof(TData).GetFields(BindingFlags.Public | BindingFlags.Instance);
            //foreach (FieldInfo fieldInfo in fieldInfos)
            //{
            //    var value = fieldInfo.GetValue(m_Data);
            //    double res_value = m_Resolutions[fieldInfo.Name] * Convert.ToDouble(value);// Resolution 값 적용
            //    fieldInfo.SetValue(m_Data, res_value);

            //    // 구조체는 값(Value) - 타입이므로 원본 값이 변경되지 않음

            //}
            TData data = m_Data;

            foreach (FieldInfo field in fieldInfos)
            {
                if (!m_Resolutions.TryGetValue(field.Name, out double resolution))
                    continue;
                if (resolution == 0) continue;
                object value = field.GetValue(data);
                 
                double converted = Convert.ToDouble(value) * resolution;

                object typedValue = Convert.ChangeType(converted, field.FieldType);

                field.SetValueDirect(__makeref(data), typedValue);
            }

            // 수정된 구조체를 프로퍼티에 다시 저장
            m_Data = data;
            IsResolutioned = true;
        }
        public  byte[]   GetObjects()
        {
            Validate();
            ApplyResolution();
            byte[] bodyBytes =  BinaryManager.SerializeStruct(m_Data);
            MESSAGEHEADER header = GetMessageHeader(bodyBytes.Length);
            return BinaryManager.SerializeWithHeader(header, bodyBytes);
        }
        public void Rect()
        {

        }
        public MESSAGEHEADER GetMessageHeader(int length)
        {
            return  new MESSAGEHEADER()
            {
                messageid = (int)MessageID,
                messagesize  = length,
                snyc = HEADER_SYNC
            };

        }
    }
}
