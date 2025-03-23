package mono.androidx.camera.view.impl;


public class ZoomGestureDetector_OnZoomGestureListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.camera.view.impl.ZoomGestureDetector.OnZoomGestureListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onZoomEvent:(Landroidx/camera/view/impl/ZoomGestureDetector$ZoomEvent;)Z:GetOnZoomEvent_Landroidx_camera_view_impl_ZoomGestureDetector_ZoomEvent_Handler:AndroidX.Camera.View.Impl.ZoomGestureDetector/IOnZoomGestureListenerInvoker, Xamarin.AndroidX.Camera.View\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Camera.View.Impl.ZoomGestureDetector+IOnZoomGestureListenerImplementor, Xamarin.AndroidX.Camera.View", ZoomGestureDetector_OnZoomGestureListenerImplementor.class, __md_methods);
	}

	public ZoomGestureDetector_OnZoomGestureListenerImplementor ()
	{
		super ();
		if (getClass () == ZoomGestureDetector_OnZoomGestureListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Camera.View.Impl.ZoomGestureDetector+IOnZoomGestureListenerImplementor, Xamarin.AndroidX.Camera.View", "", this, new java.lang.Object[] {  });
		}
	}

	public boolean onZoomEvent (androidx.camera.view.impl.ZoomGestureDetector.ZoomEvent p0)
	{
		return n_onZoomEvent (p0);
	}

	private native boolean n_onZoomEvent (androidx.camera.view.impl.ZoomGestureDetector.ZoomEvent p0);

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
