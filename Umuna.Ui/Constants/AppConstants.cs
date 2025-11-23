using Microsoft.Extensions.Configuration;
using System.IO;

namespace Umuna.Ui.Constants
{
    public static class AppConstants
    {
        public const string AppTitle = "Umuna";

        public static IConfigurationRoot Config { get; } =
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables() // allows overrides via env vars
                .Build();

        public static string AppConfigPath => Expand(
            Config["Paths:AppConfig"]!);

        public static string LogConfigPath => Expand(
            Config["Paths:LogConfig"]!);

        private static string Expand(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Configuration value cannot be null or empty.", nameof(value));
            }

            return Environment.ExpandEnvironmentVariables(value);
        }
    }
}
