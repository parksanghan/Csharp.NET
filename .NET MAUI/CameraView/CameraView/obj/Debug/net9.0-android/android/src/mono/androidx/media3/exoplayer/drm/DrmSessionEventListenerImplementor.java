package mono.androidx.media3.exoplayer.drm;


public class DrmSessionEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.drm.DrmSessionEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDrmKeysLoaded:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;)V:GetOnDrmKeysLoaded_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Handler:AndroidX.Media3.ExoPlayer.Drm.IDrmSessionEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmKeysRemoved:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;)V:GetOnDrmKeysRemoved_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Handler:AndroidX.Media3.ExoPlayer.Drm.IDrmSessionEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmKeysRestored:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;)V:GetOnDrmKeysRestored_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Handler:AndroidX.Media3.ExoPlayer.Drm.IDrmSessionEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmSessionAcquired:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;I)V:GetOnDrmSessionAcquired_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_IHandler:AndroidX.Media3.ExoPlayer.Drm.IDrmSessionEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmSessionManagerError:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;Ljava/lang/Exception;)V:GetOnDrmSessionManagerError_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Ljava_lang_Exception_Handler:AndroidX.Media3.ExoPlayer.Drm.IDrmSessionEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onDrmSessionReleased:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;)V:GetOnDrmSessionReleased_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Handler:AndroidX.Media3.ExoPlayer.Drm.IDrmSessionEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Drm.IDrmSessionEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", DrmSessionEventListenerImplementor.class, __md_methods);
	}

	public DrmSessionEventListenerImplementor ()
	{
		super ();
		if (getClass () == DrmSessionEventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Drm.IDrmSessionEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onDrmKeysLoaded (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1)
	{
		n_onDrmKeysLoaded (p0, p1);
	}

	private native void n_onDrmKeysLoaded (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1);

	public void onDrmKeysRemoved (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1)
	{
		n_onDrmKeysRemoved (p0, p1);
	}

	private native void n_onDrmKeysRemoved (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1);

	public void onDrmKeysRestored (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1)
	{
		n_onDrmKeysRestored (p0, p1);
	}

	private native void n_onDrmKeysRestored (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1);

	public void onDrmSessionAcquired (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, int p2)
	{
		n_onDrmSessionAcquired (p0, p1, p2);
	}

	private native void n_onDrmSessionAcquired (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, int p2);

	public void onDrmSessionManagerError (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, java.lang.Exception p2)
	{
		n_onDrmSessionManagerError (p0, p1, p2);
	}

	private native void n_onDrmSessionManagerError (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, java.lang.Exception p2);

	public void onDrmSessionReleased (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1)
	{
		n_onDrmSessionReleased (p0, p1);
	}

	private native void n_onDrmSessionReleased (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1);

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
