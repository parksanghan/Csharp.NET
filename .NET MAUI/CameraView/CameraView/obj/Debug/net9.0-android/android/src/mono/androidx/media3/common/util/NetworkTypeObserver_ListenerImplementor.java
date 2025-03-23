package mono.androidx.media3.common.util;


public class NetworkTypeObserver_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.common.util.NetworkTypeObserver.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNetworkTypeChanged:(I)V:GetOnNetworkTypeChanged_IHandler:AndroidX.Media3.Common.Util.NetworkTypeObserver/IListenerInvoker, Xamarin.AndroidX.Media3.Common\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.Common.Util.NetworkTypeObserver+IListenerImplementor, Xamarin.AndroidX.Media3.Common", NetworkTypeObserver_ListenerImplementor.class, __md_methods);
	}

	public NetworkTypeObserver_ListenerImplementor ()
	{
		super ();
		if (getClass () == NetworkTypeObserver_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.Common.Util.NetworkTypeObserver+IListenerImplementor, Xamarin.AndroidX.Media3.Common", "", this, new java.lang.Object[] {  });
		}
	}

	public void onNetworkTypeChanged (int p0)
	{
		n_onNetworkTypeChanged (p0);
	}

	private native void n_onNetworkTypeChanged (int p0);

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
