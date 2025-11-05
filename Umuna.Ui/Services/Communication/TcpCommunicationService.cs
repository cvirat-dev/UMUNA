using System.Net;
using System.Net.Sockets;
using System.Text;
using Umuna.Core.Services.FileDataService;
using Umuna.Ui.Models;

namespace Umuna.Ui.Services.Communication
{
    /// <summary>
    /// A simple TCP communication service for sending and receiving messages.
    /// This is the server side: 
    /// - It uses <see cref="TcpListener"/> to listen for incoming connections.
    /// - It calls <c>AcceptTcpClientAsync()</c> to accept a client connection.
    /// - Can handle multple clients if needed.
    /// </summary>
    class TcpCommunicationService(IFileSerializer<AppConfig> fileSerializer) : ICommunicationService
    {
        #region Fields
        private TcpListener? _listener;
        private TcpClient? _client;
        private AppConfig _config = fileSerializer.Load() ?? new AppConfig();
        #endregion

        #region Events
        public event Action<string>? MessageReceived;
        #endregion

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            _listener = new TcpListener(IPAddress.Loopback, _config.TcpService.Port);
            _listener.Start();

            // Start accepting in background — don't await here
            _ = Task.Run(async () =>
            {
                try
                {
                    _client = await _listener.AcceptTcpClientAsync(cancellationToken);
                    _ = ListenForMessagesAsync(_client, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[TCP] Listener stopped: {ex.Message}");
                }
            }, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Stop accepting new clients
                _listener?.Stop();
                _listener = null;

                // Close client connection
                if (_client != null)
                {
                    if (_client.Connected)
                    {
                        _client.GetStream().Close();
                    }
                    _client.Close();
                    _client = null;
                }

                // Give a small grace delay if background tasks are still cleaning up
                await Task.Delay(100, cancellationToken);

                Console.WriteLine("[TCP] Server stopped gracefully.");
            }
            catch (OperationCanceledException)
            {
                // Normal on shutdown
            }
            catch (SocketException)
            {
                // Normal on shutdown (listener stopped)
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TCP] Error while stopping server: {ex.Message}");
            }
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            if (_client?.Connected != true)
                return; // Nothing to send if no client is connected

            NetworkStream stream = _client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            await stream.WriteAsync(data, cancellationToken);
        }

        private async Task ListenForMessagesAsync(TcpClient client, CancellationToken cancellationToken)
        {
            NetworkStream stream = client.GetStream();
            var buffer = new byte[1024];

            while (!cancellationToken.IsCancellationRequested)
            {
                int bytesRead = await stream.ReadAsync(buffer, cancellationToken);
                if (bytesRead <= 0)
                    break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                MessageReceived?.Invoke(message);
            }
        }
    }
}
