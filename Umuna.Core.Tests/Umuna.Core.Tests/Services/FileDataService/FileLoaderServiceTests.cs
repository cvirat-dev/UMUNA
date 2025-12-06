using Microsoft.Extensions.Configuration;
using Umuna.Core.Services.FileDataService;
using Umuna.Core.Tests.Mocking;
using Umuna.Core.Tests.TestHelpers;

[assembly: DoNotParallelize]

namespace Umuna.Core.Tests.Services.FileDataService
{
    [TestClass]
    public class FileLoaderServiceTests
    {
        readonly Filedataserviceconfig _fileDataServiceConfig = DoConfiguration();
        IFileSerializer<MockData>? MockSerializer { get; set; }
        
            string[]? FilePathList { get; set; }
        string TempDir { get; set; } = string.Empty;

        [TestInitialize]
        public void Setup()
        {
            FilePathList = _fileDataServiceConfig.FilePathList;
            TempDir = _fileDataServiceConfig.TempDir;

            if (!Directory.Exists(TempDir))
                throw new DirectoryNotFoundException($"The temporary directory path '{TempDir}' does not exist.");
        }

        private static Filedataserviceconfig DoConfiguration()
        {
            var rootConfig = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var configPathValue = rootConfig["configPaths:FileDataService"];
            var configPath = configPathValue is not null
                ? Environment.ExpandEnvironmentVariables(configPathValue)
                : string.Empty;

            if (!Path.Exists(configPath))
                throw new DirectoryNotFoundException($"The configuration path '{configPath}' does not exist.");

            var config = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: false, reloadOnChange: false)
                .Build();

            Filedataserviceconfig fileDataServiceConfig = new();
            config.GetSection(nameof(Filedataserviceconfig)).Bind(fileDataServiceConfig);
            return fileDataServiceConfig;
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach(var file in Directory.GetFiles(TempDir))
            {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void Save_ValidData_CreatesFile()
        {
            // Act
            string fileLocation = Path.Combine(TempDir, nameof(Save_ValidData_CreatesFile));
            MockSerializer = FileSerializerFactory.Create<MockData>(fileLocation);
            MockSerializer.Save(new MockData());

            // Assert
            Assert.IsTrue(File.Exists(MockSerializer.FilePath));
        }

        [TestMethod]
        public void Save_FileExistsAndOverwriteFalse_ThrowsException()
        {
            // Arrange
            string filePath = Path.Combine(TempDir, nameof(Save_FileExistsAndOverwriteFalse_ThrowsException));
            MockSerializer = FileSerializerFactory.Create<MockData>(filePath);
            MockSerializer.Save(new MockData());

            // Act
            bool caughtException = false;
            try
            {
                MockSerializer.Save(new MockData(), overwrite: false);
            }
            catch (IOException)
            {
                caughtException = true;
            }

            // Assert
            Assert.IsTrue(caughtException);
        }

        [TestMethod]
        public void Load_FileExists_ReturnsData()
        {
            // Arrange
            string filePath = Path.Combine(TempDir, nameof(Load_FileExists_ReturnsData));
            MockSerializer = FileSerializerFactory.Create<MockData>(filePath);
            var data = MockFactory.GetMockData();
            MockSerializer.Save(data);

            // Act
            var loaded = MockSerializer.Load();

            // Assert
            Assert.IsNotNull(loaded);
            Assert.IsNotNull(loaded.metadata);
            Assert.AreEqual(data.metadata.version, loaded.metadata.version);
            Assert.IsTrue(File.Exists(MockSerializer.FilePath));
        }

        [TestMethod]
        public void Load_FileIsCorrupted_ReturnsNull()
        {
            // Arrange
            string filePath = Path.Combine(TempDir, nameof(Load_FileIsCorrupted_ReturnsNull));
            MockSerializer = FileSerializerFactory.Create<MockData>(filePath);

            // Write invalid/corrupted content to file
            File.WriteAllText(MockSerializer.FilePath, "this is not valid json");

            // Act
            var loaded = MockSerializer.Load();

            // Assert - corrupted content should result in null load
            Assert.IsNull(loaded);
        }
    }
}