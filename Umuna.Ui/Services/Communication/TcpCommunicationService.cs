using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Umuna.Core.Services.FileDataService;
using Umuna.Ui.Infrastructure.Logging;
using Umuna.Ui.Models;

namespace Umuna.Ui.Services.Communication
{
    /// <summary>
    /// Simple TCP server for single-client messaging.
    /// </summary>
    class TcpCommunicationService(ILogger<TcpCommunicationService> logger, IFileSerializer<AppConfig> fileSerializer) : ICommunicationService
    {
        #region Fields
        private TcpListener? _listener;
        private TcpClient? _client;
        private AppConfig _config = fileSerializer.Load() ?? new AppConfig();
        private readonly ILogger<TcpCommunicationService> _logger = logger;
        private CancellationTokenSource? _cts; // Cancels accept + read loops.
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

            _cts = new CancellationTokenSource();
            var linkedToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _cts.Token).Token;

            _logger.Info("Starting TCP server on {Address}:{Port}...", IPAddress.Loopback, _config.TcpService.Port);

            _listener = new TcpListener(IPAddress.Loopback, _config.TcpService.Port);
            _listener.Start();
            _logger.Info("TCP listener started and awaiting client connections.");

            _ = Task.Run(async () =>
            {
                try
                {
                    _logger.Debug("Waiting for TCP client to connect...");
                    _client = await _listener.AcceptTcpClientAsync(linkedToken);
                    _logger.LogInformation("TCP client connected from {RemoteEndPoint}.", _client.Client.RemoteEndPoint);
                    _ = ListenForMessagesAsync(_client, linkedToken);
                }
                catch (OperationCanceledException)
                {
                    _logger.Info("TCP accept task cancelled.");
                }
                catch (ObjectDisposedException)
                {
                    _logger.Debug("TCP listener disposed while waiting for a client.");
                }
                catch (SocketException ex) when (ex.ErrorCode == 995 || ex.ErrorCode == 10004)
                {
                    _logger.Debug("TCP listener stopped during shutdown.");
                }
                catch (Exception ex)
                {
                    _logger.LogErrorWithCaller(ex, "TCP listener stopped due to unexpected error.");
                }
            }, linkedToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            _logger.Info("Stopping TCP server...");
            try
            {
                // Cancel background operations first
                _cts?.Cancel();

                // Stop accepting
                if (_listener != null)
                {
                    _listener.Stop();
                    _listener = null;
                    _logger.Info("TCP listener stopped.");
                }

                // Close client
                if (_client != null)
                {
                    try
                    {
                        if (_client.Connected)
                        {
                            try
                            {
                                _client.GetStream().Close();
                                _logger.Debug("TCP client stream closed.");
                            }
                            catch (Exception ex)
                            {
                                _logger.LogDebug(ex, "Stream close produced an exception (ignored).");
                            }
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
                        _client.Dispose();
                        _client = null;
                    }
                }

                await Task.Delay(50, cancellationToken);

                _logger.Info("TCP server stopped gracefully.");
            }
            catch (OperationCanceledException)
            {
                _logger.Info("TCP server stop operation cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogErrorWithCaller(ex, "Unexpected error while stopping TCP server.");
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
            }
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken = default)
        {
            if (_client?.Connected != true)
            {
                _logger.Warning("Send skipped: no TCP client connected.");
                return;
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
                _logger.LogErrorWithCaller(ex, "Error sending TCP message.");
            }
        }

        private async Task ListenForMessagesAsync(TcpClient client, CancellationToken cancellationToken)
        {
            _logger.Debug("Starting to listen for TCP messages...");
            NetworkStream stream;

            try
            {
                stream = client.GetStream();
            }
            catch (ObjectDisposedException)
            {
                _logger.Debug("Client stream already disposed before listen loop started.");
                return;
            }

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
            catch (IOException ioEx) when (ioEx.InnerException is SocketException se && (se.ErrorCode == 995 || se.ErrorCode == 10004))
            {
                // Expected if shutdown closed the socket while ReadAsync was pending
                _logger.Debug("Read aborted due to shutdown (SocketError {Code}).", se.ErrorCode);
            }
            catch (Exception ex)
            {
                _logger.LogErrorWithCaller(ex, "Error while listening for TCP messages.");
            }
        }

        private static string Truncate(string value, int max = 200)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= max ? value : value.Substring(0, max) + "...";
        }
    }
}
