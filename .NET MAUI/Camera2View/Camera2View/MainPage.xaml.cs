namespace Camera2View;


public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();

        MyCameraPreview.OnYawDetected = (yaw) =>
        {

            // 바인딩 작업
            MainThread.BeginInvokeOnMainThread(() =>
            {
                degreeLabel.Text = $"Yaw: {yaw:F1}°";
            });
        };
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
