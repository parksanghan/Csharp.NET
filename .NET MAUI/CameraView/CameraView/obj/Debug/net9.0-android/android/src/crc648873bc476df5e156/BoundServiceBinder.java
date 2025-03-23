package crc648873bc476df5e156;


public class BoundServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("CommunityToolkit.Maui.Services.BoundServiceBinder, CommunityToolkit.Maui.MediaElement", BoundServiceBinder.class, __md_methods);
	}

	public BoundServiceBinder ()
	{
		super ();
		if (getClass () == BoundServiceBinder.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Services.BoundServiceBinder, CommunityToolkit.Maui.MediaElement", "", this, new java.lang.Object[] {  });
		}
	}

	public BoundServiceBinder (java.lang.String p0)
	{
		super (p0);
		if (getClass () == BoundServiceBinder.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Services.BoundServiceBinder, CommunityToolkit.Maui.MediaElement", "System.String, System.Private.CoreLib", this, new java.lang.Object[] { p0 });
		}
	}

	public BoundServiceBinder (communityToolkit.maui.media.services p0)
	{
		super ();
		if (getClass () == BoundServiceBinder.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Services.BoundServiceBinder, CommunityToolkit.Maui.MediaElement", "CommunityToolkit.Maui.Media.Services.MediaControlsService, CommunityToolkit.Maui.MediaElement", this, new java.lang.Object[] { p0 });
		}
	}

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
