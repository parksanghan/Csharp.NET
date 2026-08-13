using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CycloneDDSManager.DDS
{
    internal sealed class DdsNativeSampleBuffer : IDisposable
    {
        private readonly List<IntPtr> _ownedPointers = new List<IntPtr>();

        internal DdsNativeSampleBuffer(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
            Pointer = Marshal.AllocHGlobal(size);
            Marshal.Copy(new byte[size], 0, Pointer, size);
        }

        internal IntPtr Pointer { get; private set; }

        internal IntPtr Allocate(byte[] bytes)
        {
            IntPtr value = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, value, bytes.Length);
            _ownedPointers.Add(value);
            return value;
        }

        public void Dispose()
        {
            foreach (IntPtr pointer in _ownedPointers)
                Marshal.FreeHGlobal(pointer);
            _ownedPointers.Clear();

            if (Pointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Pointer);
                Pointer = IntPtr.Zero;
            }
        }
    }

    internal sealed class DdsObjectMarshaller<T>
    {
        internal DdsObjectMarshaller(DdsObjectSchema schema)
        {
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
            if (schema.ManagedType != typeof(T))
                throw new ArgumentException("Schema does not describe " + typeof(T).FullName + ".", nameof(schema));
        }

        internal DdsObjectSchema Schema { get; private set; }

        internal DdsNativeSampleBuffer Write(T value)
        {
            object boxed = value;
            if (boxed == null) throw new ArgumentNullException(nameof(value));

            var buffer = new DdsNativeSampleBuffer(Schema.NativeSize);
            try
            {
                WriteObject(buffer, buffer.Pointer, boxed, Schema);
                return buffer;
            }
            catch
            {
                buffer.Dispose();
                throw;
            }
        }

        internal T Read(IntPtr pointer)
        {
            if (pointer == IntPtr.Zero) throw new ArgumentException("A native sample pointer is required.", nameof(pointer));
            return (T)ReadObject(pointer, Schema);
        }

        private static void WriteObject(
            DdsNativeSampleBuffer owner,
            IntPtr destination,
            object value,
            DdsObjectSchema schema)
        {
            foreach (DdsMemberSchema member in schema.Members)
            {
                object memberValue = member.GetValue(value);
                IntPtr field = IntPtr.Add(destination, member.NativeOffset);
                WriteValue(owner, field, memberValue, member);
            }
        }

        private static void WriteValue(
            DdsNativeSampleBuffer owner,
            IntPtr destination,
            object value,
            DdsMemberSchema member)
        {
            DdsValueSchema type = member.ValueType;
            switch (type.Kind)
            {
                case DdsValueKind.Boolean:
                    Marshal.WriteByte(destination, (bool)value ? (byte)1 : (byte)0);
                    break;
                case DdsValueKind.Byte:
                    Marshal.WriteByte(destination, (byte)value);
                    break;
                case DdsValueKind.Int8:
                    Marshal.WriteByte(destination, unchecked((byte)(sbyte)value));
                    break;
                case DdsValueKind.Int16:
                    Marshal.WriteInt16(destination, (short)value);
                    break;
                case DdsValueKind.UInt16:
                    Marshal.WriteInt16(destination, unchecked((short)(ushort)value));
                    break;
                case DdsValueKind.Int32:
                    Marshal.WriteInt32(destination, (int)value);
                    break;
                case DdsValueKind.UInt32:
                    Marshal.WriteInt32(destination, unchecked((int)(uint)value));
                    break;
                case DdsValueKind.Int64:
                    Marshal.WriteInt64(destination, (long)value);
                    break;
                case DdsValueKind.UInt64:
                    Marshal.WriteInt64(destination, unchecked((long)(ulong)value));
                    break;
                case DdsValueKind.Float32:
                    Marshal.Copy(BitConverter.GetBytes((float)value), 0, destination, sizeof(float));
                    break;
                case DdsValueKind.Float64:
                    Marshal.Copy(BitConverter.GetBytes((double)value), 0, destination, sizeof(double));
                    break;
                case DdsValueKind.Char16:
                    Marshal.WriteInt16(destination, unchecked((short)(char)value));
                    break;
                case DdsValueKind.String8:
                    WriteString(owner, destination, value as string, type.StringBound, member.Name);
                    break;
                case DdsValueKind.Enumeration:
                    Marshal.WriteInt32(destination, Convert.ToInt32(value));
                    break;
                case DdsValueKind.Structure:
                    if (value == null)
                        throw new DdsSchemaException("Nested DDS member " + member.Name + " cannot be null.");
                    WriteObject(owner, destination, value, type.ObjectType);
                    break;
                default:
                    throw new DdsSchemaException("Unsupported DDS member kind " + type.Kind + ".");
            }
        }

        private static object ReadObject(IntPtr source, DdsObjectSchema schema)
        {
            object instance = schema.CreateInstance();
            foreach (DdsMemberSchema member in schema.Members)
            {
                IntPtr field = IntPtr.Add(source, member.NativeOffset);
                member.SetValue(instance, ReadValue(field, member.ValueType));
            }
            return instance;
        }

        private static object ReadValue(IntPtr source, DdsValueSchema type)
        {
            switch (type.Kind)
            {
                case DdsValueKind.Boolean: return Marshal.ReadByte(source) != 0;
                case DdsValueKind.Byte: return Marshal.ReadByte(source);
                case DdsValueKind.Int8: return unchecked((sbyte)Marshal.ReadByte(source));
                case DdsValueKind.Int16: return Marshal.ReadInt16(source);
                case DdsValueKind.UInt16: return unchecked((ushort)Marshal.ReadInt16(source));
                case DdsValueKind.Int32: return Marshal.ReadInt32(source);
                case DdsValueKind.UInt32: return unchecked((uint)Marshal.ReadInt32(source));
                case DdsValueKind.Int64: return Marshal.ReadInt64(source);
                case DdsValueKind.UInt64: return unchecked((ulong)Marshal.ReadInt64(source));
                case DdsValueKind.Float32: return BitConverter.ToSingle(CopyBytes(source, sizeof(float)), 0);
                case DdsValueKind.Float64: return BitConverter.ToDouble(CopyBytes(source, sizeof(double)), 0);
                case DdsValueKind.Char16: return unchecked((char)(ushort)Marshal.ReadInt16(source));
                case DdsValueKind.String8:
                    return type.StringBound == 0
                        ? ReadUtf8String(Marshal.ReadIntPtr(source), 64 * 1024 * 1024)
                        : ReadUtf8String(source, checked((int)type.StringBound + 1));
                case DdsValueKind.Enumeration:
                    return Enum.ToObject(type.ManagedType, Marshal.ReadInt32(source));
                case DdsValueKind.Structure:
                    return ReadObject(source, type.ObjectType);
                default:
                    throw new DdsSchemaException("Unsupported DDS member kind " + type.Kind + ".");
            }
        }

        private static void WriteString(
            DdsNativeSampleBuffer owner,
            IntPtr destination,
            string value,
            uint bound,
            string memberName)
        {
            value = value ?? string.Empty;
            byte[] content = Encoding.UTF8.GetBytes(value);
            if (bound != 0 && content.Length > bound)
            {
                throw new DdsSchemaException("UTF-8 value for " + memberName + " is " + content.Length +
                    " bytes, exceeding string bound " + bound + ".");
            }

            var terminated = new byte[content.Length + 1];
            Buffer.BlockCopy(content, 0, terminated, 0, content.Length);
            if (bound == 0)
                Marshal.WriteIntPtr(destination, owner.Allocate(terminated));
            else
                Marshal.Copy(terminated, 0, destination, terminated.Length);
        }

        private static string ReadUtf8String(IntPtr pointer, int maximum)
        {
            if (pointer == IntPtr.Zero) return string.Empty;
            int length = 0;
            while (length < maximum && Marshal.ReadByte(pointer, length) != 0) length++;
            if (length == maximum)
                throw new DdsSchemaException("Received DDS string is not null-terminated within its safety bound.");
            return Encoding.UTF8.GetString(CopyBytes(pointer, length));
        }

        private static byte[] CopyBytes(IntPtr source, int count)
        {
            var result = new byte[count];
            if (count > 0) Marshal.Copy(source, result, 0, count);
            return result;
        }
    }
}
