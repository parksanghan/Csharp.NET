package mono.androidx.media3.session;


public class MediaController_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.session.MediaController.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAvailableSessionCommandsChanged:(Landroidx/media3/session/MediaController;Landroidx/media3/session/SessionCommands;)V:GetOnAvailableSessionCommandsChanged_Landroidx_media3_session_MediaController_Landroidx_media3_session_SessionCommands_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"n_onCustomCommand:(Landroidx/media3/session/MediaController;Landroidx/media3/session/SessionCommand;Landroid/os/Bundle;)Lcom/google/common/util/concurrent/ListenableFuture;:GetOnCustomCommand_Landroidx_media3_session_MediaController_Landroidx_media3_session_SessionCommand_Landroid_os_Bundle_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"n_onCustomLayoutChanged:(Landroidx/media3/session/MediaController;Ljava/util/List;)V:GetOnCustomLayoutChanged_Landroidx_media3_session_MediaController_Ljava_util_List_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"n_onDisconnected:(Landroidx/media3/session/MediaController;)V:GetOnDisconnected_Landroidx_media3_session_MediaController_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"n_onError:(Landroidx/media3/session/MediaController;Landroidx/media3/session/SessionError;)V:GetOnError_Landroidx_media3_session_MediaController_Landroidx_media3_session_SessionError_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"n_onExtrasChanged:(Landroidx/media3/session/MediaController;Landroid/os/Bundle;)V:GetOnExtrasChanged_Landroidx_media3_session_MediaController_Landroid_os_Bundle_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"n_onMediaButtonPreferencesChanged:(Landroidx/media3/session/MediaController;Ljava/util/List;)V:GetOnMediaButtonPreferencesChanged_Landroidx_media3_session_MediaController_Ljava_util_List_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"n_onSessionActivityChanged:(Landroidx/media3/session/MediaController;Landroid/app/PendingIntent;)V:GetOnSessionActivityChanged_Landroidx_media3_session_MediaController_Landroid_app_PendingIntent_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"n_onSetCustomLayout:(Landroidx/media3/session/MediaController;Ljava/util/List;)Lcom/google/common/util/concurrent/ListenableFuture;:GetOnSetCustomLayout_Landroidx_media3_session_MediaController_Ljava_util_List_Handler:AndroidX.Media3.Session.MediaController/IListener, Xamarin.AndroidX.Media3.Session\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.Session.MediaController+IListenerImplementor, Xamarin.AndroidX.Media3.Session", MediaController_ListenerImplementor.class, __md_methods);
	}

	public MediaController_ListenerImplementor ()
	{
		super ();
		if (getClass () == MediaController_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.Session.MediaController+IListenerImplementor, Xamarin.AndroidX.Media3.Session", "", this, new java.lang.Object[] {  });
		}
	}

	public void onAvailableSessionCommandsChanged (androidx.media3.session.MediaController p0, androidx.media3.session.SessionCommands p1)
	{
		n_onAvailableSessionCommandsChanged (p0, p1);
	}

	private native void n_onAvailableSessionCommandsChanged (androidx.media3.session.MediaController p0, androidx.media3.session.SessionCommands p1);

	public com.google.common.util.concurrent.ListenableFuture onCustomCommand (androidx.media3.session.MediaController p0, androidx.media3.session.SessionCommand p1, android.os.Bundle p2)
	{
		return n_onCustomCommand (p0, p1, p2);
	}

	private native com.google.common.util.concurrent.ListenableFuture n_onCustomCommand (androidx.media3.session.MediaController p0, androidx.media3.session.SessionCommand p1, android.os.Bundle p2);

	public void onCustomLayoutChanged (androidx.media3.session.MediaController p0, java.util.List p1)
	{
		n_onCustomLayoutChanged (p0, p1);
	}

	private native void n_onCustomLayoutChanged (androidx.media3.session.MediaController p0, java.util.List p1);

	public void onDisconnected (androidx.media3.session.MediaController p0)
	{
		n_onDisconnected (p0);
	}

	private native void n_onDisconnected (androidx.media3.session.MediaController p0);

	public void onError (androidx.media3.session.MediaController p0, androidx.media3.session.SessionError p1)
	{
		n_onError (p0, p1);
	}

	private native void n_onError (androidx.media3.session.MediaController p0, androidx.media3.session.SessionError p1);

	public void onExtrasChanged (androidx.media3.session.MediaController p0, android.os.Bundle p1)
	{
		n_onExtrasChanged (p0, p1);
	}

	private native void n_onExtrasChanged (androidx.media3.session.MediaController p0, android.os.Bundle p1);

	public void onMediaButtonPreferencesChanged (androidx.media3.session.MediaController p0, java.util.List p1)
	{
		n_onMediaButtonPreferencesChanged (p0, p1);
	}

	private native void n_onMediaButtonPreferencesChanged (androidx.media3.session.MediaController p0, java.util.List p1);

	public void onSessionActivityChanged (androidx.media3.session.MediaController p0, android.app.PendingIntent p1)
	{
		n_onSessionActivityChanged (p0, p1);
	}

	private native void n_onSessionActivityChanged (androidx.media3.session.MediaController p0, android.app.PendingIntent p1);

	public com.google.common.util.concurrent.ListenableFuture onSetCustomLayout (androidx.media3.session.MediaController p0, java.util.List p1)
	{
		return n_onSetCustomLayout (p0, p1);
	}

	private native com.google.common.util.concurrent.ListenableFuture n_onSetCustomLayout (androidx.media3.session.MediaController p0, java.util.List p1);

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
