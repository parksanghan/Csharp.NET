using CommunityToolkit.Maui.Camera;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.Threading;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
using System.Net.Http.Headers;
using Microsoft.Maui.Storage;



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
    }
    private async Task<bool> RequestGalleryPermissionAsync()
    {
 
    if (DeviceInfo.Version.Major >= 13)
    {
        var status = await Permissions.RequestAsync<Permissions.Media>();
        return status == PermissionStatus.Granted;
    }
    else
    {
        var status = await Permissions.RequestAsync<Permissions.StorageRead>();
        return status == PermissionStatus.Granted;
    }
        return true;
    }
    private async void Button_Gallery(object sender, EventArgs e)
    {
        try
        {
            if (!await RequestGalleryPermissionAsync())
            {
                await DisplayAlert("권한 거부", "갤러리 접근 권한이 필요합니다.", "확인");
                return;
            }

            var results = await FilePicker.PickMultipleAsync(new PickOptions
            {
                PickerTitle = "이미지 선택",
                FileTypes = FilePickerFileType.Images
            });

            if (results == null || !results.Any())
                return;

            using var form = new MultipartFormDataContent();
            int i = 0;
            foreach (var file in results)
            {
                var stream = await file.OpenReadAsync();

                // 바이트 크기 측정용 메모리 스트림 복사
                using var memory = new MemoryStream();
                await stream.CopyToAsync(memory);
                var imageBytes = memory.ToArray();

                var byteContent = new ByteArrayContent(imageBytes);
                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

                // 디버깅 출력
                Console.WriteLine($"{i}+[DEBUG] 파일 이름: {file.FileName}");
                Console.WriteLine($"{i}[DEBUG] Content-Type: {byteContent.Headers.ContentType}");
                Console.WriteLine($"{i}[DEBUG] Byte 크기: {imageBytes.Length}");

                form.Add(byteContent, "files", file.FileName);
                i++;
            }
            //var httpClient = new HttpClient();
            //var response = await httpClient.PostAsync("http://192.168.0.10:8080/upload", form); // 서버 URL로 수정

            //if (response.IsSuccessStatusCode)
            //{
            //    var result = await response.Content.ReadAsStringAsync();
            //    await DisplayAlert("업로드 완료", result, "확인");
            //}
            //else
            //{
            //    await DisplayAlert("업로드 실패", $"서버 오류: {response.StatusCode}", "확인");
            //}
        }
        catch (Exception ex)
        {
            await DisplayAlert("예외", ex.Message, "확인");
        }
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
    // cameraView.CaptureImage() 시 호출됨
    private async void cameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
    {
        Console.WriteLine("📸 MediaCaptured 이벤트 호출됨");

        Console.WriteLine("📸 MediaCaptured 이벤트 호출됨");

        try
        {
            using var memoryStream = new MemoryStream();
            await e.Media.CopyToAsync(memoryStream);
            var imageBytes = memoryStream.ToArray();

            Console.WriteLine($"[DEBUG] 이미지 바이트 크기: {imageBytes.Length}");

#if ANDROID
           
            var dcimDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            var cameraDir = new Java.IO.File(dcimDir, "Camera");
            if (!cameraDir.Exists())cameraDir.Mkdir();
            var filePath = System.IO.Path.Combine(cameraDir.AbsolutePath, $"captured_{DateTime.Now:yyyyMMdd_HHmmss}.jpg");


            await File.WriteAllBytesAsync(filePath, imageBytes);
            Console.WriteLine($"✅ 이미지 저장됨: {filePath}");
            // ✅ 저장 후 MediaScanner 호출
            Android.Content.Context context = Android.App.Application.Context;
            Android.Content.Intent mediaScanIntent = new(Android.Content.Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(new Java.IO.File(filePath));
            mediaScanIntent.SetData(contentUri);
            context.SendBroadcast(mediaScanIntent);
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await DisplayAlert("저장 완료", $"이미지가 저장되었습니다.\n{filePath}", "확인");
            });
#endif
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ 예외 발생: {ex}");

            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await DisplayAlert("예외 발생", $"메시지: {ex.Message}\n스택: {ex.StackTrace}", "확인");
            });
        }

    }
    public async void MediaPicker1()
    {
        try
        {
            var status = await Permissions.RequestAsync<Permissions.Photos>();
            if (status != PermissionStatus.Granted)
            {
                await DisplayAlert("권한 부족", "갤러리 접근 권한이 필요합니다.", "확인");
                return;
            }
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "갤러리에서 이미지 선택"
            });
            if (photo == null)
            {
                await DisplayAlert("취소됨", "이미지 선택이 취소되었습니다.", "확인");
                return;
            }
            using var stream = await photo.OpenReadAsync();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var imageBytes = ms.ToArray();
            var content = new MultipartFormDataContent();
            var byteContent = new ByteArrayContent(imageBytes);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            string username = "dd"; content.Add(byteContent, "file", username);
            using var client = new HttpClient();
            Console.WriteLine($"[DEBUG] 파일명: {username}");
            Console.WriteLine($"[DEBUG] Content-Type: {byteContent.Headers.ContentType}");
            Console.WriteLine($"[DEBUG] Byte 크기: {imageBytes.Length}");

            // 전송 하는 코드
            await DisplayAlert("성공", "이미지를 성공적으로 가져왔습니다!", "확인");
        }
        catch (Exception ex)
        {
            await DisplayAlert("에러", $"예외 발생: {ex.Message}", "확인");
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
    //try
    //{
    //    await DisplayAlert("Debug", "MediaCaptured 호출됨!", "OK");

    //    if (e.Media == null)
    //    {
    //        await DisplayAlert("오류", "Media가 null입니다", "확인");
    //        return;
    //    }

    //    using var memoryStream = new MemoryStream();
    //    await e.Media.CopyToAsync(memoryStream);
    //    var imageBytes = memoryStream.ToArray();
    //    if (imageBytes == null || imageBytes.Length == 0)
    //    {
    //        await DisplayAlert("오류", "이미지 데이터가 비어있습니다", "확인");
    //        return;
    //    }


    //    var yaw = await FaceDetection.DetectYawAsync2(imageBytes, 480, 640, 0);
    //    if (yaw.HasValue)
    //    {
    //        string direction = yaw switch
    //        {
    //            < -30 => "왼쪽 30도 이상",
    //            < -15 => "왼쪽 15도",
    //            > 30 => "오른쪽 30도 이상",
    //            > 15 => "오른쪽 15도",
    //            _ => "정면"
    //        };

    //        await MainThread.InvokeOnMainThreadAsync(() =>
    //        {
    //            directionLabel.Text = $"얼굴 방향: {direction}";
    //        });
    //    }

    //}
    //catch (Exception ex)
    //{
    //    await DisplayAlert("MediaCaptured 예외 발생", $"예외: {ex.Message}\n{ex.InnerException?.Message}", "확인");
    //}
}