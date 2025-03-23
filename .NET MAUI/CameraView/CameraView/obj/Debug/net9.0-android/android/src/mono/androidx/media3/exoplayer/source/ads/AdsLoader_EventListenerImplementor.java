package mono.androidx.media3.exoplayer.source.ads;


public class AdsLoader_EventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.source.ads.AdsLoader.EventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAdClicked:()V:GetOnAdClickedHandler:AndroidX.Media3.ExoPlayer.Source.Ads.IAdsLoaderEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAdLoadError:(Landroidx/media3/exoplayer/source/ads/AdsMediaSource$AdLoadException;Landroidx/media3/datasource/DataSpec;)V:GetOnAdLoadError_Landroidx_media3_exoplayer_source_ads_AdsMediaSource_AdLoadException_Landroidx_media3_datasource_DataSpec_Handler:AndroidX.Media3.ExoPlayer.Source.Ads.IAdsLoaderEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAdPlaybackState:(Landroidx/media3/common/AdPlaybackState;)V:GetOnAdPlaybackState_Landroidx_media3_common_AdPlaybackState_Handler:AndroidX.Media3.ExoPlayer.Source.Ads.IAdsLoaderEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onAdTapped:()V:GetOnAdTappedHandler:AndroidX.Media3.ExoPlayer.Source.Ads.IAdsLoaderEventListener, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Source.Ads.IAdsLoaderEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", AdsLoader_EventListenerImplementor.class, __md_methods);
	}

	public AdsLoader_EventListenerImplementor ()
	{
		super ();
		if (getClass () == AdsLoader_EventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Source.Ads.IAdsLoaderEventListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onAdClicked ()
	{
		n_onAdClicked ();
	}

	private native void n_onAdClicked ();

	public void onAdLoadError (androidx.media3.exoplayer.source.ads.AdsMediaSource.AdLoadException p0, androidx.media3.datasource.DataSpec p1)
	{
		n_onAdLoadError (p0, p1);
	}

	private native void n_onAdLoadError (androidx.media3.exoplayer.source.ads.AdsMediaSource.AdLoadException p0, androidx.media3.datasource.DataSpec p1);

	public void onAdPlaybackState (androidx.media3.common.AdPlaybackState p0)
	{
		n_onAdPlaybackState (p0);
	}

	private native void n_onAdPlaybackState (androidx.media3.common.AdPlaybackState p0);

	public void onAdTapped ()
	{
		n_onAdTapped ();
	}

	private native void n_onAdTapped ();

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
