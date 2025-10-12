using System.Threading.Tasks;

namespace UMUNA.Assets._Scripts.Services
{
    public interface ITcpClientService<T> where T : class
    {
        Task ConnectAsync();
        void Disconnect();
        Task SendJsonAsync(T data);
    }
}