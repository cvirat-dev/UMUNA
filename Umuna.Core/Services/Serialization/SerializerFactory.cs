using System;

namespace Umuna.Core.Services.Serialization
{
    public static class SerializerFactory
    {
        public static ISerializer<TData> Create<TData>(SerializerType serializerType = SerializerType.Json) where TData : class
        {
            switch (serializerType)
            {
                case SerializerType.Json:
                    return new Json.JsonSerializer<TData>();
                case SerializerType.Xml:
                    return new Xml.XmlSerializer<TData>();
                default:
                    throw new NotImplementedException($"Serializer type {serializerType} is not implemented.");
            }
        }
        public static ISerializer<TData> Create<TData>(string serializerType = "json") where TData : class
        {
            return Create<TData>(Enum.Parse<SerializerType>(serializerType, true));
        }
    }
}
