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
using Microsoft.Extensions.DependencyInjection;
 
 
namespace DXApplication4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            CompatibilitySettings.UseLightweightThemes = true;
            ApplicationThemeHelper.Preload(PreloadCategories.Core);
 
        }

        IServiceProvider? services;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Theme.RegisterPredefinedPaletteThemes();
            ApplicationThemeHelper.ApplicationThemeName =
                PredefinedThemePalettes.Office2019Colorful.Orange.Name;

            services = new ServiceCollection()
                .AddTransient<IDataService, DataService>()
                .AddTransient<MainViewModel>()
                .BuildServiceProvider();

            DISource.Resolver = (type) =>
                type != null ? services.GetRequiredService(type) : null!;
        }
    }
}
