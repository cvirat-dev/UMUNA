using Newtonsoft.Json;
using System;
using System.IO;
using Umuna.Core.Services.Serialization;

namespace Umuna.Core.Services.FileDataService
{
    public class FileSerializer<TSerializer, TData> : IFileSerializer<TData>
        where TSerializer : class, ISerializer<TData>, new()
        where TData : class
    {
        protected TSerializer _serializer = new TSerializer();
        protected FileDescriptor _fileDescriptor;

        public TSerializer Serializer
        {
            get => _serializer;
            set => _serializer = value;
        }

        public string FileDirectory => _fileDescriptor.FileDirectory;
        public string FileExtension => _fileDescriptor.FileExtension;
        public string FileName => _fileDescriptor.FileName;
        public string FilePath => _fileDescriptor.FilePath;

        public FileSerializer(TSerializer serializer, FileDescriptor fileDescriptor)
        {
            Serializer = serializer;
            _fileDescriptor = fileDescriptor;
        }

        public FileSerializer(TSerializer serializer, string filePath)
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));

            // Expand environment variables
            filePath = Environment.ExpandEnvironmentVariables(filePath);

            // Ensure correct extension first
            string expectedExt = "." + serializer.GetExtension();
            string actualExt = Path.GetExtension(filePath);

            if (string.IsNullOrEmpty(actualExt))
            {
                filePath += expectedExt;
            }
            else if (!actualExt.Equals(expectedExt, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidDataException(
                    $"File extension '{actualExt}' does not match serializer's expected extension '{expectedExt}'.");
            }

            // Now determine directory safely
            string? directory = Path.GetDirectoryName(filePath);
            if (string.IsNullOrEmpty(directory))
                throw new ArgumentException("File path must include a directory.", nameof(filePath));

            // Create directory if needed
            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to create directory '{directory}'.", ex);
            }

            _fileDescriptor = FileDescriptor.FromPath(filePath);
        }

        public void Delete()
        {
            File.Delete(FilePath);
        }

        public void Delete(string name)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            else
            {
                throw new FileNotFoundException($"File '{FilePath}' not found.");
            }
        }

        public TData? Load()
        {
            try
            {
                if (!File.Exists(FilePath))
                    return null;

                var fileContent = File.ReadAllText(FilePath);
                return _serializer.Deserialize(fileContent);
            }
            catch (DirectoryNotFoundException)
            {
                return null;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (JsonException)
            {
                // Invalid JSON -> treat as no config
                return null;
            }
        }

        public TData? Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File '{path}' not found.");
            }
            var fileContent = File.ReadAllText(path);
            try
            {
                return _serializer.Deserialize(fileContent);
            }
            catch (JsonException) // Catches JsonReaderException and other JSON-related exceptions
            {
                // You might want to log the error here
                return null;
            }
        }

        public void Save(TData data, bool overWrite = true)
        {
            if (data == null)
            {
                throw new System.ArgumentNullException(nameof(data), "Data cannot be null.");
            }

            // Ensure target directory exists
            Directory.CreateDirectory(FileDirectory);

            if (!overWrite && File.Exists(FilePath))
            {
                throw new IOException($"File '{FilePath}' already exists and overwrite is not allowed.");
            }
            File.WriteAllText(FilePath, Serializer.Serialize(data));
        }

        public void OpenInExplorer()
        {
            if (!Directory.Exists(FileDirectory))
            {
                throw new DirectoryNotFoundException($"Directory '{FileDirectory}' not found.");
            }
            // Open the export directory in the file explorer
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "explorer",
                Arguments = FileDirectory,
                UseShellExecute = true
            });
        }

        public string? GetSerializedData(TData data)
        {
            if (data == null)
            {
                throw new System.ArgumentNullException(nameof(data), "Data cannot be null.");
            }
            try
            {
                VerifyExtension();
                return _serializer.Serialize(data);
            }
            catch (JsonException) // Catches JsonReaderException and other JSON-related exceptions
            {
                // You might want to log the error here
                return null;
            }
        }

        public void VerifyExtension()
        {
            if (string.Compare(FileExtension, _serializer.GetExtension(), System.StringComparison.OrdinalIgnoreCase) != 0)
            {
                throw new InvalidDataException($"File extension '{FileExtension}' does not match serializer's expected extension '{_serializer.GetExtension()}'.");
            }
        }

        // Helper methods
        public FileSerializer<TSerializer, TData> WithNewDirectory(string newDirectory) =>
            new FileSerializer<TSerializer, TData>(_serializer, _fileDescriptor.WithNewDirectory(newDirectory));

        public FileSerializer<TSerializer, TData> WithNewName(string newName) =>
            new FileSerializer<TSerializer, TData>(_serializer, _fileDescriptor.WithNewName(newName));

        public FileSerializer<TSerializer, TData> WithNewExtension(string newExtension) =>
            new FileSerializer<TSerializer, TData>(_serializer, _fileDescriptor.WithNewExtension(newExtension));
    }
}
