package mono.androidx.media3.exoplayer.source;


public class SampleQueue_UpstreamFormatChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.source.SampleQueue.UpstreamFormatChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onUpstreamFormatChanged:(Landroidx/media3/common/Format;)V:GetOnUpstreamFormatChanged_Landroidx_media3_common_Format_Handler:AndroidX.Media3.ExoPlayer.Source.SampleQueue/IUpstreamFormatChangedListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Source.SampleQueue+IUpstreamFormatChangedListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", SampleQueue_UpstreamFormatChangedListenerImplementor.class, __md_methods);
	}

	public SampleQueue_UpstreamFormatChangedListenerImplementor ()
	{
		super ();
		if (getClass () == SampleQueue_UpstreamFormatChangedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Source.SampleQueue+IUpstreamFormatChangedListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onUpstreamFormatChanged (androidx.media3.common.Format p0)
	{
		n_onUpstreamFormatChanged (p0);
	}

	private native void n_onUpstreamFormatChanged (androidx.media3.common.Format p0);

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
