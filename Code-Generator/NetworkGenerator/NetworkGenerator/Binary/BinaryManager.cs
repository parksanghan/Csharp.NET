using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NetworkGenerator.Binary
{
    public class BinaryManager
    {
        private static readonly Stream _stream;
        public BinaryManager() { }
        private static Dictionary<Type, MemberInfo[]> m_memberdic = new Dictionary<Type, MemberInfo[]>();
        // Csv 파일을 읽어 모든 메세지에 대한 Struct 구조로 해당 니모닉 데이터에 대한 구조체로 각 필드를 정의하는 코드 생성하는 기능 추가
        #region 직렬화 쪽 코드로 옮길 부분들 
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
    }
}
