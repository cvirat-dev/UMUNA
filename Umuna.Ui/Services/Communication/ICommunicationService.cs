using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umuna.Ui.Services.Communication
{
    public interface ICommunicationService
    {
        Task StartAsync(int port, CancellationToken cancellationToken = default);
        Task SendAsync(string message, CancellationToken cancellationToken = default);
        event Action<string>? MessageReceived;
    }
}
