using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umuna.Ui.Services.Communication
{
    public class MockCommunicationService : ICommunicationService
    {
        public event Action<string>? MessageReceived;

        public Task ConnectAsync(string host, int port)
        {
            MessageReceived?.Invoke("Mock connection established.");
            return Task.CompletedTask;
        }

        public Task SendMessageAsync(string message)
        {
            MessageReceived?.Invoke($"Mock sent: {message}");
            return Task.CompletedTask;
        }
    }
}
