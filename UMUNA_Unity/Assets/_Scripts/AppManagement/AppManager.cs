
using System;
using System.Collections.Generic;
using Umuna.Core.SharedData;
using Umuna.Core.Services.FileDataService;
using UMUNA.Assets._Scripts.Services;
using UMUNA.Configuration;
using UMUNA.Data;
using UMUNA.SavingSystem;
using Umuna.Core.SharedData.Communication;
using Umuna.Core.Services.Serialization;
using System.Threading.Tasks;

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
        readonly UmunaData _umunaData;
        readonly ExportNotes _exportNotes;
        readonly SaveLoadSystem _saveLoadSystem;
        readonly BindSystem _bindSystem;
        readonly TcpClientService<MessageDto> _tcpClientService;
        #endregion

        #region Properties
        public SaveLoadSystem SaveLoadSystem => _saveLoadSystem;
        public UmunaData UmunaData => _umunaData;
        public BindSystem BindSystem => _bindSystem;
        public ExportNotes ExportNotes => _exportNotes;
        public TcpClientService<MessageDto> TcpClientService => _tcpClientService;
        #endregion

        #region Constructors
        public AppManager()
        {
            AppConfiguration appConfig = new Configurator<AppConfiguration>().Data;
            _umunaData = new UmunaData();
            _exportNotes = new ExportNotes();
            _bindSystem = new BindSystem();
            _saveLoadSystem = new SaveLoadSystem(UmunaData, ExportNotes, BindSystem, appConfig.FileSystemConfiguration.SerializationFormat);
            _tcpClientService = new TcpClientService<MessageDto>(
                SerializerFactory.Create<MessageDto>(appConfig.FileSystemConfiguration.SerializationFormat.Value),
                appConfig.NetworkConfiguration
            );
        }
        #endregion

        #region Methods
        internal async Task Initialize()
        {
            SaveLoadSystem.Load();
            await TcpClientService.ConnectAsync();
            await TcpClientService.SendJsonAsync(new MessageDto
            {
                MessageType = MessageType.Info,
                Sender = "UnityApp",
                Payload = new { Content = "Unity App Connected" }
            });
        }
        #endregion
    }
}
