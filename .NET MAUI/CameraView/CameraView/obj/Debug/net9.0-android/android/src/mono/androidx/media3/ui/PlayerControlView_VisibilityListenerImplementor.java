package mono.androidx.media3.ui;


public class PlayerControlView_VisibilityListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.ui.PlayerControlView.VisibilityListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onVisibilityChange:(I)V:GetOnVisibilityChange_IHandler:AndroidX.Media3.UI.PlayerControlView/IVisibilityListenerInvoker, Xamarin.AndroidX.Media3.Ui\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.UI.PlayerControlView+IVisibilityListenerImplementor, Xamarin.AndroidX.Media3.Ui", PlayerControlView_VisibilityListenerImplementor.class, __md_methods);
	}

	public PlayerControlView_VisibilityListenerImplementor ()
	{
		super ();
		if (getClass () == PlayerControlView_VisibilityListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.UI.PlayerControlView+IVisibilityListenerImplementor, Xamarin.AndroidX.Media3.Ui", "", this, new java.lang.Object[] {  });
		}
	}

	public void onVisibilityChange (int p0)
	{
		n_onVisibilityChange (p0);
	}

	private native void n_onVisibilityChange (int p0);

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
