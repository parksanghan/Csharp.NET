using System;
using System.Runtime.InteropServices;

namespace CycloneDDSManager.DDS.Native
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnDataAvailable(int reader, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnDataOnReaders(int subscriber, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnInconsistentTopic(int topic, DdsInconsistentTopicStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnLivelinessLost(int writer, DdsLivelinessLostStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnOfferedDeadlineMissed(int writer, DdsOfferedDeadlineMissedStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnOfferedIncompatibleQos(int writer, DdsOfferedIncompatibleQosStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnSampleLost(int reader, DdsSampleLostStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnSampleRejected(int reader, DdsSampleRejectedStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnLivelinessChanged(int reader, DdsLivelinessChangedStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnRequestedDeadlineMissed(int reader, DdsRequestedDeadlineMissedStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnRequestedIncompatibleQos(int reader, DdsRequestedIncompatibleQosStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnPublicationMatched(int writer, DdsPublicationMatchedStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DdsOnSubscriptionMatched(int reader, DdsSubscriptionMatchedStatus status, IntPtr argument);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool DdsQueryConditionFilter(IntPtr sample);

    [StructLayout(LayoutKind.Sequential)]
    internal struct DdsDynamicTypeNative
    {
        internal IntPtr Opaque0;
        internal IntPtr Opaque1;
        internal int ReturnCode;
    }

    internal enum DdsDynamicTypeSpecKind
    {
        Unset,
        Definition,
        Primitive
    }

    [StructLayout(LayoutKind.Explicit, Size = 24)]
    internal struct DdsDynamicTypeSpecValueNative
    {
        [FieldOffset(0)] internal DdsDynamicTypeNative Type;
        [FieldOffset(0)] internal DdsDynamicTypeKind Primitive;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DdsDynamicTypeSpecNative
    {
        internal DdsDynamicTypeSpecKind Kind;
        internal DdsDynamicTypeSpecValueNative Value;

        internal static DdsDynamicTypeSpecNative FromPrimitive(DdsDynamicTypeKind primitive)
        {
            return new DdsDynamicTypeSpecNative
            {
                Kind = DdsDynamicTypeSpecKind.Primitive,
                Value = new DdsDynamicTypeSpecValueNative { Primitive = primitive }
            };
        }

        internal static DdsDynamicTypeSpecNative FromType(DdsDynamicTypeNative type)
        {
            return new DdsDynamicTypeSpecNative
            {
                Kind = DdsDynamicTypeSpecKind.Definition,
                Value = new DdsDynamicTypeSpecValueNative { Type = type }
            };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DdsDynamicTypeDescriptorNative
    {
        internal DdsDynamicTypeKind Kind;
        internal IntPtr Name;
        internal DdsDynamicTypeSpecNative BaseType;
        internal DdsDynamicTypeSpecNative DiscriminatorType;
        internal uint BoundCount;
        internal IntPtr Bounds;
        internal DdsDynamicTypeSpecNative ElementType;
        internal DdsDynamicTypeSpecNative KeyElementType;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct DdsDynamicMemberDescriptorNative
    {
        internal IntPtr Name;
        internal uint Id;
        internal DdsDynamicTypeSpecNative Type;
        internal IntPtr DefaultValue;
        internal uint Index;
        internal uint LabelCount;
        internal IntPtr Labels;
        [MarshalAs(UnmanagedType.I1)] internal bool IsDefaultLabel;
    }
}
