namespace Camera2View;


public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        

        myCameraPreview.OnYawDetected = (yaw) =>
        {

            // 바인딩 작업
            MainThread.BeginInvokeOnMainThread(() =>
            {
                degreeLabel.Text = $"Yaw: {yaw:F1}°";
            });
        };
        CheckAndStartCameraAsync(); // ← 권한요청 및 카메라 호출 추가
    }
    private async void CheckAndStartCameraAsync()
    {
        bool cameraAllowed = await CheckAndRequestCameraPermissionAsync();

        if (cameraAllowed)
        {
            myCameraPreview.StartCamera(); // 이제 이렇게 호출 가능!
        }
        else
        {
            await DisplayAlert("권한 필요", "카메라 권한이 허용되지 않았습니다.", "확인");
        }

    }
    public async Task<bool> CheckAndRequestCameraPermissionAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Camera>();
        }

        return status == PermissionStatus.Granted;
    }
    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
