package crc64f1558ff7e185dbfc;


public class CameraManager_ResolutionFilter
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.camera.core.resolutionselector.ResolutionFilter
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_filter:(Ljava/util/List;I)Ljava/util/List;:GetFilter_Ljava_util_List_IHandler:AndroidX.Camera.Core.ResolutionSelector.IResolutionFilterInvoker, Xamarin.AndroidX.Camera.Core\n" +
			"";
		mono.android.Runtime.register ("CommunityToolkit.Maui.Core.CameraManager+ResolutionFilter, CommunityToolkit.Maui.Camera", CameraManager_ResolutionFilter.class, __md_methods);
	}

	public CameraManager_ResolutionFilter ()
	{
		super ();
		if (getClass () == CameraManager_ResolutionFilter.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Core.CameraManager+ResolutionFilter, CommunityToolkit.Maui.Camera", "", this, new java.lang.Object[] {  });
		}
	}

	public CameraManager_ResolutionFilter (android.util.Size p0)
	{
		super ();
		if (getClass () == CameraManager_ResolutionFilter.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Core.CameraManager+ResolutionFilter, CommunityToolkit.Maui.Camera", "Android.Util.Size, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}

	public java.util.List filter (java.util.List p0, int p1)
	{
		return n_filter (p0, p1);
	}

	private native java.util.List n_filter (java.util.List p0, int p1);

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
