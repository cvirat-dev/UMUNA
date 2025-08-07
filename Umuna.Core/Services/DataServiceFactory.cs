using Umuna.Core.Services.FileDataService;
using Umuna.Core.Services.Serialization;
using Umuna.Core.Services.Serialization.Json;
using Umuna.Core.Services.Serialization.Xml;

namespace Umuna.Core.Services
{
    public static class DataServiceFactory
    {
        public static IDataService<TData> Create<TData>(SerializerType serializerType) where TData : class
        {
            switch (serializerType)
            {
                case SerializerType.Json:
                    var jsonSerializer = new JsonSerializer<TData>();
                    return new FileLoaderService<JsonSerializer<TData>, TData>(jsonSerializer);
                case SerializerType.Xml:
                    var xmlSerializer = new XmlSerializer<TData>();
                    return new FileLoaderService<XmlSerializer<TData>, TData>(xmlSerializer);
                default:
                    throw new System.NotImplementedException($"Serializer type {serializerType} is not implemented.");
            }
        }

        public static IDataService<TData> Create<TData>(SerializerType serializerType, string fileName) where TData : class
        {
            var dataService = Create<TData>(serializerType);
            dataService.FileName = fileName;
            return dataService;
        }
    }
}