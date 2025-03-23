package mono.androidx.media3.session;


public class MediaSessionService_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.session.MediaSessionService.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onForegroundServiceStartNotAllowedException:()V:GetOnForegroundServiceStartNotAllowedExceptionHandler:AndroidX.Media3.Session.MediaSessionService/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.Session.MediaSessionService+IListenerImplementor, Xamarin.AndroidX.Media3.Session", MediaSessionService_ListenerImplementor.class, __md_methods);
	}

	public MediaSessionService_ListenerImplementor ()
	{
		super ();
		if (getClass () == MediaSessionService_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.Session.MediaSessionService+IListenerImplementor, Xamarin.AndroidX.Media3.Session", "", this, new java.lang.Object[] {  });
		}
	}

	public void onForegroundServiceStartNotAllowedException ()
	{
		n_onForegroundServiceStartNotAllowedException ();
	}

	private native void n_onForegroundServiceStartNotAllowedException ();

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
