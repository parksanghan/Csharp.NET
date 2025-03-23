package mono.androidx.media3.exoplayer.mediacodec;


public class LoudnessCodecController_LoudnessParameterUpdateListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.mediacodec.LoudnessCodecController.LoudnessParameterUpdateListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onLoudnessParameterUpdate:(Landroid/os/Bundle;)Landroid/os/Bundle;:GetOnLoudnessParameterUpdate_Landroid_os_Bundle_Handler:AndroidX.Media3.ExoPlayer.MediaCodec.LoudnessCodecController/ILoudnessParameterUpdateListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.MediaCodec.LoudnessCodecController+ILoudnessParameterUpdateListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", LoudnessCodecController_LoudnessParameterUpdateListenerImplementor.class, __md_methods);
	}

	public LoudnessCodecController_LoudnessParameterUpdateListenerImplementor ()
	{
		super ();
		if (getClass () == LoudnessCodecController_LoudnessParameterUpdateListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.MediaCodec.LoudnessCodecController+ILoudnessParameterUpdateListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public android.os.Bundle onLoudnessParameterUpdate (android.os.Bundle p0)
	{
		return n_onLoudnessParameterUpdate (p0);
	}

	private native android.os.Bundle n_onLoudnessParameterUpdate (android.os.Bundle p0);

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
