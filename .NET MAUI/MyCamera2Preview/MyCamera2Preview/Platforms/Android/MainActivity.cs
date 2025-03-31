using Android.App;
using Android.Content.PM;
using Android.OS;
using Android;
using AndroidX.Core.App;
using AndroidX.Core.Content;
namespace MyCamera2Preview
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            // ✅ 카메라 권한 체크 및 요청
            if (CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted)
            {
                RequestPermissions(new[] { Manifest.Permission.Camera }, 0);
            }
        }
    }
}
