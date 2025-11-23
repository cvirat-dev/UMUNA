using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Windows;
using Umuna.Core.Services.FileDataService;
using Umuna.Core.Services.Serialization;
using Umuna.Ui.Constants;
using Umuna.Ui.Infrastructure.Logging;
using Umuna.Ui.Models;
using Umuna.Ui.Services.Communication;
using Umuna.Ui.Services.Logging;
using Umuna.Ui.ViewModels;

namespace Umuna.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;
        private ILogger<App>? _logger;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize logging system (Serilog + LoggerFactory) before DI and log usage
            LoggerService.Initialize();

            // Configure services and logging
            _serviceProvider = ConfigureServices();

            // Get logger from DI container
            _logger = _serviceProvider.GetRequiredService<ILogger<App>>();
            _logger.LogInformation("Application starting up");

            // Set up global exception handlers
            SetupExceptionHandling();

            // Show Main Window
            var mainWindow = new MainWindow { DataContext = _serviceProvider.GetRequiredService<RootViewModel>() };
            mainWindow.Show();
        }

        private static ServiceProvider ConfigureServices()
        {
            // Setup Dependency Injection
            ServiceCollection services = new();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog(dispose: false);
            });

            services.AddSingleton<IFileSerializer<AppConfig>>(
                provider => FileSerializerFactory.Create<AppConfig>(
                    AppConstants.AppConfigPath,
                    SerializerType.json
                    )
                );

            // Register configuration
            services.AddSingleton<IConfiguration>(AppConstants.Config);

            // Register  services
            services.AddSingleton<ICommunicationService, TcpCommunicationService>();

            // AppConfig injection
            services.AddSingleton<AppConfig>(provider =>
            {
                IFileSerializer<AppConfig> serializer = provider.GetRequiredService<IFileSerializer<AppConfig>>();
                AppConfig config = serializer.Load() ?? new AppConfig();
                return config;
            });

            // Register ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<UserCreationViewModel>();
            services.AddSingleton<RootViewModel>();

            // Build ServiceProvider once and return
            return services.BuildServiceProvider();
        }

        private void SetupExceptionHandling()
        {
            if (_logger == null)
            {
                MessageBox.Show("Logger is not initialized. Cannot set up exception handling.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // UI thread exceptions
            DispatcherUnhandledException += (s, e) =>
            {
                _logger.LogCritical(e.Exception, "Unhandled UI thread exception");
                MessageBox.Show($"An unexpected error occurred:\n\n{e.Exception.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            };

            // Non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                Exception? exception = e.ExceptionObject as Exception;
                _logger.LogCritical(exception, "Unhandled non-UI thread exception");
            };

            // Task exceptions
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                _logger.LogCriticalWithCaller(s, e.Exception, "Unhandled Task exception");
                e.SetObserved();
            };
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (_logger == null)
            {
                base.OnExit(e);
                return;
            }

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Application shutting down with exit code {ExitCode}", e.ApplicationExitCode);
                LoggerService.Shutdown();
            }

            base.OnExit(e);
        }
    }

}
