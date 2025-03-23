package mono.androidx.media3.exoplayer.drm;


public class ExoMediaDrm_OnKeyStatusChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.drm.ExoMediaDrm.OnKeyStatusChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onKeyStatusChange:(Landroidx/media3/exoplayer/drm/ExoMediaDrm;[BLjava/util/List;Z)V:GetOnKeyStatusChange_Landroidx_media3_exoplayer_drm_ExoMediaDrm_arrayBLjava_util_List_ZHandler:AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnKeyStatusChangeListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnKeyStatusChangeListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", ExoMediaDrm_OnKeyStatusChangeListenerImplementor.class, __md_methods);
	}

	public ExoMediaDrm_OnKeyStatusChangeListenerImplementor ()
	{
		super ();
		if (getClass () == ExoMediaDrm_OnKeyStatusChangeListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Drm.IExoMediaDrmOnKeyStatusChangeListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onKeyStatusChange (androidx.media3.exoplayer.drm.ExoMediaDrm p0, byte[] p1, java.util.List p2, boolean p3)
	{
		n_onKeyStatusChange (p0, p1, p2, p3);
	}

	private native void n_onKeyStatusChange (androidx.media3.exoplayer.drm.ExoMediaDrm p0, byte[] p1, java.util.List p2, boolean p3);

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
