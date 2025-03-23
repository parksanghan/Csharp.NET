package mono.androidx.media3.datasource;


public class TransferListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.datasource.TransferListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onBytesTransferred:(Landroidx/media3/datasource/DataSource;Landroidx/media3/datasource/DataSpec;ZI)V:GetOnBytesTransferred_Landroidx_media3_datasource_DataSource_Landroidx_media3_datasource_DataSpec_ZIHandler:AndroidX.Media3.DataSource.ITransferListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"n_onTransferEnd:(Landroidx/media3/datasource/DataSource;Landroidx/media3/datasource/DataSpec;Z)V:GetOnTransferEnd_Landroidx_media3_datasource_DataSource_Landroidx_media3_datasource_DataSpec_ZHandler:AndroidX.Media3.DataSource.ITransferListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"n_onTransferInitializing:(Landroidx/media3/datasource/DataSource;Landroidx/media3/datasource/DataSpec;Z)V:GetOnTransferInitializing_Landroidx_media3_datasource_DataSource_Landroidx_media3_datasource_DataSpec_ZHandler:AndroidX.Media3.DataSource.ITransferListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"n_onTransferStart:(Landroidx/media3/datasource/DataSource;Landroidx/media3/datasource/DataSpec;Z)V:GetOnTransferStart_Landroidx_media3_datasource_DataSource_Landroidx_media3_datasource_DataSpec_ZHandler:AndroidX.Media3.DataSource.ITransferListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.DataSource.ITransferListenerImplementor, Xamarin.AndroidX.Media3.DataSource", TransferListenerImplementor.class, __md_methods);
	}

	public TransferListenerImplementor ()
	{
		super ();
		if (getClass () == TransferListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.DataSource.ITransferListenerImplementor, Xamarin.AndroidX.Media3.DataSource", "", this, new java.lang.Object[] {  });
		}
	}

	public void onBytesTransferred (androidx.media3.datasource.DataSource p0, androidx.media3.datasource.DataSpec p1, boolean p2, int p3)
	{
		n_onBytesTransferred (p0, p1, p2, p3);
	}

	private native void n_onBytesTransferred (androidx.media3.datasource.DataSource p0, androidx.media3.datasource.DataSpec p1, boolean p2, int p3);

	public void onTransferEnd (androidx.media3.datasource.DataSource p0, androidx.media3.datasource.DataSpec p1, boolean p2)
	{
		n_onTransferEnd (p0, p1, p2);
	}

	private native void n_onTransferEnd (androidx.media3.datasource.DataSource p0, androidx.media3.datasource.DataSpec p1, boolean p2);

	public void onTransferInitializing (androidx.media3.datasource.DataSource p0, androidx.media3.datasource.DataSpec p1, boolean p2)
	{
		n_onTransferInitializing (p0, p1, p2);
	}

	private native void n_onTransferInitializing (androidx.media3.datasource.DataSource p0, androidx.media3.datasource.DataSpec p1, boolean p2);

	public void onTransferStart (androidx.media3.datasource.DataSource p0, androidx.media3.datasource.DataSpec p1, boolean p2)
	{
		n_onTransferStart (p0, p1, p2);
	}

	private native void n_onTransferStart (androidx.media3.datasource.DataSource p0, androidx.media3.datasource.DataSpec p1, boolean p2);

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
