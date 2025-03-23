package mono.androidx.media3.exoplayer.audio;


public class AudioRendererEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.audio.AudioRendererEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAudioCodecError:(Ljava/lang/Exception;)V:GetOnAudioCodecError_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioDecoderInitialized:(Ljava/lang/String;JJ)V:GetOnAudioDecoderInitialized_Ljava_lang_String_JJHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioDecoderReleased:(Ljava/lang/String;)V:GetOnAudioDecoderReleased_Ljava_lang_String_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioDisabled:(Landroidx/media3/exoplayer/DecoderCounters;)V:GetOnAudioDisabled_Landroidx_media3_exoplayer_DecoderCounters_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioEnabled:(Landroidx/media3/exoplayer/DecoderCounters;)V:GetOnAudioEnabled_Landroidx_media3_exoplayer_DecoderCounters_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioInputFormatChanged:(Landroidx/media3/common/Format;Landroidx/media3/exoplayer/DecoderReuseEvaluation;)V:GetOnAudioInputFormatChanged_Landroidx_media3_common_Format_Landroidx_media3_exoplayer_DecoderReuseEvaluation_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioPositionAdvancing:(J)V:GetOnAudioPositionAdvancing_JHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioSinkError:(Ljava/lang/Exception;)V:GetOnAudioSinkError_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioTrackInitialized:(Landroidx/media3/exoplayer/audio/AudioSink$AudioTrackConfig;)V:GetOnAudioTrackInitialized_Landroidx_media3_exoplayer_audio_AudioSink_AudioTrackConfig_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioTrackReleased:(Landroidx/media3/exoplayer/audio/AudioSink$AudioTrackConfig;)V:GetOnAudioTrackReleased_Landroidx_media3_exoplayer_audio_AudioSink_AudioTrackConfig_Handler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAudioUnderrun:(IJJ)V:GetOnAudioUnderrun_IJJHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSkipSilenceEnabledChanged:(Z)V:GetOnSkipSilenceEnabledChanged_ZHandler:AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", AudioRendererEventListenerImplementor.class, __md_methods);
	}

	public AudioRendererEventListenerImplementor ()
	{
		super ();
		if (getClass () == AudioRendererEventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Audio.IAudioRendererEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onAudioCodecError (java.lang.Exception p0)
	{
		n_onAudioCodecError (p0);
	}

	private native void n_onAudioCodecError (java.lang.Exception p0);

	public void onAudioDecoderInitialized (java.lang.String p0, long p1, long p2)
	{
		n_onAudioDecoderInitialized (p0, p1, p2);
	}

	private native void n_onAudioDecoderInitialized (java.lang.String p0, long p1, long p2);

	public void onAudioDecoderReleased (java.lang.String p0)
	{
		n_onAudioDecoderReleased (p0);
	}

	private native void n_onAudioDecoderReleased (java.lang.String p0);

	public void onAudioDisabled (androidx.media3.exoplayer.DecoderCounters p0)
	{
		n_onAudioDisabled (p0);
	}

	private native void n_onAudioDisabled (androidx.media3.exoplayer.DecoderCounters p0);

	public void onAudioEnabled (androidx.media3.exoplayer.DecoderCounters p0)
	{
		n_onAudioEnabled (p0);
	}

	private native void n_onAudioEnabled (androidx.media3.exoplayer.DecoderCounters p0);

	public void onAudioInputFormatChanged (androidx.media3.common.Format p0, androidx.media3.exoplayer.DecoderReuseEvaluation p1)
	{
		n_onAudioInputFormatChanged (p0, p1);
	}

	private native void n_onAudioInputFormatChanged (androidx.media3.common.Format p0, androidx.media3.exoplayer.DecoderReuseEvaluation p1);

	public void onAudioPositionAdvancing (long p0)
	{
		n_onAudioPositionAdvancing (p0);
	}

	private native void n_onAudioPositionAdvancing (long p0);

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

	public void onAudioUnderrun (int p0, long p1, long p2)
	{
		n_onAudioUnderrun (p0, p1, p2);
	}

	private native void n_onAudioUnderrun (int p0, long p1, long p2);

	public void onSkipSilenceEnabledChanged (boolean p0)
	{
		n_onSkipSilenceEnabledChanged (p0);
	}

	private native void n_onSkipSilenceEnabledChanged (boolean p0);

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
