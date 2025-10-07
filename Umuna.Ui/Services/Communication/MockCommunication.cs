using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Umuna.Ui.Services.Communication
{
    public class MockCommunicationService : ICommunicationService
    {
        public event Action<string>? MessageReceived;

        public Task StartAsync(int port, CancellationToken cancellationToken = default)
        {
            MessageReceived?.Invoke("Mock communication started.");
            return Task.CompletedTask;
        }

        public Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            MessageReceived?.Invoke($"Mock sent: {message}");
            return Task.CompletedTask;
        }
    }
}
