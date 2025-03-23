package mono.androidx.media3.exoplayer.source.preload;


public class BasePreloadManager_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.source.preload.BasePreloadManager.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCompleted:(Landroidx/media3/common/MediaItem;)V:GetOnCompleted_Landroidx_media3_common_MediaItem_Handler:AndroidX.Media3.ExoPlayer.Source.Preload.BasePreloadManager/IListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onError:(Landroidx/media3/exoplayer/source/preload/PreloadException;)V:GetOnError_Landroidx_media3_exoplayer_source_preload_PreloadException_Handler:AndroidX.Media3.ExoPlayer.Source.Preload.BasePreloadManager/IListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Source.Preload.BasePreloadManager+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", BasePreloadManager_ListenerImplementor.class, __md_methods);
	}

	public BasePreloadManager_ListenerImplementor ()
	{
		super ();
		if (getClass () == BasePreloadManager_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Source.Preload.BasePreloadManager+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onCompleted (androidx.media3.common.MediaItem p0)
	{
		n_onCompleted (p0);
	}

	private native void n_onCompleted (androidx.media3.common.MediaItem p0);

	public void onError (androidx.media3.exoplayer.source.preload.PreloadException p0)
	{
		n_onError (p0);
	}

	private native void n_onError (androidx.media3.exoplayer.source.preload.PreloadException p0);

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
