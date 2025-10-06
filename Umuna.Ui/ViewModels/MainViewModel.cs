using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        #endregion

        #region Constructors
        public MainViewModel()
        {
            _communicationService = new MockCommunicationService();
            _communicationService.MessageReceived += msg => status = msg;
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

                // Simulate a long operation (e.g., TCP connect, DB call)
                await Task.Delay(3000);

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
