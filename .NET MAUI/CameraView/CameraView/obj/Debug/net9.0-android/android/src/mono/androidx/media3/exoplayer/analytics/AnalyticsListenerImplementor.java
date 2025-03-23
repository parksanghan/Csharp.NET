package mono.androidx.media3.exoplayer.analytics;


public class AnalyticsListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.analytics.AnalyticsListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAudioAttributesChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/AudioAttributes;)V:GetOnAudioAttributesChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_AudioAttributes_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioCodecError:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/Exception;)V:GetOnAudioCodecError_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioDecoderInitialized:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;J)V:GetOnAudioDecoderInitializedDeprecated_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_JHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioDecoderInitialized:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;JJ)V:GetOnAudioDecoderInitialized_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_JJHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioDecoderReleased:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;)V:GetOnAudioDecoderReleased_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioDisabled:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/DecoderCounters;)V:GetOnAudioDisabled_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_DecoderCounters_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioEnabled:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/DecoderCounters;)V:GetOnAudioEnabled_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_DecoderCounters_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioInputFormatChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/Format;Landroidx/media3/exoplayer/DecoderReuseEvaluation;)V:GetOnAudioInputFormatChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_Format_Landroidx_media3_exoplayer_DecoderReuseEvaluation_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioPositionAdvancing:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;J)V:GetOnAudioPositionAdvancing_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_JHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioSessionIdChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;I)V:GetOnAudioSessionIdChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioSinkError:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/Exception;)V:GetOnAudioSinkError_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioTrackInitialized:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/audio/AudioSink$AudioTrackConfig;)V:GetOnAudioTrackInitialized_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_audio_AudioSink_AudioTrackConfig_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioTrackReleased:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/audio/AudioSink$AudioTrackConfig;)V:GetOnAudioTrackReleased_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_audio_AudioSink_AudioTrackConfig_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioUnderrun:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;IJJ)V:GetOnAudioUnderrun_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IJJHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAvailableCommandsChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/Player$Commands;)V:GetOnAvailableCommandsChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_Player_Commands_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onBandwidthEstimate:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;IJJ)V:GetOnBandwidthEstimate_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IJJHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onCues:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/text/CueGroup;)V:GetOnCues_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_text_CueGroup_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onCues:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/util/List;)V:GetOnCuesDeprecated_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_util_List_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDeviceInfoChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/DeviceInfo;)V:GetOnDeviceInfoChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_DeviceInfo_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDeviceVolumeChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;IZ)V:GetOnDeviceVolumeChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IZHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDownstreamFormatChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnDownstreamFormatChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmKeysLoaded:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;)V:GetOnDrmKeysLoaded_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmKeysRemoved:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;)V:GetOnDrmKeysRemoved_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmKeysRestored:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;)V:GetOnDrmKeysRestored_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmSessionAcquired:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;)V:GetOnDrmSessionAcquiredDeprecated_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmSessionAcquired:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;I)V:GetOnDrmSessionAcquired_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmSessionManagerError:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/Exception;)V:GetOnDrmSessionManagerError_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmSessionReleased:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;)V:GetOnDrmSessionReleased_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDroppedVideoFrames:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;IJ)V:GetOnDroppedVideoFrames_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IJHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onEvents:(Landroidx/media3/common/Player;Landroidx/media3/exoplayer/analytics/AnalyticsListener$Events;)V:GetOnEvents_Landroidx_media3_common_Player_Landroidx_media3_exoplayer_analytics_AnalyticsListener_Events_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onIsLoadingChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Z)V:GetOnIsLoadingChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_ZHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onIsPlayingChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Z)V:GetOnIsPlayingChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_ZHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadCanceled:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/source/LoadEventInfo;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnLoadCanceled_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_source_LoadEventInfo_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadCompleted:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/source/LoadEventInfo;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnLoadCompleted_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_source_LoadEventInfo_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadError:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/source/LoadEventInfo;Landroidx/media3/exoplayer/source/MediaLoadData;Ljava/io/IOException;Z)V:GetOnLoadError_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_source_LoadEventInfo_Landroidx_media3_exoplayer_source_MediaLoadData_Ljava_io_IOException_ZHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadStarted:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/source/LoadEventInfo;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnLoadStarted_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_source_LoadEventInfo_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadingChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Z)V:GetOnLoadingChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_ZHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onMaxSeekToPreviousPositionChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;J)V:GetOnMaxSeekToPreviousPositionChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_JHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onMediaItemTransition:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/MediaItem;I)V:GetOnMediaItemTransition_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_MediaItem_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onMediaMetadataChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/MediaMetadata;)V:GetOnMediaMetadataChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_MediaMetadata_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onMetadata:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/Metadata;)V:GetOnMetadata_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_Metadata_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlayWhenReadyChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;ZI)V:GetOnPlayWhenReadyChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_ZIHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlaybackParametersChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/PlaybackParameters;)V:GetOnPlaybackParametersChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_PlaybackParameters_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlaybackStateChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;I)V:GetOnPlaybackStateChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlaybackSuppressionReasonChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;I)V:GetOnPlaybackSuppressionReasonChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlayerError:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/PlaybackException;)V:GetOnPlayerError_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_PlaybackException_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlayerErrorChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/PlaybackException;)V:GetOnPlayerErrorChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_PlaybackException_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlayerReleased:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;)V:GetOnPlayerReleased_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlayerStateChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;ZI)V:GetOnPlayerStateChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_ZIHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPlaylistMetadataChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/MediaMetadata;)V:GetOnPlaylistMetadataChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_MediaMetadata_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPositionDiscontinuity:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/Player$PositionInfo;Landroidx/media3/common/Player$PositionInfo;I)V:GetOnPositionDiscontinuity_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_Player_PositionInfo_Landroidx_media3_common_Player_PositionInfo_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPositionDiscontinuity:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;I)V:GetOnPositionDiscontinuityDeprecated_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onRenderedFirstFrame:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/Object;J)V:GetOnRenderedFirstFrame_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_Object_JHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onRendererReadyChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;IIZ)V:GetOnRendererReadyChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IIZHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onRepeatModeChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;I)V:GetOnRepeatModeChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSeekBackIncrementChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;J)V:GetOnSeekBackIncrementChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_JHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSeekForwardIncrementChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;J)V:GetOnSeekForwardIncrementChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_JHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSeekStarted:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;)V:GetOnSeekStarted_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onShuffleModeChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Z)V:GetOnShuffleModeChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_ZHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSkipSilenceEnabledChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Z)V:GetOnSkipSilenceEnabledChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_ZHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSurfaceSizeChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;II)V:GetOnSurfaceSizeChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IIHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onTimelineChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;I)V:GetOnTimelineChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onTrackSelectionParametersChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/TrackSelectionParameters;)V:GetOnTrackSelectionParametersChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_TrackSelectionParameters_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onTracksChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/Tracks;)V:GetOnTracksChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_Tracks_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onUpstreamDiscarded:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnUpstreamDiscarded_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoCodecError:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/Exception;)V:GetOnVideoCodecError_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoDecoderInitialized:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;J)V:GetOnVideoDecoderInitializedDeprecated_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_JHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoDecoderInitialized:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;JJ)V:GetOnVideoDecoderInitialized_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_JJHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoDecoderReleased:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;)V:GetOnVideoDecoderReleased_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoDisabled:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/DecoderCounters;)V:GetOnVideoDisabled_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_DecoderCounters_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoEnabled:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/exoplayer/DecoderCounters;)V:GetOnVideoEnabled_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_exoplayer_DecoderCounters_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoFrameProcessingOffset:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;JI)V:GetOnVideoFrameProcessingOffset_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_JIHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoInputFormatChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/Format;Landroidx/media3/exoplayer/DecoderReuseEvaluation;)V:GetOnVideoInputFormatChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_Format_Landroidx_media3_exoplayer_DecoderReuseEvaluation_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoSizeChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Landroidx/media3/common/VideoSize;)V:GetOnVideoSizeChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Landroidx_media3_common_VideoSize_Handler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoSizeChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;IIIF)V:GetOnVideoSizeChangedDeprecated_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_IIIFHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVolumeChanged:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;F)V:GetOnVolumeChanged_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_FHandler:AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", AnalyticsListenerImplementor.class, __md_methods);
	}

	public AnalyticsListenerImplementor ()
	{
		super ();
		if (getClass () == AnalyticsListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Analytics.IAnalyticsListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onAudioAttributesChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.AudioAttributes p1)
	{
		n_onAudioAttributesChanged (p0, p1);
	}

	private native void n_onAudioAttributesChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.AudioAttributes p1);

	public void onAudioCodecError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1)
	{
		n_onAudioCodecError (p0, p1);
	}

	private native void n_onAudioCodecError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1);

	public void onAudioDecoderInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2)
	{
		n_onAudioDecoderInitialized (p0, p1, p2);
	}

	private native void n_onAudioDecoderInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2);

	public void onAudioDecoderInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2, long p3)
	{
		n_onAudioDecoderInitialized (p0, p1, p2, p3);
	}

	private native void n_onAudioDecoderInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2, long p3);

	public void onAudioDecoderReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1)
	{
		n_onAudioDecoderReleased (p0, p1);
	}

	private native void n_onAudioDecoderReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1);

	public void onAudioDisabled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.DecoderCounters p1)
	{
		n_onAudioDisabled (p0, p1);
	}

	private native void n_onAudioDisabled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.DecoderCounters p1);

	public void onAudioEnabled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.DecoderCounters p1)
	{
		n_onAudioEnabled (p0, p1);
	}

	private native void n_onAudioEnabled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.DecoderCounters p1);

	public void onAudioInputFormatChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Format p1, androidx.media3.exoplayer.DecoderReuseEvaluation p2)
	{
		n_onAudioInputFormatChanged (p0, p1, p2);
	}

	private native void n_onAudioInputFormatChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Format p1, androidx.media3.exoplayer.DecoderReuseEvaluation p2);

	public void onAudioPositionAdvancing (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1)
	{
		n_onAudioPositionAdvancing (p0, p1);
	}

	private native void n_onAudioPositionAdvancing (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1);

	public void onAudioSessionIdChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onAudioSessionIdChanged (p0, p1);
	}

	private native void n_onAudioSessionIdChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1);

	public void onAudioSinkError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1)
	{
		n_onAudioSinkError (p0, p1);
	}

	private native void n_onAudioSinkError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1);

	public void onAudioTrackInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.audio.AudioSink.AudioTrackConfig p1)
	{
		n_onAudioTrackInitialized (p0, p1);
	}

	private native void n_onAudioTrackInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.audio.AudioSink.AudioTrackConfig p1);

	public void onAudioTrackReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.audio.AudioSink.AudioTrackConfig p1)
	{
		n_onAudioTrackReleased (p0, p1);
	}

	private native void n_onAudioTrackReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.audio.AudioSink.AudioTrackConfig p1);

	public void onAudioUnderrun (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, long p2, long p3)
	{
		n_onAudioUnderrun (p0, p1, p2, p3);
	}

	private native void n_onAudioUnderrun (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, long p2, long p3);

	public void onAvailableCommandsChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Player.Commands p1)
	{
		n_onAvailableCommandsChanged (p0, p1);
	}

	private native void n_onAvailableCommandsChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Player.Commands p1);

	public void onBandwidthEstimate (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, long p2, long p3)
	{
		n_onBandwidthEstimate (p0, p1, p2, p3);
	}

	private native void n_onBandwidthEstimate (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, long p2, long p3);

	public void onCues (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.text.CueGroup p1)
	{
		n_onCues (p0, p1);
	}

	private native void n_onCues (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.text.CueGroup p1);

	public void onCues (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.util.List p1)
	{
		n_onCues (p0, p1);
	}

	private native void n_onCues (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.util.List p1);

	public void onDeviceInfoChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.DeviceInfo p1)
	{
		n_onDeviceInfoChanged (p0, p1);
	}

	private native void n_onDeviceInfoChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.DeviceInfo p1);

	public void onDeviceVolumeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, boolean p2)
	{
		n_onDeviceVolumeChanged (p0, p1, p2);
	}

	private native void n_onDeviceVolumeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, boolean p2);

	public void onDownstreamFormatChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.MediaLoadData p1)
	{
		n_onDownstreamFormatChanged (p0, p1);
	}

	private native void n_onDownstreamFormatChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.MediaLoadData p1);

	public void onDrmKeysLoaded (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmKeysLoaded (p0);
	}

	private native void n_onDrmKeysLoaded (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0);

	public void onDrmKeysRemoved (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmKeysRemoved (p0);
	}

	private native void n_onDrmKeysRemoved (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0);

	public void onDrmKeysRestored (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmKeysRestored (p0);
	}

	private native void n_onDrmKeysRestored (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0);

	public void onDrmSessionAcquired (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmSessionAcquired (p0);
	}

	private native void n_onDrmSessionAcquired (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0);

	public void onDrmSessionAcquired (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onDrmSessionAcquired (p0, p1);
	}

	private native void n_onDrmSessionAcquired (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1);

	public void onDrmSessionManagerError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1)
	{
		n_onDrmSessionManagerError (p0, p1);
	}

	private native void n_onDrmSessionManagerError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1);

	public void onDrmSessionReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0)
	{
		n_onDrmSessionReleased (p0);
	}

	private native void n_onDrmSessionReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0);

	public void onDroppedVideoFrames (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, long p2)
	{
		n_onDroppedVideoFrames (p0, p1, p2);
	}

	private native void n_onDroppedVideoFrames (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, long p2);

	public void onEvents (androidx.media3.common.Player p0, androidx.media3.exoplayer.analytics.AnalyticsListener.Events p1)
	{
		n_onEvents (p0, p1);
	}

	private native void n_onEvents (androidx.media3.common.Player p0, androidx.media3.exoplayer.analytics.AnalyticsListener.Events p1);

	public void onIsLoadingChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onIsLoadingChanged (p0, p1);
	}

	private native void n_onIsLoadingChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1);

	public void onIsPlayingChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onIsPlayingChanged (p0, p1);
	}

	private native void n_onIsPlayingChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1);

	public void onLoadCanceled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.LoadEventInfo p1, androidx.media3.exoplayer.source.MediaLoadData p2)
	{
		n_onLoadCanceled (p0, p1, p2);
	}

	private native void n_onLoadCanceled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.LoadEventInfo p1, androidx.media3.exoplayer.source.MediaLoadData p2);

	public void onLoadCompleted (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.LoadEventInfo p1, androidx.media3.exoplayer.source.MediaLoadData p2)
	{
		n_onLoadCompleted (p0, p1, p2);
	}

	private native void n_onLoadCompleted (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.LoadEventInfo p1, androidx.media3.exoplayer.source.MediaLoadData p2);

	public void onLoadError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.LoadEventInfo p1, androidx.media3.exoplayer.source.MediaLoadData p2, java.io.IOException p3, boolean p4)
	{
		n_onLoadError (p0, p1, p2, p3, p4);
	}

	private native void n_onLoadError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.LoadEventInfo p1, androidx.media3.exoplayer.source.MediaLoadData p2, java.io.IOException p3, boolean p4);

	public void onLoadStarted (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.LoadEventInfo p1, androidx.media3.exoplayer.source.MediaLoadData p2)
	{
		n_onLoadStarted (p0, p1, p2);
	}

	private native void n_onLoadStarted (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.LoadEventInfo p1, androidx.media3.exoplayer.source.MediaLoadData p2);

	public void onLoadingChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onLoadingChanged (p0, p1);
	}

	private native void n_onLoadingChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1);

	public void onMaxSeekToPreviousPositionChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1)
	{
		n_onMaxSeekToPreviousPositionChanged (p0, p1);
	}

	private native void n_onMaxSeekToPreviousPositionChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1);

	public void onMediaItemTransition (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.MediaItem p1, int p2)
	{
		n_onMediaItemTransition (p0, p1, p2);
	}

	private native void n_onMediaItemTransition (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.MediaItem p1, int p2);

	public void onMediaMetadataChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.MediaMetadata p1)
	{
		n_onMediaMetadataChanged (p0, p1);
	}

	private native void n_onMediaMetadataChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.MediaMetadata p1);

	public void onMetadata (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Metadata p1)
	{
		n_onMetadata (p0, p1);
	}

	private native void n_onMetadata (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Metadata p1);

	public void onPlayWhenReadyChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1, int p2)
	{
		n_onPlayWhenReadyChanged (p0, p1, p2);
	}

	private native void n_onPlayWhenReadyChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1, int p2);

	public void onPlaybackParametersChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.PlaybackParameters p1)
	{
		n_onPlaybackParametersChanged (p0, p1);
	}

	private native void n_onPlaybackParametersChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.PlaybackParameters p1);

	public void onPlaybackStateChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onPlaybackStateChanged (p0, p1);
	}

	private native void n_onPlaybackStateChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1);

	public void onPlaybackSuppressionReasonChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onPlaybackSuppressionReasonChanged (p0, p1);
	}

	private native void n_onPlaybackSuppressionReasonChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1);

	public void onPlayerError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.PlaybackException p1)
	{
		n_onPlayerError (p0, p1);
	}

	private native void n_onPlayerError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.PlaybackException p1);

	public void onPlayerErrorChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.PlaybackException p1)
	{
		n_onPlayerErrorChanged (p0, p1);
	}

	private native void n_onPlayerErrorChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.PlaybackException p1);

	public void onPlayerReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0)
	{
		n_onPlayerReleased (p0);
	}

	private native void n_onPlayerReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0);

	public void onPlayerStateChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1, int p2)
	{
		n_onPlayerStateChanged (p0, p1, p2);
	}

	private native void n_onPlayerStateChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1, int p2);

	public void onPlaylistMetadataChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.MediaMetadata p1)
	{
		n_onPlaylistMetadataChanged (p0, p1);
	}

	private native void n_onPlaylistMetadataChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.MediaMetadata p1);

	public void onPositionDiscontinuity (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Player.PositionInfo p1, androidx.media3.common.Player.PositionInfo p2, int p3)
	{
		n_onPositionDiscontinuity (p0, p1, p2, p3);
	}

	private native void n_onPositionDiscontinuity (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Player.PositionInfo p1, androidx.media3.common.Player.PositionInfo p2, int p3);

	public void onPositionDiscontinuity (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onPositionDiscontinuity (p0, p1);
	}

	private native void n_onPositionDiscontinuity (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1);

	public void onRenderedFirstFrame (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Object p1, long p2)
	{
		n_onRenderedFirstFrame (p0, p1, p2);
	}

	private native void n_onRenderedFirstFrame (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Object p1, long p2);

	public void onRendererReadyChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, int p2, boolean p3)
	{
		n_onRendererReadyChanged (p0, p1, p2, p3);
	}

	private native void n_onRendererReadyChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, int p2, boolean p3);

	public void onRepeatModeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onRepeatModeChanged (p0, p1);
	}

	private native void n_onRepeatModeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1);

	public void onSeekBackIncrementChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1)
	{
		n_onSeekBackIncrementChanged (p0, p1);
	}

	private native void n_onSeekBackIncrementChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1);

	public void onSeekForwardIncrementChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1)
	{
		n_onSeekForwardIncrementChanged (p0, p1);
	}

	private native void n_onSeekForwardIncrementChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1);

	public void onSeekStarted (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0)
	{
		n_onSeekStarted (p0);
	}

	private native void n_onSeekStarted (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0);

	public void onShuffleModeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onShuffleModeChanged (p0, p1);
	}

	private native void n_onShuffleModeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1);

	public void onSkipSilenceEnabledChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1)
	{
		n_onSkipSilenceEnabledChanged (p0, p1);
	}

	private native void n_onSkipSilenceEnabledChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, boolean p1);

	public void onSurfaceSizeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, int p2)
	{
		n_onSurfaceSizeChanged (p0, p1, p2);
	}

	private native void n_onSurfaceSizeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, int p2);

	public void onTimelineChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1)
	{
		n_onTimelineChanged (p0, p1);
	}

	private native void n_onTimelineChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1);

	public void onTrackSelectionParametersChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.TrackSelectionParameters p1)
	{
		n_onTrackSelectionParametersChanged (p0, p1);
	}

	private native void n_onTrackSelectionParametersChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.TrackSelectionParameters p1);

	public void onTracksChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Tracks p1)
	{
		n_onTracksChanged (p0, p1);
	}

	private native void n_onTracksChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Tracks p1);

	public void onUpstreamDiscarded (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.MediaLoadData p1)
	{
		n_onUpstreamDiscarded (p0, p1);
	}

	private native void n_onUpstreamDiscarded (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.source.MediaLoadData p1);

	public void onVideoCodecError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1)
	{
		n_onVideoCodecError (p0, p1);
	}

	private native void n_onVideoCodecError (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.Exception p1);

	public void onVideoDecoderInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2)
	{
		n_onVideoDecoderInitialized (p0, p1, p2);
	}

	private native void n_onVideoDecoderInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2);

	public void onVideoDecoderInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2, long p3)
	{
		n_onVideoDecoderInitialized (p0, p1, p2, p3);
	}

	private native void n_onVideoDecoderInitialized (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, long p2, long p3);

	public void onVideoDecoderReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1)
	{
		n_onVideoDecoderReleased (p0, p1);
	}

	private native void n_onVideoDecoderReleased (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1);

	public void onVideoDisabled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.DecoderCounters p1)
	{
		n_onVideoDisabled (p0, p1);
	}

	private native void n_onVideoDisabled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.DecoderCounters p1);

	public void onVideoEnabled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.DecoderCounters p1)
	{
		n_onVideoEnabled (p0, p1);
	}

	private native void n_onVideoEnabled (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.exoplayer.DecoderCounters p1);

	public void onVideoFrameProcessingOffset (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1, int p2)
	{
		n_onVideoFrameProcessingOffset (p0, p1, p2);
	}

	private native void n_onVideoFrameProcessingOffset (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, long p1, int p2);

	public void onVideoInputFormatChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Format p1, androidx.media3.exoplayer.DecoderReuseEvaluation p2)
	{
		n_onVideoInputFormatChanged (p0, p1, p2);
	}

	private native void n_onVideoInputFormatChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.Format p1, androidx.media3.exoplayer.DecoderReuseEvaluation p2);

	public void onVideoSizeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.VideoSize p1)
	{
		n_onVideoSizeChanged (p0, p1);
	}

	private native void n_onVideoSizeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, androidx.media3.common.VideoSize p1);

	public void onVideoSizeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, int p2, int p3, float p4)
	{
		n_onVideoSizeChanged (p0, p1, p2, p3, p4);
	}

	private native void n_onVideoSizeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, int p1, int p2, int p3, float p4);

	public void onVolumeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, float p1)
	{
		n_onVolumeChanged (p0, p1);
	}

	private native void n_onVolumeChanged (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, float p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
