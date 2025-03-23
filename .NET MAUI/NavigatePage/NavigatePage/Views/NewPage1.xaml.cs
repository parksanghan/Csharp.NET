#if ANDROID
 
using NavigatePage.Platforms.Android.Api;
#endif

using NavigatePage.Platforms.Android.Api;

namespace NavigatePage.Views;

public partial class NewPage1 : ContentPage
{
    public NewPage1()
    {
        InitializeComponent();
    }
    private async Task RequestCameraPermissionAsync()
    {
        var status = await Permissions.RequestAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("권한 필요", "권한이 필요합니다,", "확인");
        }
        else
        {
            cameraView.IsVisible = true; // or cameraView.Shutter() 등
        }
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await RequestCameraPermissionAsync();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        OnAppearing();
    }
    private async void CameraView_OnFrameAvailable(object sender, CameraFrameBufferEventArgs e)
         
    {
        var data = e.Data;
        var width = e.Width;
        var height = e.Height;

        var rotation = 0; // 기기의 회전 각도 (일반적으로 0도, 직접 설정 가능)
 
        var yaw = await FaceDetectionService.DetectYawAsync(data, width, height, rotation);

        if (yaw.HasValue)
        {
            string direction;
            if (yaw < -30)
                direction = "왼쪽 30도 이상";
            else if (yaw < -15)
                direction = "왼쪽 15도";
            else if (yaw > 30)
                direction = "오른쪽 30도 이상";
            else if (yaw > 15)
                direction = "오른쪽 15도";
            else
                direction = "정면";

            Device.BeginInvokeOnMainThread(() =>
            {
                directionLabel.Text = $"얼굴 방향: {direction}";
            });
        }
    }
}