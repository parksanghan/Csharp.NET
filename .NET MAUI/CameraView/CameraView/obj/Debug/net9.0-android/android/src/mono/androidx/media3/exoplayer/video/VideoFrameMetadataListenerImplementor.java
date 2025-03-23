package mono.androidx.media3.exoplayer.video;


public class VideoFrameMetadataListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.video.VideoFrameMetadataListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onVideoFrameAboutToBeRendered:(JJLandroidx/media3/common/Format;Landroid/media/MediaFormat;)V:GetOnVideoFrameAboutToBeRendered_JJLandroidx_media3_common_Format_Landroid_media_MediaFormat_Handler:AndroidX.Media3.ExoPlayer.Video.IVideoFrameMetadataListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Video.IVideoFrameMetadataListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", VideoFrameMetadataListenerImplementor.class, __md_methods);
	}

	public VideoFrameMetadataListenerImplementor ()
	{
		super ();
		if (getClass () == VideoFrameMetadataListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Video.IVideoFrameMetadataListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onVideoFrameAboutToBeRendered (long p0, long p1, androidx.media3.common.Format p2, android.media.MediaFormat p3)
	{
		n_onVideoFrameAboutToBeRendered (p0, p1, p2, p3);
	}

	private native void n_onVideoFrameAboutToBeRendered (long p0, long p1, androidx.media3.common.Format p2, android.media.MediaFormat p3);

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
