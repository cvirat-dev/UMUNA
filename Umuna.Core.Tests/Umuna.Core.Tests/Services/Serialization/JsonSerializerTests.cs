using Umuna.Core.Data;
using Umuna.Core.Services.Serialization.Json;
using Umuna.Core.Tests.TestHelpers;

namespace Umuna.Core.Tests.Services.Serialization
{
    [TestClass]
    public class JsonSerializerTests
    {
        private JsonSerializer<UmunaData> _serializer;
        private UmunaData _testData;

        [TestInitialize]
        public void Setup()
        {
            _serializer = new JsonSerializer<UmunaData>();
            _testData = TestDataFactory.CreateTestData();
        }

        [TestMethod]
        public void Serialize_ValidData_ReturnsValidJsonString()
        {
            // Act
            string json = _serializer.Serialize(_testData);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(json));
            Assert.IsTrue(json.Contains("DemoPlayer"));
            Assert.IsTrue(json.Contains("123456789"));
            Assert.IsTrue(json.Contains("DemoGame"));
            Assert.IsTrue(json.Contains("Position1"));
            Assert.IsTrue(json.Contains("1"));
            Assert.IsTrue(json.Contains("2"));
            Assert.IsTrue(json.Contains("3"));
            Assert.IsTrue(json.Contains("0"));
            Assert.IsTrue(json.Contains("180"));
        }

        [TestMethod]
        public void Deserialize_ValidJsonString_ReturnsCorrectObject()
        {
            // Arrange
            string json = _serializer.Serialize(_testData);

            // Act
            UmunaData? result = _serializer.Deserialize(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("DemoPlayer", result.UserData.PlayerName);
            Assert.AreEqual("123456789", result.UserData.PlayerId);
            Assert.AreEqual("DemoGame", result.GameName);
            Assert.AreEqual(2, result.CameraData.SavedPositions.Count);
        }
    }
}