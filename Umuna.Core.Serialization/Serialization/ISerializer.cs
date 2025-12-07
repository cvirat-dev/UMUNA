namespace Umuna.Core.Serialization.Serialization
{
    public interface ISerializer<T> where T : class
    {
        /// <summary>
        /// Serializes the specified data object into a string representation and returns it.
        /// </summary>
        /// <remarks>The exact format of the serialized string depends on the implementation of the
        /// method. Ensure that the type <typeparamref name="T"/> is supported by the serialization logic.</remarks>
        /// <param name="data">The data object to serialize. This must be a valid instance of type <typeparamref name="T"/>.</param>
        /// <returns>A string containing the serialized representation of the <paramref name="data"/> object, or <see
        /// langword="null"/> if the serialization fails or the input is <see langword="null"/>.</returns>
        string? Serialize(T? data);
        /// <summary>
        /// Deserializes a string representation back into an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="serializedData">The serialized string to deserialize.</param>
        /// <returns>An instance of type <typeparamref name="T"/> if deserialization is successful; otherwise, <see langword="null"/>.</returns>
        T? Deserialize(string serializedData);
        
        string GetExtension();
    }
}
