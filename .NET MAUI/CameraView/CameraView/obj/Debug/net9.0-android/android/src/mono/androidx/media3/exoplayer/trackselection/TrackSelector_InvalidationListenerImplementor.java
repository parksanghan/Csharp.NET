package mono.androidx.media3.exoplayer.trackselection;


public class TrackSelector_InvalidationListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.trackselection.TrackSelector.InvalidationListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTrackSelectionsInvalidated:()V:GetOnTrackSelectionsInvalidatedHandler:AndroidX.Media3.ExoPlayer.TrackSelection.TrackSelector/IInvalidationListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onRendererCapabilitiesChanged:(Landroidx/media3/exoplayer/Renderer;)V:GetOnRendererCapabilitiesChanged_Landroidx_media3_exoplayer_Renderer_Handler:AndroidX.Media3.ExoPlayer.TrackSelection.TrackSelector/IInvalidationListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.TrackSelection.TrackSelector+IInvalidationListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", TrackSelector_InvalidationListenerImplementor.class, __md_methods);
	}

	public TrackSelector_InvalidationListenerImplementor ()
	{
		super ();
		if (getClass () == TrackSelector_InvalidationListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.TrackSelection.TrackSelector+IInvalidationListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onTrackSelectionsInvalidated ()
	{
		n_onTrackSelectionsInvalidated ();
	}

	private native void n_onTrackSelectionsInvalidated ();

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
