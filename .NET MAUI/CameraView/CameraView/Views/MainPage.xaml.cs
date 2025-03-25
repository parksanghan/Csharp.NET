using CommunityToolkit.Maui.Camera;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.Threading;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
#if ANDROID
using CameraView.Platforms.Android.Api;
#endif

namespace CameraView.Views;

public partial class MainPage : ContentPage
{     
    #if ANDROID
    private static bool _mlkitInitialized = false;
#endif
    int count = 0;
    public CameraInfo? SelectedCamera { get; set; }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;

#if ANDROID
    InitializeMLKit(); // 👈 이 줄 추가
#endif
    }

    private async void Button_Clicked3(object sender, EventArgs e)
    {
        try
        {
            if (cameraView.SelectedCamera == null)
            {
                await DisplayAlert("경고", "카메라가 선택되지 않았습니다", "확인");
                return;
            }

            await Task.Delay(1000);
            await cameraView.CaptureImage(CancellationToken.None);
        }
        catch (Exception ex)
        {
            await DisplayAlert("예외 발생", $"메시지: {ex.Message}\n내부 예외: {ex.InnerException?.Message}", "확인");
        }
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;
        CounterBtn.Text = count == 1 ? $"Clicked {count} time" : $"Clicked {count} times";
        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
#if WINDOWS
            bool result = await CheckCameraPermission();
            if (!result) return;
#endif
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

    private async Task<bool> CheckCameraPermission()
    {
        var status = await Permissions.RequestAsync<Permissions.Camera>();
        return status == PermissionStatus.Granted;
    }

    private async void Button_Clicked2(object sender, EventArgs e)
    {
        try
        {
#if WINDOWS
            bool result = await CheckCameraPermission();
            if (!result) return;
#endif
            var cameras = await cameraView.GetAvailableCameras(CancellationToken.None);
            if (cameras == null || cameras.Count == 0)
            {
                await DisplayAlert("경고", "사용 가능한 카메라가 없습니다", "확인");
                return;
            }

            SelectedCamera = cameras.FirstOrDefault(c => c.Position == CameraPosition.Front);
            if (SelectedCamera == null)
            {
                await DisplayAlert("경고", "전면 카메라를 찾을 수 없습니다", "확인");
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

    private async void Button_Clicked1(object sender, EventArgs e)
    {
        var cameras = await cameraView.GetAvailableCameras(CancellationToken.None);
        if (cameras == null || cameras.Count == 0)
        {
            await DisplayAlert("경고", "카메라가 없습니다", "확인");
            return;
        }

        SelectedCamera = cameras.FirstOrDefault(c => c.Position == CameraPosition.Rear);
        cameraView.SelectedCamera = SelectedCamera;
        await cameraView.StartCameraPreview(CancellationToken.None);
    }

    private async void cameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
    {
        try
        {
            await DisplayAlert("Debug", "MediaCaptured 호출됨!", "OK");

            if (e.Media == null)
            {
                await DisplayAlert("오류", "Media가 null입니다", "확인");
                return;
            }

            using var memoryStream = new MemoryStream();
            await e.Media.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();
            if (imageBytes == null || imageBytes.Length == 0)
            {
                await DisplayAlert("오류", "이미지 데이터가 비어있습니다", "확인");
                return;
            }

#if ANDROID
            var yaw = await FaceDetectionService.DetectYawAsync2(imageBytes, 480, 640, 0);
            if (yaw.HasValue)
            {
                string direction = yaw switch
                {
                    < -30 => "왼쪽 30도 이상",
                    < -15 => "왼쪽 15도",
                    > 30 => "오른쪽 30도 이상",
                    > 15 => "오른쪽 15도",
                    _ => "정면"
                };

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    directionLabel.Text = $"얼굴 방향: {direction}";
                });
            }
#endif
        }
        catch (Exception ex)
        {
            await DisplayAlert("MediaCaptured 예외 발생", $"예외: {ex.Message}\n{ex.InnerException?.Message}", "확인");
        }
    }
#if ANDROID
private async void InitializeMLKit()
{
    if (_mlkitInitialized) return;

    try
    {
        // MLKit 관련 클래스 한 번 초기화 → 모델 미리 로딩
        await Task.Run(() =>
        {
            var dummy = new FaceDetectionService();
        });

        _mlkitInitialized = true;
        System.Diagnostics.Debug.WriteLine("✅ MLKit 초기화 완료");
    }
    catch (Exception ex)
    {
        System.Diagnostics.Debug.WriteLine($"❌ MLKit 초기화 실패: {ex.Message}");
    }
}
#endif

}