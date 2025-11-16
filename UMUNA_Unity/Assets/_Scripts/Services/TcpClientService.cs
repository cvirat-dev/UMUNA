using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Umuna.Core.Services.Serialization;
using UnityEngine;

namespace UMUNA.Assets._Scripts.Services
{
    /// <summary>
    /// A simple TCP client service for sending and receiving JSON messages.
    /// - Uses <see cref="TcpClient"/> to connect to a TCP server.
    /// - Specifies the server's IP and port via <see cref="NetworkConfiguration"/>.
    /// - Initiates connection with <c>ConnectAsync()</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TcpClientService<T> : ITcpClientService<T> where T : class
    {
        #region Fields
        private readonly TcpClient _client;
        private NetworkStream _stream;
        private readonly string _serverIp;
        private readonly int _serverPort;
        private readonly ISerializer<T> _serializer;
        #endregion

        #region Constructors
        public TcpClientService(ISerializer<T> serializer, AppConfiguration appConfig)
        {
            _serverIp = appConfig.NetworkConfiguration.TcpConfig.ServerIp;
            _serverPort = appConfig.NetworkConfiguration.TcpConfig.Port;
            _client = new TcpClient();
            _serializer = serializer;
        }
        #endregion

        #region Public Methods
        public async Task ConnectAsync()
        {
            try
            {
                await _client.ConnectAsync(_serverIp, _serverPort);
                _stream = _client.GetStream();

                Debug.Log($"[TCP] Connected to {_serverIp}:{_serverPort}");

                // (Optional) Start listening in the background
                _ = Task.Run(() => ListenForMessagesAsync());
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[TCP] Connection failed: {ex.Message}");
            }
        }

        public async Task SendJsonAsync(T data)
        {
            if (_stream == null)
            {
                Debug.LogWarning("[TCP] Not connected.");
                return;
            }
            var json = _serializer.Serialize(data);
            byte[] bytes = Encoding.UTF8.GetBytes(json + "\n");

            await _stream.WriteAsync(bytes, 0, bytes.Length);
            await _stream.FlushAsync();

            Debug.Log($"[TCP] Sent: {json}");
        }

        public void Disconnect()
        {
            if (_client.Connected)
            {
                _stream.Close();
                _client.Close();
                Debug.Log("[TCP] Disconnected.");
            }
        }
        #endregion

        #region Private Methods
        private async void ListenForMessagesAsync()
        {
            var buffer = new byte[1024];
            try
            {
                while (_client.Connected)
                {
                    int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log($"[TCP] Received: {msg}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[TCP] Disconnected: {ex.Message}");
            }
        }
        #endregion
    }
}
