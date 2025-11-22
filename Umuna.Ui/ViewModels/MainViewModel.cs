using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Umuna.Ui.Models;
using Umuna.Ui.Services.Communication;

namespace Umuna.Ui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        #region Fields
        private readonly ICommunicationService _communicationService;


        [ObservableProperty]
        private string status = "Disconnected";

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string log = "Server starting...";
        #endregion

        #region Events
        public event Action? RequestStartHost;
        public event Action? RequestStopHost;
        #endregion

        #region Properties
        public string ExecutablePath { get; }
        #endregion

        #region Constructors
        public MainViewModel(ICommunicationService communicationService, AppConfig config)
        {
            _communicationService = communicationService;
            _communicationService.MessageReceived += msg => Log += $"\nClient: {msg}";
            ExecutablePath = config.ExternalAppHost.ExecutablePath;
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async Task ConnectAsync()
        {
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
            RequestStartHost?.Invoke(); 
        }

        [RelayCommand]
        private async Task StopHost()
        {
            RequestStopHost?.Invoke();
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
