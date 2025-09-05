package mono.androidx.camera.core.impl;


public class CameraStateRegistry_OnConfigureAvailableListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.camera.core.impl.CameraStateRegistry.OnConfigureAvailableListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onConfigureAvailable:()V:GetOnConfigureAvailableHandler:AndroidX.Camera.Core.Impl.CameraStateRegistry/IOnConfigureAvailableListenerInvoker, Xamarin.AndroidX.Camera.Core\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Camera.Core.Impl.CameraStateRegistry+IOnConfigureAvailableListenerImplementor, Xamarin.AndroidX.Camera.Core", CameraStateRegistry_OnConfigureAvailableListenerImplementor.class, __md_methods);
	}

	public CameraStateRegistry_OnConfigureAvailableListenerImplementor ()
	{
		super ();
		if (getClass () == CameraStateRegistry_OnConfigureAvailableListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Camera.Core.Impl.CameraStateRegistry+IOnConfigureAvailableListenerImplementor, Xamarin.AndroidX.Camera.Core", "", this, new java.lang.Object[] {  });
		}
	}

	public void onConfigureAvailable ()
	{
		n_onConfigureAvailable ();
	}

	private native void n_onConfigureAvailable ();

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
