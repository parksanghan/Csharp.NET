package mono.androidx.media3.exoplayer.video;


public class VideoSink_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.video.VideoSink.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onError:(Landroidx/media3/exoplayer/video/VideoSink;Landroidx/media3/exoplayer/video/VideoSink$VideoSinkException;)V:GetOnError_Landroidx_media3_exoplayer_video_VideoSink_Landroidx_media3_exoplayer_video_VideoSink_VideoSinkException_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoSinkListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onFirstFrameRendered:(Landroidx/media3/exoplayer/video/VideoSink;)V:GetOnFirstFrameRendered_Landroidx_media3_exoplayer_video_VideoSink_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoSinkListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onFrameDropped:(Landroidx/media3/exoplayer/video/VideoSink;)V:GetOnFrameDropped_Landroidx_media3_exoplayer_video_VideoSink_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoSinkListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoSizeChanged:(Landroidx/media3/exoplayer/video/VideoSink;Landroidx/media3/common/VideoSize;)V:GetOnVideoSizeChanged_Landroidx_media3_exoplayer_video_VideoSink_Landroidx_media3_common_VideoSize_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoSinkListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Video.IVideoSinkListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", VideoSink_ListenerImplementor.class, __md_methods);
	}

	public VideoSink_ListenerImplementor ()
	{
		super ();
		if (getClass () == VideoSink_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Video.IVideoSinkListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onError (androidx.media3.exoplayer.video.VideoSink p0, androidx.media3.exoplayer.video.VideoSink.VideoSinkException p1)
	{
		n_onError (p0, p1);
	}

	private native void n_onError (androidx.media3.exoplayer.video.VideoSink p0, androidx.media3.exoplayer.video.VideoSink.VideoSinkException p1);

	public void onFirstFrameRendered (androidx.media3.exoplayer.video.VideoSink p0)
	{
		n_onFirstFrameRendered (p0);
	}

	private native void n_onFirstFrameRendered (androidx.media3.exoplayer.video.VideoSink p0);

	public void onFrameDropped (androidx.media3.exoplayer.video.VideoSink p0)
	{
		n_onFrameDropped (p0);
	}

	private native void n_onFrameDropped (androidx.media3.exoplayer.video.VideoSink p0);

	public void onVideoSizeChanged (androidx.media3.exoplayer.video.VideoSink p0, androidx.media3.common.VideoSize p1)
	{
		n_onVideoSizeChanged (p0, p1);
	}

	private native void n_onVideoSizeChanged (androidx.media3.exoplayer.video.VideoSink p0, androidx.media3.common.VideoSize p1);

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
