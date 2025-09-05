package crc64f1558ff7e185dbfc;


public class CameraManager_FutureCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.camera.core.impl.utils.futures.FutureCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFailure:(Ljava/lang/Throwable;)V:GetOnFailure_Ljava_lang_Throwable_Handler:AndroidX.Camera.Core.Impl.Utils.Futures.IFutureCallbackInvoker, Xamarin.AndroidX.Camera.Core\n" +
			"n_onSuccess:(Ljava/lang/Object;)V:GetOnSuccess_Ljava_lang_Object_Handler:AndroidX.Camera.Core.Impl.Utils.Futures.IFutureCallbackInvoker, Xamarin.AndroidX.Camera.Core\n" +
			"";
		mono.android.Runtime.register ("CommunityToolkit.Maui.Core.CameraManager+FutureCallback, CommunityToolkit.Maui.Camera", CameraManager_FutureCallback.class, __md_methods);
	}

	public CameraManager_FutureCallback ()
	{
		super ();
		if (getClass () == CameraManager_FutureCallback.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Core.CameraManager+FutureCallback, CommunityToolkit.Maui.Camera", "", this, new java.lang.Object[] {  });
		}
	}

	public void onFailure (java.lang.Throwable p0)
	{
		n_onFailure (p0);
	}

	private native void n_onFailure (java.lang.Throwable p0);

	public void onSuccess (java.lang.Object p0)
	{
		n_onSuccess (p0);
	}

	private native void n_onSuccess (java.lang.Object p0);

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
