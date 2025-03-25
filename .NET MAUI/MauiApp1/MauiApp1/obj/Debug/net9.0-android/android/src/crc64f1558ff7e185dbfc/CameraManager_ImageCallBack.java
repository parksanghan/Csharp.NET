package crc64f1558ff7e185dbfc;


public class CameraManager_ImageCallBack
	extends androidx.camera.core.ImageCapture.OnImageCapturedCallback
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCaptureSuccess:(Landroidx/camera/core/ImageProxy;)V:GetOnCaptureSuccess_Landroidx_camera_core_ImageProxy_Handler\n" +
			"n_onError:(Landroidx/camera/core/ImageCaptureException;)V:GetOnError_Landroidx_camera_core_ImageCaptureException_Handler\n" +
			"";
		mono.android.Runtime.register ("CommunityToolkit.Maui.Core.CameraManager+ImageCallBack, CommunityToolkit.Maui.Camera", CameraManager_ImageCallBack.class, __md_methods);
	}

	public CameraManager_ImageCallBack ()
	{
		super ();
		if (getClass () == CameraManager_ImageCallBack.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Core.CameraManager+ImageCallBack, CommunityToolkit.Maui.Camera", "", this, new java.lang.Object[] {  });
		}
	}

	public void onCaptureSuccess (androidx.camera.core.ImageProxy p0)
	{
		n_onCaptureSuccess (p0);
	}

	private native void n_onCaptureSuccess (androidx.camera.core.ImageProxy p0);

	public void onError (androidx.camera.core.ImageCaptureException p0)
	{
		n_onError (p0);
	}

	private native void n_onError (androidx.camera.core.ImageCaptureException p0);

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
