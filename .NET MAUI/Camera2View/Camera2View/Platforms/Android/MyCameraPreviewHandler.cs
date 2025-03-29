using Android.Content;
using Camera2View.Platforms.Android;
using MauiApp1.Platforms.Android;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera2View.Platforms.Android
{
    public class MyCameraPreviewHandler : ViewHandler<MyCameraPreview, Camera2Preview>
    {
        public MyCameraPreviewHandler() : base(Mapper)
        {
        }

        public static IPropertyMapper<MyCameraPreview, MyCameraPreviewHandler> Mapper =
        new PropertyMapper<MyCameraPreview, MyCameraPreviewHandler>(ViewHandler.ViewMapper);
        protected override Camera2Preview CreatePlatformView()
        {
            return new Camera2Preview(Context);
        }
    }
}
