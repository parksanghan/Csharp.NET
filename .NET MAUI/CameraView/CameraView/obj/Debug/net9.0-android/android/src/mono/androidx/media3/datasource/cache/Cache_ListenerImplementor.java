package mono.androidx.media3.datasource.cache;


public class Cache_ListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.media3.datasource.cache.Cache.Listener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSpanAdded:(Landroidx/media3/datasource/cache/Cache;Landroidx/media3/datasource/cache/CacheSpan;)V:GetOnSpanAdded_Landroidx_media3_datasource_cache_Cache_Landroidx_media3_datasource_cache_CacheSpan_Handler:AndroidX.Media3.DataSource.Cache.ICacheListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"n_onSpanRemoved:(Landroidx/media3/datasource/cache/Cache;Landroidx/media3/datasource/cache/CacheSpan;)V:GetOnSpanRemoved_Landroidx_media3_datasource_cache_Cache_Landroidx_media3_datasource_cache_CacheSpan_Handler:AndroidX.Media3.DataSource.Cache.ICacheListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"n_onSpanTouched:(Landroidx/media3/datasource/cache/Cache;Landroidx/media3/datasource/cache/CacheSpan;Landroidx/media3/datasource/cache/CacheSpan;)V:GetOnSpanTouched_Landroidx_media3_datasource_cache_Cache_Landroidx_media3_datasource_cache_CacheSpan_Landroidx_media3_datasource_cache_CacheSpan_Handler:AndroidX.Media3.DataSource.Cache.ICacheListenerInvoker, Xamarin.AndroidX.Media3.DataSource\n" +
			"";
		mono.android.Runtime.register ("AndroidX.Media3.DataSource.Cache.ICacheListenerImplementor, Xamarin.AndroidX.Media3.DataSource", Cache_ListenerImplementor.class, __md_methods);
	}

	public Cache_ListenerImplementor ()
	{
		super ();
		if (getClass () == Cache_ListenerImplementor.class) {
			mono.android.TypeManager.Activate ("AndroidX.Media3.DataSource.Cache.ICacheListenerImplementor, Xamarin.AndroidX.Media3.DataSource", "", this, new java.lang.Object[] {  });
		}
	}

	public void onSpanAdded (androidx.media3.datasource.cache.Cache p0, androidx.media3.datasource.cache.CacheSpan p1)
	{
		n_onSpanAdded (p0, p1);
	}

	private native void n_onSpanAdded (androidx.media3.datasource.cache.Cache p0, androidx.media3.datasource.cache.CacheSpan p1);

	public void onSpanRemoved (androidx.media3.datasource.cache.Cache p0, androidx.media3.datasource.cache.CacheSpan p1)
	{
		n_onSpanRemoved (p0, p1);
	}

	private native void n_onSpanRemoved (androidx.media3.datasource.cache.Cache p0, androidx.media3.datasource.cache.CacheSpan p1);

	public void onSpanTouched (androidx.media3.datasource.cache.Cache p0, androidx.media3.datasource.cache.CacheSpan p1, androidx.media3.datasource.cache.CacheSpan p2)
	{
		n_onSpanTouched (p0, p1, p2);
	}

	private native void n_onSpanTouched (androidx.media3.datasource.cache.Cache p0, androidx.media3.datasource.cache.CacheSpan p1, androidx.media3.datasource.cache.CacheSpan p2);

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
