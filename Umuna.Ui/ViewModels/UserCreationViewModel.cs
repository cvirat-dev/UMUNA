using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Umuna.Ui.Constants;
using Umuna.Ui.Models;

namespace Umuna.Ui.ViewModels
{
    // User creation logic ViewModel without source generator attributes (manual properties)
    public class UserCreationViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        private string _userName = string.Empty;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private string _successMessage = string.Empty;
        public string SuccessMessage
        {
            get => _successMessage;
            set => SetProperty(ref _successMessage, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public IRelayCommand CreateUserCommand { get; }
        public IRelayCommand NavigateToLoginCommand { get; }

        // Event raised when navigation to login view is requested
        public event EventHandler? NavigationToLoginRequested;

        public UserCreationViewModel(AppConfig appConfig)
        {
            AppConfig config = appConfig;
            string baseUrl = config.Backend.BaseUrl?.TrimEnd('/') + "/";
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl!) };

            CreateUserCommand = new AsyncRelayCommand(CreateUserAsync, () => !IsBusy);
            NavigateToLoginCommand = new RelayCommand(NavigateToLoginView);
        }

        private void NavigateToLoginView()
        {
            // Raise event to request navigation to login view
            NavigationToLoginRequested?.Invoke(this, EventArgs.Empty);
        }

        private async Task CreateUserAsync()
         {
             ErrorMessage = string.Empty;
             SuccessMessage = string.Empty;

             if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
             {
                 ErrorMessage = "All fields are required.";
                 return;
             }

             try
             {
                 IsBusy = true;
                 (CreateUserCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();
                 var payload = new { Name = UserName, Email, Password };
                 var json = JsonSerializer.Serialize(payload);
                 using var content = new StringContent(json, Encoding.UTF8, "application/json");
                 using var response = await _httpClient.PostAsync(ApiRoutes.AddUser, content);

                 if (response.IsSuccessStatusCode)
                 {
                     SuccessMessage = "User created successfully.";
                     Password = string.Empty; // clear password field
                     return;
                 }

                 var error = await response.Content.ReadAsStringAsync();
                 ErrorMessage = string.IsNullOrWhiteSpace(error) ? "Failed to create user." : error;
             }
             catch (HttpRequestException ex)
             {
                ErrorMessage = $"Network error: {ex.Message}";
             }
             catch (System.Exception ex)
             {
                ErrorMessage = $"Unexpected error: {ex.Message}";
             }
             finally
             {
                IsBusy = false;
                (CreateUserCommand as AsyncRelayCommand)?.NotifyCanExecuteChanged();

                NavigateToLoginView();
            }
         }
     }
}
