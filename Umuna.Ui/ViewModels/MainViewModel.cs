using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Umuna.Ui.Infrastructure.Logging;
using Umuna.Ui.Models;
using Umuna.Ui.Services.Communication;

namespace Umuna.Ui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region Fields
        private readonly ILogger<MainViewModel> _logger;
        private readonly ICommunicationService _communicationService;

        [ObservableProperty]
        private string _status = "Disconnected";

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _log = "Server starting...";

        [ObservableProperty]
        private bool _isHostRunning;
        #endregion

        #region Events
        public event Action? RequestStartHost;
        public event Action? RequestStopHost;
        #endregion

        #region Properties
        public string ExecutablePath { get; }
        #endregion

        #region Constructors
        public MainViewModel(ILogger<MainViewModel> logger, ICommunicationService communicationService, AppConfig config)
        {
            _logger = logger;
            _communicationService = communicationService;
            _communicationService.MessageReceived += msg => Log += $"\nClient: {msg}";
            ExecutablePath = config.ExternalAppHost.ExecutablePath;
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async Task ConnectAsync()
        {
            _logger.Info("{Command} invoked", nameof(ConnectAsync));
            try
            {
                IsBusy = true;
                Status = "Connecting...";

                await _communicationService.StartAsync();
                Log += "\nServer listening on port 5000...";

                Status = "Connected successfully!";
            }
            catch (Exception ex)
            {
                Status = $"Connection failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DisconnectAsync()
        {
            _logger.Info("{Command} invoked", nameof(DisconnectAsync));
            try
            {
                IsBusy = true;
                Status = "Disconnecting...";
                await _communicationService.StopAsync();
                Log += "\nServer stopped.";
                Status = "Disconnected";
            }
            catch (Exception ex)
            {

                Status = $"Disconnection failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task StartHost()
        {
            _logger.Info("{Command} invoked", nameof(StartHost));

            // Early exit if host is already running
            if (IsHostRunning)
            {
                _logger.LogWarningWithCaller("Host is already running. StartHost command will not proceed.");
                return;
            }

            RequestStartHost?.Invoke();
        }

        [RelayCommand]
        private async Task StopHost()
        {
            _logger.Info("{Command} invoked", nameof(StopHost));

            // Early exit if host is not running
            if (!IsHostRunning)
            {
                _logger.LogWarningWithCaller("Host is not running. StopHost command will not proceed.");
                return;
            }

            RequestStopHost?.Invoke();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Disconnect the Socket, Stop the Host, and prepare for log off.
        /// </summary>
        /// <returns></returns>
        public async Task PrepareLogOff()
        {
            _logger.Info("{Method} invoked", nameof(PrepareLogOff));
            try
            {
                IsBusy = true;
                // Disconnect communication service
                await DisconnectAsync();
                // Stop host if running
                if (IsHostRunning)
                {
                    RequestStopHost?.Invoke();
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorWithCaller(ex, "Error during {Method}", nameof(PrepareLogOff));
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
