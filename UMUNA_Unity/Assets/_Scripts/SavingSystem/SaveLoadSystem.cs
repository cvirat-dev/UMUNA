using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Umuna.Core.Data;
using Umuna.Core.Services;
using Umuna.Core.Services.FileDataService;
using Umuna.Core.Services.Serialization;
using UMUNA.Data;
using UnityEngine;

namespace UMUNA.SavingSystem
{
    public class SaveLoadSystem
    {
        #region Fields
        private ExportNotes _exportNotes;
        private BindSystem _bindSystem;
        private UmunaData _umunaData;
        private UmunaDataBinder _binder;
        private IFileSerializer<UmunaData> _umunaDataService;
        private IFileSerializer<ExportNotes> _exportNotesDataService;
        #endregion

        #region Properties
        public IFileSerializer<UmunaData> UmunaDataService => _umunaDataService;
        public IFileSerializer<ExportNotes> ExportNotesDataService => _exportNotesDataService;

        public ExportNotes ExportNotes
        {
            get => _exportNotes;
            set
            {
                _exportNotes = value;
            }
        }
        #endregion

        #region Constructors
        public SaveLoadSystem(
            UmunaData umunaData,
            ExportNotes exportNotes,
            BindSystem bindSystem,
            Dictionary<string, IFileDescriptor> fileServices,
            SerializerType serializerType = SerializerType.Json)
        {
            _umunaData = umunaData;
            _exportNotes = exportNotes;
            _bindSystem = bindSystem;
            _umunaDataService = DataServiceFactory.Create<UmunaData>(nameof(UmunaData), serializerType);
            fileServices.Add(nameof(UmunaData), _umunaDataService);
            _exportNotesDataService = DataServiceFactory.Create<ExportNotes>(nameof(ExportNotes), serializerType);
            fileServices.Add(nameof(ExportNotes), _exportNotesDataService);
        }
        #endregion

        #region Public Methods
        public void Save()
        {
            SaveData();
        }

        public async void SaveAsync()
        {
            // ShowSavingUi();
            await Task.Run(() => SaveData());
            // HideSavingUi();
        }

        public void Load()
        {
            LoadData();
        }

        #endregion

        #region Private Methods
        private void SaveData()
        {
            Thread.Sleep(500);
            UmunaDataService.Save(_umunaData);
            ExportNotesDataService.Save(ExportNotes);
            Debug.Log("Saved data");
        }

        private void LoadData()
        {
            _umunaData = UmunaDataService.Load();
            if(_binder == null)
                _bindSystem.Bind(_umunaData, out _binder);
            else
                _binder.Bind(_umunaData);
            Debug.Log("Loaded data"); // Dont forget to add Logging at a later point
        }
        #endregion
    }
}
