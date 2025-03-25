using CommunityToolkit.Maui.Core.Primitives;
using CommunityToolkit.Maui.Core;

namespace MauiApp1.Views;

public partial class CameraViewPage : ContentPage
{
    public CameraInfo? SelectedCamera { get; set; }
    public CameraViewPage()
    {
        InitializeComponent();

    }

   //  전면 카메라 변경
    private async void ButtonFront(object sender, EventArgs e)
    {
        try
        {

            var cameras = await cameraView.GetAvailableCameras(CancellationToken.None);

            if (cameras == null || cameras.Count == 0)
            {
                await DisplayAlert("경고", "사용 가능한 카메라가 없습니다", "확인");
                return;
            }

            SelectedCamera = cameras.FirstOrDefault(c => c.Position == CameraPosition.Front);
            if (SelectedCamera == null)
            {
                await DisplayAlert("경고", "후면 카메라를 찾을 수 없습니다", "확인");
                return;
            }

            cameraView.SelectedCamera = SelectedCamera;
            await cameraView.StartCameraPreview(CancellationToken.None);
        }
        catch (Exception ex)
        {
            await DisplayAlert("에러", ex.Message, "확인");
        }
    }
    // 후면카메라 전환
    private async void ButtonRear(object sender, EventArgs e)
    {
        try
        {

            var cameras = await cameraView.GetAvailableCameras(CancellationToken.None);

            if (cameras == null || cameras.Count == 0)
            {
                await DisplayAlert("경고", "사용 가능한 카메라가 없습니다", "확인");
                return;
            }

            SelectedCamera = cameras.FirstOrDefault(c => c.Position == CameraPosition.Rear);
            if (SelectedCamera == null)
            {
                await DisplayAlert("경고", "후면 카메라를 찾을 수 없습니다", "확인");
                return;
            }

            cameraView.SelectedCamera = SelectedCamera;
            await cameraView.StartCameraPreview(CancellationToken.None);
        }
        catch (Exception ex)
        {
            await DisplayAlert("에러", ex.Message, "확인");
        }
    }
    private async void StartCameraManually()
    {
        if (!cameraView.IsCameraBusy)
        {
            await cameraView.StartCameraPreview(CancellationToken.None);
        }
    }
}