package mono.androidx.camera.core;


public class ImageCapture_ScreenFlashListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.camera.core.ImageCapture.ScreenFlashListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCompleted:()V:GetOnCompletedHandler:AndroidX.Camera.Core.ImageCapture/IScreenFlashListenerInvoker, Xamarin.AndroidX.Camera.Core\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Camera.Core.ImageCapture+IScreenFlashListenerImplementor, Xamarin.AndroidX.Camera.Core", ImageCapture_ScreenFlashListenerImplementor.class, __md_methods);
	}

	public ImageCapture_ScreenFlashListenerImplementor ()
	{
		super ();
		if (getClass () == ImageCapture_ScreenFlashListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Camera.Core.ImageCapture+IScreenFlashListenerImplementor, Xamarin.AndroidX.Camera.Core", "", this, new java.lang.Object[] {  });
		}
	}

	public void onCompleted ()
	{
		n_onCompleted ();
	}

	private native void n_onCompleted ();

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
