using Microsoft.Extensions.Logging;
#if ANDROID
using MyCamera2Preview.Platforms.Android;
#endif
namespace MyCamera2Preview
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
    handlers.AddHandler<MyCameraPreview, MyCameraPreviewHandler>();
#endif
            })
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
