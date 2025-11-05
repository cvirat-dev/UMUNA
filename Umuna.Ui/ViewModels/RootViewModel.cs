using CommunityToolkit.Mvvm.ComponentModel;

namespace Umuna.Ui.ViewModels
{
    public partial class RootViewModel : ObservableObject
    {
        private readonly MainViewModel _mainViewModel;
        private readonly LoginViewModel _loginViewModel;

        [ObservableProperty]
        private object? currentViewModel;

        public RootViewModel(MainViewModel mainViewModel, LoginViewModel loginViewModel)
        {
            _mainViewModel = mainViewModel;
            _loginViewModel = loginViewModel;

            _loginViewModel.LoginSucceeded += (_, __) => CurrentViewModel = _mainViewModel;

            // Show Login first
            CurrentViewModel = _loginViewModel;
        }
    }
}