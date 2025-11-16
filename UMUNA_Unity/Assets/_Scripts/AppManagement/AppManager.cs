
using UMUNA.Assets._Scripts.Services;
using UMUNA.SavingSystem;
using System.Threading.Tasks;
using Umuna.Core.Communication.Contracts;

namespace UMUNA.AppManagement
{
    /// <summary>
    /// - No configuration loading
    /// - No manual service creation
    /// - No MonoBehaviour inheritance
    /// - Completely DI-constructed
    /// </summary>
    public class AppManager
    {
        private readonly ISaveLoadSystem saveLoadSystem;
        private readonly ITcpClientService<MessageDto> tcpClientService;

        #region Properties
        public ISaveLoadSystem SaveLoadSystem => saveLoadSystem;
        public ITcpClientService<MessageDto> TcpClientService => tcpClientService;
        #endregion

        public AppManager(
            ISaveLoadSystem                 saveLoadSystem,
            ITcpClientService<MessageDto>   tcpClientService)
        {
            this.saveLoadSystem = saveLoadSystem;
            this.tcpClientService = tcpClientService;
        }

        public async Task Initialize()
        {
            saveLoadSystem.Load();
            await tcpClientService.ConnectAsync();

            await tcpClientService.SendJsonAsync(new MessageDto
            {
                MessageType = MessageType.Info,
                Sender = "UnityApp",
                Payload = new { Content = "Unity App Connected" }
            });
        }
    }
}
