using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Controls;
using Umuna.Ui.Constants;
using Umuna.Ui.Infrastructure.Logging;
using Umuna.Ui.Infrastructure.Services;
using Umuna.Ui.Models;

namespace Umuna.Ui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginViewModel> _logger;
        private readonly IErrorDialogService _errorDialogService;
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
        public LoginViewModel(
            ILogger<LoginViewModel>     logger,
            IErrorDialogService         errorDialogService,
            AppConfig                   appConfig)
        {
            _logger = logger;
            _errorDialogService = errorDialogService;
            // Load configuration and prepare HttpClient with the configured BaseUrl
            _config = appConfig;
            string baseUrl = _config.Backend.BaseUrl?.TrimEnd('/') + "/";
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl!) };
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async Task LoginAsync(PasswordBox? passwordBox)
        {
            _logger.Info("{Command} invoked", nameof(LoginAsync));
            ErrorMessage = string.Empty;
            string password = passwordBox?.Password ?? string.Empty;

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

                using HttpResponseMessage response = await _httpClient.PostAsync(ApiRoutes.AuthLogin, content);

                if (response.IsSuccessStatusCode)
                {
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);
                    return;
                }

                string error = await response.Content.ReadAsStringAsync();
                ErrorMessage = string.IsNullOrWhiteSpace(error)
                    ? $"Login failed: {(int)response.StatusCode} {response.ReasonPhrase}"
                    : error;
            }
            catch (HttpRequestException ex)
            {
                await _errorDialogService.ShowErrorAsync(
                    ex,
                    "Unable to reach the server"
                );
            }
            catch (Exception ex)
            {
                await _errorDialogService.ShowErrorAsync(
                    ex,
                    "An unexpected error occurred during login"
                );
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}