using Android.Content;
using Camera2preview.Platforms.Android.Controls;
using Microsoft.Maui.Handlers;
using System;
 
using Camera2preview.Controls;
 
 
namespace Camera2preview.Platforms.Android.NewFolder
{
    public class Camera2ViewHandler : ViewHandler<Camera2ViewControl, Camera2View>
    {
        protected override Camera2View CreatePlatformView()
        {
            return new Camera2View(Context);
        }

        protected override void ConnectHandler(Camera2View platformView)
        {
            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(Camera2View platformView)
        {
            base.DisconnectHandler(platformView);
        }
    }
}
