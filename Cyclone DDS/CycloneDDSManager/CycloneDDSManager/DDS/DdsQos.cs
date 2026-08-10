using System;
using System.Runtime.InteropServices;
using CycloneDDSManager.DDS.Native;

namespace CycloneDDSManager.DDS
{
    /// <summary>Owns a native dds_qos_t object.</summary>
    public sealed class DdsQos : IDisposable
    {
        private IntPtr _handle;

        public DdsQos()
        {
            _handle = DdsNative.dds_create_qos();
            if (_handle == IntPtr.Zero)
                throw new OutOfMemoryException("dds_create_qos returned null.");
        }

        internal IntPtr NativeHandle
        {
            get
            {
                if (_handle == IntPtr.Zero)
                    throw new ObjectDisposedException(nameof(DdsQos));
                return _handle;
            }
        }

        public DdsQos Reset()
        {
            DdsNative.dds_reset_qos(NativeHandle);
            return this;
        }

        public DdsQos CopyFrom(DdsQos source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            DdsNative.dds_copy_qos(NativeHandle, source.NativeHandle);
            return this;
        }

        public DdsQos MergeFrom(DdsQos source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            DdsNative.dds_merge_qos(NativeHandle, source.NativeHandle);
            return this;
        }

        public DdsQos SetUserData(byte[] value) { SetBytes(value, DdsNative.dds_qset_userdata); return this; }
        public DdsQos SetTopicData(byte[] value) { SetBytes(value, DdsNative.dds_qset_topicdata); return this; }
        public DdsQos SetGroupData(byte[] value) { SetBytes(value, DdsNative.dds_qset_groupdata); return this; }

        public DdsQos SetDurability(DdsDurabilityKind kind)
        {
            DdsNative.dds_qset_durability(NativeHandle, kind);
            return this;
        }

        public DdsQos SetHistory(DdsHistoryKind kind, int depth = 1)
        {
            DdsNative.dds_qset_history(NativeHandle, kind, depth);
            return this;
        }

        public DdsQos SetResourceLimits(int maxSamples, int maxInstances, int maxSamplesPerInstance)
        {
            DdsNative.dds_qset_resource_limits(NativeHandle, maxSamples, maxInstances, maxSamplesPerInstance);
            return this;
        }

        public DdsQos SetPresentation(DdsPresentationAccessScope scope, bool coherentAccess, bool orderedAccess)
        {
            DdsNative.dds_qset_presentation(NativeHandle, scope, coherentAccess, orderedAccess);
            return this;
        }

        public DdsQos SetLifespan(TimeSpan value)
        {
            DdsNative.dds_qset_lifespan(NativeHandle, DdsConstants.ToNanoseconds(value));
            return this;
        }

        public DdsQos SetDeadline(TimeSpan value)
        {
            DdsNative.dds_qset_deadline(NativeHandle, DdsConstants.ToNanoseconds(value));
            return this;
        }

        public DdsQos SetLatencyBudget(TimeSpan value)
        {
            DdsNative.dds_qset_latency_budget(NativeHandle, DdsConstants.ToNanoseconds(value));
            return this;
        }

        public DdsQos SetOwnership(DdsOwnershipKind kind)
        {
            DdsNative.dds_qset_ownership(NativeHandle, kind);
            return this;
        }

        public DdsQos SetOwnershipStrength(int value)
        {
            DdsNative.dds_qset_ownership_strength(NativeHandle, value);
            return this;
        }

        public DdsQos SetLiveliness(DdsLivelinessKind kind, TimeSpan leaseDuration)
        {
            DdsNative.dds_qset_liveliness(NativeHandle, kind, DdsConstants.ToNanoseconds(leaseDuration));
            return this;
        }

        public DdsQos SetTimeBasedFilter(TimeSpan minimumSeparation)
        {
            DdsNative.dds_qset_time_based_filter(NativeHandle, DdsConstants.ToNanoseconds(minimumSeparation));
            return this;
        }

        public DdsQos SetPartition(string partition)
        {
            using (var native = new Utf8String(partition))
                DdsNative.dds_qset_partition1(NativeHandle, native.Pointer);
            return this;
        }

        public DdsQos SetPartitions(params string[] partitions)
        {
            if (partitions == null) throw new ArgumentNullException(nameof(partitions));
            var strings = new Utf8String[partitions.Length];
            IntPtr array = IntPtr.Zero;
            try
            {
                var pointers = new IntPtr[partitions.Length];
                for (int i = 0; i < partitions.Length; i++)
                {
                    strings[i] = new Utf8String(partitions[i]);
                    pointers[i] = strings[i].Pointer;
                }

                if (pointers.Length > 0)
                {
                    array = Marshal.AllocHGlobal(IntPtr.Size * pointers.Length);
                    Marshal.Copy(pointers, 0, array, pointers.Length);
                }
                DdsNative.dds_qset_partition(NativeHandle, (uint)pointers.Length, array);
            }
            finally
            {
                if (array != IntPtr.Zero) Marshal.FreeHGlobal(array);
                foreach (Utf8String value in strings)
                    if (value != null) value.Dispose();
            }
            return this;
        }

        public DdsQos SetReliability(DdsReliabilityKind kind, TimeSpan maxBlockingTime)
        {
            DdsNative.dds_qset_reliability(NativeHandle, kind, DdsConstants.ToNanoseconds(maxBlockingTime));
            return this;
        }

        public DdsQos SetTransportPriority(int value)
        {
            DdsNative.dds_qset_transport_priority(NativeHandle, value);
            return this;
        }

        public DdsQos SetDestinationOrder(DdsDestinationOrderKind kind)
        {
            DdsNative.dds_qset_destination_order(NativeHandle, kind);
            return this;
        }

        public DdsQos SetWriterDataLifecycle(bool autoDispose)
        {
            DdsNative.dds_qset_writer_data_lifecycle(NativeHandle, autoDispose);
            return this;
        }

        public DdsQos SetReaderDataLifecycle(TimeSpan noWriterDelay, TimeSpan disposedDelay)
        {
            DdsNative.dds_qset_reader_data_lifecycle(
                NativeHandle,
                DdsConstants.ToNanoseconds(noWriterDelay),
                DdsConstants.ToNanoseconds(disposedDelay));
            return this;
        }

        public DdsQos SetWriterBatching(bool enabled)
        {
            DdsNative.dds_qset_writer_batching(NativeHandle, enabled);
            return this;
        }

        public DdsQos SetIgnoreLocal(DdsIgnoreLocalKind kind)
        {
            DdsNative.dds_qset_ignorelocal(NativeHandle, kind);
            return this;
        }

        public DdsQos SetProperty(string name, string value)
        {
            using (var nativeName = new Utf8String(name))
            using (var nativeValue = new Utf8String(value))
                DdsNative.dds_qset_prop(NativeHandle, nativeName.Pointer, nativeValue.Pointer);
            return this;
        }

        public DdsQos SetEntityName(string name)
        {
            using (var native = new Utf8String(name))
                DdsNative.dds_qset_entity_name(NativeHandle, native.Pointer);
            return this;
        }

        public DdsQos SetTypeConsistency(
            DdsTypeConsistencyKind kind,
            bool ignoreSequenceBounds = false,
            bool ignoreStringBounds = false,
            bool ignoreMemberNames = false,
            bool preventTypeWidening = false,
            bool forceTypeValidation = false)
        {
            DdsNative.dds_qset_type_consistency(
                NativeHandle, kind, ignoreSequenceBounds, ignoreStringBounds,
                ignoreMemberNames, preventTypeWidening, forceTypeValidation);
            return this;
        }

        public void Dispose()
        {
            IntPtr handle = _handle;
            if (handle == IntPtr.Zero) return;
            _handle = IntPtr.Zero;
            DdsNative.dds_delete_qos(handle);
        }

        private delegate void SetBytesDelegate(IntPtr qos, IntPtr value, UIntPtr size);

        private void SetBytes(byte[] value, SetBytesDelegate setter)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            IntPtr pointer = IntPtr.Zero;
            try
            {
                if (value.Length > 0)
                {
                    pointer = Marshal.AllocHGlobal(value.Length);
                    Marshal.Copy(value, 0, pointer, value.Length);
                }
                setter(NativeHandle, pointer, new UIntPtr((uint)value.Length));
            }
            finally
            {
                if (pointer != IntPtr.Zero) Marshal.FreeHGlobal(pointer);
            }
        }
    }
}
