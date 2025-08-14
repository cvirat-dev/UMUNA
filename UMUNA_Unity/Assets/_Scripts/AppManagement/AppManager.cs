
using System.Collections.Generic;
using Umuna.Core.Data;
using Umuna.Core.Services.FileDataService;
using UMUNA.Configuration;
using UMUNA.Data;
using UMUNA.SavingSystem;

namespace UMUNA.AppManagement
{
    /// <summary>
    /// This class is responsible two main things:
    /// 1) It creates all the necessary objects for the app to run.
    /// 2) It provides access to the objects it creates.
    /// </summary>
    public class AppManager
    {
        #region Fields
        Configurator _configurator;
        UmunaData _umunaData;
        Dictionary<string, IFileDescriptor> _dataServices;
        ExportNotes _exportNotes;
        SaveLoadSystem _saveLoadSystem;
        BindSystem _bindSystem;
        #endregion

        #region Properties
        public Configurator Configurator => _configurator ??= new Configurator(DataServices);
        public Dictionary<string, IFileDescriptor> DataServices => _dataServices ??= new();
        public SaveLoadSystem SaveLoadSystem => _saveLoadSystem;
        public UmunaData UmunaData
        {
            get => _umunaData;
        }
        public BindSystem BindSystem => _bindSystem;
        public ExportNotes ExportNotes => _exportNotes;
        #endregion

        #region Constructors
        public AppManager()
        {
            _configurator = new Configurator(DataServices);
            _umunaData = new UmunaData();
            _exportNotes = new ExportNotes();
            _bindSystem = new BindSystem();
            _saveLoadSystem = new SaveLoadSystem(UmunaData, ExportNotes, BindSystem, DataServices);
        }
        #endregion
    }
}
