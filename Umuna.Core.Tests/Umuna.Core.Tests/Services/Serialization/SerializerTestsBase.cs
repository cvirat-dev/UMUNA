using Umuna.Core.Services;
using Umuna.Core.Services.Serialization;
using Umuna.Core.Tests.Mocking;
using Umuna.Core.Tests.TestHelpers;

namespace Umuna.Core.Tests.Services.Serialization
{
    [TestClass]
    public abstract class SerializerTestsBase
    {
        public required ISerializer<MockData> serializer;

        public abstract void Setup();

        [TestMethod]
        public void Deserialize_ValidJsonString_ReturnsCorrectObject()
        {
            // Act
            string? content = serializer.Serialize(MockFactory.GetMockData());

            // Assert
            Assert.IsNotNull(content);
            Assert.IsNotNull(serializer.Deserialize(content));
        }

        [TestMethod]
        public void Serialize_ValidData_ReturnsValidJsonString()
        {
            // Act
            string? content = serializer.Serialize(MockFactory.GetMockData());

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(content));
            foreach (string substring in MockFactory.GetSampleValues())
            {
                StringAssert.Contains(content, substring);
            }
        }

        [TestMethod]
        public void Serialize_ValidData_ProducesDeserializableOutput()
        {
            // Arrange
            var original = MockFactory.GetMockData();

            // Act
            string? json = serializer.Serialize(original);
            var deserialized = serializer.Deserialize(json);

            // Assert
            Assert.IsNotNull(deserialized);
            Assert.AreEqual(original.users[0].id, deserialized.users[0].id);
            Assert.AreEqual(original.users[0].name, deserialized.users[0].name);
            Assert.AreEqual(original.users[0].email, deserialized.users[0].email);
            Assert.AreEqual(original.products[0].price, deserialized.products[0].price);
            Assert.AreEqual(original.metadata.version, deserialized.metadata.version);
        }

        [TestMethod]
        public void Serialize_NullObject_ReturnsNull()
        {
            // Act
            string? result = serializer.Serialize(null);

            // Assert
            Assert.AreEqual(Constants.NULL, result);
        }
    }
}