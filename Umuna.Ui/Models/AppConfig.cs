
namespace Umuna.Ui.Models
{
    public class AppConfig
    {
        public string MainWindowTitle { get; set; } = "Umuna Application";
        public Backend Backend { get; set; } = new();
        public TcpService TcpService { get; set; } = new();
        public ExternalAppHost ExternalAppHost { get; set; } = new();
    }

    public class Backend
    {
        public string BaseUrl { get; set; } = "http://localhost:5000";
    }
    public class TcpService
    {
        public int Port { get; set; } = 5000;
    }

    public class ExternalAppHost
    {
        public string ExecutablePath { get; set; } = @"C:\Path\To\ExternalApp.exe";
    }
}
