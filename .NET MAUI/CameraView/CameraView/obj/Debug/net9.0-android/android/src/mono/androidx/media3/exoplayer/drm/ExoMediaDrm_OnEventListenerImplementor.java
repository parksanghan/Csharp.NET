package mono.androidx.media3.exoplayer.drm;


public class ExoMediaDrm_OnEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.drm.ExoMediaDrm.OnEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEvent:(Landroidx/media3/exoplayer/drm/ExoMediaDrm;[BII[B)V:GetOnEvent_Landroidx_media3_exoplayer_drm_ExoMediaDrm_arrayBIIarrayBHandler:AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnEventListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", ExoMediaDrm_OnEventListenerImplementor.class, __md_methods);
	}

	public ExoMediaDrm_OnEventListenerImplementor ()
	{
		super ();
		if (getClass () == ExoMediaDrm_OnEventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onEvent (androidx.media3.exoplayer.drm.ExoMediaDrm p0, byte[] p1, int p2, int p3, byte[] p4)
	{
		n_onEvent (p0, p1, p2, p3, p4);
	}

	private native void n_onEvent (androidx.media3.exoplayer.drm.ExoMediaDrm p0, byte[] p1, int p2, int p3, byte[] p4);

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
