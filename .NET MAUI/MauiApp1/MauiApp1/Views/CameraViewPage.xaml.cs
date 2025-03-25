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

   //  ���� ī�޶� ����
    private async void ButtonFront(object sender, EventArgs e)
    {
        try
        {

            var cameras = await cameraView.GetAvailableCameras(CancellationToken.None);

            if (cameras == null || cameras.Count == 0)
            {
                await DisplayAlert("���", "��� ������ ī�޶� �����ϴ�", "Ȯ��");
                return;
            }

            SelectedCamera = cameras.FirstOrDefault(c => c.Position == CameraPosition.Front);
            if (SelectedCamera == null)
            {
                await DisplayAlert("���", "�ĸ� ī�޶� ã�� �� �����ϴ�", "Ȯ��");
                return;
            }

            cameraView.SelectedCamera = SelectedCamera;
            await cameraView.StartCameraPreview(CancellationToken.None);
        }
        catch (Exception ex)
        {
            await DisplayAlert("����", ex.Message, "Ȯ��");
        }
    }
    // �ĸ�ī�޶� ��ȯ
    private async void ButtonRear(object sender, EventArgs e)
    {
        try
        {

            var cameras = await cameraView.GetAvailableCameras(CancellationToken.None);

            if (cameras == null || cameras.Count == 0)
            {
                await DisplayAlert("���", "��� ������ ī�޶� �����ϴ�", "Ȯ��");
                return;
            }

            SelectedCamera = cameras.FirstOrDefault(c => c.Position == CameraPosition.Rear);
            if (SelectedCamera == null)
            {
                await DisplayAlert("���", "�ĸ� ī�޶� ã�� �� �����ϴ�", "Ȯ��");
                return;
            }

            cameraView.SelectedCamera = SelectedCamera;
            await cameraView.StartCameraPreview(CancellationToken.None);
        }
        catch (Exception ex)
        {
            await DisplayAlert("����", ex.Message, "Ȯ��");
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