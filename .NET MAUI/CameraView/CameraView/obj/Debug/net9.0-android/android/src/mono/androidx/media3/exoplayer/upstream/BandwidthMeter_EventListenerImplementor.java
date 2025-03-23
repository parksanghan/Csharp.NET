package mono.androidx.media3.exoplayer.upstream;


public class BandwidthMeter_EventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.upstream.BandwidthMeter.EventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBandwidthSample:(IJJ)V:GetOnBandwidthSample_IJJHandler:AndroidX.Media3.ExoPlayer.Upstream.IBandwidthMeterEventListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Upstream.IBandwidthMeterEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", BandwidthMeter_EventListenerImplementor.class, __md_methods);
	}

	public BandwidthMeter_EventListenerImplementor ()
	{
		super ();
		if (getClass () == BandwidthMeter_EventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Upstream.IBandwidthMeterEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onBandwidthSample (int p0, long p1, long p2)
	{
		n_onBandwidthSample (p0, p1, p2);
	}

	private native void n_onBandwidthSample (int p0, long p1, long p2);

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
