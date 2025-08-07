using System.Threading;
using System.Threading.Tasks;
using Umuna.Core.Data;
using Umuna.Core.Services;
using Umuna.Core.Services.FileDataService;
using Umuna.Core.Services.Serialization;
using UMUNA.AppManagement;
using UMUNA.Data;
using UnityEngine;

namespace UMUNA.SavingSystem
{
    public class SaveLoadSystem
    {
        #region Fields
        private IDataService<UmunaData> _umunaDataService;
        private IDataService<ExportNotes> _exportNotesDataService;
        private AppManager _appManager;
        private ExportNotes _exportNotes;
        #endregion

        #region Properties
        public IDataService<UmunaData> UmunaDataService => _umunaDataService;
        public IDataService<ExportNotes> ExportNotesDataService => _exportNotesDataService;

        public AppManager AppManager => _appManager;
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
        public SaveLoadSystem(AppManager appManager, UmunaData umunaData, ExportNotes exportNotes, SerializerType serializerType = SerializerType.Json)
        {
            _appManager = appManager;
            _exportNotes = exportNotes;
            _umunaDataService = DataServiceFactory.Create<UmunaData>(serializerType, nameof(UmunaData));
            _exportNotesDataService = DataServiceFactory.Create<ExportNotes>(serializerType, nameof(ExportNotes));
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

        public void Delete(string name)
        {
            _umunaDataService.Delete(name);
            _exportNotesDataService.Delete(name);
        }
        public void DeleteAll()
        {
            _umunaDataService.DeleteAll();
            _exportNotesDataService.DeleteAll();
        }
        #endregion

        #region Private Methods
        private void SaveData()
        {
            Thread.Sleep(500);
            UmunaDataService.Save(AppManager.UmunaData);
            ExportNotesDataService.Save(ExportNotes);
            Debug.Log("Saved data");
        }

        private void LoadData()
        {
            AppManager.UmunaData = UmunaDataService.Load();
            Debug.Log("Loaded data"); // Dont forget to add Logging at a later point
        }
        #endregion
    }
}
