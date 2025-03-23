package mono.androidx.media3.exoplayer.mediacodec;


public class MediaCodecAdapter_OnBufferAvailableListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.mediacodec.MediaCodecAdapter.OnBufferAvailableListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInputBufferAvailable:()V:GetOnInputBufferAvailableHandler:AndroidX.Media3.ExoPlayer.MediaCodec.IMediaCodecAdapterOnBufferAvailableListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onOutputBufferAvailable:()V:GetOnOutputBufferAvailableHandler:AndroidX.Media3.ExoPlayer.MediaCodec.IMediaCodecAdapterOnBufferAvailableListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.MediaCodec.IMediaCodecAdapterOnBufferAvailableListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", MediaCodecAdapter_OnBufferAvailableListenerImplementor.class, __md_methods);
	}

	public MediaCodecAdapter_OnBufferAvailableListenerImplementor ()
	{
		super ();
		if (getClass () == MediaCodecAdapter_OnBufferAvailableListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.MediaCodec.IMediaCodecAdapterOnBufferAvailableListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onInputBufferAvailable ()
	{
		n_onInputBufferAvailable ();
	}

	private native void n_onInputBufferAvailable ();

	public void onOutputBufferAvailable ()
	{
		n_onOutputBufferAvailable ();
	}

	private native void n_onOutputBufferAvailable ();

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
