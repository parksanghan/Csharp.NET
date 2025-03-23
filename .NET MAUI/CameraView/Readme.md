# CameraView

# CameraView

## Cameraview ms guide-line
https://learn.microsoft.com/ko-kr/dotnet/communitytoolkit/maui/views/camera-view?tabs=android
## CommunityToolkit Cameraview git
https://github.com/CommunityToolkit/Maui/tree/main/src/CommunityToolkit.Maui.Camera
```xml
Microsoft.Maui.Controls {9.0.30} CameraView
CommunityToolkit.Maui.MediaElement {6.0.1} CameraView
CommunityToolkit.Maui {11.1.0} CameraView
Microsoft.NET.ILLink.Tasks {9.0.3} CameraView
Xamarin.Google.MLKit.FaceDetection {116.1.7.2} CameraView
CommunityToolkit.Maui.Camera {2.0.2} CameraView
Microsoft.Extensions.Logging.Debug {9.0.0} CameraView
```

위의 nuget 다운 

```csharp
using CommunityToolkit.Maui;
 
using Microsoft.Extensions.Logging;

namespace CameraView
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<Views.App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitCamera()
                .UseMauiCommunityToolkitMediaElement()
                // Builder 에서 추가필요 !
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
// mainProgram.cs
```

```csharp
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
	<application android:allowBackup="true" android:icon="@mipmap/appicon" android:roundIcon="@mipmap/appicon_round" android:supportsRtl="true"></application>
	<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
	<uses-permission android:name="android.permission.INTERNET" />
	<uses-permission android:name="android.permission.CAMERA" /> 요거임당
</manifest>
위에 처럼 각 플랫폼마다 permission 추가 필요 
```

```csharp
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
             // 위의 toolkit 추가 필요 
             x:Class="CameraView.Views.MainPage">

    <ScrollView>
        <VerticalStackLayout
            Padding="30,0"
            Spacing="25">
            <Image
                Source="dotnet_bot.png"
                HeightRequest="185"
                Aspect="AspectFit"
                SemanticProperties.Description="dot net bot in a hovercraft number nine" />

            <Label
                Text="Hello, World!"
                Style="{StaticResource Headline}"
                SemanticProperties.HeadingLevel="Level1" />

            <Label
                Text="Welcome to &#10;.NET Multi-platform App UI"
                Style="{StaticResource SubHeadline}"
                SemanticProperties.HeadingLevel="Level2"
                SemanticProperties.Description="Welcome to dot net Multi platform App U I" />

            <Button
                x:Name="CounterBtn"
                Text="Click me" 
                SemanticProperties.Hint="Counts the number of times you click"
                Clicked="OnCounterClicked"
                HorizontalOptions="Fill" />
            <Button 
                Text="카메라 작동"
                Clicked="Button_Clicked"
                HorizontalOptions="Center" />
            <Grid ColumnDefinitions="*,*,*" RowDefinitions="*,30,30">
                <toolkit:CameraView 
                    x:Name="cameraView"
            Grid.ColumnSpan="3" 
            Grid.Row="0" 
            SelectedCamera="{Binding SelectedCamera}" />
            </Grid>

        </VerticalStackLayout>
    </ScrollView>

</ContentPage>
// mainPage.xaml
```

```csharp
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
//mainPage.cs
```
