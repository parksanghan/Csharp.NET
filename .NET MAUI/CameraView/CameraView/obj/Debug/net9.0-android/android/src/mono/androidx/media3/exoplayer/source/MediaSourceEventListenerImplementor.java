package mono.androidx.media3.exoplayer.source;


public class MediaSourceEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.source.MediaSourceEventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDownstreamFormatChanged:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnDownstreamFormatChanged_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Source.IMediaSourceEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadCanceled:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;Landroidx/media3/exoplayer/source/LoadEventInfo;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnLoadCanceled_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Landroidx_media3_exoplayer_source_LoadEventInfo_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Source.IMediaSourceEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadCompleted:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;Landroidx/media3/exoplayer/source/LoadEventInfo;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnLoadCompleted_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Landroidx_media3_exoplayer_source_LoadEventInfo_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Source.IMediaSourceEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadError:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;Landroidx/media3/exoplayer/source/LoadEventInfo;Landroidx/media3/exoplayer/source/MediaLoadData;Ljava/io/IOException;Z)V:GetOnLoadError_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Landroidx_media3_exoplayer_source_LoadEventInfo_Landroidx_media3_exoplayer_source_MediaLoadData_Ljava_io_IOException_ZHandler:AndroidX.Media3.ExoPlayer.Source.IMediaSourceEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onLoadStarted:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;Landroidx/media3/exoplayer/source/LoadEventInfo;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnLoadStarted_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Landroidx_media3_exoplayer_source_LoadEventInfo_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Source.IMediaSourceEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onUpstreamDiscarded:(ILandroidx/media3/exoplayer/source/MediaSource$MediaPeriodId;Landroidx/media3/exoplayer/source/MediaLoadData;)V:GetOnUpstreamDiscarded_ILandroidx_media3_exoplayer_source_MediaSource_MediaPeriodId_Landroidx_media3_exoplayer_source_MediaLoadData_Handler:AndroidX.Media3.ExoPlayer.Source.IMediaSourceEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Source.IMediaSourceEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", MediaSourceEventListenerImplementor.class, __md_methods);
	}

	public MediaSourceEventListenerImplementor ()
	{
		super ();
		if (getClass () == MediaSourceEventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Source.IMediaSourceEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onDownstreamFormatChanged (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.MediaLoadData p2)
	{
		n_onDownstreamFormatChanged (p0, p1, p2);
	}

	private native void n_onDownstreamFormatChanged (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.MediaLoadData p2);

	public void onLoadCanceled (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.LoadEventInfo p2, androidx.media3.exoplayer.source.MediaLoadData p3)
	{
		n_onLoadCanceled (p0, p1, p2, p3);
	}

	private native void n_onLoadCanceled (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.LoadEventInfo p2, androidx.media3.exoplayer.source.MediaLoadData p3);

	public void onLoadCompleted (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.LoadEventInfo p2, androidx.media3.exoplayer.source.MediaLoadData p3)
	{
		n_onLoadCompleted (p0, p1, p2, p3);
	}

	private native void n_onLoadCompleted (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.LoadEventInfo p2, androidx.media3.exoplayer.source.MediaLoadData p3);

	public void onLoadError (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.LoadEventInfo p2, androidx.media3.exoplayer.source.MediaLoadData p3, java.io.IOException p4, boolean p5)
	{
		n_onLoadError (p0, p1, p2, p3, p4, p5);
	}

	private native void n_onLoadError (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.LoadEventInfo p2, androidx.media3.exoplayer.source.MediaLoadData p3, java.io.IOException p4, boolean p5);

	public void onLoadStarted (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.LoadEventInfo p2, androidx.media3.exoplayer.source.MediaLoadData p3)
	{
		n_onLoadStarted (p0, p1, p2, p3);
	}

	private native void n_onLoadStarted (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.LoadEventInfo p2, androidx.media3.exoplayer.source.MediaLoadData p3);

	public void onUpstreamDiscarded (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.MediaLoadData p2)
	{
		n_onUpstreamDiscarded (p0, p1, p2);
	}

	private native void n_onUpstreamDiscarded (int p0, androidx.media3.exoplayer.source.MediaSource.MediaPeriodId p1, androidx.media3.exoplayer.source.MediaLoadData p2);

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
