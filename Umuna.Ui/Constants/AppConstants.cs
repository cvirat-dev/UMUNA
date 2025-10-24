
using System;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;

namespace Umuna.Ui.Constants
{
    public static class AppConstants
    {
        public const string AppTitle = "Umuna";

        public static string ConfigFilePath => Environment.GetEnvironmentVariable("APP_CONFIG_PATH") 
            ?? throw new InvalidOperationException("The environment variable 'APP_CONFIG_PATH' is not set.");
    }
}
