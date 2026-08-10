using System;
using System.Runtime.InteropServices;

namespace CycloneDDSManager.DDS
{
    public enum DdsReturnCode
    {
        Ok = 0,
        Error = -1,
        Unsupported = -2,
        BadParameter = -3,
        PreconditionNotMet = -4,
        OutOfResources = -5,
        NotEnabled = -6,
        ImmutablePolicy = -7,
        InconsistentPolicy = -8,
        AlreadyDeleted = -9,
        Timeout = -10,
        NoData = -11,
        IllegalOperation = -12,
        NotAllowedBySecurity = -13
    }

    [Flags]
    public enum DdsStatusMask : uint
    {
        None = 0,
        InconsistentTopic = 1u << 0,
        OfferedDeadlineMissed = 1u << 1,
        RequestedDeadlineMissed = 1u << 2,
        OfferedIncompatibleQos = 1u << 3,
        RequestedIncompatibleQos = 1u << 4,
        SampleLost = 1u << 5,
        SampleRejected = 1u << 6,
        DataOnReaders = 1u << 7,
        DataAvailable = 1u << 8,
        LivelinessLost = 1u << 9,
        LivelinessChanged = 1u << 10,
        PublicationMatched = 1u << 11,
        SubscriptionMatched = 1u << 12,
        All = (1u << 13) - 1
    }

    [Flags]
    public enum DdsStateMask : uint
    {
        ReadSample = 1,
        NotReadSample = 2,
        AnySample = ReadSample | NotReadSample,
        NewView = 4,
        NotNewView = 8,
        AnyView = NewView | NotNewView,
        AliveInstance = 16,
        NotAliveDisposedInstance = 32,
        NotAliveNoWritersInstance = 64,
        AnyInstance = AliveInstance | NotAliveDisposedInstance | NotAliveNoWritersInstance,
        Any = AnySample | AnyView | AnyInstance
    }

    public enum DdsSampleState : uint { Read = 1, NotRead = 2 }
    public enum DdsViewState : uint { New = 4, NotNew = 8 }
    public enum DdsInstanceState : uint { Alive = 16, NotAliveDisposed = 32, NotAliveNoWriters = 64 }

    public enum DdsDurabilityKind { Volatile, TransientLocal, Transient, Persistent }
    public enum DdsHistoryKind { KeepLast, KeepAll }
    public enum DdsOwnershipKind { Shared, Exclusive }
    public enum DdsLivelinessKind { Automatic, ManualByParticipant, ManualByTopic }
    public enum DdsReliabilityKind { BestEffort, Reliable }
    public enum DdsDestinationOrderKind { ByReceptionTimestamp, BySourceTimestamp }
    public enum DdsPresentationAccessScope { Instance, Topic, Group }
    public enum DdsIgnoreLocalKind { None, Participant, Process }
    public enum DdsTypeConsistencyKind { DisallowTypeCoercion, AllowTypeCoercion }
    public enum DdsSampleRejectedReason { NotRejected, InstancesLimit, SamplesLimit, SamplesPerInstanceLimit }
    public enum DdsFindScope { Global, LocalDomain, Participant }

    public enum DdsDynamicTypeKind
    {
        None,
        Boolean,
        Byte,
        Int16,
        Int32,
        Int64,
        UInt16,
        UInt32,
        UInt64,
        Float32,
        Float64,
        Float128,
        Int8,
        UInt8,
        Char8,
        Char16,
        String8,
        String16,
        Enumeration,
        Bitmask,
        Alias,
        Array,
        Sequence,
        Map,
        Structure,
        Union,
        Bitset
    }

    public enum DdsDynamicTypeExtensibility { Final, Appendable, Mutable }
    public enum DdsDynamicTypeAutoId { Sequential, Hash }
    public enum DdsDynamicTypeTryConstruct { Discard, UseDefault, Trim }
    public enum DdsDynamicEnumValueKind { NextAvailable, Explicit }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsDynamicEnumLiteralValue
    {
        public DdsDynamicEnumValueKind ValueKind;
        public int Value;

        public static DdsDynamicEnumLiteralValue Automatic()
        {
            return new DdsDynamicEnumLiteralValue { ValueKind = DdsDynamicEnumValueKind.NextAvailable };
        }

        public static DdsDynamicEnumLiteralValue Explicit(int value)
        {
            return new DdsDynamicEnumLiteralValue
            {
                ValueKind = DdsDynamicEnumValueKind.Explicit,
                Value = value
            };
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsSampleInfo
    {
        public DdsSampleState SampleState;
        public DdsViewState ViewState;
        public DdsInstanceState InstanceState;
        [MarshalAs(UnmanagedType.I1)] public bool ValidData;
        public long SourceTimestamp;
        public ulong InstanceHandle;
        public ulong PublicationHandle;
        public uint DisposedGenerationCount;
        public uint NoWritersGenerationCount;
        public uint SampleRank;
        public uint GenerationRank;
        public uint AbsoluteGenerationRank;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsOfferedDeadlineMissedStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
        public ulong LastInstanceHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsOfferedIncompatibleQosStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
        public uint LastPolicyId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsPublicationMatchedStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
        public uint CurrentCount;
        public int CurrentCountChange;
        public ulong LastSubscriptionHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsLivelinessLostStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsSubscriptionMatchedStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
        public uint CurrentCount;
        public int CurrentCountChange;
        public ulong LastPublicationHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsSampleRejectedStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
        public DdsSampleRejectedReason LastReason;
        public ulong LastInstanceHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsLivelinessChangedStatus
    {
        public uint AliveCount;
        public uint NotAliveCount;
        public int AliveCountChange;
        public int NotAliveCountChange;
        public ulong LastPublicationHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsRequestedDeadlineMissedStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
        public ulong LastInstanceHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsRequestedIncompatibleQosStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
        public uint LastPolicyId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsSampleLostStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DdsInconsistentTopicStatus
    {
        public uint TotalCount;
        public int TotalCountChange;
    }

    public static class DdsConstants
    {
        public const uint DefaultDomain = uint.MaxValue;
        public const ulong NilInstanceHandle = 0;
        public const int UnlimitedLength = -1;
        public const long InfiniteDuration = long.MaxValue;
        public const long Never = long.MaxValue;
        public const uint DynamicMemberIdAuto = 0x0f000000u;
        public const uint DynamicMemberIndexStart = 0;
        public const uint DynamicMemberIndexEnd = uint.MaxValue;
        public const ushort DynamicBitmaskPositionAuto = ushort.MaxValue;

        public static long ToNanoseconds(TimeSpan duration)
        {
            if (duration == System.Threading.Timeout.InfiniteTimeSpan)
                return InfiniteDuration;

            checked
            {
                return duration.Ticks * 100L;
            }
        }
    }
}
