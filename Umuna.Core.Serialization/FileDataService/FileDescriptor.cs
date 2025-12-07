using System;
using System.IO;

namespace Umuna.Core.Serialization.FileDataService
{
    public class FileDescriptor : IFileDescriptor
    {
        public string FileDirectory { get; }
        public string FileExtension { get; }
        public string FileName { get; }
        public string FilePath => Path.Combine(FileDirectory, FileName + FileExtension);

        public FileDescriptor(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
                throw new ArgumentException("Full path cannot be null or empty.", nameof(fullPath));
            var dir = Path.GetDirectoryName(fullPath) ?? string.Empty;
            var name = Path.GetFileNameWithoutExtension(fullPath) ?? string.Empty;
            var ext = Path.GetExtension(fullPath) ?? string.Empty;
            FileDirectory = dir.Trim();
            FileName = name.Trim();
            FileExtension = ext.Trim();
        }

        public FileDescriptor(string fileDirectory, string fileName, string fileExtension)
        {
            if (string.IsNullOrWhiteSpace(fileDirectory))
                throw new ArgumentException("File directory cannot be null or empty.", nameof(fileDirectory));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

            fileDirectory = fileDirectory.Trim();
            fileName = fileName.Trim();

            // Normalize extension
            fileExtension = (fileExtension ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(fileExtension) && !fileExtension.StartsWith("."))
                fileExtension = "." + fileExtension;

            FileDirectory = fileDirectory;
            FileName = fileName;
            FileExtension = fileExtension;
        }

        // Factory method from a full path
        public static FileDescriptor FromPath(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath))
                throw new ArgumentException("Path cannot be null or empty.", nameof(fullPath));

            var dir = Path.GetDirectoryName(fullPath) ?? string.Empty;
            var name = Path.GetFileNameWithoutExtension(fullPath) ?? string.Empty;
            var ext = Path.GetExtension(fullPath) ?? string.Empty;

            return new FileDescriptor(dir, name, ext);
        }

        // Helper methods
        public FileDescriptor WithNewDirectory(string newDirectory) =>
            new FileDescriptor(newDirectory, FileName, FileExtension);

        public FileDescriptor WithNewName(string newName) =>
            new FileDescriptor(FileDirectory, newName, FileExtension);

        public FileDescriptor WithNewExtension(string newExtension) =>
            new FileDescriptor(FileDirectory, FileName, newExtension);
    }
}