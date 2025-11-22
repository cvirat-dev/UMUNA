using System;
using System.IO;

namespace Umuna.Ui.Constants
{
    public static class AppConstants
    {
        const string APP_CONFIG_PATH_ENV = "APP_CONFIG_PATH";

        public const string AppTitle = "Umuna";

        private static readonly Lazy<string> _configFilePath = new(() =>
        {
            string? raw = Environment.GetEnvironmentVariable(APP_CONFIG_PATH_ENV);
            if (!string.IsNullOrEmpty(raw))
                return Expand(raw);

            string localEnvFile = $"{APP_CONFIG_PATH_ENV}.env";
            if (File.Exists(localEnvFile))
                return Expand(File.ReadAllText(localEnvFile).Trim());

            // fallback near the executable
            return Path.Combine(AppContext.BaseDirectory, "AppConfig.json");
        });

        public static string ConfigFilePath => _configFilePath.Value;

        private static string Expand(string value) => Environment.ExpandEnvironmentVariables(value);
    }
}
