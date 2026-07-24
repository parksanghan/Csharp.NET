using NetworkGenerator.MessageStructs;
using NetworkGenerator.Packets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Xsl;

namespace NetworkGenerator.Binary
{
    public class BinaryManager
    {
        private static readonly Stream _stream;
        public BinaryManager() { }

        private static Dictionary<Type, MemberInfo[]> m_memberdic = new Dictionary<Type, MemberInfo[]>();
        // Csv 파일을 읽어 모든 메세지에 대한 Struct 구조로 해당 니모닉 데이터에 대한 구조체로 각 필드를 정의하는 코드 생성하는 기능 추가
        #region 직렬화 쪽 코드로 옮길 부분들 
        public static CntlCmdUdpData m_data = new CntlCmdUdpData()
        {
            PttStatus = 0,
            RadioRxVolStatus = 0,
            UvhfCommand = 0,
        };
        public static byte[] GetObject() {
            byte[] bodybytes = SerializeStruct(m_data);
            MESSAGEHEADER header = GetMessageHeader(1, 1);
            return SerializeWithHeader(header, bodybytes);

        }
        public static MESSAGEHEADER GetMessageHeader(int msgidx , int bodylenght)
        {
            return  new MESSAGEHEADER()
            {
                 snyc =  (ushort)msgidx,
                messageid = (int)msgidx,
                messagesize = bodylenght ,
                
            }; 
        }
        public static byte[] SerializeWithHeader<THeader>(THeader header, byte[] payloads) where THeader : struct
        {
            byte[] headerdata = SerializeStruct(header);
            byte[] result = new byte[headerdata.Length + payloads.Length];
            Array.Copy(headerdata, 0, result, 0, headerdata.Length);
            Array.Copy(payloads, 0, result, headerdata.Length, payloads.Length);

            return result;
        }
        
        public static void SerializeStruct<T>(Stream stream ,T value) where T : struct
        {
            using (var writer = new BinaryWriter(
                stream,
                Encoding.UTF8,
                leaveOpen: true))
            {
                SerializeObject(writer, value, typeof(T));
            }
        }
        public static byte[] SerializeStruct<T>(T value) where T : struct
        {
            using (var stream = new MemoryStream())
            {
                SerializeStruct(stream, value);
                return stream.ToArray();    
            }
        }
        public static void SerializeObject(BinaryWriter writer, object value, Type type)
        {
            if( type.IsClass || type.IsValueType)// class 또는 Struct 타입
            {
                Serealize(writer, value, type);
            }
            //var serializer = FindSerializer(type);
        }
        private static void Serealize(BinaryWriter writer, object value ,  Type type)
        {
            if(value == null) throw new ArgumentNullException("value");

            var members = GetSerializeMembers(type);
            foreach(var member in members)
            {
                if(member is FieldInfo field)
                {
                    var marshalAsAttr = field.GetCustomAttribute<System.Runtime.InteropServices.MarshalAsAttribute>(); // 로컬 변수
                    if (marshalAsAttr != null &&
                        marshalAsAttr.Value == System.Runtime.InteropServices.UnmanagedType.ByValArray &&
                        marshalAsAttr.SizeConst > 0)
                    {
                        // 고정 크기 배열 직렬화 (길이 prefix 없이)
                        SerializeFixedSizeArray(writer, field, value, marshalAsAttr.SizeConst);

                        continue;

                    }
                }
            }

            
        }
        private static void  SerializeFixedSizeArray(BinaryWriter writer , FieldInfo field, object obj, int size)
        {
            var fieldValue = field.GetValue(obj) ?? throw new InvalidOperationException(
                   $"Fixed-size array field '{field.Name}' cannot be null. " +
                   "Initialize with new byte[{sizeConst}] before serialization.");

            if (!(fieldValue is byte[] byteArray))
            {
                throw new NotSupportedException(
                    $"Fixed-size array of type {field.FieldType.Name} is not supported. " +
                    "Only byte[] is supported for ByteProcess compatibility.");

            }

            if (byteArray.Length != size)
            {
                throw new InvalidOperationException(
                    $"Fixed-size array field '{field.Name}' size mismatch: " +
                    $"expected {size}, got {byteArray.Length}. " +
                    "Array size must match MarshalAs SizeConst.");

            }

            WriteFixedBytes(byteArray, size);
        }
        private static void WriteFixedBytes(byte[] value , int size)
        {
             
            /// Fixed buffer 직렬화 (길이 prefix 없이 고정 크기만큼 쓰기)
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Fixed buffer cannot be null");

            if (value.Length != size)
            {
                throw new ArgumentException(
                    $"Fixed buffer size mismatch: expected {size}, got {value.Length}",
                    nameof(value));

            }
            _stream.WriteByte((byte)value.Length);

            _stream.Write(value, 0, size);
        }
        #endregion
        #region Binary 부분  override로  static 부분 삭제 예정
        private static MemberInfo[] GetSerializeMembers(Type type)
        {
            lock (m_memberdic)
            {
                if (m_memberdic.TryGetValue(type, out var cachedMember))
                {
                    return cachedMember;
                }
            }
         var members = ComputeSerializableMembers(type);
                lock (m_memberdic)
                {
                    if (!m_memberdic.ContainsKey(type))
                    {
                        m_memberdic[type] = members;   
                    }
                }
                return members;   
        }
        private static MemberInfo[] ComputeSerializableMembers(Type type)
        {
            IEnumerable<PropertyInfo> propertyInfos =  GetPropertyInfos(type);
            IEnumerable<FieldInfo> fieldInfos = GetFieldInfos(type);



            return GetAutoMembers(propertyInfos,fieldInfos);
        }
        private static IEnumerable<PropertyInfo> GetPropertyInfos(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
               .Where(p => p.CanRead && p.CanWrite)
               .Where(p => p.GetIndexParameters().Length == 0)  // 인덱서 제외
               .Where(p => !p.IsSpecialName)  // 특수 이름 속성 제외 (컴파일러 생성)
               .Where(p => p.GetGetMethod() != null && p.GetSetMethod() != null);  // getter/setter 확인
        }
        private static IEnumerable<FieldInfo> GetFieldInfos(Type type) {
            return type.GetFields(BindingFlags.Public | BindingFlags.Instance);
        }
        private static MemberInfo[] GetAutoMembers(IEnumerable<PropertyInfo> propertyInfos , IEnumerable<FieldInfo> fieldInfos)
        {
            /// 자동 모드: 모든 public 속성/필드를 선언 순서대로 반환 (ByteProcess 호환)
            var members = new List<MemberInfo>(); // 로컬 변수

            // MetadataToken을 사용하여 선언 순서 유지
            members.AddRange(propertyInfos.OrderBy(p => p.MetadataToken));

            members.AddRange(fieldInfos.OrderBy(f => f.MetadataToken));

            return members.ToArray();
        }
        #endregion

        #region 역직렬화 부분 
        public static unsafe int ParsingPacket(byte[] data , int length)
        {
            int msgId = BitConverter.ToInt32(data, 2); // 메세지 정보
            int payloadSize =  BitConverter.ToInt32(data, 6);
            byte[] buffer = new byte[payloadSize];
            Array.Copy(data, sizeof(MESSAGEHEADER), buffer, 0, payloadSize);
            return sizeof(MESSAGEHEADER)+payloadSize;
        }
        public static object DeserializeStruct(byte[] data, Type type)  
        {
            using (var stream = new MemoryStream(data))
            using (var reader = new BinaryReader(
                stream,
                Encoding.UTF8,
                leaveOpen: false))
            {
                return DeserializeObject(reader, type);
            }
        }
        public static T DeserializeStruct<T>(byte[] data) where T : struct
        {
            return (T)DeserializeStruct(data, typeof(T));
        }
        //public static void DeSerealizeStream<T>(byte[] data, int byteread) where T : struct {
        //    using (var stream = new MemoryStream(data))
        //    {
        //        long startpos  = stream.Position;
        //        T res = DeSerealizeStream<T>(stream);

        //    }
        //}
        //public static object Deserialize(BinaryReader reader ,Type type)
        //{

        //}

        // 해당 타입을 만들어 인스턴스 얻음
        public static object Deserialize(BinaryReader reader, Type type)
        {
            var instance = Activator.CreateInstance(type);     // 해당 구조체 타입을 가져옴 
            MemberInfo[] members = GetSerializeMembers(type);
            foreach (MemberInfo member in members)
            {
                if (member is FieldInfo field)
                {
                    var marshalAsAttr = field.GetCustomAttribute<System.Runtime.InteropServices.MarshalAsAttribute>(); // 로컬 변수
                    if (marshalAsAttr != null &&
                        marshalAsAttr.Value == System.Runtime.InteropServices.UnmanagedType.ByValArray &&
                        marshalAsAttr.SizeConst > 0)
                    {
                        // 고정 크기 배열 역직렬화 (길이 prefix 없이)
                        DeserializeFixedSizeArray(reader, field, instance, marshalAsAttr.SizeConst);

                        continue;

                    }
                }
                var memberType = GetMemberType(member); // 로컬 변수
                var memberValue = DeserializeObject(reader, memberType); // 로컬 변수

                MemberinfoSetValue(member, instance, memberValue);
            }
            return instance;
        }
        public static Type GetMemberType(MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case PropertyInfo property:
                    return property.PropertyType;

                case FieldInfo field:
                    return field.FieldType;

                default:
                    throw new NotSupportedException(
                        $"Member type {memberInfo.GetType().Name} is not supported. " +
                        "Only PropertyInfo and FieldInfo are supported.");

            }
        }
        public static void DeserializeFixedSizeArray(BinaryReader reader, FieldInfo field, object obj, int size)
        {
            byte[] buffer = ReadFixedBytes(size);
                field.SetValue(obj, buffer);
        }
        public static byte[] ReadFixedBytes(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Fixed size must be positive", nameof(size));

            var bytes = new byte[size]; // 로컬 변수
            ReadBytes(bytes, 0, size);

            return bytes;
        }
        public static void ReadBytes(byte[] buffer, int offset, int count )
        {
            var totalRead = 0; // 로컬 변수
            while (totalRead < count)
            {
                var read = _stream.Read(buffer, offset + totalRead, count - totalRead); // 로컬 변수
                if (read == 0)
                    throw new EndOfStreamException();

                totalRead += read;

            }
        }
        public static void MemberinfoSetValue(MemberInfo member, object obj, object value)
        {
            switch (member)
            {
                case PropertyInfo property:
                    property.SetValue(obj, value);

                    break;

                case FieldInfo field:
                    field.SetValue(obj, value);

                    break;

                default:
                    throw new NotSupportedException(
                        $"Member type {member.GetType().Name} is not supported. " +
                        "Only PropertyInfo and FieldInfo are supported.");

            }
        }
        public static object DeserializeObject(BinaryReader reader, Type type)
        {
            /// 내부 역직렬화 메서드 (TypeSerializer에게 작업 위임)
            // Strategy Pattern: 타입에 맞는 역직렬화 전략 선택

            if (type.IsClass || type.IsValueType)// class 또는 Struct 타입
            {

                // 선택된 전략에게 실제 역직렬화 작업 위임
                return DeserializeDataType(reader, type);
            }
            return null;
        }
        public static object DeserializeDataType(BinaryReader reader , Type type)
        {
            // enum은 실제 저장 타입을 읽은 후 enum으로 변환
            if (type.IsEnum)
            {
                Type underlyingType = Enum.GetUnderlyingType(type);
                object rawValue = DeserializeDataType(reader, underlyingType);

                return Enum.ToObject(type, rawValue);
            }

            // 기본 자료형
            if (type == typeof(byte)) return reader.ReadByte();
            if (type == typeof(sbyte)) return reader.ReadSByte();
            if (type == typeof(short)) return reader.ReadInt16();
            if (type == typeof(ushort)) return reader.ReadUInt16();
            if (type == typeof(int)) return reader.ReadInt32();
            if (type == typeof(uint)) return reader.ReadUInt32();
            if (type == typeof(long)) return reader.ReadInt64();
            if (type == typeof(ulong)) return reader.ReadUInt64();
            if (type == typeof(float)) return reader.ReadSingle();
            if (type == typeof(double)) return reader.ReadDouble();
            if (type == typeof(bool)) return reader.ReadBoolean();
            if (type == typeof(string)) return reader.ReadString();
            if (type == typeof(decimal)) return reader.ReadDecimal();

            // 기본형이 아닌 구조체/class는 멤버 단위로 역직렬화
            if (type.IsValueType || type.IsClass)
                return Deserialize(reader, type);

            throw new NotSupportedException(
                $"Type {type.FullName} cannot be deserialized.");
        }
        #endregion
    }
}
