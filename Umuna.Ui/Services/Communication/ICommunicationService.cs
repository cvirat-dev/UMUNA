
namespace Umuna.Ui.Services.Communication
{
    public interface ICommunicationService
    {
        Task StartAsync(CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
        Task SendAsync(string message, CancellationToken cancellationToken = default);
        event Action<string>? MessageReceived;
    }
}
