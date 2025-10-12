using Umuna.Core.Services.FileDataService;

namespace UMUNA.Configuration
{
    public interface IConfigurator<T> where T : class
    {
        T Data { get; }
        IFileSerializer<T> DataService { get; }

        bool TryReload(out T data);
    }
}