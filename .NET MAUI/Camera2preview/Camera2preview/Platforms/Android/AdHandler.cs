using Android.Content;
using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camera2preview.Platforms.Android
{
    public class MyCameraPreviewHandler : ViewHandler<MyCameraPreview, Camera2Preview>
    {
        public static IPropertyMapper<MyCameraPreview, MyCameraPreviewHandler> Mapper =
            new PropertyMapper<MyCameraPreview, MyCameraPreviewHandler>(ViewHandler.ViewMapper);

        public MyCameraPreviewHandler() : base(Mapper) { }

        protected override Camera2preview CreatePlatformView()
        {
            Camera2preview
            return new Camera2Preview(Context);
        }
    }
}
