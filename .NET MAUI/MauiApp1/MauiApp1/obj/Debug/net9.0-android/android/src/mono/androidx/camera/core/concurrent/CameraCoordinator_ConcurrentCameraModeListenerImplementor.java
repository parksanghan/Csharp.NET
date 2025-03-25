package mono.androidx.camera.core.concurrent;


public class CameraCoordinator_ConcurrentCameraModeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.camera.core.concurrent.CameraCoordinator.ConcurrentCameraModeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCameraOperatingModeUpdated:(II)V:GetOnCameraOperatingModeUpdated_IIHandler:AndroidX.Camera.Core.Concurrent.ICameraCoordinatorConcurrentCameraModeListenerInvoker, Xamarin.AndroidX.Camera.Core\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Camera.Core.Concurrent.ICameraCoordinatorConcurrentCameraModeListenerImplementor, Xamarin.AndroidX.Camera.Core", CameraCoordinator_ConcurrentCameraModeListenerImplementor.class, __md_methods);
	}

	public CameraCoordinator_ConcurrentCameraModeListenerImplementor ()
	{
		super ();
		if (getClass () == CameraCoordinator_ConcurrentCameraModeListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Camera.Core.Concurrent.ICameraCoordinatorConcurrentCameraModeListenerImplementor, Xamarin.AndroidX.Camera.Core", "", this, new java.lang.Object[] {  });
		}
	}

	public void onCameraOperatingModeUpdated (int p0, int p1)
	{
		n_onCameraOperatingModeUpdated (p0, p1);
	}

	private native void n_onCameraOperatingModeUpdated (int p0, int p1);

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
