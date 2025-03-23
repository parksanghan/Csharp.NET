package mono.androidx.media3.ui;


public class TimeBar_OnScrubListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.ui.TimeBar.OnScrubListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrubMove:(Landroidx/media3/ui/TimeBar;J)V:GetOnScrubMove_Landroidx_media3_ui_TimeBar_JHandler:AndroidX.Media3.UI.ITimeBarOnScrubListenerInvoker, Xamarin.AndroidX.Media3.Ui\n" +
			"n_onScrubStart:(Landroidx/media3/ui/TimeBar;J)V:GetOnScrubStart_Landroidx_media3_ui_TimeBar_JHandler:AndroidX.Media3.UI.ITimeBarOnScrubListenerInvoker, Xamarin.AndroidX.Media3.Ui\n" +
			"n_onScrubStop:(Landroidx/media3/ui/TimeBar;JZ)V:GetOnScrubStop_Landroidx_media3_ui_TimeBar_JZHandler:AndroidX.Media3.UI.ITimeBarOnScrubListenerInvoker, Xamarin.AndroidX.Media3.Ui\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.UI.ITimeBarOnScrubListenerImplementor, Xamarin.AndroidX.Media3.Ui", TimeBar_OnScrubListenerImplementor.class, __md_methods);
	}

	public TimeBar_OnScrubListenerImplementor ()
	{
		super ();
		if (getClass () == TimeBar_OnScrubListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.UI.ITimeBarOnScrubListenerImplementor, Xamarin.AndroidX.Media3.Ui", "", this, new java.lang.Object[] {  });
		}
	}

	public void onScrubMove (androidx.media3.ui.TimeBar p0, long p1)
	{
		n_onScrubMove (p0, p1);
	}

	private native void n_onScrubMove (androidx.media3.ui.TimeBar p0, long p1);

	public void onScrubStart (androidx.media3.ui.TimeBar p0, long p1)
	{
		n_onScrubStart (p0, p1);
	}

	private native void n_onScrubStart (androidx.media3.ui.TimeBar p0, long p1);

	public void onScrubStop (androidx.media3.ui.TimeBar p0, long p1, boolean p2)
	{
		n_onScrubStop (p0, p1, p2);
	}

	private native void n_onScrubStop (androidx.media3.ui.TimeBar p0, long p1, boolean p2);

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
