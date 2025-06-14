using Microsoft.Extensions.Logging;
#if ANDROID
using Android.Webkit; // ✅ 반드시 필요
#endif
namespace WebView
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).
                ConfigureMauiHandlers(handlers =>
                {

#if ANDROID
                    Microsoft.Maui.Handlers.WebViewHandler.Mapper.AppendToMapping("EnableJavaScript", (handler, view) =>
                    {
                        if (handler.PlatformView is Android.Webkit.WebView androidWebView)
                        {
                            androidWebView.Settings.MixedContentMode = MixedContentHandling.AlwaysAllow;
                            androidWebView.Settings.JavaScriptEnabled = true;
                            androidWebView.Settings.DomStorageEnabled = true;
                            androidWebView.Settings.JavaScriptCanOpenWindowsAutomatically = true;
                            androidWebView.Settings.SetSupportMultipleWindows(true);
                            androidWebView.Settings.CacheMode = CacheModes.Normal;
                            androidWebView.Settings.LoadWithOverviewMode = true;
                            androidWebView.Settings.UseWideViewPort = true;
                            androidWebView.SetWebChromeClient(new WebChromeClient()); // 팝업 등 JS 연동 대응
                        }
                    });
#endif
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
