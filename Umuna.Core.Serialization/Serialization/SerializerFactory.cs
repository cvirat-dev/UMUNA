using System;

namespace Umuna.Core.Services.Serialization
{
    public static class SerializerFactory
    {
        public static ISerializer<TData> Create<TData>(SerializerType serializerType = SerializerType.json) where TData : class
        {
            switch (serializerType)
            {
                case SerializerType.json:
                    return new Json.JsonSerializer<TData>();
                case SerializerType.xml:
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
