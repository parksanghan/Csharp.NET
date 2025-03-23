package mono.androidx.media3.common;


public class VideoGraph_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.common.VideoGraph.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onEnded:(J)V:GetOnEnded_JHandler:AndroidX.Media3.Common.IVideoGraphListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onError:(Landroidx/media3/common/VideoFrameProcessingException;)V:GetOnError_Landroidx_media3_common_VideoFrameProcessingException_Handler:AndroidX.Media3.Common.IVideoGraphListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onOutputFrameAvailableForRendering:(J)V:GetOnOutputFrameAvailableForRendering_JHandler:AndroidX.Media3.Common.IVideoGraphListener, Xamarin.AndroidX.Media3.Common\n" +
			"n_onOutputSizeChanged:(II)V:GetOnOutputSizeChanged_IIHandler:AndroidX.Media3.Common.IVideoGraphListener, Xamarin.AndroidX.Media3.Common\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.Common.IVideoGraphListenerImplementor, Xamarin.AndroidX.Media3.Common", VideoGraph_ListenerImplementor.class, __md_methods);
	}

	public VideoGraph_ListenerImplementor ()
	{
		super ();
		if (getClass () == VideoGraph_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.Common.IVideoGraphListenerImplementor, Xamarin.AndroidX.Media3.Common", "", this, new java.lang.Object[] {  });
		}
	}

	public void onEnded (long p0)
	{
		n_onEnded (p0);
	}

	private native void n_onEnded (long p0);

	public void onError (androidx.media3.common.VideoFrameProcessingException p0)
	{
		n_onError (p0);
	}

	private native void n_onError (androidx.media3.common.VideoFrameProcessingException p0);

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
