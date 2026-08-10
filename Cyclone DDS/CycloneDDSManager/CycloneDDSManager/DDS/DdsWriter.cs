using System;
using CycloneDDSManager.DDS.Native;

namespace CycloneDDSManager.DDS
{
    public sealed class DdsWriter : DdsEntity
    {
        private readonly DdsTopic _topic;

        internal DdsWriter(int handle, object owner, DdsTopic topic, DdsListener listener)
            : base(handle, owner, listener)
        {
            _topic = topic;
        }

        public void Write(IntPtr sample)
        {
            RequireSample(sample);
            DdsError.Check(DdsNative.dds_write(Handle, sample), "dds_write");
        }

        public void Write<T>(T sample) where T : struct
        {
            NativeSample.Invoke(sample, Write);
        }

        public void WriteAt(IntPtr sample, long sourceTimestampNanoseconds)
        {
            RequireSample(sample);
            DdsError.Check(DdsNative.dds_write_ts(Handle, sample, sourceTimestampNanoseconds), "dds_write_ts");
        }

        public void WriteAt<T>(T sample, long sourceTimestampNanoseconds) where T : struct
        {
            NativeSample.Invoke(sample, pointer => WriteAt(pointer, sourceTimestampNanoseconds));
        }

        public void WriteDispose(IntPtr sample)
        {
            RequireSample(sample);
            DdsError.Check(DdsNative.dds_writedispose(Handle, sample), "dds_writedispose");
        }

        public void DisposeInstance(IntPtr keySample)
        {
            RequireSample(keySample);
            DdsError.Check(DdsNative.dds_dispose(Handle, keySample), "dds_dispose");
        }

        public void DisposeInstanceAt(IntPtr keySample, long sourceTimestampNanoseconds)
        {
            RequireSample(keySample);
            DdsError.Check(DdsNative.dds_dispose_ts(Handle, keySample, sourceTimestampNanoseconds), "dds_dispose_ts");
        }

        public void DisposeInstance(ulong instanceHandle)
        {
            DdsError.Check(DdsNative.dds_dispose_ih(Handle, instanceHandle), "dds_dispose_ih");
        }

        public ulong RegisterInstance(IntPtr keySample)
        {
            RequireSample(keySample);
            ulong value;
            DdsError.Check(DdsNative.dds_register_instance(Handle, out value, keySample), "dds_register_instance");
            return value;
        }

        public ulong RegisterInstance<T>(T keySample) where T : struct
        {
            return NativeSample.Invoke(keySample, RegisterInstance);
        }

        public void UnregisterInstance(IntPtr keySample)
        {
            RequireSample(keySample);
            DdsError.Check(DdsNative.dds_unregister_instance(Handle, keySample), "dds_unregister_instance");
        }

        public void UnregisterInstance(ulong instanceHandle)
        {
            DdsError.Check(DdsNative.dds_unregister_instance_ih(Handle, instanceHandle), "dds_unregister_instance_ih");
        }

        public ulong LookupInstance(IntPtr keySample)
        {
            RequireSample(keySample);
            return DdsNative.dds_lookup_instance(Handle, keySample);
        }

        public void GetInstanceKey(ulong instanceHandle, IntPtr destination)
        {
            RequireSample(destination);
            DdsError.Check(DdsNative.dds_instance_get_key(Handle, instanceHandle, destination), "dds_instance_get_key");
        }

        public void Flush()
        {
            DdsError.Check(DdsNative.dds_write_flush(Handle), "dds_write_flush");
        }

        public void WaitForAcknowledgments(TimeSpan timeout)
        {
            DdsError.Check(DdsNative.dds_wait_for_acks(Handle, DdsConstants.ToNanoseconds(timeout)), "dds_wait_for_acks");
        }

        public DdsPublicationMatchedStatus GetPublicationMatchedStatus()
        {
            DdsPublicationMatchedStatus value;
            DdsError.Check(DdsNative.dds_get_publication_matched_status(Handle, out value),
                "dds_get_publication_matched_status");
            return value;
        }

        public DdsLivelinessLostStatus GetLivelinessLostStatus()
        {
            DdsLivelinessLostStatus value;
            DdsError.Check(DdsNative.dds_get_liveliness_lost_status(Handle, out value),
                "dds_get_liveliness_lost_status");
            return value;
        }

        public DdsOfferedDeadlineMissedStatus GetOfferedDeadlineMissedStatus()
        {
            DdsOfferedDeadlineMissedStatus value;
            DdsError.Check(DdsNative.dds_get_offered_deadline_missed_status(Handle, out value),
                "dds_get_offered_deadline_missed_status");
            return value;
        }

        public DdsOfferedIncompatibleQosStatus GetOfferedIncompatibleQosStatus()
        {
            DdsOfferedIncompatibleQosStatus value;
            DdsError.Check(DdsNative.dds_get_offered_incompatible_qos_status(Handle, out value),
                "dds_get_offered_incompatible_qos_status");
            return value;
        }

        private static void RequireSample(IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
                throw new ArgumentException("A native sample pointer is required.", nameof(pointer));
        }
    }
}
