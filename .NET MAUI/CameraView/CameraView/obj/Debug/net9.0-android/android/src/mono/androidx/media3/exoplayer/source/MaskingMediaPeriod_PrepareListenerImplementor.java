package mono.androidx.media3.exoplayer.source;


public class MaskingMediaPeriod_PrepareListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.source.MaskingMediaPeriod.PrepareListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPrepareComplete:(Landroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;)V:GetOnPrepareComplete_Landroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Handler:AndroidX.Media3.ExoPlayer.Source.MaskingMediaPeriod/IPrepareListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPrepareError:(Landroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;Ljava/io/IOException;)V:GetOnPrepareError_Landroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Ljava_io_IOException_Handler:AndroidX.Media3.ExoPlayer.Source.MaskingMediaPeriod/IPrepareListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Source.MaskingMediaPeriod+IPrepareListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", MaskingMediaPeriod_PrepareListenerImplementor.class, __md_methods);
	}

	public MaskingMediaPeriod_PrepareListenerImplementor ()
	{
		super ();
		if (getClass () == MaskingMediaPeriod_PrepareListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Source.MaskingMediaPeriod+IPrepareListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onPrepareComplete (androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p0)
	{
		n_onPrepareComplete (p0);
	}

	private native void n_onPrepareComplete (androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p0);

	public void onPrepareError (androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p0, java.io.IOException p1)
	{
		n_onPrepareError (p0, p1);
	}

	private native void n_onPrepareError (androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p0, java.io.IOException p1);

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
