package mono.androidx.media3.exoplayer;


public class RendererCapabilities_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.RendererCapabilities.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onRendererCapabilitiesChanged:(Landroidx/media3/exoplayer/Renderer;)V:GetOnRendererCapabilitiesChanged_Landroidx_media3_exoplayer_Renderer_Handler:AndroidX.Media3.ExoPlayer.IRendererCapabilitiesListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.IRendererCapabilitiesListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", RendererCapabilities_ListenerImplementor.class, __md_methods);
	}

	public RendererCapabilities_ListenerImplementor ()
	{
		super ();
		if (getClass () == RendererCapabilities_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.IRendererCapabilitiesListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onRendererCapabilitiesChanged (androidx.media3.exoplayer.Renderer p0)
	{
		n_onRendererCapabilitiesChanged (p0);
	}

	private native void n_onRendererCapabilitiesChanged (androidx.media3.exoplayer.Renderer p0);

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
