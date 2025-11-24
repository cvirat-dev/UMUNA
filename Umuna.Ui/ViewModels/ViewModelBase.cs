using CommunityToolkit.Mvvm.ComponentModel;

namespace Umuna.Ui.ViewModels
{
    public abstract partial class ViewModelBase : ObservableObject, IStatusViewModel
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string statusMessage = string.Empty;
    }
}
