package mono.androidx.media3.ui;


public class PlayerView_FullscreenButtonClickListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.ui.PlayerView.FullscreenButtonClickListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFullscreenButtonClick:(Z)V:GetOnFullscreenButtonClick_ZHandler:AndroidX.Media3.UI.PlayerView/IFullscreenButtonClickListenerInvoker, Xamarin.AndroidX.Media3.Ui\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.UI.PlayerView+IFullscreenButtonClickListenerImplementor, Xamarin.AndroidX.Media3.Ui", PlayerView_FullscreenButtonClickListenerImplementor.class, __md_methods);
	}

	public PlayerView_FullscreenButtonClickListenerImplementor ()
	{
		super ();
		if (getClass () == PlayerView_FullscreenButtonClickListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.UI.PlayerView+IFullscreenButtonClickListenerImplementor, Xamarin.AndroidX.Media3.Ui", "", this, new java.lang.Object[] {  });
		}
	}

	public void onFullscreenButtonClick (boolean p0)
	{
		n_onFullscreenButtonClick (p0);
	}

	private native void n_onFullscreenButtonClick (boolean p0);

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
