
using System.IO;
using Umuna.Core.Services.FileDataService;
using UMUNA.Constants.Configuration;
using UnityEngine;

namespace UMUNA.Configuration
{
    public class Configurator<T> : IConfigurator<T> where T : class
    {
        #region Fields
        readonly T _data;
        readonly IFileSerializer<T> _dataService;
        #endregion

        #region Properties
        public T Data => _data;
        public IFileSerializer<T> DataService => _dataService;
        #endregion

        #region Constructors
        public Configurator()
        {
            _dataService = FileSerializerFactory.Create<T>(
                Path.Combine(Application.persistentDataPath, ConfigConstants.ConfigFileRelativePath));
            _data = _dataService.Load();
            if (_data == null)
            {
                throw new FileNotFoundException($"Configuration file not found at {ConfigConstants.ConfigFileRelativePath}. Please ensure the file exists.");
            }
        }
        #endregion

        #region Public Methods
        public bool TryReload(out T data)
        {
            data = _dataService.Load();
            if (data == null)
            {
                Debug.LogError($"Failed to reload configuration from {ConfigConstants.ConfigFileRelativePath}. Please ensure the file exists and is valid.");
                return false;
            }
            return true;
        }
        #endregion
    }
}