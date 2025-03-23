package mono.androidx.media3.exoplayer;


public class Renderer_WakeupListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.Renderer.WakeupListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSleep:()V:GetOnSleepHandler:AndroidX.Media3.ExoPlayer.IRendererWakeupListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onWakeup:()V:GetOnWakeupHandler:AndroidX.Media3.ExoPlayer.IRendererWakeupListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.IRendererWakeupListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", Renderer_WakeupListenerImplementor.class, __md_methods);
	}

	public Renderer_WakeupListenerImplementor ()
	{
		super ();
		if (getClass () == Renderer_WakeupListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.IRendererWakeupListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onSleep ()
	{
		n_onSleep ();
	}

	private native void n_onSleep ();

	public void onWakeup ()
	{
		n_onWakeup ();
	}

	private native void n_onWakeup ();

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
