using System;
using CycloneDDSManager.DDS.Native;

namespace CycloneDDSManager.DDS
{
    /// <summary>
    /// Owns dds_listener_t and roots all unmanaged callback delegates for as
    /// long as this object (or an entity created with it) is reachable.
    /// </summary>
    public sealed class DdsListener : IDisposable
    {
        private IntPtr _handle;
        private DdsOnDataAvailable _dataAvailable;
        private DdsOnDataOnReaders _dataOnReaders;
        private DdsOnInconsistentTopic _inconsistentTopic;
        private DdsOnLivelinessLost _livelinessLost;
        private DdsOnOfferedDeadlineMissed _offeredDeadlineMissed;
        private DdsOnOfferedIncompatibleQos _offeredIncompatibleQos;
        private DdsOnSampleLost _sampleLost;
        private DdsOnSampleRejected _sampleRejected;
        private DdsOnLivelinessChanged _livelinessChanged;
        private DdsOnRequestedDeadlineMissed _requestedDeadlineMissed;
        private DdsOnRequestedIncompatibleQos _requestedIncompatibleQos;
        private DdsOnPublicationMatched _publicationMatched;
        private DdsOnSubscriptionMatched _subscriptionMatched;

        public DdsListener()
        {
            _handle = DdsNative.dds_create_listener(IntPtr.Zero);
            if (_handle == IntPtr.Zero)
                throw new OutOfMemoryException("dds_create_listener returned null.");
        }

        public event Action<Exception> CallbackException;

        public Exception LastCallbackException { get; private set; }

        internal IntPtr NativeHandle
        {
            get
            {
                if (_handle == IntPtr.Zero)
                    throw new ObjectDisposedException(nameof(DdsListener));
                return _handle;
            }
        }

        public DdsListener OnDataAvailable(Action<int> callback, bool resetStatus = true)
        {
            Require(callback);
            _dataAvailable = (entity, arg) => Invoke(() => callback(entity));
            DdsError.Check(DdsNative.dds_lset_data_available_arg(
                NativeHandle, _dataAvailable, IntPtr.Zero, resetStatus), "dds_lset_data_available_arg");
            return this;
        }

        public DdsListener OnDataOnReaders(Action<int> callback, bool resetStatus = true)
        {
            Require(callback);
            _dataOnReaders = (entity, arg) => Invoke(() => callback(entity));
            DdsError.Check(DdsNative.dds_lset_data_on_readers_arg(
                NativeHandle, _dataOnReaders, IntPtr.Zero, resetStatus), "dds_lset_data_on_readers_arg");
            return this;
        }

        public DdsListener OnInconsistentTopic(Action<int, DdsInconsistentTopicStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _inconsistentTopic = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_inconsistent_topic_arg(
                NativeHandle, _inconsistentTopic, IntPtr.Zero, resetStatus), "dds_lset_inconsistent_topic_arg");
            return this;
        }

        public DdsListener OnLivelinessLost(Action<int, DdsLivelinessLostStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _livelinessLost = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_liveliness_lost_arg(
                NativeHandle, _livelinessLost, IntPtr.Zero, resetStatus), "dds_lset_liveliness_lost_arg");
            return this;
        }

        public DdsListener OnOfferedDeadlineMissed(Action<int, DdsOfferedDeadlineMissedStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _offeredDeadlineMissed = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_offered_deadline_missed_arg(
                NativeHandle, _offeredDeadlineMissed, IntPtr.Zero, resetStatus), "dds_lset_offered_deadline_missed_arg");
            return this;
        }

        public DdsListener OnOfferedIncompatibleQos(Action<int, DdsOfferedIncompatibleQosStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _offeredIncompatibleQos = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_offered_incompatible_qos_arg(
                NativeHandle, _offeredIncompatibleQos, IntPtr.Zero, resetStatus), "dds_lset_offered_incompatible_qos_arg");
            return this;
        }

        public DdsListener OnSampleLost(Action<int, DdsSampleLostStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _sampleLost = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_sample_lost_arg(
                NativeHandle, _sampleLost, IntPtr.Zero, resetStatus), "dds_lset_sample_lost_arg");
            return this;
        }

        public DdsListener OnSampleRejected(Action<int, DdsSampleRejectedStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _sampleRejected = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_sample_rejected_arg(
                NativeHandle, _sampleRejected, IntPtr.Zero, resetStatus), "dds_lset_sample_rejected_arg");
            return this;
        }

        public DdsListener OnLivelinessChanged(Action<int, DdsLivelinessChangedStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _livelinessChanged = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_liveliness_changed_arg(
                NativeHandle, _livelinessChanged, IntPtr.Zero, resetStatus), "dds_lset_liveliness_changed_arg");
            return this;
        }

        public DdsListener OnRequestedDeadlineMissed(Action<int, DdsRequestedDeadlineMissedStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _requestedDeadlineMissed = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_requested_deadline_missed_arg(
                NativeHandle, _requestedDeadlineMissed, IntPtr.Zero, resetStatus), "dds_lset_requested_deadline_missed_arg");
            return this;
        }

        public DdsListener OnRequestedIncompatibleQos(Action<int, DdsRequestedIncompatibleQosStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _requestedIncompatibleQos = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_requested_incompatible_qos_arg(
                NativeHandle, _requestedIncompatibleQos, IntPtr.Zero, resetStatus), "dds_lset_requested_incompatible_qos_arg");
            return this;
        }

        public DdsListener OnPublicationMatched(Action<int, DdsPublicationMatchedStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _publicationMatched = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_publication_matched_arg(
                NativeHandle, _publicationMatched, IntPtr.Zero, resetStatus), "dds_lset_publication_matched_arg");
            return this;
        }

        public DdsListener OnSubscriptionMatched(Action<int, DdsSubscriptionMatchedStatus> callback, bool resetStatus = true)
        {
            Require(callback);
            _subscriptionMatched = (entity, status, arg) => Invoke(() => callback(entity, status));
            DdsError.Check(DdsNative.dds_lset_subscription_matched_arg(
                NativeHandle, _subscriptionMatched, IntPtr.Zero, resetStatus), "dds_lset_subscription_matched_arg");
            return this;
        }

        public DdsListener Reset()
        {
            DdsNative.dds_reset_listener(NativeHandle);
            return this;
        }

        public void Dispose()
        {
            IntPtr handle = _handle;
            if (handle == IntPtr.Zero) return;
            _handle = IntPtr.Zero;
            DdsNative.dds_delete_listener(handle);
            // Delegate fields intentionally stay rooted. An entity may contain
            // a native copy of these callbacks and itself keeps this object alive.
        }

        private static void Require(Delegate callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
        }

        private void Invoke(Action callback)
        {
            try
            {
                callback();
            }
            catch (Exception exception)
            {
                LastCallbackException = exception;
                Action<Exception> handler = CallbackException;
                if (handler != null)
                {
                    try { handler(exception); }
                    catch { /* Never allow a managed exception across the C ABI. */ }
                }
            }
        }
    }
}
