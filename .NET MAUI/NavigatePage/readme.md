# Navigator

## Views 네임스페이스 이동 시

각 xaml 파일 변동

```xml
xClass="NavigatePage.App">             =>     x:Class="NavigatePage.Views.App">
xmlns:local="clr-namespace:NavigatePage.Views"  => xmlns:local="clr-namespace:NavigatePage.Views"
// 각 파일 xaml 상담에서 저렇게 바꿔주새요
// 각파일의 namespace도 알맞게 변겨해주세요 
// EX namespace NavigatePage.Views;
<?xml version = "1.0" encoding = "UTF-8" ?>
<Application xmlns="<http://schemas.microsoft.com/dotnet/2021/maui>"
             xmlns:x="<http://schemas.microsoft.com/winfx/2009/xaml>"
             xmlns:local="clr-namespace:MauiApp1.Views"
             x:Class="MauiApp1.Views.App">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="/Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="/Resources/Styles/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>

ResourceDictionary 에서 원래 Resources/Style에서  /Resources/Style/ 로 변경

```

mainProgram.cs도 아래와 같이 변경해주세요

```csharp
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace NavigatePage
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<Views.App>() // 원랜 <APP>으로 되어있음
                .UseMauiCommunityToolkit()
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

```

```xml
<Shell xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:local="clr-namespace:MyApp"
       x:Class="MyApp.AppShell">

    <TabBar>
        <ShellContent Title="홈" ContentTemplate="{DataTemplate local:MainPage}" />
        <ShellContent Title="설정" ContentTemplate="{DataTemplate local:SettingsPage}" />
    </TabBar>

</Shell>
```

```xml
<FlyoutItem Title="홈">
    <ShellContent ContentTemplate="{DataTemplate local:MainPage}" />
</FlyoutItem>

<FlyoutItem Title="설정">
    <ShellContent ContentTemplate="{DataTemplate local:SettingsPage}" />
</FlyoutItem>
```

| 항목 | `<TabBar>` 사용 시 | `<FlyoutItem>` 사용 시 |
| --- | --- | --- |
| **UI 형태** | 하단 탭바 | 햄버거 메뉴 (사이드 메뉴) |
| **표시 방식** | 하단에 탭 버튼들이 고정 | 왼쪽 상단 ☰ 클릭 시 메뉴 펼쳐짐 |
| **사용자 경험** | 앱 전환이 빠르고 직관적 | 메뉴가 많을 때 유리함 |
| **용도 추천** | 2~5개 자주 쓰는 메뉴 | 5개 이상, 중첩 메뉴 필요할 때 |
| **모바일 친화도** | 매우 높음 (모바일 앱 탭 구조와 유사) | 넓은 구조에 적합 (웹/태블릿) |

```csharp
namespace NavigatePage.Views;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(NewPage1), typeof(NewPage1));

    }
}
// 위와 같이 라우팅 등록 필요 
```

```xml
      
      // somethingPage.xaml
      <Button 
          Text="설정 페이지로 이동"
          Clicked="OnGoToSettingsClicked"
          HorizontalOptions="Center" />
		  </VerticalStackLayout>
```

```csharp
   private async void OnGoToSettingsClicked(object sender , EventArgs e)
   {
       await Shell.Current.GoToAsync(nameof(NewPage1));
   }
```

위와 같이  네비게이션을 이동