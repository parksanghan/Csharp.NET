package mono.androidx.camera.video.internal.encoder;


public class Encoder_SurfaceInput_OnSurfaceUpdateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.camera.video.internal.encoder.Encoder.SurfaceInput.OnSurfaceUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSurfaceUpdate:(Landroid/view/Surface;)V:GetOnSurfaceUpdate_Landroid_view_Surface_Handler:AndroidX.Camera.Video.Internal.Encoder.IEncoderSurfaceInputOnSurfaceUpdateListenerInvoker, Xamarin.AndroidX.Camera.Video\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Camera.Video.Internal.Encoder.IEncoderSurfaceInputOnSurfaceUpdateListenerImplementor, Xamarin.AndroidX.Camera.Video", Encoder_SurfaceInput_OnSurfaceUpdateListenerImplementor.class, __md_methods);
	}

	public Encoder_SurfaceInput_OnSurfaceUpdateListenerImplementor ()
	{
		super ();
		if (getClass () == Encoder_SurfaceInput_OnSurfaceUpdateListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Camera.Video.Internal.Encoder.IEncoderSurfaceInputOnSurfaceUpdateListenerImplementor, Xamarin.AndroidX.Camera.Video", "", this, new java.lang.Object[] {  });
		}
	}

	public void onSurfaceUpdate (android.view.Surface p0)
	{
		n_onSurfaceUpdate (p0);
	}

	private native void n_onSurfaceUpdate (android.view.Surface p0);

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
