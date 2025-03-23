package mono.androidx.media3.exoplayer;


public class ExoPlayer_AudioOffloadListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.ExoPlayer.AudioOffloadListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onOffloadedPlayback:(Z)V:GetOnOffloadedPlayback_ZHandler:AndroidX.Media3.ExoPlayer.IExoPlayerAudioOffloadListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSleepingForOffloadChanged:(Z)V:GetOnSleepingForOffloadChanged_ZHandler:AndroidX.Media3.ExoPlayer.IExoPlayerAudioOffloadListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.IExoPlayerAudioOffloadListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", ExoPlayer_AudioOffloadListenerImplementor.class, __md_methods);
	}

	public ExoPlayer_AudioOffloadListenerImplementor ()
	{
		super ();
		if (getClass () == ExoPlayer_AudioOffloadListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.IExoPlayerAudioOffloadListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onOffloadedPlayback (boolean p0)
	{
		n_onOffloadedPlayback (p0);
	}

	private native void n_onOffloadedPlayback (boolean p0);

	public void onSleepingForOffloadChanged (boolean p0)
	{
		n_onSleepingForOffloadChanged (p0);
	}

	private native void n_onSleepingForOffloadChanged (boolean p0);

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
