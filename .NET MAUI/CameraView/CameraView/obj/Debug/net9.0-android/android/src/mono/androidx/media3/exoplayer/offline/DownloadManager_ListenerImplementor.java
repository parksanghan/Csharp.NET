package mono.androidx.media3.exoplayer.offline;


public class DownloadManager_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.offline.DownloadManager.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDownloadChanged:(Landroidx/media3/exoplayer/offline/DownloadManager;Landroidx/media3/exoplayer/offline/Download;Ljava/lang/Exception;)V:GetOnDownloadChanged_Landroidx_media3_exoplayer_offline_DownloadManager_Landroidx_media3_exoplayer_offline_Download_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Offline.DownloadManager/IListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDownloadRemoved:(Landroidx/media3/exoplayer/offline/DownloadManager;Landroidx/media3/exoplayer/offline/Download;)V:GetOnDownloadRemoved_Landroidx_media3_exoplayer_offline_DownloadManager_Landroidx_media3_exoplayer_offline_Download_Handler:AndroidX.Media3.ExoPlayer.Offline.DownloadManager/IListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDownloadsPausedChanged:(Landroidx/media3/exoplayer/offline/DownloadManager;Z)V:GetOnDownloadsPausedChanged_Landroidx_media3_exoplayer_offline_DownloadManager_ZHandler:AndroidX.Media3.ExoPlayer.Offline.DownloadManager/IListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onIdle:(Landroidx/media3/exoplayer/offline/DownloadManager;)V:GetOnIdle_Landroidx_media3_exoplayer_offline_DownloadManager_Handler:AndroidX.Media3.ExoPlayer.Offline.DownloadManager/IListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onInitialized:(Landroidx/media3/exoplayer/offline/DownloadManager;)V:GetOnInitialized_Landroidx_media3_exoplayer_offline_DownloadManager_Handler:AndroidX.Media3.ExoPlayer.Offline.DownloadManager/IListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onRequirementsStateChanged:(Landroidx/media3/exoplayer/offline/DownloadManager;Landroidx/media3/exoplayer/scheduler/Requirements;I)V:GetOnRequirementsStateChanged_Landroidx_media3_exoplayer_offline_DownloadManager_Landroidx_media3_exoplayer_scheduler_Requirements_IHandler:AndroidX.Media3.ExoPlayer.Offline.DownloadManager/IListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onWaitingForRequirementsChanged:(Landroidx/media3/exoplayer/offline/DownloadManager;Z)V:GetOnWaitingForRequirementsChanged_Landroidx_media3_exoplayer_offline_DownloadManager_ZHandler:AndroidX.Media3.ExoPlayer.Offline.DownloadManager/IListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Offline.DownloadManager+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", DownloadManager_ListenerImplementor.class, __md_methods);
	}

	public DownloadManager_ListenerImplementor ()
	{
		super ();
		if (getClass () == DownloadManager_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Offline.DownloadManager+IListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onDownloadChanged (androidx.media3.exoplayer.offline.DownloadManager p0, androidx.media3.exoplayer.offline.Download p1, java.lang.Exception p2)
	{
		n_onDownloadChanged (p0, p1, p2);
	}

	private native void n_onDownloadChanged (androidx.media3.exoplayer.offline.DownloadManager p0, androidx.media3.exoplayer.offline.Download p1, java.lang.Exception p2);

	public void onDownloadRemoved (androidx.media3.exoplayer.offline.DownloadManager p0, androidx.media3.exoplayer.offline.Download p1)
	{
		n_onDownloadRemoved (p0, p1);
	}

	private native void n_onDownloadRemoved (androidx.media3.exoplayer.offline.DownloadManager p0, androidx.media3.exoplayer.offline.Download p1);

	public void onDownloadsPausedChanged (androidx.media3.exoplayer.offline.DownloadManager p0, boolean p1)
	{
		n_onDownloadsPausedChanged (p0, p1);
	}

	private native void n_onDownloadsPausedChanged (androidx.media3.exoplayer.offline.DownloadManager p0, boolean p1);

	public void onIdle (androidx.media3.exoplayer.offline.DownloadManager p0)
	{
		n_onIdle (p0);
	}

	private native void n_onIdle (androidx.media3.exoplayer.offline.DownloadManager p0);

	public void onInitialized (androidx.media3.exoplayer.offline.DownloadManager p0)
	{
		n_onInitialized (p0);
	}

	private native void n_onInitialized (androidx.media3.exoplayer.offline.DownloadManager p0);

	public void onRequirementsStateChanged (androidx.media3.exoplayer.offline.DownloadManager p0, androidx.media3.exoplayer.scheduler.Requirements p1, int p2)
	{
		n_onRequirementsStateChanged (p0, p1, p2);
	}

	private native void n_onRequirementsStateChanged (androidx.media3.exoplayer.offline.DownloadManager p0, androidx.media3.exoplayer.scheduler.Requirements p1, int p2);

	public void onWaitingForRequirementsChanged (androidx.media3.exoplayer.offline.DownloadManager p0, boolean p1)
	{
		n_onWaitingForRequirementsChanged (p0, p1);
	}

	private native void n_onWaitingForRequirementsChanged (androidx.media3.exoplayer.offline.DownloadManager p0, boolean p1);

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
