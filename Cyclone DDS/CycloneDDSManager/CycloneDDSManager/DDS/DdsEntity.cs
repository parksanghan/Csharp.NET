using System;
using System.Threading;
using CycloneDDSManager.DDS.Native;

namespace CycloneDDSManager.DDS
{
    public abstract class DdsEntity : IDisposable
    {
        private int _handle;
        private readonly object _ownerLifetime;
        private readonly DdsListener _listenerLifetime;

        internal DdsEntity(int handle, object ownerLifetime = null, DdsListener listenerLifetime = null)
        {
            _handle = DdsError.CheckEntity(handle, "DDS entity creation");
            _ownerLifetime = ownerLifetime;
            _listenerLifetime = listenerLifetime;
        }

        public int Handle
        {
            get
            {
                int value = Volatile.Read(ref _handle);
                if (value == 0) throw new ObjectDisposedException(GetType().Name);
                return value;
            }
        }

        public DdsStatusMask StatusChanges
        {
            get
            {
                uint value;
                DdsError.Check(DdsNative.dds_get_status_changes(Handle, out value), "dds_get_status_changes");
                return (DdsStatusMask)value;
            }
        }

        public DdsStatusMask EnabledStatuses
        {
            get
            {
                uint value;
                DdsError.Check(DdsNative.dds_get_status_mask(Handle, out value), "dds_get_status_mask");
                return (DdsStatusMask)value;
            }
            set
            {
                DdsError.Check(DdsNative.dds_set_status_mask(Handle, (uint)value), "dds_set_status_mask");
            }
        }

        public ulong InstanceHandle
        {
            get
            {
                ulong value;
                DdsError.Check(DdsNative.dds_get_instance_handle(Handle, out value), "dds_get_instance_handle");
                return value;
            }
        }

        public DdsStatusMask ReadStatus(DdsStatusMask mask)
        {
            uint value;
            DdsError.Check(DdsNative.dds_read_status(Handle, out value, (uint)mask), "dds_read_status");
            return (DdsStatusMask)value;
        }

        public DdsStatusMask TakeStatus(DdsStatusMask mask)
        {
            uint value;
            DdsError.Check(DdsNative.dds_take_status(Handle, out value, (uint)mask), "dds_take_status");
            return (DdsStatusMask)value;
        }

        public bool IsTriggered
        {
            get
            {
                int result = DdsNative.dds_triggered(Handle);
                DdsError.Check(result, "dds_triggered");
                return result != 0;
            }
        }

        public DdsQos GetQos()
        {
            var qos = new DdsQos();
            try
            {
                DdsError.Check(DdsNative.dds_get_qos(Handle, qos.NativeHandle), "dds_get_qos");
                return qos;
            }
            catch
            {
                qos.Dispose();
                throw;
            }
        }

        public void SetQos(DdsQos qos)
        {
            if (qos == null) throw new ArgumentNullException(nameof(qos));
            DdsError.Check(DdsNative.dds_set_qos(Handle, qos.NativeHandle), "dds_set_qos");
        }

        public virtual void Dispose()
        {
            int handle = Interlocked.Exchange(ref _handle, 0);
            if (handle != 0)
                DdsNative.dds_delete(handle);

            GC.KeepAlive(_ownerLifetime);
            GC.KeepAlive(_listenerLifetime);
        }

        internal static IntPtr QosHandle(DdsQos qos)
        {
            return qos == null ? IntPtr.Zero : qos.NativeHandle;
        }

        internal static IntPtr ListenerHandle(DdsListener listener)
        {
            return listener == null ? IntPtr.Zero : listener.NativeHandle;
        }
    }

    public sealed class DdsParticipant : DdsEntity
    {
        private DdsParticipant(int handle, DdsListener listener)
            : base(handle, null, listener) { }

        public static DdsParticipant Create(
            uint domain = DdsConstants.DefaultDomain,
            DdsQos qos = null,
            DdsListener listener = null)
        {
            int entity = DdsNative.dds_create_participant(domain, QosHandle(qos), ListenerHandle(listener));
            return new DdsParticipant(DdsError.CheckEntity(entity, "dds_create_participant"), listener);
        }

        public DdsPublisher CreatePublisher(DdsQos qos = null, DdsListener listener = null)
        {
            int entity = DdsNative.dds_create_publisher(Handle, QosHandle(qos), ListenerHandle(listener));
            return new DdsPublisher(DdsError.CheckEntity(entity, "dds_create_publisher"), this, listener);
        }

        public DdsSubscriber CreateSubscriber(DdsQos qos = null, DdsListener listener = null)
        {
            int entity = DdsNative.dds_create_subscriber(Handle, QosHandle(qos), ListenerHandle(listener));
            return new DdsSubscriber(DdsError.CheckEntity(entity, "dds_create_subscriber"), this, listener);
        }

        /// <param name="descriptor">Address of an idlc-generated dds_topic_descriptor_t.</param>
        public DdsTopic CreateTopic(IntPtr descriptor, string name, DdsQos qos = null, DdsListener listener = null)
        {
            if (descriptor == IntPtr.Zero) throw new ArgumentException("A topic descriptor is required.", nameof(descriptor));
            using (var nativeName = new Utf8String(name))
            {
                int entity = DdsNative.dds_create_topic(
                    Handle, descriptor, nativeName.Pointer, QosHandle(qos), ListenerHandle(listener));
                return new DdsTopic(DdsError.CheckEntity(entity, "dds_create_topic"), this, name, listener);
            }
        }

        public DdsWriter CreateWriter(DdsTopic topic, DdsQos qos = null, DdsListener listener = null)
        {
            if (topic == null) throw new ArgumentNullException(nameof(topic));
            int entity = DdsNative.dds_create_writer(Handle, topic.Handle, QosHandle(qos), ListenerHandle(listener));
            return new DdsWriter(DdsError.CheckEntity(entity, "dds_create_writer"), this, topic, listener);
        }

        public DdsReader CreateReader(DdsTopic topic, DdsQos qos = null, DdsListener listener = null)
        {
            if (topic == null) throw new ArgumentNullException(nameof(topic));
            int entity = DdsNative.dds_create_reader(Handle, topic.Handle, QosHandle(qos), ListenerHandle(listener));
            return new DdsReader(DdsError.CheckEntity(entity, "dds_create_reader"), this, topic, listener);
        }

        public DdsWaitSet CreateWaitSet()
        {
            return new DdsWaitSet(
                DdsError.CheckEntity(DdsNative.dds_create_waitset(Handle), "dds_create_waitset"), this);
        }

        public DdsGuardCondition CreateGuardCondition()
        {
            return new DdsGuardCondition(
                DdsError.CheckEntity(DdsNative.dds_create_guardcondition(Handle), "dds_create_guardcondition"), this);
        }
    }

    public sealed class DdsTopic : DdsEntity
    {
        internal DdsTopic(int handle, DdsParticipant owner, string name, DdsListener listener)
            : base(handle, owner, listener) { Name = name; }

        public string Name { get; private set; }

        public DdsInconsistentTopicStatus GetInconsistentTopicStatus()
        {
            DdsInconsistentTopicStatus value;
            DdsError.Check(DdsNative.dds_get_inconsistent_topic_status(Handle, out value),
                "dds_get_inconsistent_topic_status");
            return value;
        }
    }

    public sealed class DdsPublisher : DdsEntity
    {
        internal DdsPublisher(int handle, DdsParticipant owner, DdsListener listener)
            : base(handle, owner, listener) { }

        public DdsWriter CreateWriter(DdsTopic topic, DdsQos qos = null, DdsListener listener = null)
        {
            if (topic == null) throw new ArgumentNullException(nameof(topic));
            int entity = DdsNative.dds_create_writer(Handle, topic.Handle, QosHandle(qos), ListenerHandle(listener));
            return new DdsWriter(DdsError.CheckEntity(entity, "dds_create_writer"), this, topic, listener);
        }

        public void Suspend() { DdsError.Check(DdsNative.dds_suspend(Handle), "dds_suspend"); }
        public void Resume() { DdsError.Check(DdsNative.dds_resume(Handle), "dds_resume"); }

        public void WaitForAcknowledgments(TimeSpan timeout)
        {
            DdsError.Check(DdsNative.dds_wait_for_acks(Handle, DdsConstants.ToNanoseconds(timeout)), "dds_wait_for_acks");
        }
    }

    public sealed class DdsSubscriber : DdsEntity
    {
        internal DdsSubscriber(int handle, DdsParticipant owner, DdsListener listener)
            : base(handle, owner, listener) { }

        public DdsReader CreateReader(DdsTopic topic, DdsQos qos = null, DdsListener listener = null)
        {
            if (topic == null) throw new ArgumentNullException(nameof(topic));
            int entity = DdsNative.dds_create_reader(Handle, topic.Handle, QosHandle(qos), ListenerHandle(listener));
            return new DdsReader(DdsError.CheckEntity(entity, "dds_create_reader"), this, topic, listener);
        }

        public void NotifyReaders()
        {
            DdsError.Check(DdsNative.dds_notify_readers(Handle), "dds_notify_readers");
        }
    }
}
