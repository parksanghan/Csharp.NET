using NetworkGenerator.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace NetworkGenerator.Binary
{
    public static class BinaryManager
    {
        private static readonly Dictionary<Type, MemberInfo[]> MemberCache =
            new Dictionary<Type, MemberInfo[]>();

        public static byte[] SerializeStruct<T>(T value) where T : struct
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                SerializeValue(writer, value, typeof(T));
                writer.Flush();
                return stream.ToArray();
            }
        }

        public static byte[] SerializeWithHeader<THeader>(
            THeader header,
            byte[] payload) where THeader : struct
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            byte[] headerBytes = SerializeStruct(header);
            byte[] result = new byte[headerBytes.Length + payload.Length];
            Buffer.BlockCopy(headerBytes, 0, result, 0, headerBytes.Length);
            Buffer.BlockCopy(payload, 0, result, headerBytes.Length, payload.Length);
            return result;
        }

        public static T DeserializeStruct<T>(byte[] data) where T : struct
        {
            return (T)DeserializeStruct(data, typeof(T));
        }

        public static object DeserializeStruct(byte[] data, Type type)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            using (var stream = new MemoryStream(data, false))
            using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
            {
                object value = DeserializeValue(reader, type);
                if (stream.Position != stream.Length)
                {
                    throw new InvalidDataException(
                        "Payload contains " +
                        (stream.Length - stream.Position) +
                        " unread bytes.");
                }

                return value;
            }
        }

        private static void SerializeValue(
            BinaryWriter writer,
            object value,
            Type type)
        {
            if (type.IsEnum)
            {
                Type underlyingType = Enum.GetUnderlyingType(type);
                SerializeValue(
                    writer,
                    Convert.ChangeType(value, underlyingType),
                    underlyingType);
                return;
            }

            if (type == typeof(byte)) { writer.Write((byte)value); return; }
            if (type == typeof(sbyte)) { writer.Write((sbyte)value); return; }
            if (type == typeof(short)) { writer.Write((short)value); return; }
            if (type == typeof(ushort)) { writer.Write((ushort)value); return; }
            if (type == typeof(int)) { writer.Write((int)value); return; }
            if (type == typeof(uint)) { writer.Write((uint)value); return; }
            if (type == typeof(long)) { writer.Write((long)value); return; }
            if (type == typeof(ulong)) { writer.Write((ulong)value); return; }
            if (type == typeof(float)) { writer.Write((float)value); return; }
            if (type == typeof(double)) { writer.Write((double)value); return; }
            if (type == typeof(bool)) { writer.Write((byte)((bool)value ? 1 : 0)); return; }
            if (type == typeof(char)) { writer.Write((ushort)(char)value); return; }
            if (type == typeof(decimal)) { writer.Write((decimal)value); return; }
            if (type == typeof(string)) { WriteString(writer, (string)value); return; }

            if (type.IsArray)
            {
                WriteArray(writer, (Array)value, type.GetElementType());
                return;
            }

            if (IsList(type))
            {
                WriteList(writer, (IList)value, type.GetGenericArguments()[0]);
                return;
            }

            if (type.IsValueType)
            {
                SerializeComplex(writer, value, type);
                return;
            }

            throw new NotSupportedException(
                "Type " + type.FullName + " cannot be serialized.");
        }

        private static object DeserializeValue(BinaryReader reader, Type type)
        {
            if (type.IsEnum)
            {
                Type underlyingType = Enum.GetUnderlyingType(type);
                object rawValue = DeserializeValue(reader, underlyingType);
                return Enum.ToObject(type, rawValue);
            }

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
            if (type == typeof(bool)) return reader.ReadByte() != 0;
            if (type == typeof(char)) return (char)reader.ReadUInt16();
            if (type == typeof(decimal)) return reader.ReadDecimal();
            if (type == typeof(string)) return ReadString(reader);

            if (type.IsArray)
            {
                return ReadArray(reader, type.GetElementType());
            }

            if (IsList(type))
            {
                return ReadList(reader, type, type.GetGenericArguments()[0]);
            }

            if (type.IsValueType)
            {
                return DeserializeComplex(reader, type);
            }

            throw new NotSupportedException(
                "Type " + type.FullName + " cannot be deserialized.");
        }

        private static void SerializeComplex(
            BinaryWriter writer,
            object value,
            Type type)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            foreach (MemberInfo member in GetSerializableMembers(type))
            {
                FieldInfo field = member as FieldInfo;
                MarshalAsAttribute marshalAs = field == null
                    ? null
                    : field.GetCustomAttribute<MarshalAsAttribute>(false);

                if (marshalAs != null &&
                    marshalAs.Value == UnmanagedType.ByValArray &&
                    marshalAs.SizeConst > 0)
                {
                    WriteFixedArray(
                        writer,
                        (Array)GetMemberValue(member, value),
                        field.FieldType.GetElementType(),
                        marshalAs.SizeConst);
                    continue;
                }

                SerializeValue(
                    writer,
                    GetMemberValue(member, value),
                    GetMemberType(member));
            }
        }

        private static object DeserializeComplex(BinaryReader reader, Type type)
        {
            object instance = Activator.CreateInstance(type);

            foreach (MemberInfo member in GetSerializableMembers(type))
            {
                FieldInfo field = member as FieldInfo;
                MarshalAsAttribute marshalAs = field == null
                    ? null
                    : field.GetCustomAttribute<MarshalAsAttribute>(false);

                object memberValue;
                if (marshalAs != null &&
                    marshalAs.Value == UnmanagedType.ByValArray &&
                    marshalAs.SizeConst > 0)
                {
                    memberValue = ReadFixedArray(
                        reader,
                        field.FieldType.GetElementType(),
                        marshalAs.SizeConst);
                }
                else
                {
                    memberValue = DeserializeValue(
                        reader,
                        GetMemberType(member));
                }

                SetMemberValue(member, instance, memberValue);
            }

            return instance;
        }

        private static void WriteFixedArray(
            BinaryWriter writer,
            Array array,
            Type elementType,
            int size)
        {
            if (array == null || array.Length != size)
            {
                throw new InvalidOperationException(
                    "Fixed array length must be exactly " + size + ".");
            }

            for (int i = 0; i < size; i++)
            {
                SerializeValue(writer, array.GetValue(i), elementType);
            }
        }

        private static Array ReadFixedArray(
            BinaryReader reader,
            Type elementType,
            int size)
        {
            Array array = Array.CreateInstance(elementType, size);
            for (int i = 0; i < size; i++)
            {
                array.SetValue(DeserializeValue(reader, elementType), i);
            }

            return array;
        }

        private static void WriteArray(
            BinaryWriter writer,
            Array array,
            Type elementType)
        {
            if (array == null)
            {
                writer.Write(-1);
                return;
            }

            writer.Write(array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                SerializeValue(writer, array.GetValue(i), elementType);
            }
        }

        private static Array ReadArray(BinaryReader reader, Type elementType)
        {
            int length = ReadCollectionLength(reader);
            if (length == -1)
            {
                return null;
            }

            Array array = Array.CreateInstance(elementType, length);
            for (int i = 0; i < length; i++)
            {
                array.SetValue(DeserializeValue(reader, elementType), i);
            }

            return array;
        }

        private static void WriteList(
            BinaryWriter writer,
            IList list,
            Type elementType)
        {
            if (list == null)
            {
                writer.Write(-1);
                return;
            }

            writer.Write(list.Count);
            foreach (object item in list)
            {
                SerializeValue(writer, item, elementType);
            }
        }

        private static object ReadList(
            BinaryReader reader,
            Type listType,
            Type elementType)
        {
            int length = ReadCollectionLength(reader);
            if (length == -1)
            {
                return null;
            }

            IList list = (IList)Activator.CreateInstance(listType);
            for (int i = 0; i < length; i++)
            {
                list.Add(DeserializeValue(reader, elementType));
            }

            return list;
        }

        private static int ReadCollectionLength(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            if (length < -1 || length > 1000000)
            {
                throw new InvalidDataException(
                    "Invalid collection length: " + length + ".");
            }

            return length;
        }

        private static void WriteString(BinaryWriter writer, string value)
        {
            if (value == null)
            {
                writer.Write(-1);
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(value);
            writer.Write(bytes.Length);
            writer.Write(bytes);
        }

        private static string ReadString(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            if (length == -1)
            {
                return null;
            }

            if (length < 0 || length > 16777216)
            {
                throw new InvalidDataException(
                    "Invalid string length: " + length + ".");
            }

            return Encoding.UTF8.GetString(ReadExactly(reader, length));
        }

        private static byte[] ReadExactly(BinaryReader reader, int count)
        {
            byte[] result = reader.ReadBytes(count);
            if (result.Length != count)
            {
                throw new EndOfStreamException(
                    "Expected " + count +
                    " bytes, but read " + result.Length + ".");
            }

            return result;
        }

        private static bool IsList(Type type)
        {
            return type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(List<>);
        }

        private static MemberInfo[] GetSerializableMembers(Type type)
        {
            lock (MemberCache)
            {
                MemberInfo[] cached;
                if (MemberCache.TryGetValue(type, out cached))
                {
                    return cached;
                }

                IEnumerable<MemberInfo> properties = type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p =>
                        p.CanRead &&
                        p.CanWrite &&
                        p.GetIndexParameters().Length == 0 &&
                        p.GetCustomAttribute<NonSerializableAttribute>(false) == null)
                    .Cast<MemberInfo>();

                IEnumerable<MemberInfo> fields = type
                    .GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f =>
                        !f.IsNotSerialized &&
                        f.GetCustomAttribute<NonSerializableAttribute>(false) == null)
                    .Cast<MemberInfo>();

                MemberInfo[] members = properties
                    .Concat(fields)
                    .OrderBy(GetMemberOrder)
                    .ThenBy(m => m.MetadataToken)
                    .ToArray();

                MemberCache.Add(type, members);
                return members;
            }
        }

        private static int GetMemberOrder(MemberInfo member)
        {
            PacketFieldAttribute attribute =
                member.GetCustomAttribute<PacketFieldAttribute>(false);

            return attribute == null ? int.MaxValue : attribute.Order;
        }

        private static Type GetMemberType(MemberInfo member)
        {
            PropertyInfo property = member as PropertyInfo;
            if (property != null)
            {
                return property.PropertyType;
            }

            FieldInfo field = member as FieldInfo;
            if (field != null)
            {
                return field.FieldType;
            }

            throw new NotSupportedException(member.MemberType.ToString());
        }

        private static object GetMemberValue(MemberInfo member, object instance)
        {
            PropertyInfo property = member as PropertyInfo;
            if (property != null)
            {
                return property.GetValue(instance, null);
            }

            return ((FieldInfo)member).GetValue(instance);
        }

        private static void SetMemberValue(
            MemberInfo member,
            object instance,
            object value)
        {
            PropertyInfo property = member as PropertyInfo;
            if (property != null)
            {
                property.SetValue(instance, value, null);
                return;
            }

            ((FieldInfo)member).SetValue(instance, value);
        }
    }
}
