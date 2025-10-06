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
        private void Connect()
        {
            Status = "Connecting...";
        }

        #endregion

    }
}
