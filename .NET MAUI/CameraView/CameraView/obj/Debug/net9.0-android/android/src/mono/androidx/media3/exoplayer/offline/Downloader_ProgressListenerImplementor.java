package mono.androidx.media3.exoplayer.offline;


public class Downloader_ProgressListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.offline.Downloader.ProgressListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onProgress:(JJF)V:GetOnProgress_JJFHandler:AndroidX.Media3.ExoPlayer.Offline.IDownloaderProgressListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Offline.IDownloaderProgressListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", Downloader_ProgressListenerImplementor.class, __md_methods);
	}

	public Downloader_ProgressListenerImplementor ()
	{
		super ();
		if (getClass () == Downloader_ProgressListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Offline.IDownloaderProgressListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onProgress (long p0, long p1, float p2)
	{
		n_onProgress (p0, p1, p2);
	}

	private native void n_onProgress (long p0, long p1, float p2);

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
