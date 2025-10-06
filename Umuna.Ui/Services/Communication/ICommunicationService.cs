using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umuna.Ui.Services.Communication
{
    public interface ICommunicationService
    {
        Task ConnectAsync(string host, int port);
        Task SendMessageAsync(string message);
        event Action<string>? MessageReceived;
    }
}
