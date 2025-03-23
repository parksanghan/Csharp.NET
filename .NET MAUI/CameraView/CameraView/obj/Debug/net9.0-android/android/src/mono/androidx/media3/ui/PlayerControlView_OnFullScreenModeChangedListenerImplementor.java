package mono.androidx.media3.ui;


public class PlayerControlView_OnFullScreenModeChangedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.ui.PlayerControlView.OnFullScreenModeChangedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFullScreenModeChanged:(Z)V:GetOnFullScreenModeChanged_ZHandler:AndroidX.Media3.UI.PlayerControlView/IOnFullScreenModeChangedListenerInvoker, Xamarin.AndroidX.Media3.Ui\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.UI.PlayerControlView+IOnFullScreenModeChangedListenerImplementor, Xamarin.AndroidX.Media3.Ui", PlayerControlView_OnFullScreenModeChangedListenerImplementor.class, __md_methods);
	}

	public PlayerControlView_OnFullScreenModeChangedListenerImplementor ()
	{
		super ();
		if (getClass () == PlayerControlView_OnFullScreenModeChangedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.UI.PlayerControlView+IOnFullScreenModeChangedListenerImplementor, Xamarin.AndroidX.Media3.Ui", "", this, new java.lang.Object[] {  });
		}
	}

	public void onFullScreenModeChanged (boolean p0)
	{
		n_onFullScreenModeChanged (p0);
	}

	private native void n_onFullScreenModeChanged (boolean p0);

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
