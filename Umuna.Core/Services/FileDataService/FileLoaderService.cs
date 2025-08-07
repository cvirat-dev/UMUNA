using Newtonsoft.Json;
using System.Collections.Generic;
using Umuna.Core.Services.Serialization;

namespace Umuna.Core.Services.FileDataService
{
    public class FileLoaderService<TSerializer, TData> : IDataService<TData>
        where TSerializer : class, ISerializer<TData>, new()
        where TData : class
    {

        readonly TSerializer _serializer;
        string _exportdirPath;
        string _fileName;
        string _fileExtension;

        public TSerializer Serializer => _serializer;

        public string ExportDirPath 
        { 
            get => _exportdirPath; 
            set => _exportdirPath = value;
        }
        public string FileName 
        { 
            get => _fileName; 
            set => _fileName = value; 
        }

        public string FileExtension 
        { 
            get => _fileExtension; 
            set => _fileExtension = value; 
        }

        public bool FileExists => System.IO.File.Exists(FilePath);
        public string FilePath => System.IO.Path.Combine(_exportdirPath, $"{_fileName}.{_fileExtension}");

        public FileLoaderService(TSerializer serializer)
        {
            _serializer = serializer;

            // default data path is persistent: %USERPROFILE%\AppData\LocalLow\DefaultCompany\UMUNA
            _exportdirPath = System.IO.Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                "DefaultCompany",
                "UMUNA"
            );

            _fileName = "data";

            _fileExtension = _serializer.GetExtension();
            if (_fileExtension.StartsWith("."))
            {
                _fileExtension = _fileExtension.TrimStart('.');
            }
        }

        public FileLoaderService(TSerializer serializer, string fileName)
        {
            _serializer = serializer;
            // default data path is persistent: %USERPROFILE%\AppData\LocalLow\DefaultCompany\UMUNA
            _exportdirPath = System.IO.Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                "DefaultCompany",
                "UMUNA"
            );
            _fileName = fileName;
            _fileExtension = _serializer.GetExtension();
            if (_fileExtension.StartsWith("."))
            {
                _fileExtension = _fileExtension.TrimStart('.');
            }
        }

        public FileLoaderService(TSerializer serializer, string exportdirPath, string fileName = "data")
        {
            _serializer = serializer;
            _exportdirPath = exportdirPath;
            _fileName = fileName;
            _fileExtension = _serializer.GetExtension();
        }

        public void Delete()
        {
            System.IO.File.Delete(FilePath);
        }

        public void Delete(string name)
        {
            var filePath = System.IO.Path.Combine(_exportdirPath, $"{name}.{_fileExtension}");
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            else
            {
                throw new System.IO.FileNotFoundException($"File '{filePath}' not found.");
            }
        }

        public void DeleteAll()
        {
            var files = GetFiles();
            if (files != null)
            {
                foreach (var file in files)
                {
                    System.IO.File.Delete(file);
                }
            }
        }

        public IEnumerable<string>? GetFiles()
        {
            try
            {
                return System.IO.Directory.GetFiles(_exportdirPath, $"*.{_fileExtension}");
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                // Handle the case where the directory does not exist
                return null;
            }
        }

        public TData? Load()
        {
            if (!FileExists)
            {
                throw new System.IO.FileNotFoundException($"File '{FilePath}' not found.");
            }
            var fileContent = System.IO.File.ReadAllText(FilePath);
            return _serializer.Deserialize(fileContent);
        }

        public TData? Load(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new System.IO.FileNotFoundException($"File '{path}' not found.");
            }
            var fileContent = System.IO.File.ReadAllText(path);
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
            if (!overWrite && FileExists)
            {
                throw new System.IO.IOException($"File '{FilePath}' already exists and overwrite is not allowed.");
            }
            // Ensure the directory exists before writing the file
            if (!System.IO.Directory.Exists(_exportdirPath))
            {
                System.IO.Directory.CreateDirectory(_exportdirPath);
            }
            var serializedData = _serializer.Serialize(data);
            System.IO.File.WriteAllText(FilePath, serializedData);
        }

        public void Save(TData data, string name, bool overWrite = true)
        {
            if (data == null)
            {
                throw new System.ArgumentNullException(nameof(data), "Data cannot be null.");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            var filePath = System.IO.Path.Combine(_exportdirPath, $"{name}.{_fileExtension}");
            if (!overWrite && System.IO.File.Exists(filePath))
            {
                throw new System.IO.IOException($"File '{filePath}' already exists and overwrite is not allowed.");
            }
            // Ensure the directory exists before writing the file
            if (!System.IO.Directory.Exists(_exportdirPath))
            {
                System.IO.Directory.CreateDirectory(_exportdirPath);
            }
            var serializedData = _serializer.Serialize(data);
            System.IO.File.WriteAllText(filePath, serializedData);
        }

        public void OpenInExplorer()
        {
            if (!System.IO.Directory.Exists(_exportdirPath))
            {
                throw new System.IO.DirectoryNotFoundException($"Directory '{_exportdirPath}' not found.");
            }
            // Open the export directory in the file explorer
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "explorer",
                Arguments = _exportdirPath,
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
                return _serializer.Serialize(data);
            }
            catch (JsonException) // Catches JsonReaderException and other JSON-related exceptions
            {
                // You might want to log the error here
                return null;
            }
        }
    }
}
