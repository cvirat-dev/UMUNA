
namespace Umuna.Core.Tests.Services.FileDataService
{

    public class Rootobject
    {
        public Filedataserviceconfig FileDataServiceConfig { get; set; }
    }

    public class Filedataserviceconfig
    {
        public string[] FilePathList { get; set; }
        public string TempDir { get; set; }
    }


}
