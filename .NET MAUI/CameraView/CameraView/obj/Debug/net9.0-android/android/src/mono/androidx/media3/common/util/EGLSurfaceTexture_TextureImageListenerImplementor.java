package mono.androidx.media3.common.util;


public class EGLSurfaceTexture_TextureImageListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.common.util.EGLSurfaceTexture.TextureImageListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onFrameAvailable:()V:GetOnFrameAvailableHandler:AndroidX.Media3.Common.Util.EGLSurfaceTexture/ITextureImageListenerInvoker, Xamarin.AndroidX.Media3.Common\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.Common.Util.EGLSurfaceTexture+ITextureImageListenerImplementor, Xamarin.AndroidX.Media3.Common", EGLSurfaceTexture_TextureImageListenerImplementor.class, __md_methods);
	}

	public EGLSurfaceTexture_TextureImageListenerImplementor ()
	{
		super ();
		if (getClass () == EGLSurfaceTexture_TextureImageListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.Common.Util.EGLSurfaceTexture+ITextureImageListenerImplementor, Xamarin.AndroidX.Media3.Common", "", this, new java.lang.Object[] {  });
		}
	}

	public void onFrameAvailable ()
	{
		n_onFrameAvailable ();
	}

	private native void n_onFrameAvailable ();

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
