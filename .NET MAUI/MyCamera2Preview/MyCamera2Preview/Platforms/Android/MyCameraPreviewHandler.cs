// Platforms/Android/AdHandler/MyCameraPreviewHandler.cs
using Microsoft.Maui.Handlers;
using Android.Content;
using MyCamera2Preview.Platforms.Android;

namespace MyCamera2Preview.Platforms.Android
{
    public class MyCameraPreviewHandler : ViewHandler<MyCameraPreview, Camera2Preview>
    {
        public MyCameraPreviewHandler() : base(Mapper) 
        { }
        public static IPropertyMapper<MyCameraPreview, MyCameraPreviewHandler> Mapper =
            new PropertyMapper<MyCameraPreview, MyCameraPreviewHandler>(ViewHandler.ViewMapper);

         

        protected override Camera2Preview CreatePlatformView()
        {
            return new Camera2Preview(Context);
        }
    }
}
