package mono.androidx.media3.exoplayer.video;


public class VideoRendererEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.video.VideoRendererEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDroppedFrames:(IJ)V:GetOnDroppedFrames_IJHandler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onRenderedFirstFrame:(Ljava/lang/Object;J)V:GetOnRenderedFirstFrame_Ljava_lang_Object_JHandler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoCodecError:(Ljava/lang/Exception;)V:GetOnVideoCodecError_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoDecoderInitialized:(Ljava/lang/String;JJ)V:GetOnVideoDecoderInitialized_Ljava_lang_String_JJHandler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoDecoderReleased:(Ljava/lang/String;)V:GetOnVideoDecoderReleased_Ljava_lang_String_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoDisabled:(Landroidx/media3/exoplayer/DecoderCounters;)V:GetOnVideoDisabled_Landroidx_media3_exoplayer_DecoderCounters_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoEnabled:(Landroidx/media3/exoplayer/DecoderCounters;)V:GetOnVideoEnabled_Landroidx_media3_exoplayer_DecoderCounters_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoFrameProcessingOffset:(JI)V:GetOnVideoFrameProcessingOffset_JIHandler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoInputFormatChanged:(Landroidx/media3/common/Format;Landroidx/media3/exoplayer/DecoderReuseEvaluation;)V:GetOnVideoInputFormatChanged_Landroidx_media3_common_Format_Landroidx_media3_exoplayer_DecoderReuseEvaluation_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onVideoSizeChanged:(Landroidx/media3/common/VideoSize;)V:GetOnVideoSizeChanged_Landroidx_media3_common_VideoSize_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", VideoRendererEventListenerImplementor.class, __md_methods);
	}

	public VideoRendererEventListenerImplementor ()
	{
		super ();
		if (getClass () == VideoRendererEventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Video.IVideoRendererEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onDroppedFrames (int p0, long p1)
	{
		n_onDroppedFrames (p0, p1);
	}

	private native void n_onDroppedFrames (int p0, long p1);

	public void onRenderedFirstFrame (java.lang.Object p0, long p1)
	{
		n_onRenderedFirstFrame (p0, p1);
	}

	private native void n_onRenderedFirstFrame (java.lang.Object p0, long p1);

	public void onVideoCodecError (java.lang.Exception p0)
	{
		n_onVideoCodecError (p0);
	}

	private native void n_onVideoCodecError (java.lang.Exception p0);

	public void onVideoDecoderInitialized (java.lang.String p0, long p1, long p2)
	{
		n_onVideoDecoderInitialized (p0, p1, p2);
	}

	private native void n_onVideoDecoderInitialized (java.lang.String p0, long p1, long p2);

	public void onVideoDecoderReleased (java.lang.String p0)
	{
		n_onVideoDecoderReleased (p0);
	}

	private native void n_onVideoDecoderReleased (java.lang.String p0);

	public void onVideoDisabled (androidx.media3.exoplayer.DecoderCounters p0)
	{
		n_onVideoDisabled (p0);
	}

	private native void n_onVideoDisabled (androidx.media3.exoplayer.DecoderCounters p0);

	public void onVideoEnabled (androidx.media3.exoplayer.DecoderCounters p0)
	{
		n_onVideoEnabled (p0);
	}

	private native void n_onVideoEnabled (androidx.media3.exoplayer.DecoderCounters p0);

	public void onVideoFrameProcessingOffset (long p0, int p1)
	{
		n_onVideoFrameProcessingOffset (p0, p1);
	}

	private native void n_onVideoFrameProcessingOffset (long p0, int p1);

	public void onVideoInputFormatChanged (androidx.media3.common.Format p0, androidx.media3.exoplayer.DecoderReuseEvaluation p1)
	{
		n_onVideoInputFormatChanged (p0, p1);
	}

	private native void n_onVideoInputFormatChanged (androidx.media3.common.Format p0, androidx.media3.exoplayer.DecoderReuseEvaluation p1);

	public void onVideoSizeChanged (androidx.media3.common.VideoSize p0)
	{
		n_onVideoSizeChanged (p0);
	}

	private native void n_onVideoSizeChanged (androidx.media3.common.VideoSize p0);

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
