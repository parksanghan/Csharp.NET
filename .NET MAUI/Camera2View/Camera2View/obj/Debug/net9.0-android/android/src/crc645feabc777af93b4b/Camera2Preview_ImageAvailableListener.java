package crc645feabc777af93b4b;


public class Camera2Preview_ImageAvailableListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.media.ImageReader.OnImageAvailableListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onImageAvailable:(Landroid/media/ImageReader;)V:GetOnImageAvailable_Landroid_media_ImageReader_Handler:Android.Media.ImageReader/IOnImageAvailableListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("MauiApp1.Platforms.Android.Camera2Preview+ImageAvailableListener, Camera2View", Camera2Preview_ImageAvailableListener.class, __md_methods);
	}

	public Camera2Preview_ImageAvailableListener ()
	{
		super ();
		if (getClass () == Camera2Preview_ImageAvailableListener.class) {
			mono.android.TypeManager.Activate ("MauiApp1.Platforms.Android.Camera2Preview+ImageAvailableListener, Camera2View", "", this, new java.lang.Object[] {  });
		}
	}

	public void onImageAvailable (android.media.ImageReader p0)
	{
		n_onImageAvailable (p0);
	}

	private native void n_onImageAvailable (android.media.ImageReader p0);

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
