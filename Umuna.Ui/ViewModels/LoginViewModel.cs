using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows.Controls;
using Umuna.Core.Contracts.Api.Requests;
using Umuna.Ui.Constants;
using Umuna.Ui.Infrastructure.Logging;
using Umuna.Ui.Infrastructure.Services;
using Umuna.Ui.Models;

namespace Umuna.Ui.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly ILogger<LoginViewModel> _logger;
        private readonly IErrorDialogService _errorDialogService;
        private readonly AppConfig _config;
        private CancellationTokenSource? _loginCts;
        #endregion

        #region Properties
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string userName = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;
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
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(IsBusy))
            {
                CancelCommand.NotifyCanExecuteChanged();
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync(PasswordBox? passwordBox)
        {
            _logger.Info("{Command} invoked", nameof(LoginAsync));
            ErrorMessage = string.Empty;
            string password = passwordBox?.Password ?? string.Empty;

            // Cancel any previous pending login
            _loginCts?.Cancel();
            _loginCts?.Dispose();
            _loginCts = new CancellationTokenSource();
            CancellationToken ct = _loginCts.Token;

            try
            {
                IsBusy = true;

                LoginRequestDto payload = new() { UserName = UserName, Password = password };
                using var content = new StringContent(
                    JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await _httpClient.PostAsync(
                    ApiRoutes.AuthLogin,
                    content,
                    ct);

                if (response.IsSuccessStatusCode)
                {
                    StatusMessage = "Login successful";
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);
                    return;
                }

                string error = await response.Content.ReadAsStringAsync(ct);
                ErrorMessage = string.IsNullOrWhiteSpace(error)
                    ? $"Login failed: {(int)response.StatusCode} {response.ReasonPhrase}"
                    : error;
            }
            catch (OperationCanceledException)
            {
                if (_loginCts?.IsCancellationRequested == true)
                {
                    ErrorMessage = "Login canceled.";
                    StatusMessage = "Login canceled";
                    _logger.Info("Login request canceled by user.");
                }
            }
            catch (HttpRequestException ex)
            {
                StatusMessage = "Connection failed";
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
                StatusMessage = string.Empty;
                _loginCts?.Dispose();
                _loginCts = null;
            }
        }

        [RelayCommand(CanExecute = nameof(CanCancel))]
        private void Cancel()
        {
            _logger.Info("{Command} invoked", nameof(Cancel));
            if (_loginCts != null && !_loginCts.IsCancellationRequested)
            {
                _loginCts.Cancel();
            }
        }
        #endregion

        #region Private Methods
        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(UserName) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !IsBusy;
        }

        private bool CanCancel()
        {
            return IsBusy;
        }
        #endregion
    }
}