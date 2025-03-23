package mono.androidx.media3.exoplayer.video.spherical;


public class CameraMotionListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.video.spherical.CameraMotionListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCameraMotion:(J[F)V:GetOnCameraMotion_JarrayFHandler:AndroidX.Media3.ExoPlayer.Video.Spherical.ICameraMotionListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onCameraMotionReset:()V:GetOnCameraMotionResetHandler:AndroidX.Media3.ExoPlayer.Video.Spherical.ICameraMotionListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Video.Spherical.ICameraMotionListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", CameraMotionListenerImplementor.class, __md_methods);
	}

	public CameraMotionListenerImplementor ()
	{
		super ();
		if (getClass () == CameraMotionListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Video.Spherical.ICameraMotionListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onCameraMotion (long p0, float[] p1)
	{
		n_onCameraMotion (p0, p1);
	}

	private native void n_onCameraMotion (long p0, float[] p1);

	public void onCameraMotionReset ()
	{
		n_onCameraMotionReset ();
	}

	private native void n_onCameraMotionReset ();

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
