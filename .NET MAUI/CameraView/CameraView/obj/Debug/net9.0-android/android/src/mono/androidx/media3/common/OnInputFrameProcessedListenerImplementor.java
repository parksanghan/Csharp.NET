package mono.androidx.media3.common;


public class OnInputFrameProcessedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.common.OnInputFrameProcessedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onInputFrameProcessed:(IJ)V:GetOnInputFrameProcessed_IJHandler:AndroidX.Media3.Common.IOnInputFrameProcessedListenerInvoker, Xamarin.AndroidX.Media3.Common\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.Common.IOnInputFrameProcessedListenerImplementor, Xamarin.AndroidX.Media3.Common", OnInputFrameProcessedListenerImplementor.class, __md_methods);
	}

	public OnInputFrameProcessedListenerImplementor ()
	{
		super ();
		if (getClass () == OnInputFrameProcessedListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.Common.IOnInputFrameProcessedListenerImplementor, Xamarin.AndroidX.Media3.Common", "", this, new java.lang.Object[] {  });
		}
	}

	public void onInputFrameProcessed (int p0, long p1)
	{
		n_onInputFrameProcessed (p0, p1);
	}

	private native void n_onInputFrameProcessed (int p0, long p1);

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
