using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM_ToolKit.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
namespace MVVM_ToolKit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;       
        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder()
            .ConfigureServices(s =>
            {
                // DI 등록
                s.AddSingleton<MainViewModel>(); //프로그램에서 하나만 존재하는 MainViewModel 등록
                s.AddSingleton<MainWindow>(); // 메인 윈도우는 앱 생명주기 동안 딱 하나만 사용
            })
            .Build();

            var main = _host.Services.GetRequiredService<MainWindow>(); // 서비스 컨테이너에서 MainWindow 인스턴스 가져오기
            // 해당 시점에서 WPF 창이 생성
            main.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            // MainViewModel 인스턴스를 DataContext로  컨테이너에서 꺼내서 주입
            this.MainWindow = main;
            this.MainWindow.Show();

            base.OnStartup(e);
        }

    }

}
