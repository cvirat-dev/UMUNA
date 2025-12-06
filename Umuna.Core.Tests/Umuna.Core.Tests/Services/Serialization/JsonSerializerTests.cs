using Umuna.Core.Services.Serialization.Json;
using Umuna.Core.Tests.Mocking;

namespace Umuna.Core.Tests.Services.Serialization
{
    [TestClass]
    public class JsonSerializerTests : SerializerTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            serializer = new JsonSerializer<MockData>();
        }
    }
}