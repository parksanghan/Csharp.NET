using Android.Content;
using Camera2View.Platforms.Android;
 
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
       new PropertyMapper<MyCameraPreview, MyCameraPreviewHandler>(ViewHandler.ViewMapper)
       {
           [nameof(MyCameraPreview.StartCamera)] = MapStartCamera, // 매핑 추가
       };
        protected override Camera2Preview CreatePlatformView()
        {
            var nativeView = new Camera2Preview(Context);
            nativeView.OnYawDetected = VirtualView.OnYawDetected; // 이게 핵심
            return nativeView;
        }
        public void StartCameraIfPermissionGranted()
        {
            PlatformView?.TryStartCamera();
        }

        public static void MapStartCamera(MyCameraPreviewHandler handler, MyCameraPreview view)
        {
            handler.PlatformView?.TryStartCamera();
        }

        public static void MapOnYawDetected(MyCameraPreviewHandler handler, MyCameraPreview view)
        {
            handler.PlatformView.OnYawDetected = view.OnYawDetected;
        }
    }
}
