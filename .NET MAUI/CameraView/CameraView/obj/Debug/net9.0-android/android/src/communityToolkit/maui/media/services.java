package communityToolkit.maui.media;


public class services
	extends android.app.Service
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBind:(Landroid/content/Intent;)Landroid/os/IBinder;:GetOnBind_Landroid_content_Intent_Handler\n" +
			"n_onCreate:()V:GetOnCreateHandler\n" +
			"n_onStartCommand:(Landroid/content/Intent;II)I:GetOnStartCommand_Landroid_content_Intent_IIHandler\n" +
			"n_onTaskRemoved:(Landroid/content/Intent;)V:GetOnTaskRemoved_Landroid_content_Intent_Handler\n" +
			"n_onDestroy:()V:GetOnDestroyHandler\n" +
			"n_onRebind:(Landroid/content/Intent;)V:GetOnRebind_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("CommunityToolkit.Maui.Media.Services.MediaControlsService, CommunityToolkit.Maui.MediaElement", services.class, __md_methods);
	}

	public services ()
	{
		super ();
		if (getClass () == services.class) {
			mono.android.TypeManager.Activate ("CommunityToolkit.Maui.Media.Services.MediaControlsService, CommunityToolkit.Maui.MediaElement", "", this, new java.lang.Object[] {  });
		}
	}

	public android.os.IBinder onBind (android.content.Intent p0)
	{
		return n_onBind (p0);
	}

	private native android.os.IBinder n_onBind (android.content.Intent p0);

	public void onCreate ()
	{
		n_onCreate ();
	}

	private native void n_onCreate ();

	public int onStartCommand (android.content.Intent p0, int p1, int p2)
	{
		return n_onStartCommand (p0, p1, p2);
	}

	private native int n_onStartCommand (android.content.Intent p0, int p1, int p2);

	public void onTaskRemoved (android.content.Intent p0)
	{
		n_onTaskRemoved (p0);
	}

	private native void n_onTaskRemoved (android.content.Intent p0);

	public void onDestroy ()
	{
		n_onDestroy ();
	}

	private native void n_onDestroy ();

	public void onRebind (android.content.Intent p0)
	{
		n_onRebind (p0);
	}

	private native void n_onRebind (android.content.Intent p0);

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
