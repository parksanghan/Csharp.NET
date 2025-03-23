package mono.androidx.media3.exoplayer.scheduler;


public class RequirementsWatcher_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.scheduler.RequirementsWatcher.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRequirementsStateChanged:(Landroidx/media3/exoplayer/scheduler/RequirementsWatcher;I)V:GetOnRequirementsStateChanged_Landroidx_media3_exoplayer_scheduler_RequirementsWatcher_IHandler:AndroidX.Media3.ExoPlayer.Scheduler.RequirementsWatcher/IListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Scheduler.RequirementsWatcher+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", RequirementsWatcher_ListenerImplementor.class, __md_methods);
	}

	public RequirementsWatcher_ListenerImplementor ()
	{
		super ();
		if (getClass () == RequirementsWatcher_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Scheduler.RequirementsWatcher+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onRequirementsStateChanged (androidx.media3.exoplayer.scheduler.RequirementsWatcher p0, int p1)
	{
		n_onRequirementsStateChanged (p0, p1);
	}

	private native void n_onRequirementsStateChanged (androidx.media3.exoplayer.scheduler.RequirementsWatcher p0, int p1);

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
