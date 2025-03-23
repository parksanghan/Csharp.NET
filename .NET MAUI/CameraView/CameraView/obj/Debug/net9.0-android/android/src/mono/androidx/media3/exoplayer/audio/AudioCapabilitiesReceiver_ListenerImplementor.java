package mono.androidx.media3.exoplayer.audio;


public class AudioCapabilitiesReceiver_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.audio.AudioCapabilitiesReceiver.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAudioCapabilitiesChanged:(Landroidx/media3/exoplayer/audio/AudioCapabilities;)V:GetOnAudioCapabilitiesChanged_Landroidx_media3_exoplayer_audio_AudioCapabilities_Handler:AndroidX.Media3.ExoPlayer.Audio.AudioCapabilitiesReceiver/IListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Audio.AudioCapabilitiesReceiver+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", AudioCapabilitiesReceiver_ListenerImplementor.class, __md_methods);
	}

	public AudioCapabilitiesReceiver_ListenerImplementor ()
	{
		super ();
		if (getClass () == AudioCapabilitiesReceiver_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Audio.AudioCapabilitiesReceiver+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onAudioCapabilitiesChanged (androidx.media3.exoplayer.audio.AudioCapabilities p0)
	{
		n_onAudioCapabilitiesChanged (p0);
	}

	private native void n_onAudioCapabilitiesChanged (androidx.media3.exoplayer.audio.AudioCapabilities p0);

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
