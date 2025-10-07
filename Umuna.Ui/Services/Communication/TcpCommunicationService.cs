using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Umuna.Ui.Services.Communication
{
    class TcpCommunicationService : ICommunicationService
    {
        private TcpListener? _listener;
        private TcpClient? _client;

        public event Action<string>? MessageReceived;

        public async Task StartAsync(int port, CancellationToken cancellationToken = default)
        {
            _listener = new TcpListener(IPAddress.Loopback, port);
            _listener.Start();

            _client = await _listener.AcceptTcpClientAsync(cancellationToken);
            _ = Task.Run(() => ListenForMessagesAsync(_client, cancellationToken), cancellationToken);
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            if (_client?.Connected != true)
                return; // Nothing to send if no client is connected

            var stream = _client.GetStream();
            var data = Encoding.UTF8.GetBytes(message + "\n");
            await stream.WriteAsync(data, cancellationToken);
        }

        private async Task ListenForMessagesAsync(TcpClient client, CancellationToken cancellationToken)
        {
            var stream = client.GetStream();
            var buffer = new byte[1024];

            while (!cancellationToken.IsCancellationRequested)
            {
                int bytesRead = await stream.ReadAsync(buffer, cancellationToken);
                if (bytesRead <= 0)
                    break;

                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                MessageReceived?.Invoke(message);
            }
        }
    }
}
