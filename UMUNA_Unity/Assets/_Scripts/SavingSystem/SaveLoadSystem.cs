using System.Threading;
using System.Threading.Tasks;
using Umuna.Core.SharedData;
using Umuna.Core.Services.FileDataService;
using UMUNA.Data;
using UnityEngine;
using Umuna.Core.Services.Serialization;
using System;

namespace UMUNA.SavingSystem
{
    public class SaveLoadSystem : ISaveLoadSystem
    {
        #region Fields
        private ExportNotes _exportNotes;
        private readonly IBindSystem _bindSystem;
        private UmunaData _umunaData;
        private UmunaDataBinder _binder;

        #endregion

        #region Properties
        public AppConfiguration AppConfig { get; init; }
        public IFileSerializer<UmunaData> UmunaDataService { get; init; }
        public IFileSerializer<ExportNotes> ExportNotesDataService { get; init; }

        public SerializerType SerializerType
            => (SerializerType)Enum.Parse(typeof(SerializerType), AppConfig.FileSystemConfiguration.SerializationFormat.Value);

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
            UmunaData           umunaData,
            ExportNotes         exportNotes,
            IBindSystem         bindSystem,
            AppConfiguration    appConfig)
        {
            _umunaData = umunaData;
            _exportNotes = exportNotes;
            _bindSystem = bindSystem;
            AppConfig = appConfig;

            UmunaDataService = FileSerializerFactory.Create<UmunaData>(
                nameof(UmunaData), 
                (SerializerType)Enum.Parse(
                    typeof(SerializerType), 
                    AppConfig.FileSystemConfiguration.SerializationFormat.Value,
                    ignoreCase: true));
            
            ExportNotesDataService = FileSerializerFactory.Create<ExportNotes>(
                nameof(ExportNotes), 
                (SerializerType)Enum.Parse(
                    typeof(SerializerType), 
                    AppConfig.FileSystemConfiguration.SerializationFormat.Value,
                    ignoreCase: true));
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
            if (_binder == null)
                _bindSystem.Bind(_umunaData, out _binder);
            _binder.Bind(_umunaData);
            Debug.Log("Loaded data"); // Dont forget to add Logging at a later point
        }
        #endregion
    }
}
