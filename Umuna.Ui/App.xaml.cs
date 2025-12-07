using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Windows;
using System.Windows.Threading;
using Umuna.Core.Serialization.FileDataService;
using Umuna.Core.Serialization.Serialization;
using Umuna.Ui.Constants;
using Umuna.Ui.Infrastructure.Services;
using Umuna.Ui.Infrastructure.Services.Communication;
using Umuna.Ui.Infrastructure.Services.Logging;
using Umuna.Ui.Models;
using Umuna.Ui.ViewModels;
using Umuna.Ui.Views;

namespace Umuna.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider? _serviceProvider;
        private ILogger<App>? _logger;
        private IErrorDialogService? _errorDialogService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize logging system (Serilog + LoggerFactory) before DI and log usage
            LoggerService.Initialize();

            // Configure services and logging
            _serviceProvider = ConfigureServices();
            _errorDialogService = _serviceProvider.GetRequiredService<IErrorDialogService>();

            // Get logger from DI container
            _logger = _serviceProvider.GetRequiredService<ILogger<App>>();
            _logger.LogInformation("Application starting up");

            // Set up global exception handlers
            SetupExceptionHandling();

            // Show Main Window
            MainWindow mainWindow = new() { DataContext = _serviceProvider.GetRequiredService<RootViewModel>() };
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
            services.AddSingleton<IErrorDialogService, ErrorDialogService>();

            // AppConfig injection
            services.AddSingleton<AppConfig>(provider =>
            {
                IFileSerializer<AppConfig> serializer = provider.GetRequiredService<IFileSerializer<AppConfig>>();
                AppConfig config = serializer.Load() ?? new AppConfig();
                return config;
            });

            // Register ViewModels
            services.AddSingleton<RootViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<UserCreationViewModel>();
            services.AddSingleton<RootViewModel>();

            // Register Views
            services.AddSingleton<MainWindow>();
            services.AddTransient<LoginView>();
            services.AddTransient<UserCreationView>();

            // Build ServiceProvider once and return
            return services.BuildServiceProvider();
        }

        private void SetupExceptionHandling()
        {
            // UI thread exceptions
            DispatcherUnhandledException += OnDispatcherUnhandledException;

            // Non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            // Task exceptions
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.LogCritical(e.Exception, "Unhandled UI thread exception");

            _errorDialogService.ShowErrorAsync(
                "Unexpected Error",
                "An unexpected error occurred. The application may need to restart.",
                e.Exception
            ).Wait();

            e.Handled = true; // Prevent crash
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception? exception = e.ExceptionObject as Exception;
            _logger.LogCritical(exception, "Unhandled non-UI thread exception. Terminating: {IsTerminating}",
                e.IsTerminating);

            if (exception != null)
            {
                _errorDialogService.ShowErrorAsync(
                    "Critical Error",
                    "A critical error occurred. The application will close.",
                    exception
                ).Wait();
            }
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _logger.LogError(e.Exception, "Unobserved task exception");

            _errorDialogService.ShowErrorAsync(
                "Background Task Error",
                "An error occurred in a background operation.",
                e.Exception
            ).Wait();

            e.SetObserved(); // Prevent crash
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
