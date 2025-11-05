using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Umuna.Ui.Models.Root;

namespace Umuna.Ui.ViewModels
{
    public partial class RootViewModel : ObservableObject
    {
        #region Fields
        private readonly MainViewModel _mainViewModel;
        private readonly LoginViewModel _loginViewModel;
        private RootData _rootData = new();
        #endregion

        #region Properties
        [ObservableProperty]
        private object? currentViewModel;

        [ObservableProperty]
        private string? currentUser;
        partial void OnCurrentUserChanged(string? value)
        {
            _rootData.User.PlayerName = value ?? string.Empty;
        }
        #endregion

        #region Constructors
        public RootViewModel(MainViewModel mainViewModel, LoginViewModel loginViewModel)
        {
            _mainViewModel = mainViewModel;
            _loginViewModel = loginViewModel;

            _loginViewModel.LoginSucceeded += OnLoginSucceeded;

            // Show Login first
            CurrentViewModel = _loginViewModel;
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async Task LogoutAsync()
        {
            try
            {
                // Try to gracefully disconnect if possible
                await _mainViewModel.DisconnectCommand.ExecuteAsync(null);
            }
            catch
            {
                // Ignore disconnect errors on logout
            }
            finally
            {
                // Reset UI state and go back to login
                CurrentUser = null;
                _loginViewModel.ErrorMessage = string.Empty;
                _loginViewModel.IsBusy = false;
                // Optionally clear remembered username:
                // _loginViewModel.UserName = string.Empty;

                CurrentViewModel = _loginViewModel;
            }
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void OnLoginSucceeded(object? sender, EventArgs e)
        {
            CurrentUser = _loginViewModel.UserName;
            CurrentViewModel = _mainViewModel;
        }
        #endregion

    }
}