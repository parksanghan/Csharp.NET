package mono.androidx.media3.exoplayer.mediacodec;


public class MediaCodecAdapter_OnFrameRenderedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.mediacodec.MediaCodecAdapter.OnFrameRenderedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFrameRendered:(Landroidx/media3/exoplayer/mediacodec/MediaCodecAdapter;JJ)V:GetOnFrameRendered_Landroidx_media3_exoplayer_mediacodec_MediaCodecAdapter_JJHandler:AndroidX.Media3.ExoPlayer.MediaCodec.IMediaCodecAdapterOnFrameRenderedListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.MediaCodec.IMediaCodecAdapterOnFrameRenderedListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", MediaCodecAdapter_OnFrameRenderedListenerImplementor.class, __md_methods);
	}

	public MediaCodecAdapter_OnFrameRenderedListenerImplementor ()
	{
		super ();
		if (getClass () == MediaCodecAdapter_OnFrameRenderedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.MediaCodec.IMediaCodecAdapterOnFrameRenderedListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onFrameRendered (androidx.media3.exoplayer.mediacodec.MediaCodecAdapter p0, long p1, long p2)
	{
		n_onFrameRendered (p0, p1, p2);
	}

	private native void n_onFrameRendered (androidx.media3.exoplayer.mediacodec.MediaCodecAdapter p0, long p1, long p2);

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
