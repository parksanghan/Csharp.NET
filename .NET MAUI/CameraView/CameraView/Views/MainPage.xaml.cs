using CommunityToolkit.Maui.Camera;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.Threading;

using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;
namespace CameraView.Views;


public partial class MainPage : ContentPage
{
    int count = 0;
    public CameraInfo SelectedCamera { get; set; }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this; 
        // xaml 바인딩을 위한 설정임 이렇게안하면 안되는듯?
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

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var cameras = await cameraView.GetAvailableCameras(CancellationToken.None);

        // 후면 카메라 선택
        // 후면 카메라 선택
        SelectedCamera = cameras.FirstOrDefault(c => c.Position == CameraPosition.Rear);

        // XAML 바인딩을 썼지만 INotifyPropertyChanged가 없으니 수동 할당
        cameraView.SelectedCamera = SelectedCamera;

        await cameraView.StartCameraPreview(CancellationToken.None);
    }

}
