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
        private readonly UserCreationViewModel _userCreationViewModel;
        private RootData _rootData = new();
        #endregion

        #region Properties
        [ObservableProperty]
        private object? currentViewModel;

        [ObservableProperty]
        private string? currentUser;
        partial void OnCurrentUserChanged(string? value)
        {
            _rootData.User.Name = value ?? string.Empty;
        }
        #endregion

        #region Constructors
        public RootViewModel(MainViewModel mainViewModel, LoginViewModel loginViewModel, UserCreationViewModel userCreationViewModel)
        {
            _mainViewModel = mainViewModel;
            _loginViewModel = loginViewModel;
            _userCreationViewModel = userCreationViewModel;

            _loginViewModel.LoginSucceeded += OnLoginSucceeded;
            _userCreationViewModel.NavigationToLoginRequested += OnNavigationToLoginRequested;

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

        [RelayCommand]
        private void ShowUserCreation()
        {
            CurrentViewModel = _userCreationViewModel;
        }

        [RelayCommand]
        private void ShowLogin()
        {
            CurrentViewModel = _loginViewModel;
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

        private void OnNavigationToLoginRequested(object? sender, EventArgs e)
        {
            // Clear any success/error messages when navigating back to login
            _userCreationViewModel.ErrorMessage = string.Empty;
            _userCreationViewModel.SuccessMessage = string.Empty;
            CurrentViewModel = _loginViewModel;
        }
        #endregion

    }
}