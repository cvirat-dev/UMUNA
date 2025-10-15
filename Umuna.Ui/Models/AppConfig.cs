
namespace Umuna.Ui.Models
{
    public class AppConfig
    {
        public string MainWindowTitle { get; set; } = "Umuna Application";
        public ServerConfiguration ServerConfiguration { get; set; } = new();
    }

    public class ServerConfiguration
    {
        public int Port { get; set; } = 5000;
    }
}
