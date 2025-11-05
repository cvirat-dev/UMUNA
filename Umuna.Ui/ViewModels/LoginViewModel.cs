using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;

namespace Umuna.Ui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        // Raised when login succeeds so the RootViewModel can switch to MainView
        public event EventHandler? LoginSucceeded;

        [RelayCommand]
        private async Task LoginAsync(PasswordBox? passwordBox)
        {
            ErrorMessage = string.Empty;

            var password = passwordBox?.Password ?? string.Empty;

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "Please enter both username and password.";
                return;
            }

            try
            {
                IsBusy = true;

                // Simulate authentication (replace with real call as needed)
                await Task.Delay(400);

                // Simple success rule for now: any non-empty username/password
                LoginSucceeded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}