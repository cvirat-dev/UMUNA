
using Umuna.Core.Data;
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
        UmunaData _umunaData;
        ExportNotes _exportNotes;
        SaveLoadSystem _saveLoadSystem;
        BindSystem _bindSystem;
        #endregion

        #region Properties
        public SaveLoadSystem SaveLoadSystem => _saveLoadSystem;
        public UmunaData UmunaData
        {
            get => _umunaData;
            set
            {
                _umunaData = value;
                _bindSystem.UpdateBindings(_umunaData);
            }
        }
        public BindSystem BindSystem => _bindSystem;
        public ExportNotes ExportNotes => _exportNotes;
        #endregion

        #region Constructors
        public AppManager()
        {
            _umunaData = new UmunaData();
            _exportNotes = new ExportNotes();
            _bindSystem = new BindSystem(UmunaData);
            _saveLoadSystem = new SaveLoadSystem(this, UmunaData, ExportNotes);
        }
        #endregion
    }
}
