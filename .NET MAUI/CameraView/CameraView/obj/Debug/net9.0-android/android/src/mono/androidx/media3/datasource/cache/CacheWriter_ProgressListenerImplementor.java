package mono.androidx.media3.datasource.cache;


public class CacheWriter_ProgressListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.datasource.cache.CacheWriter.ProgressListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onProgress:(JJJ)V:GetOnProgress_JJJHandler:AndroidX.Media3.DataSource.Cache.CacheWriter/IProgressListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.DataSource.Cache.CacheWriter+IProgressListenerImplementor, Xamarin.AndroidX.Media3.DataSource", CacheWriter_ProgressListenerImplementor.class, __md_methods);
	}

	public CacheWriter_ProgressListenerImplementor ()
	{
		super ();
		if (getClass () == CacheWriter_ProgressListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.DataSource.Cache.CacheWriter+IProgressListenerImplementor, Xamarin.AndroidX.Media3.DataSource", "", this, new java.lang.Object[] {  });
		}
	}

	public void onProgress (long p0, long p1, long p2)
	{
		n_onProgress (p0, p1, p2);
	}

	private native void n_onProgress (long p0, long p1, long p2);

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
