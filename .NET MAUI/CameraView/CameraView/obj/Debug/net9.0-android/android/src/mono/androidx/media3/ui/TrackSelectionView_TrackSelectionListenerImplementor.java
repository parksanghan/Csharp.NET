package mono.androidx.media3.ui;


public class TrackSelectionView_TrackSelectionListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.ui.TrackSelectionView.TrackSelectionListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTrackSelectionChanged:(ZLjava/util/Map;)V:GetOnTrackSelectionChanged_ZLjava_util_Map_Handler:AndroidX.Media3.UI.TrackSelectionView/ITrackSelectionListenerInvoker, Xamarin.AndroidX.Media3.Ui\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.UI.TrackSelectionView+ITrackSelectionListenerImplementor, Xamarin.AndroidX.Media3.Ui", TrackSelectionView_TrackSelectionListenerImplementor.class, __md_methods);
	}

	public TrackSelectionView_TrackSelectionListenerImplementor ()
	{
		super ();
		if (getClass () == TrackSelectionView_TrackSelectionListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.UI.TrackSelectionView+ITrackSelectionListenerImplementor, Xamarin.AndroidX.Media3.Ui", "", this, new java.lang.Object[] {  });
		}
	}

	public void onTrackSelectionChanged (boolean p0, java.util.Map p1)
	{
		n_onTrackSelectionChanged (p0, p1);
	}

	private native void n_onTrackSelectionChanged (boolean p0, java.util.Map p1);

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
