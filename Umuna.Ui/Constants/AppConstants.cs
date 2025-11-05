

using System.IO;

namespace Umuna.Ui.Constants
{
    public static class AppConstants
    {
        const string APP_CONFIG_PATH_ENV = "APP_CONFIG_PATH";

        public const string AppTitle = "Umuna";

        private static readonly Lazy<string> _configFilePath = new(() =>
        {
            string? path = Environment.GetEnvironmentVariable(APP_CONFIG_PATH_ENV);
            if (!string.IsNullOrEmpty(path))
                return path;

            string localEnvFile = Path.Combine(AppContext.BaseDirectory, $"{APP_CONFIG_PATH_ENV}.env");
            if (File.Exists(localEnvFile))
                return File.ReadAllText(localEnvFile).Trim();

            throw new InvalidOperationException(
                "APP_CONFIG_PATH is not set and no local .env file found.");
        });

        public static string ConfigFilePath => _configFilePath.Value;
    }
}
