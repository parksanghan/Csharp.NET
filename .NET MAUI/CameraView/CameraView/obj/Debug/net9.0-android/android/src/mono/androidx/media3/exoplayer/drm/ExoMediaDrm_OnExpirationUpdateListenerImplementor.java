package mono.androidx.media3.exoplayer.drm;


public class ExoMediaDrm_OnExpirationUpdateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.drm.ExoMediaDrm.OnExpirationUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onExpirationUpdate:(Landroidx/media3/exoplayer/drm/ExoMediaDrm;[BJ)V:GetOnExpirationUpdate_Landroidx_media3_exoplayer_drm_ExoMediaDrm_arrayBJHandler:AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnExpirationUpdateListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnExpirationUpdateListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", ExoMediaDrm_OnExpirationUpdateListenerImplementor.class, __md_methods);
	}

	public ExoMediaDrm_OnExpirationUpdateListenerImplementor ()
	{
		super ();
		if (getClass () == ExoMediaDrm_OnExpirationUpdateListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnExpirationUpdateListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onExpirationUpdate (androidx.media3.exoplayer.drm.ExoMediaDrm p0, byte[] p1, long p2)
	{
		n_onExpirationUpdate (p0, p1, p2);
	}

	private native void n_onExpirationUpdate (androidx.media3.exoplayer.drm.ExoMediaDrm p0, byte[] p1, long p2);

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
