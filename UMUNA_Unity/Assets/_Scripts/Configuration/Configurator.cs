
using System.Collections.Generic;
using System.IO;
using Umuna.Core.Services;
using Umuna.Core.Services.FileDataService;
using UMUNA.Constants.Configuration;
using UnityEngine;

namespace UMUNA.Configuration
{
    public class Configurator
    {
        #region Fields
        readonly AppConfiguration _appConfiguration;
        readonly IFileSerializer<AppConfiguration> _appconfigDataService;
        #endregion

        #region Properties
        public AppConfiguration AppConfiguration => _appConfiguration;
        public IFileSerializer<AppConfiguration> AppConfigDataService => _appconfigDataService;
        #endregion

        #region Constructors
        public Configurator(Dictionary<string, IFileDescriptor> fileServices)
        {
            string fullPath = Path.Combine(Application.persistentDataPath, ConfigConstants.ConfigFileRelativePath);
            _appconfigDataService = DataServiceFactory.Create<AppConfiguration>(fullPath);
            _appConfiguration = _appconfigDataService.Load();
            if (_appConfiguration == null)
            {
                throw new FileNotFoundException($"Configuration file not found at {ConfigConstants.ConfigFileRelativePath}. Please ensure the file exists.");
            }
            fileServices.Add(nameof(AppConfiguration), _appconfigDataService);
        }
        #endregion
    }
}