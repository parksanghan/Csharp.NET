using System;
using CycloneDDSManager.DDS.Native;

namespace CycloneDDSManager.DDS
{
    public sealed class DdsGuardCondition : DdsEntity
    {
        internal DdsGuardCondition(int handle, DdsParticipant owner) : base(handle, owner) { }

        public bool TriggerValue
        {
            get
            {
                bool value;
                DdsError.Check(DdsNative.dds_read_guardcondition(Handle, out value), "dds_read_guardcondition");
                return value;
            }
            set
            {
                DdsError.Check(DdsNative.dds_set_guardcondition(Handle, value), "dds_set_guardcondition");
            }
        }

        public bool TakeTrigger()
        {
            bool value;
            DdsError.Check(DdsNative.dds_take_guardcondition(Handle, out value), "dds_take_guardcondition");
            return value;
        }
    }

    public sealed class DdsWaitSet : DdsEntity
    {
        internal DdsWaitSet(int handle, DdsParticipant owner) : base(handle, owner) { }

        public void Attach(DdsEntity entity, IntPtr attachment)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            DdsError.Check(DdsNative.dds_waitset_attach(Handle, entity.Handle, attachment), "dds_waitset_attach");
        }

        public void Detach(DdsEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            DdsError.Check(DdsNative.dds_waitset_detach(Handle, entity.Handle), "dds_waitset_detach");
        }

        public void SetTrigger(bool value)
        {
            DdsError.Check(DdsNative.dds_waitset_set_trigger(Handle, value), "dds_waitset_set_trigger");
        }

        public IntPtr[] Wait(int capacity, TimeSpan timeout)
        {
            return WaitNanoseconds(capacity, DdsConstants.ToNanoseconds(timeout));
        }

        public IntPtr[] WaitNanoseconds(int capacity, long relativeTimeout)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            var values = new IntPtr[capacity];
            int count = DdsNative.dds_waitset_wait(Handle, values, new UIntPtr((uint)capacity), relativeTimeout);
            DdsError.Check(count, "dds_waitset_wait");
            return Trim(values, Math.Min(count, capacity));
        }

        public IntPtr[] WaitUntil(int capacity, long absoluteTimeNanoseconds)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            var values = new IntPtr[capacity];
            int count = DdsNative.dds_waitset_wait_until(
                Handle, values, new UIntPtr((uint)capacity), absoluteTimeNanoseconds);
            DdsError.Check(count, "dds_waitset_wait_until");
            return Trim(values, Math.Min(count, capacity));
        }

        public int[] GetEntities(int capacity = 32)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            var values = new int[capacity];
            int count = DdsNative.dds_waitset_get_entities(Handle, values, new UIntPtr((uint)capacity));
            DdsError.Check(count, "dds_waitset_get_entities");
            int resultCount = Math.Min(count, capacity);
            var result = new int[resultCount];
            Array.Copy(values, result, resultCount);
            return result;
        }

        private static IntPtr[] Trim(IntPtr[] values, int count)
        {
            var result = new IntPtr[count];
            Array.Copy(values, result, count);
            return result;
        }
    }
}
