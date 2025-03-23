package mono.androidx.media3.exoplayer.analytics;


public class PlaybackSessionManager_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.exoplayer.analytics.PlaybackSessionManager.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAdPlaybackStarted:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;Ljava/lang/String;)V:GetOnAdPlaybackStarted_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Ljava_lang_String_Handler:AndroidX.Media3.ExoPlayer.Analytics.IPlaybackSessionManagerListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSessionActive:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;)V:GetOnSessionActive_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Handler:AndroidX.Media3.ExoPlayer.Analytics.IPlaybackSessionManagerListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSessionCreated:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;)V:GetOnSessionCreated_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_Handler:AndroidX.Media3.ExoPlayer.Analytics.IPlaybackSessionManagerListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"n_onSessionFinished:(Landroidx/media3/exoplayer/analytics/AnalyticsListener$EventTime;Ljava/lang/String;Z)V:GetOnSessionFinished_Landroidx_media3_exoplayer_analytics_AnalyticsListener_EventTime_Ljava_lang_String_ZHandler:AndroidX.Media3.ExoPlayer.Analytics.IPlaybackSessionManagerListenerInvoker, Xamarin.AndroidX.Media3.ExoPlayer\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.ExoPlayer.Analytics.IPlaybackSessionManagerListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", PlaybackSessionManager_ListenerImplementor.class, __md_methods);
	}

	public PlaybackSessionManager_ListenerImplementor ()
	{
		super ();
		if (getClass () == PlaybackSessionManager_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.ExoPlayer.Analytics.IPlaybackSessionManagerListenerImplementor, Xamarin.AndroidX.Media3.ExoPlayer", "", this, new java.lang.Object[] {  });
		}
	}

	public void onAdPlaybackStarted (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, java.lang.String p2)
	{
		n_onAdPlaybackStarted (p0, p1, p2);
	}

	private native void n_onAdPlaybackStarted (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, java.lang.String p2);

	public void onSessionActive (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1)
	{
		n_onSessionActive (p0, p1);
	}

	private native void n_onSessionActive (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1);

	public void onSessionCreated (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1)
	{
		n_onSessionCreated (p0, p1);
	}

	private native void n_onSessionCreated (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1);

	public void onSessionFinished (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, boolean p2)
	{
		n_onSessionFinished (p0, p1, p2);
	}

	private native void n_onSessionFinished (androidx.media3.exoplayer.analytics.AnalyticsListener.EventTime p0, java.lang.String p1, boolean p2);

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
