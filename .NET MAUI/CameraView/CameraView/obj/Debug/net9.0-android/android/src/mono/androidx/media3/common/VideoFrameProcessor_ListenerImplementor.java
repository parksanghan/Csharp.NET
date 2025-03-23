package mono.androidx.media3.common;


public class VideoFrameProcessor_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.common.VideoFrameProcessor.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEnded:()V:GetOnEndedHandler:AndroidX.Media3.Common.IVideoFrameProcessorListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onError:(Landroidx/media3/common/VideoFrameProcessingException;)V:GetOnError_Landroidx_media3_common_VideoFrameProcessingException_Handler:AndroidX.Media3.Common.IVideoFrameProcessorListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onInputStreamRegistered:(ILjava/util/List;Landroidx/media3/common/FrameInfo;)V:GetOnInputStreamRegistered_ILjava_util_List_Landroidx_media3_common_FrameInfo_Handler:AndroidX.Media3.Common.IVideoFrameProcessorListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onOutputFrameAvailableForRendering:(J)V:GetOnOutputFrameAvailableForRendering_JHandler:AndroidX.Media3.Common.IVideoFrameProcessorListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onOutputSizeChanged:(II)V:GetOnOutputSizeChanged_IIHandler:AndroidX.Media3.Common.IVideoFrameProcessorListener, Xamarin.AndroidX.Media3.Common\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.Common.IVideoFrameProcessorListenerImplementor, Xamarin.AndroidX.Media3.Common", VideoFrameProcessor_ListenerImplementor.class, __md_methods);
	}

	public VideoFrameProcessor_ListenerImplementor ()
	{
		super ();
		if (getClass () == VideoFrameProcessor_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.Common.IVideoFrameProcessorListenerImplementor, Xamarin.AndroidX.Media3.Common", "", this, new java.lang.Object[] {  });
		}
	}

	public void onEnded ()
	{
		n_onEnded ();
	}

	private native void n_onEnded ();

	public void onError (androidx.media3.common.VideoFrameProcessingException p0)
	{
		n_onError (p0);
	}

	private native void n_onError (androidx.media3.common.VideoFrameProcessingException p0);

	public void onInputStreamRegistered (int p0, java.util.List p1, androidx.media3.common.FrameInfo p2)
	{
		n_onInputStreamRegistered (p0, p1, p2);
	}

	private native void n_onInputStreamRegistered (int p0, java.util.List p1, androidx.media3.common.FrameInfo p2);

	public void onOutputFrameAvailableForRendering (long p0)
	{
		n_onOutputFrameAvailableForRendering (p0);
	}

	private native void n_onOutputFrameAvailableForRendering (long p0);

	public void onOutputSizeChanged (int p0, int p1)
	{
		n_onOutputSizeChanged (p0, p1);
	}

	private native void n_onOutputSizeChanged (int p0, int p1);

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
