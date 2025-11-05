using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Controls;
using Umuna.Core.Services.FileDataService;
using Umuna.Ui.Constants;
using Umuna.Ui.Models;
using static System.Net.WebRequestMethods;

namespace Umuna.Ui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        #region Fields
        private readonly HttpClient _http;
        private readonly AppConfig _config;
        #endregion

        #region Properties
        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool isBusy;
        #endregion

        #region Events
        // Raised when login succeeds so the RootViewModel can switch to MainView
        public event EventHandler? LoginSucceeded;
        #endregion

        #region Constructors
        public LoginViewModel(IFileSerializer<AppConfig> fileSerializer)
        {
            // Load configuration and prepare HttpClient with the configured BaseUrl
            _config = fileSerializer.Load() ?? new AppConfig();

            var baseUrl = _config.Backend.BaseUrl?.TrimEnd('/') + "/";
            _http = new HttpClient { BaseAddress = new Uri(baseUrl!) };
        }
        #endregion

        #region Commands
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

                var payload = new { UserName, Password = password };
                using var content = new StringContent(
                    JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                using var response = await _http.PostAsync(ApiRoutes.AuthLogin, content);

                if (response.IsSuccessStatusCode)
                {
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);
                    return;
                }

                var error = await response.Content.ReadAsStringAsync();
                ErrorMessage = string.IsNullOrWhiteSpace(error)
                    ? $"Login failed: {(int)response.StatusCode} {response.ReasonPhrase}"
                    : error;
            }
            catch (HttpRequestException ex)
            {
                ErrorMessage = $"Unable to reach the server. Details: {ex.Message}";
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
        #endregion
    }
}