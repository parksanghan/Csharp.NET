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
            await DisplayAlert("���� �ʿ�", "������ �ʿ��մϴ�,", "Ȯ��");
        }
        else
        {
            cameraView.IsVisible = true; // or cameraView.Shutter() ��
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

        var rotation = 0; // ����� ȸ�� ���� (�Ϲ������� 0��, ���� ���� ����)
 
        var yaw = await FaceDetectionService.DetectYawAsync(data, width, height, rotation);

        if (yaw.HasValue)
        {
            string direction;
            if (yaw < -30)
                direction = "���� 30�� �̻�";
            else if (yaw < -15)
                direction = "���� 15��";
            else if (yaw > 30)
                direction = "������ 30�� �̻�";
            else if (yaw > 15)
                direction = "������ 15��";
            else
                direction = "����";

            Device.BeginInvokeOnMainThread(() =>
            {
                directionLabel.Text = $"�� ����: {direction}";
            });
        }
    }
}