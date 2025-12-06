using Umuna.Core.Services.Serialization.Xml;
using Umuna.Core.Tests.Mocking;

namespace Umuna.Core.Tests.Services.Serialization
{
    [TestClass]
    public class XmlSerializerTests : SerializerTestsBase
    {
        [TestInitialize]
        public override void Setup()
        {
            serializer = new XmlSerializer<MockData>();
        }
    }
}
