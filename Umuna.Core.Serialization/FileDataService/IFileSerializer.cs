namespace Umuna.Core.Serialization.FileDataService
{
    public interface IFileSerializer<T> : IFileDescriptor where T : class
    {

        /// <summary>
        /// Saves the specified data to the underlying storage.
        /// </summary>
        /// <remarks>If <paramref name="overwrite"/> is <see langword="false"/> and the data already
        /// exists,  the method will not save the new data and will throw an exception </remarks>
        /// <param name="data">The data to be saved. Cannot be null.</param>
        /// <param name="overwrite">A value indicating whether to overwrite existing data if it already exists. <see langword="true"/> to
        /// overwrite; otherwise, <see langword="false"/>.</param>
        void Save(T data, bool overwrite = true);

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
        /// Delete the current file.
        /// </summary>
        void Delete();

        /// <summary>
        /// Open the export directory in the file explorer.
        /// </summary>
        void OpenInExplorer();
    }
}
