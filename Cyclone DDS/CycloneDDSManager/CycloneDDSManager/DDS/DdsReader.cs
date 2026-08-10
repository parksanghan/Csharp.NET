using System;
using System.Runtime.InteropServices;
using CycloneDDSManager.DDS.Native;

namespace CycloneDDSManager.DDS
{
    public sealed class DdsLoanedSamples : IDisposable
    {
        private readonly DdsEntity _owner;
        private readonly int _readerOrCondition;
        private IntPtr[] _samples;
        private readonly DdsSampleInfo[] _sampleInfo;

        internal DdsLoanedSamples(
            DdsEntity owner,
            int readerOrCondition,
            IntPtr[] samples,
            DdsSampleInfo[] sampleInfo,
            int count)
        {
            _owner = owner;
            _readerOrCondition = readerOrCondition;
            _samples = samples;
            _sampleInfo = sampleInfo;
            Count = count;
        }

        public int Count { get; private set; }

        public IntPtr GetPointer(int index)
        {
            EnsureIndex(index);
            EnsureNotReturned();
            return _samples[index];
        }

        public DdsSampleInfo GetInfo(int index)
        {
            EnsureIndex(index);
            return _sampleInfo[index];
        }

        /// <summary>
        /// Copies a loaned C sample into a managed sequential-layout struct.
        /// Any pointer-backed values must be consumed before this loan is returned.
        /// </summary>
        public T Get<T>(int index) where T : struct
        {
            IntPtr pointer = GetPointer(index);
            return (T)Marshal.PtrToStructure(pointer, typeof(T));
        }

        public void Return()
        {
            IntPtr[] samples = _samples;
            if (samples == null) return;

            if (Count > 0 && samples[0] != IntPtr.Zero)
                DdsError.Check(DdsNative.dds_return_loan(_readerOrCondition, samples, Count), "dds_return_loan");

            _samples = null;
            Count = 0;
            GC.KeepAlive(_owner);
        }

        public void Dispose()
        {
            try { Return(); }
            catch (DdsException) { /* Dispose must not hide an active exception. */ }
        }

        private void EnsureIndex(int index)
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
        }

        private void EnsureNotReturned()
        {
            if (_samples == null) throw new ObjectDisposedException(nameof(DdsLoanedSamples));
        }
    }

    internal static class DdsReadOperations
    {
        internal static DdsLoanedSamples Read(DdsEntity owner, int handle, int maxSamples, DdsStateMask? mask)
        {
            return Execute(owner, handle, maxSamples, mask, false, null);
        }

        internal static DdsLoanedSamples Take(DdsEntity owner, int handle, int maxSamples, DdsStateMask? mask)
        {
            return Execute(owner, handle, maxSamples, mask, true, null);
        }

        internal static DdsLoanedSamples ReadInstance(DdsEntity owner, int handle, ulong instance, int maxSamples)
        {
            return Execute(owner, handle, maxSamples, null, false, instance);
        }

        internal static DdsLoanedSamples TakeInstance(DdsEntity owner, int handle, ulong instance, int maxSamples)
        {
            return Execute(owner, handle, maxSamples, null, true, instance);
        }

        private static DdsLoanedSamples Execute(
            DdsEntity owner,
            int handle,
            int maxSamples,
            DdsStateMask? mask,
            bool take,
            ulong? instance)
        {
            if (maxSamples <= 0) throw new ArgumentOutOfRangeException(nameof(maxSamples));

            var samples = new IntPtr[maxSamples]; // buf[0] == null requests middleware loans.
            var info = new DdsSampleInfo[maxSamples];
            UIntPtr size = new UIntPtr((uint)maxSamples);
            int result;

            if (instance.HasValue)
            {
                result = take
                    ? DdsNative.dds_take_instance(handle, samples, info, size, (uint)maxSamples, instance.Value)
                    : DdsNative.dds_read_instance(handle, samples, info, size, (uint)maxSamples, instance.Value);
            }
            else if (mask.HasValue)
            {
                result = take
                    ? DdsNative.dds_take_mask(handle, samples, info, size, (uint)maxSamples, (uint)mask.Value)
                    : DdsNative.dds_read_mask(handle, samples, info, size, (uint)maxSamples, (uint)mask.Value);
            }
            else
            {
                result = take
                    ? DdsNative.dds_take(handle, samples, info, size, (uint)maxSamples)
                    : DdsNative.dds_read(handle, samples, info, size, (uint)maxSamples);
            }

            DdsError.Check(result, take ? "dds_take" : "dds_read");
            return new DdsLoanedSamples(owner, handle, samples, info, result);
        }
    }

    public sealed class DdsReader : DdsEntity
    {
        private readonly DdsTopic _topic;

        internal DdsReader(int handle, object owner, DdsTopic topic, DdsListener listener)
            : base(handle, owner, listener)
        {
            _topic = topic;
        }

        public DdsLoanedSamples Read(int maxSamples = 32)
        {
            return DdsReadOperations.Read(this, Handle, maxSamples, null);
        }

        public DdsLoanedSamples Read(DdsStateMask mask, int maxSamples = 32)
        {
            return DdsReadOperations.Read(this, Handle, maxSamples, mask);
        }

        public DdsLoanedSamples Take(int maxSamples = 32)
        {
            return DdsReadOperations.Take(this, Handle, maxSamples, null);
        }

        public DdsLoanedSamples Take(DdsStateMask mask, int maxSamples = 32)
        {
            return DdsReadOperations.Take(this, Handle, maxSamples, mask);
        }

        public DdsLoanedSamples ReadInstance(ulong instanceHandle, int maxSamples = 32)
        {
            return DdsReadOperations.ReadInstance(this, Handle, instanceHandle, maxSamples);
        }

        public DdsLoanedSamples TakeInstance(ulong instanceHandle, int maxSamples = 32)
        {
            return DdsReadOperations.TakeInstance(this, Handle, instanceHandle, maxSamples);
        }

        public DdsReadCondition CreateReadCondition(DdsStateMask mask)
        {
            int entity = DdsNative.dds_create_readcondition(Handle, (uint)mask);
            return new DdsReadCondition(DdsError.CheckEntity(entity, "dds_create_readcondition"), this);
        }

        public DdsQueryCondition CreateQueryCondition(DdsStateMask mask, Func<IntPtr, bool> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            DdsQueryConditionFilter nativeFilter = pointer =>
            {
                try { return filter(pointer); }
                catch { return false; }
            };
            int entity = DdsNative.dds_create_querycondition(Handle, (uint)mask, nativeFilter);
            return new DdsQueryCondition(
                DdsError.CheckEntity(entity, "dds_create_querycondition"), this, nativeFilter);
        }

        public void WaitForHistoricalData(TimeSpan maxWait)
        {
            DdsError.Check(DdsNative.dds_reader_wait_for_historical_data(
                Handle, DdsConstants.ToNanoseconds(maxWait)), "dds_reader_wait_for_historical_data");
        }

        public ulong LookupInstance(IntPtr keySample)
        {
            if (keySample == IntPtr.Zero) throw new ArgumentException("A key sample is required.", nameof(keySample));
            return DdsNative.dds_lookup_instance(Handle, keySample);
        }

        public void GetInstanceKey(ulong instanceHandle, IntPtr destination)
        {
            if (destination == IntPtr.Zero) throw new ArgumentException("A destination is required.", nameof(destination));
            DdsError.Check(DdsNative.dds_instance_get_key(Handle, instanceHandle, destination), "dds_instance_get_key");
        }

        public DdsSubscriptionMatchedStatus GetSubscriptionMatchedStatus()
        {
            DdsSubscriptionMatchedStatus value;
            DdsError.Check(DdsNative.dds_get_subscription_matched_status(Handle, out value),
                "dds_get_subscription_matched_status");
            return value;
        }

        public DdsLivelinessChangedStatus GetLivelinessChangedStatus()
        {
            DdsLivelinessChangedStatus value;
            DdsError.Check(DdsNative.dds_get_liveliness_changed_status(Handle, out value),
                "dds_get_liveliness_changed_status");
            return value;
        }

        public DdsSampleRejectedStatus GetSampleRejectedStatus()
        {
            DdsSampleRejectedStatus value;
            DdsError.Check(DdsNative.dds_get_sample_rejected_status(Handle, out value),
                "dds_get_sample_rejected_status");
            return value;
        }

        public DdsSampleLostStatus GetSampleLostStatus()
        {
            DdsSampleLostStatus value;
            DdsError.Check(DdsNative.dds_get_sample_lost_status(Handle, out value),
                "dds_get_sample_lost_status");
            return value;
        }

        public DdsRequestedDeadlineMissedStatus GetRequestedDeadlineMissedStatus()
        {
            DdsRequestedDeadlineMissedStatus value;
            DdsError.Check(DdsNative.dds_get_requested_deadline_missed_status(Handle, out value),
                "dds_get_requested_deadline_missed_status");
            return value;
        }

        public DdsRequestedIncompatibleQosStatus GetRequestedIncompatibleQosStatus()
        {
            DdsRequestedIncompatibleQosStatus value;
            DdsError.Check(DdsNative.dds_get_requested_incompatible_qos_status(Handle, out value),
                "dds_get_requested_incompatible_qos_status");
            return value;
        }
    }

    public class DdsReadCondition : DdsEntity
    {
        internal DdsReadCondition(int handle, DdsReader reader) : base(handle, reader) { }

        public DdsStateMask Mask
        {
            get
            {
                uint value;
                DdsError.Check(DdsNative.dds_get_mask(Handle, out value), "dds_get_mask");
                return (DdsStateMask)value;
            }
        }

        public DdsLoanedSamples Read(int maxSamples = 32)
        {
            return DdsReadOperations.Read(this, Handle, maxSamples, null);
        }

        public DdsLoanedSamples Take(int maxSamples = 32)
        {
            return DdsReadOperations.Take(this, Handle, maxSamples, null);
        }
    }

    public sealed class DdsQueryCondition : DdsReadCondition
    {
        private readonly DdsQueryConditionFilter _filterLifetime;

        internal DdsQueryCondition(int handle, DdsReader reader, DdsQueryConditionFilter filter)
            : base(handle, reader)
        {
            _filterLifetime = filter;
        }

        public override void Dispose()
        {
            base.Dispose();
            GC.KeepAlive(_filterLifetime);
        }
    }
}
