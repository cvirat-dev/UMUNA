using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;
using System.Windows;
using Umuna.Ui.Constants;

namespace Umuna.Ui.Services.Logging
{
    public class LoggerService
    {
        private static ILoggerFactory? _loggerFactory;

        public static void Initialize()
        {
            // Check if LogConfig.json exists, if not, create a default one
            if (!File.Exists(AppConstants.LogConfigPath))
            {
                throw new FileNotFoundException($"Log configuration file not found at {AppConstants.LogConfigPath}");
            }

            // Load the log configuration
            IConfigurationRoot logConfiguration = new ConfigurationBuilder()
                .AddJsonFile(AppConstants.LogConfigPath, optional: false, reloadOnChange: true)
                .Build();

            // Configure Serilog with custom value expansion
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(logConfiguration)
                .Enrich.FromLogContext()
                .CreateLogger();

            // Create LoggerFactory
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog(dispose: true);
            });

            Log.Information("Log system initialized.");
        }

        public static ILogger<T> GetLogger<T>()
        {
            if (_loggerFactory == null)
            {
                // Show MessageBox or log to console as fallback
                MessageBox.Show("LoggerService has not been initialized. Call Initialize() first.", "Logging Error", MessageBoxButton.OK, MessageBoxImage.Error);
                throw new InvalidOperationException("LoggerService has not been initialized. Call Initialize() first.");
            }

            return _loggerFactory.CreateLogger<T>();
        }

        public static void Shutdown()
        {
            Log.Information("Application shutting down");
            Log.CloseAndFlush();
        }
    }
}
