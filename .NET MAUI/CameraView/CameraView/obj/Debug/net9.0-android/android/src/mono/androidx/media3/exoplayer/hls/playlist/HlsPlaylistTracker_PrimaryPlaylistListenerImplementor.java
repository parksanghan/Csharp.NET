package mono.androidx.media3.exoplayer.hls.playlist;


public class HlsPlaylistTracker_PrimaryPlaylistListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.hls.playlist.HlsPlaylistTracker.PrimaryPlaylistListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPrimaryPlaylistRefreshed:(Landroidx/media3/exoplayer/hls/playlist/HlsMediaPlaylist;)V:GetOnPrimaryPlaylistRefreshed_Landroidx_media3_exoplayer_hls_playlist_HlsMediaPlaylist_Handler:AndroidX.Media3.ExoPlayer.Hls.Playlist.IHlsPlaylistTrackerPrimaryPlaylistListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer.Hls\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Hls.Playlist.IHlsPlaylistTrackerPrimaryPlaylistListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer.Hls", HlsPlaylistTracker_PrimaryPlaylistListenerImplementor.class, __md_methods);
	}

	public HlsPlaylistTracker_PrimaryPlaylistListenerImplementor ()
	{
		super ();
		if (getClass () == HlsPlaylistTracker_PrimaryPlaylistListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Hls.Playlist.IHlsPlaylistTrackerPrimaryPlaylistListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer.Hls", "", this, new java.lang.Object[] {  });
		}
	}

	public void onPrimaryPlaylistRefreshed (androidx.media3.exoplayer.hls.playlist.HlsMediaPlaylist p0)
	{
		n_onPrimaryPlaylistRefreshed (p0);
	}

	private native void n_onPrimaryPlaylistRefreshed (androidx.media3.exoplayer.hls.playlist.HlsMediaPlaylist p0);

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
