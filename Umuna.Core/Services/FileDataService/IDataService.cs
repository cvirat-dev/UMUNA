using System.Collections.Generic;

namespace Umuna.Core.Services.FileDataService
{
    public interface IDataService<T> where T : class
    {
        string ExportDirPath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get;}

        /// <summary>
        /// Saves the specified data to the underlying storage.
        /// </summary>
        /// <remarks>If <paramref name="overWrite"/> is <see langword="false"/> and the data already
        /// exists,  the method will not save the new data and will throw an exception </remarks>
        /// <param name="data">The data to be saved. Cannot be null.</param>
        /// <param name="overWrite">A value indicating whether to overwrite existing data if it already exists. <see langword="true"/> to
        /// overwrite; otherwise, <see langword="false"/>.</param>
        void Save(T data, bool overWrite = true);

        /// <summary>
        /// Saves the specified data to a storage medium with the given name.
        /// </summary>
        /// <param name="data">The data to be saved. Cannot be null.</param>
        /// <param name="name">The name under which the data will be saved. Cannot be null or empty.</param>
        /// <param name="overWrite">A value indicating whether to overwrite existing data with the same name. <see langword="true"/> to
        /// overwrite existing data; otherwise, <see langword="false"/>.</param>
        void Save(T data, string name, bool overWrite = true);

        /// <summary>
        /// Serializes the specified data object into a string representation and returns it.
        /// </summary>
        /// <remarks>The exact format of the serialized string depends on the implementation of the
        /// method.  Ensure that the type <typeparamref name="T"/> is supported by the serialization logic.</remarks>
        /// <param name="data">The data object to serialize. This must be a valid instance of type <typeparamref name="T"/>.</param>
        /// <returns>A string containing the serialized representation of the <paramref name="data"/> object,  or <see
        /// langword="null"/> if the serialization fails or the input is <see langword="null"/>.</returns>
        string? GetSerializedData(T data);  

        /// <summary>
        /// Load the current file.
        /// </summary>
        /// <returns></returns>
        T? Load();

        /// <summary>
        /// Load a file by path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        T? Load(string path);

        /// <summary>
        /// Delete the current file.
        /// </summary>
        void Delete();

        /// <summary>
        /// Delete a file by name.
        /// </summary>
        /// <param name="name"></param>
        void Delete(string name);

        /// <summary>
        /// Delete all files in the export directory.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Get all files in the export directory.
        /// </summary>
        /// <returns></returns>
        IEnumerable<string>? GetFiles();

        /// <summary>
        /// Open the export directory in the file explorer.
        /// </summary>
        void OpenInExplorer();
    }
}
