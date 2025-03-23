package mono.androidx.media3.exoplayer.audio;


public class WaveformAudioBufferSink_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.audio.WaveformAudioBufferSink.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onNewWaveformBar:(ILandroidx/media3/exoplayer/audio/WaveformAudioBufferSink$WaveformBar;)V:GetOnNewWaveformBar_ILandroidx_media3_exoplayer_audio_WaveformAudioBufferSink_WaveformBar_Handler:AndroidX.Media3.ExoPlayer.Audio.WaveformAudioBufferSink/IListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Audio.WaveformAudioBufferSink+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", WaveformAudioBufferSink_ListenerImplementor.class, __md_methods);
	}

	public WaveformAudioBufferSink_ListenerImplementor ()
	{
		super ();
		if (getClass () == WaveformAudioBufferSink_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Audio.WaveformAudioBufferSink+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onNewWaveformBar (int p0, androidx.media3.exoplayer.audio.WaveformAudioBufferSink.WaveformBar p1)
	{
		n_onNewWaveformBar (p0, p1);
	}

	private native void n_onNewWaveformBar (int p0, androidx.media3.exoplayer.audio.WaveformAudioBufferSink.WaveformBar p1);

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
