package mono.androidx.media3.exoplayer.audio;


public class AudioSink_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.audio.AudioSink.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPositionDiscontinuity:()V:GetOnPositionDiscontinuityHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSkipSilenceEnabledChanged:(Z)V:GetOnSkipSilenceEnabledChanged_ZHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onUnderrun:(IJJ)V:GetOnUnderrun_IJJHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioCapabilitiesChanged:()V:GetOnAudioCapabilitiesChangedHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioSinkError:(Ljava/lang/Exception;)V:GetOnAudioSinkError_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioTrackInitialized:(Landroidx/media3/exoplayer/audio/AudioSink$AudioTrackConfig;)V:GetOnAudioTrackInitialized_Landroidx_media3_exoplayer_audio_AudioSink_AudioTrackConfig_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioTrackReleased:(Landroidx/media3/exoplayer/audio/AudioSink$AudioTrackConfig;)V:GetOnAudioTrackReleased_Landroidx_media3_exoplayer_audio_AudioSink_AudioTrackConfig_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onOffloadBufferEmptying:()V:GetOnOffloadBufferEmptyingHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onOffloadBufferFull:()V:GetOnOffloadBufferFullHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onPositionAdvancing:(J)V:GetOnPositionAdvancing_JHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSilenceSkipped:()V:GetOnSilenceSkippedHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", AudioSink_ListenerImplementor.class, __md_methods);
	}

	public AudioSink_ListenerImplementor ()
	{
		super ();
		if (getClass () == AudioSink_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Audio.IAudioSinkListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onPositionDiscontinuity ()
	{
		n_onPositionDiscontinuity ();
	}

	private native void n_onPositionDiscontinuity ();

	public void onSkipSilenceEnabledChanged (boolean p0)
	{
		n_onSkipSilenceEnabledChanged (p0);
	}

	private native void n_onSkipSilenceEnabledChanged (boolean p0);

	public void onUnderrun (int p0, long p1, long p2)
	{
		n_onUnderrun (p0, p1, p2);
	}

	private native void n_onUnderrun (int p0, long p1, long p2);

	public void onAudioCapabilitiesChanged ()
	{
		n_onAudioCapabilitiesChanged ();
	}

	private native void n_onAudioCapabilitiesChanged ();

	public void onAudioSinkError (java.lang.Exception p0)
	{
		n_onAudioSinkError (p0);
	}

	private native void n_onAudioSinkError (java.lang.Exception p0);

	public void onAudioTrackInitialized (androidx.media3.exoplayer.audio.AudioSink.AudioTrackConfig p0)
	{
		n_onAudioTrackInitialized (p0);
	}

	private native void n_onAudioTrackInitialized (androidx.media3.exoplayer.audio.AudioSink.AudioTrackConfig p0);

	public void onAudioTrackReleased (androidx.media3.exoplayer.audio.AudioSink.AudioTrackConfig p0)
	{
		n_onAudioTrackReleased (p0);
	}

	private native void n_onAudioTrackReleased (androidx.media3.exoplayer.audio.AudioSink.AudioTrackConfig p0);

	public void onOffloadBufferEmptying ()
	{
		n_onOffloadBufferEmptying ();
	}

	private native void n_onOffloadBufferEmptying ();

	public void onOffloadBufferFull ()
	{
		n_onOffloadBufferFull ();
	}

	private native void n_onOffloadBufferFull ();

	public void onPositionAdvancing (long p0)
	{
		n_onPositionAdvancing (p0);
	}

	private native void n_onPositionAdvancing (long p0);

	public void onSilenceSkipped ()
	{
		n_onSilenceSkipped ();
	}

	private native void n_onSilenceSkipped ();

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
