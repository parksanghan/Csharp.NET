package crc64ceb75e76f4b66147;


public class MediaManager
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.common.Player.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAudioAttributesChanged:(Landroidx/media3/common/AudioAttributes;)V:GetOnAudioAttributesChanged_Landroidx_media3_common_AudioAttributes_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onAudioSessionIdChanged:(I)V:GetOnAudioSessionIdChanged_IHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onAvailableCommandsChanged:(Landroidx/media3/common/Player$Commands;)V:GetOnAvailableCommandsChanged_Landroidx_media3_common_Player_Commands_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onCues:(Landroidx/media3/common/text/CueGroup;)V:GetOnCues_Landroidx_media3_common_text_CueGroup_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onCues:(Ljava/util/List;)V:GetOnCuesDeprecated_Ljava_util_List_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onDeviceInfoChanged:(Landroidx/media3/common/DeviceInfo;)V:GetOnDeviceInfoChanged_Landroidx_media3_common_DeviceInfo_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onDeviceVolumeChanged:(IZ)V:GetOnDeviceVolumeChanged_IZHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onEvents:(Landroidx/media3/common/Player;Landroidx/media3/common/Player$Events;)V:GetOnEvents_Landroidx_media3_common_Player_Landroidx_media3_common_Player_Events_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onIsLoadingChanged:(Z)V:GetOnIsLoadingChanged_ZHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onIsPlayingChanged:(Z)V:GetOnIsPlayingChanged_ZHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onLoadingChanged:(Z)V:GetOnLoadingChanged_ZHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onMaxSeekToPreviousPositionChanged:(J)V:GetOnMaxSeekToPreviousPositionChanged_JHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onMediaItemTransition:(Landroidx/media3/common/MediaItem;I)V:GetOnMediaItemTransition_Landroidx_media3_common_MediaItem_IHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onMediaMetadataChanged:(Landroidx/media3/common/MediaMetadata;)V:GetOnMediaMetadataChanged_Landroidx_media3_common_MediaMetadata_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onMetadata:(Landroidx/media3/common/Metadata;)V:GetOnMetadata_Landroidx_media3_common_Metadata_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPlayWhenReadyChanged:(ZI)V:GetOnPlayWhenReadyChanged_ZIHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPlaybackParametersChanged:(Landroidx/media3/common/PlaybackParameters;)V:GetOnPlaybackParametersChanged_Landroidx_media3_common_PlaybackParameters_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPlaybackStateChanged:(I)V:GetOnPlaybackStateChanged_IHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPlaybackSuppressionReasonChanged:(I)V:GetOnPlaybackSuppressionReasonChanged_IHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPlayerError:(Landroidx/media3/common/PlaybackException;)V:GetOnPlayerError_Landroidx_media3_common_PlaybackException_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPlayerErrorChanged:(Landroidx/media3/common/PlaybackException;)V:GetOnPlayerErrorChanged_Landroidx_media3_common_PlaybackException_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPlayerStateChanged:(ZI)V:GetOnPlayerStateChanged_ZIHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPlaylistMetadataChanged:(Landroidx/media3/common/MediaMetadata;)V:GetOnPlaylistMetadataChanged_Landroidx_media3_common_MediaMetadata_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onPositionDiscontinuity:(Landroidx/media3/common/Player$PositionInfo;Landroidx/media3/common/Player$PositionInfo;I)V:GetOnPositionDiscontinuity_Landroidx_media3_common_Player_PositionInfo_Landroidx_media3_common_Player_PositionInfo_IHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onRenderedFirstFrame:()V:GetOnRenderedFirstFrameHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onRepeatModeChanged:(I)V:GetOnRepeatModeChanged_IHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onSeekBackIncrementChanged:(J)V:GetOnSeekBackIncrementChanged_JHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onSeekForwardIncrementChanged:(J)V:GetOnSeekForwardIncrementChanged_JHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onShuffleModeEnabledChanged:(Z)V:GetOnShuffleModeEnabledChanged_ZHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onSkipSilenceEnabledChanged:(Z)V:GetOnSkipSilenceEnabledChanged_ZHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onSurfaceSizeChanged:(II)V:GetOnSurfaceSizeChanged_IIHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onTimelineChanged:(Landroidx/media3/common/Timeline;I)V:GetOnTimelineChanged_Landroidx_media3_common_Timeline_IHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onTrackSelectionParametersChanged:(Landroidx/media3/common/TrackSelectionParameters;)V:GetOnTrackSelectionParametersChanged_Landroidx_media3_common_TrackSelectionParameters_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onTracksChanged:(Landroidx/media3/common/Tracks;)V:GetOnTracksChanged_Landroidx_media3_common_Tracks_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onVideoSizeChanged:(Landroidx/media3/common/VideoSize;)V:GetOnVideoSizeChanged_Landroidx_media3_common_VideoSize_Handler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onVolumeChanged:(F)V:GetOnVolumeChanged_FHandler:AndroidX.Media3.Common.IPlayerListener, Xamarin.AndroidX.Media3.Common\n" +
			"";
		mono.android.Runtime.register ("CommunityToolkit.Maui.Core.Views.MediaManager, CommunityToolkit.Maui.MediaElement", MediaManager.class, __md_methods);
	}

	public MediaManager ()
	{
		super ();
		if (getClass () == MediaManager.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Core.Views.MediaManager, CommunityToolkit.Maui.MediaElement", "", this, new java.lang.Object[] {  });
		}
	}

	public void onAudioAttributesChanged (androidx.media3.common.AudioAttributes p0)
	{
		n_onAudioAttributesChanged (p0);
	}

	private native void n_onAudioAttributesChanged (androidx.media3.common.AudioAttributes p0);

	public void onAudioSessionIdChanged (int p0)
	{
		n_onAudioSessionIdChanged (p0);
	}

	private native void n_onAudioSessionIdChanged (int p0);

	public void onAvailableCommandsChanged (androidx.media3.common.Player.Commands p0)
	{
		n_onAvailableCommandsChanged (p0);
	}

	private native void n_onAvailableCommandsChanged (androidx.media3.common.Player.Commands p0);

	public void onCues (androidx.media3.common.text.CueGroup p0)
	{
		n_onCues (p0);
	}

	private native void n_onCues (androidx.media3.common.text.CueGroup p0);

	public void onCues (java.util.List p0)
	{
		n_onCues (p0);
	}

	private native void n_onCues (java.util.List p0);

	public void onDeviceInfoChanged (androidx.media3.common.DeviceInfo p0)
	{
		n_onDeviceInfoChanged (p0);
	}

	private native void n_onDeviceInfoChanged (androidx.media3.common.DeviceInfo p0);

	public void onDeviceVolumeChanged (int p0, boolean p1)
	{
		n_onDeviceVolumeChanged (p0, p1);
	}

	private native void n_onDeviceVolumeChanged (int p0, boolean p1);

	public void onEvents (androidx.media3.common.Player p0, androidx.media3.common.Player.Events p1)
	{
		n_onEvents (p0, p1);
	}

	private native void n_onEvents (androidx.media3.common.Player p0, androidx.media3.common.Player.Events p1);

	public void onIsLoadingChanged (boolean p0)
	{
		n_onIsLoadingChanged (p0);
	}

	private native void n_onIsLoadingChanged (boolean p0);

	public void onIsPlayingChanged (boolean p0)
	{
		n_onIsPlayingChanged (p0);
	}

	private native void n_onIsPlayingChanged (boolean p0);

	public void onLoadingChanged (boolean p0)
	{
		n_onLoadingChanged (p0);
	}

	private native void n_onLoadingChanged (boolean p0);

	public void onMaxSeekToPreviousPositionChanged (long p0)
	{
		n_onMaxSeekToPreviousPositionChanged (p0);
	}

	private native void n_onMaxSeekToPreviousPositionChanged (long p0);

	public void onMediaItemTransition (androidx.media3.common.MediaItem p0, int p1)
	{
		n_onMediaItemTransition (p0, p1);
	}

	private native void n_onMediaItemTransition (androidx.media3.common.MediaItem p0, int p1);

	public void onMediaMetadataChanged (androidx.media3.common.MediaMetadata p0)
	{
		n_onMediaMetadataChanged (p0);
	}

	private native void n_onMediaMetadataChanged (androidx.media3.common.MediaMetadata p0);

	public void onMetadata (androidx.media3.common.Metadata p0)
	{
		n_onMetadata (p0);
	}

	private native void n_onMetadata (androidx.media3.common.Metadata p0);

	public void onPlayWhenReadyChanged (boolean p0, int p1)
	{
		n_onPlayWhenReadyChanged (p0, p1);
	}

	private native void n_onPlayWhenReadyChanged (boolean p0, int p1);

	public void onPlaybackParametersChanged (androidx.media3.common.PlaybackParameters p0)
	{
		n_onPlaybackParametersChanged (p0);
	}

	private native void n_onPlaybackParametersChanged (androidx.media3.common.PlaybackParameters p0);

	public void onPlaybackStateChanged (int p0)
	{
		n_onPlaybackStateChanged (p0);
	}

	private native void n_onPlaybackStateChanged (int p0);

	public void onPlaybackSuppressionReasonChanged (int p0)
	{
		n_onPlaybackSuppressionReasonChanged (p0);
	}

	private native void n_onPlaybackSuppressionReasonChanged (int p0);

	public void onPlayerError (androidx.media3.common.PlaybackException p0)
	{
		n_onPlayerError (p0);
	}

	private native void n_onPlayerError (androidx.media3.common.PlaybackException p0);

	public void onPlayerErrorChanged (androidx.media3.common.PlaybackException p0)
	{
		n_onPlayerErrorChanged (p0);
	}

	private native void n_onPlayerErrorChanged (androidx.media3.common.PlaybackException p0);

	public void onPlayerStateChanged (boolean p0, int p1)
	{
		n_onPlayerStateChanged (p0, p1);
	}

	private native void n_onPlayerStateChanged (boolean p0, int p1);

	public void onPlaylistMetadataChanged (androidx.media3.common.MediaMetadata p0)
	{
		n_onPlaylistMetadataChanged (p0);
	}

	private native void n_onPlaylistMetadataChanged (androidx.media3.common.MediaMetadata p0);

	public void onPositionDiscontinuity (androidx.media3.common.Player.PositionInfo p0, androidx.media3.common.Player.PositionInfo p1, int p2)
	{
		n_onPositionDiscontinuity (p0, p1, p2);
	}

	private native void n_onPositionDiscontinuity (androidx.media3.common.Player.PositionInfo p0, androidx.media3.common.Player.PositionInfo p1, int p2);

	public void onRenderedFirstFrame ()
	{
		n_onRenderedFirstFrame ();
	}

	private native void n_onRenderedFirstFrame ();

	public void onRepeatModeChanged (int p0)
	{
		n_onRepeatModeChanged (p0);
	}

	private native void n_onRepeatModeChanged (int p0);

	public void onSeekBackIncrementChanged (long p0)
	{
		n_onSeekBackIncrementChanged (p0);
	}

	private native void n_onSeekBackIncrementChanged (long p0);

	public void onSeekForwardIncrementChanged (long p0)
	{
		n_onSeekForwardIncrementChanged (p0);
	}

	private native void n_onSeekForwardIncrementChanged (long p0);

	public void onShuffleModeEnabledChanged (boolean p0)
	{
		n_onShuffleModeEnabledChanged (p0);
	}

	private native void n_onShuffleModeEnabledChanged (boolean p0);

	public void onSkipSilenceEnabledChanged (boolean p0)
	{
		n_onSkipSilenceEnabledChanged (p0);
	}

	private native void n_onSkipSilenceEnabledChanged (boolean p0);

	public void onSurfaceSizeChanged (int p0, int p1)
	{
		n_onSurfaceSizeChanged (p0, p1);
	}

	private native void n_onSurfaceSizeChanged (int p0, int p1);

	public void onTimelineChanged (androidx.media3.common.Timeline p0, int p1)
	{
		n_onTimelineChanged (p0, p1);
	}

	private native void n_onTimelineChanged (androidx.media3.common.Timeline p0, int p1);

	public void onTrackSelectionParametersChanged (androidx.media3.common.TrackSelectionParameters p0)
	{
		n_onTrackSelectionParametersChanged (p0);
	}

	private native void n_onTrackSelectionParametersChanged (androidx.media3.common.TrackSelectionParameters p0);

	public void onTracksChanged (androidx.media3.common.Tracks p0)
	{
		n_onTracksChanged (p0);
	}

	private native void n_onTracksChanged (androidx.media3.common.Tracks p0);

	public void onVideoSizeChanged (androidx.media3.common.VideoSize p0)
	{
		n_onVideoSizeChanged (p0);
	}

	private native void n_onVideoSizeChanged (androidx.media3.common.VideoSize p0);

	public void onVolumeChanged (float p0)
	{
		n_onVolumeChanged (p0);
	}

	private native void n_onVolumeChanged (float p0);

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
