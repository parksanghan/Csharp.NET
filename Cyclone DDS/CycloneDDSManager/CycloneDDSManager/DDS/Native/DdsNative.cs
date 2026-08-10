using System;
using System.Runtime.InteropServices;

namespace CycloneDDSManager.DDS.Native
{
    /// <summary>
    /// ABI declarations for Cyclone DDS 11.0.1.  All char* parameters are
    /// IntPtr on purpose: Cyclone DDS expects UTF-8 while LPStr uses the
    /// process ANSI code page on .NET Framework.
    /// </summary>
    internal static class DdsNative
    {
        internal const string DllName = "ddsc";
        private const CallingConvention CallConvention = CallingConvention.Cdecl;

        // Error/memory ------------------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern IntPtr dds_strretcode(int ret);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern IntPtr dds_alloc(UIntPtr size);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_free(IntPtr pointer);

        // Entity/core -------------------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_participant(uint domain, IntPtr qos, IntPtr listener);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_publisher(int participant, IntPtr qos, IntPtr listener);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_subscriber(int participant, IntPtr qos, IntPtr listener);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_topic(
            int participant,
            IntPtr descriptor,
            IntPtr name,
            IntPtr qos,
            IntPtr listener);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_writer(
            int participantOrPublisher,
            int topic,
            IntPtr qos,
            IntPtr listener);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_reader(
            int participantOrSubscriber,
            int topic,
            IntPtr qos,
            IntPtr listener);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_delete(int entity);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_parent(int entity);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_participant(int entity);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_children(int entity, [Out] int[] children, UIntPtr size);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_domainid(int entity, out uint domainId);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_instance_handle(int entity, out ulong instanceHandle);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_qos(int entity, IntPtr qos);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_set_qos(int entity, IntPtr qos);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_status_changes(int entity, out uint status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_read_status(int entity, out uint status, uint mask);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_take_status(int entity, out uint status, uint mask);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_status_mask(int entity, out uint mask);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_set_status_mask(int entity, uint mask);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_triggered(int entity);

        // Publisher/subscriber ---------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_suspend(int publisher);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_resume(int publisher);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_wait_for_acks(int publisherOrWriter, long timeout);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_notify_readers(int subscriber);

        // Writer/instance ---------------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_write(int writer, IntPtr data);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_write_ts(int writer, IntPtr data, long timestamp);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_writedispose(int writer, IntPtr data);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_writedispose_ts(int writer, IntPtr data, long timestamp);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dispose(int writer, IntPtr data);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dispose_ts(int writer, IntPtr data, long timestamp);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dispose_ih(int writer, ulong instanceHandle);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dispose_ih_ts(int writer, ulong instanceHandle, long timestamp);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_register_instance(int writer, out ulong instanceHandle, IntPtr data);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_unregister_instance(int writer, IntPtr data);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_unregister_instance_ts(int writer, IntPtr data, long timestamp);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_unregister_instance_ih(int writer, ulong instanceHandle);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_unregister_instance_ih_ts(int writer, ulong instanceHandle, long timestamp);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern ulong dds_lookup_instance(int entity, IntPtr data);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_instance_get_key(int entity, ulong instanceHandle, IntPtr data);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_write_flush(int entity);

        // Reader ------------------------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_read(
            int readerOrCondition,
            [In, Out] IntPtr[] buffers,
            [Out] DdsSampleInfo[] sampleInfo,
            UIntPtr bufferSize,
            uint maxSamples);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_read_mask(
            int readerOrCondition,
            [In, Out] IntPtr[] buffers,
            [Out] DdsSampleInfo[] sampleInfo,
            UIntPtr bufferSize,
            uint maxSamples,
            uint mask);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_take(
            int readerOrCondition,
            [In, Out] IntPtr[] buffers,
            [Out] DdsSampleInfo[] sampleInfo,
            UIntPtr bufferSize,
            uint maxSamples);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_take_mask(
            int readerOrCondition,
            [In, Out] IntPtr[] buffers,
            [Out] DdsSampleInfo[] sampleInfo,
            UIntPtr bufferSize,
            uint maxSamples,
            uint mask);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_read_instance(
            int readerOrCondition,
            [In, Out] IntPtr[] buffers,
            [Out] DdsSampleInfo[] sampleInfo,
            UIntPtr bufferSize,
            uint maxSamples,
            ulong instanceHandle);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_take_instance(
            int readerOrCondition,
            [In, Out] IntPtr[] buffers,
            [Out] DdsSampleInfo[] sampleInfo,
            UIntPtr bufferSize,
            uint maxSamples,
            ulong instanceHandle);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_return_loan(int entity, [In, Out] IntPtr[] buffers, int bufferSize);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_reader_wait_for_historical_data(int reader, long maxWait);

        // Conditions/waitset -----------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_readcondition(int reader, uint mask);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_querycondition(int reader, uint mask, DdsQueryConditionFilter filter);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_mask(int condition, out uint mask);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_guardcondition(int owner);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_set_guardcondition(int guardCondition, [MarshalAs(UnmanagedType.I1)] bool triggered);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_read_guardcondition(
            int guardCondition,
            [MarshalAs(UnmanagedType.I1)] out bool triggered);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_take_guardcondition(
            int guardCondition,
            [MarshalAs(UnmanagedType.I1)] out bool triggered);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_waitset(int owner);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_waitset_get_entities(int waitset, [Out] int[] entities, UIntPtr size);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_waitset_attach(int waitset, int entity, IntPtr attachment);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_waitset_detach(int waitset, int entity);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_waitset_set_trigger(
            int waitset,
            [MarshalAs(UnmanagedType.I1)] bool trigger);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_waitset_wait(
            int waitset,
            [Out] IntPtr[] attachments,
            UIntPtr attachmentCount,
            long relativeTimeout);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_waitset_wait_until(
            int waitset,
            [Out] IntPtr[] attachments,
            UIntPtr attachmentCount,
            long absoluteTimeout);

        // QoS ---------------------------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern IntPtr dds_create_qos();

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_delete_qos(IntPtr qos);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_reset_qos(IntPtr qos);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_copy_qos(IntPtr destination, IntPtr source);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_merge_qos(IntPtr destination, IntPtr source);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_userdata(IntPtr qos, IntPtr value, UIntPtr size);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_topicdata(IntPtr qos, IntPtr value, UIntPtr size);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_groupdata(IntPtr qos, IntPtr value, UIntPtr size);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_durability(IntPtr qos, DdsDurabilityKind kind);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_history(IntPtr qos, DdsHistoryKind kind, int depth);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_resource_limits(
            IntPtr qos,
            int maxSamples,
            int maxInstances,
            int maxSamplesPerInstance);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_presentation(
            IntPtr qos,
            DdsPresentationAccessScope accessScope,
            [MarshalAs(UnmanagedType.I1)] bool coherentAccess,
            [MarshalAs(UnmanagedType.I1)] bool orderedAccess);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_lifespan(IntPtr qos, long lifespan);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_deadline(IntPtr qos, long deadline);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_latency_budget(IntPtr qos, long duration);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_ownership(IntPtr qos, DdsOwnershipKind kind);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_ownership_strength(IntPtr qos, int value);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_liveliness(IntPtr qos, DdsLivelinessKind kind, long leaseDuration);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_time_based_filter(IntPtr qos, long minimumSeparation);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_partition(IntPtr qos, uint count, IntPtr partitions);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_partition1(IntPtr qos, IntPtr name);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_reliability(IntPtr qos, DdsReliabilityKind kind, long maxBlockingTime);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_transport_priority(IntPtr qos, int value);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_destination_order(IntPtr qos, DdsDestinationOrderKind kind);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_writer_data_lifecycle(
            IntPtr qos,
            [MarshalAs(UnmanagedType.I1)] bool autoDispose);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_reader_data_lifecycle(
            IntPtr qos,
            long autoPurgeNoWriterSamplesDelay,
            long autoPurgeDisposedSamplesDelay);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_writer_batching(
            IntPtr qos,
            [MarshalAs(UnmanagedType.I1)] bool batchUpdates);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_ignorelocal(IntPtr qos, DdsIgnoreLocalKind ignore);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_prop(IntPtr qos, IntPtr name, IntPtr value);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_entity_name(IntPtr qos, IntPtr name);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_qset_type_consistency(
            IntPtr qos,
            DdsTypeConsistencyKind kind,
            [MarshalAs(UnmanagedType.I1)] bool ignoreSequenceBounds,
            [MarshalAs(UnmanagedType.I1)] bool ignoreStringBounds,
            [MarshalAs(UnmanagedType.I1)] bool ignoreMemberNames,
            [MarshalAs(UnmanagedType.I1)] bool preventTypeWidening,
            [MarshalAs(UnmanagedType.I1)] bool forceTypeValidation);

        // Listener ----------------------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern IntPtr dds_create_listener(IntPtr argument);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_delete_listener(IntPtr listener);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern void dds_reset_listener(IntPtr listener);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_data_available_arg(
            IntPtr listener, DdsOnDataAvailable callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_data_on_readers_arg(
            IntPtr listener, DdsOnDataOnReaders callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_inconsistent_topic_arg(
            IntPtr listener, DdsOnInconsistentTopic callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_liveliness_lost_arg(
            IntPtr listener, DdsOnLivelinessLost callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_offered_deadline_missed_arg(
            IntPtr listener, DdsOnOfferedDeadlineMissed callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_offered_incompatible_qos_arg(
            IntPtr listener, DdsOnOfferedIncompatibleQos callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_sample_lost_arg(
            IntPtr listener, DdsOnSampleLost callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_sample_rejected_arg(
            IntPtr listener, DdsOnSampleRejected callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_liveliness_changed_arg(
            IntPtr listener, DdsOnLivelinessChanged callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_requested_deadline_missed_arg(
            IntPtr listener, DdsOnRequestedDeadlineMissed callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_requested_incompatible_qos_arg(
            IntPtr listener, DdsOnRequestedIncompatibleQos callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_publication_matched_arg(
            IntPtr listener, DdsOnPublicationMatched callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_lset_subscription_matched_arg(
            IntPtr listener, DdsOnSubscriptionMatched callback, IntPtr argument,
            [MarshalAs(UnmanagedType.I1)] bool resetOnInvoke);

        // Status getters ----------------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_inconsistent_topic_status(int topic, out DdsInconsistentTopicStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_publication_matched_status(int writer, out DdsPublicationMatchedStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_liveliness_lost_status(int writer, out DdsLivelinessLostStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_offered_deadline_missed_status(int writer, out DdsOfferedDeadlineMissedStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_offered_incompatible_qos_status(int writer, out DdsOfferedIncompatibleQosStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_subscription_matched_status(int reader, out DdsSubscriptionMatchedStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_liveliness_changed_status(int reader, out DdsLivelinessChangedStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_sample_rejected_status(int reader, out DdsSampleRejectedStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_sample_lost_status(int reader, out DdsSampleLostStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_requested_deadline_missed_status(int reader, out DdsRequestedDeadlineMissedStatus status);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_get_requested_incompatible_qos_status(int reader, out DdsRequestedIncompatibleQosStatus status);

        // Dynamic Type/XTypes ----------------------------------------------

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern DdsDynamicTypeNative dds_dynamic_type_create(
            int entity,
            DdsDynamicTypeDescriptorNative descriptor);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_type_set_extensibility(
            ref DdsDynamicTypeNative type,
            DdsDynamicTypeExtensibility extensibility);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_type_set_autoid(
            ref DdsDynamicTypeNative type,
            DdsDynamicTypeAutoId autoId);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_type_set_nested(
            ref DdsDynamicTypeNative type,
            [MarshalAs(UnmanagedType.I1)] bool nested);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_type_add_member(
            ref DdsDynamicTypeNative type,
            DdsDynamicMemberDescriptorNative memberDescriptor);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_member_set_key(ref DdsDynamicTypeNative type, uint memberId,
            [MarshalAs(UnmanagedType.I1)] bool isKey);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_member_set_optional(ref DdsDynamicTypeNative type, uint memberId,
            [MarshalAs(UnmanagedType.I1)] bool isOptional);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_member_set_external(ref DdsDynamicTypeNative type, uint memberId,
            [MarshalAs(UnmanagedType.I1)] bool isExternal);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_member_set_must_understand(ref DdsDynamicTypeNative type, uint memberId,
            [MarshalAs(UnmanagedType.I1)] bool mustUnderstand);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_type_add_enum_literal(
            ref DdsDynamicTypeNative type,
            IntPtr name,
            DdsDynamicEnumLiteralValue value,
            [MarshalAs(UnmanagedType.I1)] bool isDefault);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_type_add_bitmask_field(
            ref DdsDynamicTypeNative type,
            IntPtr name,
            ushort position);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_type_register(ref DdsDynamicTypeNative type, out IntPtr typeInfo);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern DdsDynamicTypeNative dds_dynamic_type_ref(ref DdsDynamicTypeNative type);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_dynamic_type_unref(ref DdsDynamicTypeNative type);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_create_topic_descriptor(
            DdsFindScope scope,
            int participant,
            IntPtr typeInfo,
            long timeout,
            out IntPtr descriptor);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_delete_topic_descriptor(IntPtr descriptor);

        [DllImport(DllName, CallingConvention = CallConvention, ExactSpelling = true)]
        internal static extern int dds_free_typeinfo(IntPtr typeInfo);
    }
}
