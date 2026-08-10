using System;
using System.Runtime.InteropServices;
using System.Text;
using CycloneDDSManager.DDS.Native;

namespace CycloneDDSManager.DDS
{
    public sealed class DdsException : Exception
    {
        public DdsException(int returnCode, string operation)
            : base(operation + " failed: " + DdsError.GetMessage(returnCode) + " (" + returnCode + ")")
        {
            ReturnCode = returnCode;
            Operation = operation;
        }

        public int ReturnCode { get; private set; }
        public string Operation { get; private set; }
    }

    internal static class DdsError
    {
        internal static void Check(int returnCode, string operation)
        {
            if (returnCode < 0)
                throw new DdsException(returnCode, operation);
        }

        internal static int CheckEntity(int entity, string operation)
        {
            if (entity < 0)
                throw new DdsException(entity, operation);
            if (entity == 0)
                throw new InvalidOperationException(operation + " returned DDS_ENTITY_NIL.");
            return entity;
        }

        internal static string GetMessage(int returnCode)
        {
            try
            {
                IntPtr value = DdsNative.dds_strretcode(returnCode);
                return value == IntPtr.Zero ? "Unknown DDS error" : Marshal.PtrToStringAnsi(value);
            }
            catch (DllNotFoundException)
            {
                return "ddsc native library was not found";
            }
        }
    }

    internal sealed class Utf8String : IDisposable
    {
        internal Utf8String(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            byte[] bytes = Encoding.UTF8.GetBytes(value + "\0");
            Pointer = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, Pointer, bytes.Length);
        }

        internal IntPtr Pointer { get; private set; }

        public void Dispose()
        {
            if (Pointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Pointer);
                Pointer = IntPtr.Zero;
            }
        }
    }

    internal static class NativeSample
    {
        internal static void Invoke<T>(T sample, Action<IntPtr> action) where T : struct
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            IntPtr pointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));
            bool initialized = false;
            try
            {
                Marshal.StructureToPtr(sample, pointer, false);
                initialized = true;
                action(pointer);
            }
            finally
            {
                if (initialized)
                    Marshal.DestroyStructure(pointer, typeof(T));
                Marshal.FreeHGlobal(pointer);
            }
        }

        internal static TResult Invoke<T, TResult>(T sample, Func<IntPtr, TResult> action) where T : struct
        {
            TResult result = default(TResult);
            Invoke(sample, pointer => result = action(pointer));
            return result;
        }
    }
}
