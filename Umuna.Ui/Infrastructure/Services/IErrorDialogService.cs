
namespace Umuna.Ui.Infrastructure.Services
{
    public interface IErrorDialogService
    {
        Task ShowErrorAsync(Exception exception, string userMessage = null);
        Task ShowErrorAsync(string title, string message, Exception exception = null);
        Task<bool> ShowErrorWithRetryAsync(Exception exception, string userMessage = null);
    }
}
