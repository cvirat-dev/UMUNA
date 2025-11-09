using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Umuna.Core.Services.FileDataService;
using Umuna.Core.Services.Serialization;
using Umuna.Ui.Constants;
using Umuna.Ui.Models;
using Umuna.Ui.Services.Communication;
using Umuna.Ui.ViewModels;

namespace Umuna.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Setup Dependency Injection
            var services = new ServiceCollection();

            services.AddSingleton(provider => FileSerializerFactory.Create<AppConfig>(AppConstants.ConfigFilePath, SerializerType.Json));
            services.AddSingleton<ICommunicationService, TcpCommunicationService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<UserCreationViewModel>();
            services.AddSingleton<RootViewModel>();

            // Build ServiceProvider
            Services = services.BuildServiceProvider();

            // Show Main Window
            var mainWindow = new MainWindow { DataContext = Services.GetRequiredService<RootViewModel>() };
            mainWindow.Show();
        }
    }

}
