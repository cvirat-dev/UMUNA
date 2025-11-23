using Microsoft.Extensions.Logging;
using System.Windows;
using Umuna.Ui.Views;

namespace Umuna.Ui.Infrastructure.Services
{
    public class ErrorDialogService(ILogger<ErrorDialogService> logger) : IErrorDialogService
    {
        private readonly ILogger<ErrorDialogService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public Task ShowErrorAsync(Exception exception, string? userMessage = null)
        {
            return ShowErrorAsync("Error", userMessage ?? "An error occurred", exception);
        }

        public async Task ShowErrorAsync(string title, string message, Exception? exception = null)
        {
            // Log the error with full details
            if (exception != null)
            {
                _logger.LogError(exception, "Error dialog shown: {Message}", message);
            }
            else
            {
                _logger.LogError("Error dialog shown: {Message}", message);
            }

            // Show dialog on UI thread
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ErrorDialog dialog = new()
                {
                    Owner = Application.Current.MainWindow,
                    ErrorTitle = title,
                    ErrorMessage = message,
                    ExceptionDetails = exception?.ToString() ?? string.Empty
                };

                dialog.ShowDialog();
            });
        }

        public async Task<bool> ShowErrorWithRetryAsync(Exception exception, string userMessage = null)
        {
            _logger.LogError(exception, "Error with retry option: {Message}", userMessage);

            return await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                ErrorDialog dialog = new()
                {
                    Owner = Application.Current.MainWindow,
                    ErrorTitle = "Error",
                    ErrorMessage = userMessage ?? "An error occurred. Would you like to retry?",
                    ExceptionDetails = exception?.ToString() ?? string.Empty,
                    ShowRetryButton = true
                };

                return dialog.ShowDialog() == true;
            });
        }
    }
}
