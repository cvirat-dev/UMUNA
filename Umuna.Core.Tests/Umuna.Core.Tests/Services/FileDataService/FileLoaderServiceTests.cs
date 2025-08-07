using Umuna.Core.Data;
using Umuna.Core.Services.FileDataService;
using Umuna.Core.Services.Serialization.Json;
using Umuna.Core.Tests.TestHelpers;

namespace Umuna.Core.Tests.Services.FileDataService
{
    [TestClass]
    public class FileLoaderServiceTests
    {
        private JsonSerializer<UmunaData> _serializer;
        private FileLoaderService<JsonSerializer<UmunaData>, UmunaData> _fileLoaderService;
        private UmunaData _testData;
        private List<string> _testFilePaths = new List<string>();

        [TestInitialize]
        public void Setup()
        {
            _serializer = new JsonSerializer<UmunaData>();
            _fileLoaderService = new FileLoaderService<JsonSerializer<UmunaData>, UmunaData>(_serializer, nameof(FileLoaderServiceTests));
            _fileLoaderService.ExportDirPath = Path.Combine(_fileLoaderService.ExportDirPath, "Tests");

            _testData = TestDataFactory.CreateTestData();

            // Delete all files in the export directory before each test
            PathHelpers.DeleteAllFiles(_fileLoaderService.FilePath);

            //_fileLoaderService.OpenInExplorer();
        }

        [TestCleanup]
        public void Cleanup()
        {
            foreach (var filePath in _testFilePaths)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

        [TestMethod]
        public void Save_ValidData_CreatesFile()
        {
            // Act + Assert
            _fileLoaderService.FileName = nameof(Save_ValidData_CreatesFile);
            _testFilePaths.Add(_fileLoaderService.FilePath);
            _fileLoaderService.Save(_testData, _fileLoaderService.FilePath, overWrite: true);
        }

        [TestMethod]
        public void Save_FileExistsAndOverwriteFalse_ReturnsFalse()
        {
            // Arrange
            _fileLoaderService.FileName = nameof(Save_FileExistsAndOverwriteFalse_ReturnsFalse);
            _testFilePaths.Add(_fileLoaderService.FilePath);
            bool success = true;

            // Act
            try
            {
                _fileLoaderService.Save(_testData, _fileLoaderService.FilePath, overWrite: false);
            }
            catch (IOException)
            {
                success = false;
            }

            // Assert
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Load_FileExists_ReturnsData()
        {
            // Arrange
            _fileLoaderService.FileName = nameof(Load_FileExists_ReturnsData);
            _testFilePaths.Add(_fileLoaderService.FilePath);
            _fileLoaderService.Save(_testData);

            // Act
            UmunaData? result = _fileLoaderService.Load();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("DemoPlayer", result.UserData.PlayerName);
            Assert.AreEqual("123456789", result.UserData.PlayerId);
            Assert.AreEqual("DemoGame", result.GameName);
        }

        [TestMethod]
        public void Load_FileIsCorrupted_ReturnsError()
        {
            // Arrange
            _fileLoaderService.FileName = nameof(Load_FileIsCorrupted_ReturnsError);
            _testFilePaths.Add(_fileLoaderService.FilePath);
            File.WriteAllText(_fileLoaderService.FilePath, "This is not valid JSON");
            // Act
            UmunaData? result = _fileLoaderService.Load(_fileLoaderService.FilePath);
            // Assert
            Assert.IsNull(result);
        }
    }
}