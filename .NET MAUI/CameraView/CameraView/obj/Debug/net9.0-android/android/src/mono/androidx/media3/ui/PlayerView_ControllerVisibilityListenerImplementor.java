package mono.androidx.media3.ui;


public class PlayerView_ControllerVisibilityListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.ui.PlayerView.ControllerVisibilityListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onVisibilityChanged:(I)V:GetOnVisibilityChanged_IHandler:AndroidX.Media3.UI.PlayerView/IControllerVisibilityListenerInvoker, Xamarin.AndroidX.Media3.Ui\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.UI.PlayerView+IControllerVisibilityListenerImplementor, Xamarin.AndroidX.Media3.Ui", PlayerView_ControllerVisibilityListenerImplementor.class, __md_methods);
	}

	public PlayerView_ControllerVisibilityListenerImplementor ()
	{
		super ();
		if (getClass () == PlayerView_ControllerVisibilityListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.UI.PlayerView+IControllerVisibilityListenerImplementor, Xamarin.AndroidX.Media3.Ui", "", this, new java.lang.Object[] {  });
		}
	}

	public void onVisibilityChanged (int p0)
	{
		n_onVisibilityChanged (p0);
	}

	private native void n_onVisibilityChanged (int p0);

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
