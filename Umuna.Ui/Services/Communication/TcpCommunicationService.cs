using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Umuna.Core.Services.FileDataService;
using Umuna.Ui.Infrastructure.Logging;
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
    class TcpCommunicationService(ILogger<TcpCommunicationService> logger, IFileSerializer<AppConfig> fileSerializer) : ICommunicationService
    {
        #region Fields
        private TcpListener? _listener;
        private TcpClient? _client;
        private AppConfig _config = fileSerializer.Load() ?? new AppConfig();
        private readonly ILogger<TcpCommunicationService> _logger = logger;
        #endregion

        #region Events
        public event Action<string>? MessageReceived;
        #endregion

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (_listener != null)
            {
                _logger.Warning("TCP server already started on port {Port}.", _config.TcpService.Port);
                return;
            }

            _logger.Info("Starting TCP server on {Address}:{Port}...", IPAddress.Loopback, _config.TcpService.Port);

            _listener = new TcpListener(IPAddress.Loopback, _config.TcpService.Port);
            _listener.Start();
            _logger.Info("TCP listener started and awaiting client connections.");

            // Start accepting in background — don't await here
            _ = Task.Run(async () =>
            {
                try
                {
                    _logger.Debug("Waiting for TCP client to connect...");
                    _client = await _listener.AcceptTcpClientAsync(cancellationToken);
                    _logger.LogInformation("TCP client connected from {RemoteEndPoint}.", _client.Client.RemoteEndPoint);
                    _ = ListenForMessagesAsync(_client, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.Info("TCP accept task cancelled.");
                }
                catch (ObjectDisposedException)
                {
                    // Listener disposed during shutdown
                    _logger.Debug("TCP listener disposed while waiting for a client.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "TCP listener stopped due to unexpected error.");
                }
            }, cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            _logger.Info("Stopping TCP server...");
            try
            {
                // Stop accepting new clients
                if (_listener != null)
                {
                    _listener.Stop();
                    _listener = null;
                    _logger.Info("TCP listener stopped.");
                }

                // Close client connection
                if (_client != null)
                {
                    try
                    {
                        if (_client.Connected)
                        {
                            _client.GetStream().Close();
                            _logger.Debug("TCP client stream closed.");
                        }
                        _client.Close();
                        _logger.Info("TCP client connection closed.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error while closing TCP client.");
                    }
                    finally
                    {
                        _client = null;
                    }
                }

                // Give a small grace delay if background tasks are still cleaning up
                await Task.Delay(100, cancellationToken);

                _logger.Info("TCP server stopped gracefully.");
            }
            catch (OperationCanceledException)
            {
                _logger.Info("TCP server stop operation cancelled.");
            }
            catch (SocketException ex)
            {
                _logger.LogDebug(ex, "SocketException encountered during TCP stop (likely during shutdown).");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while stopping TCP server.");
            }
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            if (_client?.Connected != true)
            {
                _logger.Warning("Send skipped: no TCP client connected.");
                return; // Nothing to send if no client is connected
            }

            try
            {
                NetworkStream stream = _client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                await stream.WriteAsync(data, cancellationToken);
                _logger.Debug("Sent TCP message ({Length} bytes): {Message}", data.Length, Truncate(message));
            }
            catch (OperationCanceledException)
            {
                _logger.Info("Send operation cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending TCP message.");
            }
        }

        private async Task ListenForMessagesAsync(TcpClient client, CancellationToken cancellationToken)
        {
            _logger.Debug("Starting to listen for TCP messages...");
            NetworkStream stream = client.GetStream();
            var buffer = new byte[1024];

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    int bytesRead = await stream.ReadAsync(buffer, cancellationToken);
                    if (bytesRead <= 0)
                    {
                        _logger.Info("TCP client disconnected.");
                        break;
                    }

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    _logger.Debug("Received TCP message ({Length} bytes): {Message}", bytesRead, Truncate(message));
                    MessageReceived?.Invoke(message);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.Info("TCP message listening cancelled.");
            }
            catch (ObjectDisposedException)
            {
                _logger.Debug("TCP stream disposed, stopping listen loop.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while listening for TCP messages.");
            }
        }

        private static string Truncate(string value, int max = 200)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= max ? value : value.Substring(0, max) + "...";
        }
    }
}
