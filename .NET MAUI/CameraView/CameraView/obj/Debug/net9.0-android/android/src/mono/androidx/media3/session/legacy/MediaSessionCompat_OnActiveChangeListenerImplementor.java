package mono.androidx.media3.session.legacy;


public class MediaSessionCompat_OnActiveChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.session.legacy.MediaSessionCompat.OnActiveChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onActiveChanged:()V:GetOnActiveChangedHandler:AndroidX.Media3.Session.Legacy.MediaSessionCompat/IOnActiveChangeListenerInvoker, Xamarin.AndroidX.Media3.Session\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.Session.Legacy.MediaSessionCompat+IOnActiveChangeListenerImplementor, Xamarin.AndroidX.Media3.Session", MediaSessionCompat_OnActiveChangeListenerImplementor.class, __md_methods);
	}

	public MediaSessionCompat_OnActiveChangeListenerImplementor ()
	{
		super ();
		if (getClass () == MediaSessionCompat_OnActiveChangeListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.Session.Legacy.MediaSessionCompat+IOnActiveChangeListenerImplementor, Xamarin.AndroidX.Media3.Session", "", this, new java.lang.Object[] {  });
		}
	}

	public void onActiveChanged ()
	{
		n_onActiveChanged ();
	}

	private native void n_onActiveChanged ();

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
