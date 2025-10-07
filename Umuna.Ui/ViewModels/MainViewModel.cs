using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Documents;
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

        #region Properties
        #endregion

        #region Constructors
        public MainViewModel(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
            _communicationService.MessageReceived +=  msg => Log += $"\nClient: {msg}";
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

                await _communicationService.StartAsync(5000);
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
        #endregion

    }
}
