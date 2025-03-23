package mono.androidx.media3.datasource.cache;


public class CacheDataSource_EventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.datasource.cache.CacheDataSource.EventListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCacheIgnored:(I)V:GetOnCacheIgnored_IHandler:AndroidX.Media3.DataSource.Cache.CacheDataSource/IEventListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"n_onCachedBytesRead:(JJ)V:GetOnCachedBytesRead_JJHandler:AndroidX.Media3.DataSource.Cache.CacheDataSource/IEventListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.DataSource.Cache.CacheDataSource+IEventListenerImplementor, Xamarin.AndroidX.Media3.DataSource", CacheDataSource_EventListenerImplementor.class, __md_methods);
	}

	public CacheDataSource_EventListenerImplementor ()
	{
		super ();
		if (getClass () == CacheDataSource_EventListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.DataSource.Cache.CacheDataSource+IEventListenerImplementor, Xamarin.AndroidX.Media3.DataSource", "", this, new java.lang.Object[] {  });
		}
	}

	public void onCacheIgnored (int p0)
	{
		n_onCacheIgnored (p0);
	}

	private native void n_onCacheIgnored (int p0);

	public void onCachedBytesRead (long p0, long p1)
	{
		n_onCachedBytesRead (p0, p1);
	}

	private native void n_onCachedBytesRead (long p0, long p1);

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
