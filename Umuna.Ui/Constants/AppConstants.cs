
using System;

namespace Umuna.Ui.Constants
{
    public static class AppConstants
    {
        public const string AppTitle = "Umuna";

        private static readonly Lazy<string> _configFilePath = new Lazy<string>(() =>
            Environment.GetEnvironmentVariable("APP_CONFIG_PATH")
            ?? throw new InvalidOperationException(
                "The environment variable 'APP_CONFIG_PATH' is not set. " +
                "Please set it to the absolute path of the configuration file."));

        public static string ConfigFilePath => _configFilePath.Value;
    }
}
