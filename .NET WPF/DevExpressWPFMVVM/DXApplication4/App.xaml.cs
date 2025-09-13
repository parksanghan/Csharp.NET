using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Core;
using DXApplication4.Infrastructure;
using DXApplication4.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
 using System.Windows.Media;    
namespace DXApplication4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; } = null!;
        static App()
        {
            CompatibilitySettings.UseLightweightThemes = true;
            ApplicationThemeHelper.Preload(PreloadCategories.Core);
 
        }

        IServiceProvider? services;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
           
// 기존 코드
// ApplicationThemeHelper.ApplicationThemeName =
//     PredefinedThemePalettes.Office2016SEPalettes.Blue.Name;

// 수정된 코드
            ApplicationThemeHelper.ApplicationThemeName = Theme.VS2019Blue.Name;
 
            Configuration = new ConfigurationBuilder()
         .SetBasePath(AppContext.BaseDirectory)
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
         .Build();
            services = new ServiceCollection()
                  .AddSingleton<IConfiguration>(Configuration)        
                .AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>()
                .AddTransient<IDataService, DataService>()
                .AddTransient<MainViewModel>()
                 .AddTransient<DXApplication4.Services.IUsersService, DXApplication4.Services.UsersService>()
                 .AddTransient<DXApplication4.Services.IChatLogsService, DXApplication4.Services.ChatLogsService>()
                 .AddTransient<DXApplication4.ViewModels.UsersViewModel>()
                  .AddTransient<DXApplication4.ViewModels.ChatLogsViewModel>()

                .BuildServiceProvider();
            DISource.Resolver = t => t != null ? services.GetRequiredService(t) : null;
        }
    }
}
