using System;
using Umuna.Core.Services.Serialization;
using Umuna.Core.Services.Serialization.Json;
using Umuna.Core.Services.Serialization.Xml;

namespace Umuna.Core.Services.FileDataService
{
    public static class FileSerializerFactory
    {
        public static IFileSerializer<TData> Create<TData>(SerializerType serializerType = SerializerType.Json) where TData : class
        {
            switch (serializerType)
            {
                case SerializerType.Json:
                    var jsonSerializer = new JsonSerializer<TData>();
                    return new FileSerializer<JsonSerializer<TData>, TData>(jsonSerializer);
                case SerializerType.Xml:
                    var xmlSerializer = new XmlSerializer<TData>();
                    return new FileSerializer<XmlSerializer<TData>, TData>(xmlSerializer);
                default:
                    throw new System.NotImplementedException($"Serializer type {serializerType} is not implemented.");
            }
        }

        public static IFileSerializer<TData> Create<TData>(string serializerType = "json") where TData : class
        {
            return Create<TData>(Enum.Parse<SerializerType>(serializerType, true));
        }

        /// <summary>
        /// Creates a file serializer for the specified data type and file path.
        /// </summary>
        /// <typeparam name="TData">The type of data to be serialized/deserialized. Must be a class.</typeparam>
        /// <param name="path">The full path or relative path to the file. If relative, it will be relative to the main directory.</param>
        /// <param name="serializerType">The type of serializer to use (e.g., Json, Xml). Default is Json.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static IFileSerializer<TData> Create<TData>(string path, SerializerType serializerType = SerializerType.Json) where TData : class
        {
            switch (serializerType)
            {
                case SerializerType.Json:
                    var jsonSerializer = new JsonSerializer<TData>();
                    return new FileSerializer<JsonSerializer<TData>, TData>(jsonSerializer, relativePath: path);
                case SerializerType.Xml:
                    var xmlSerializer = new XmlSerializer<TData>();
                    return new FileSerializer<XmlSerializer<TData>, TData>(xmlSerializer, relativePath: path);
                default:
                    throw new System.NotImplementedException($"Serializer type {serializerType} is not implemented.");
            }
        }

        /// <summary>
        /// Creates a file serializer for the specified data type and file path.
        /// </summary>
        /// <typeparam name="TData">The type of data to be serialized/deserialized. Must be a class.</typeparam>
        /// <param name="path">The full path or relative path to the file. If relative, it will be relative to the main directory.</param>
        /// <param name="serializerType">The type of serializer to use (e.g., Json, Xml). Default is Json.</param>
        /// <returns></returns>
        public static IFileSerializer<TData> Create<TData>(string path, string serializerType = "json") where TData : class
        {
            return Create<TData>(path, Enum.Parse<SerializerType>(serializerType, true));
        }
    }
}