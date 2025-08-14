using System;
using System.Collections.Generic;

namespace UMUNA.AppManagement.DataProviders
{
    /// <summary>
    /// Provides a global data storage and notification system for application-wide data management.
    /// Allows storing, retrieving, and subscribing to changes of any reference type using string keys.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Retrieves data of the specified type associated with the given key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve. Must be a reference type.</typeparam>
        /// <param name="key">The unique identifier for the data.</param>
        /// <returns>The data of type T if found and can be cast to T; otherwise, null.</returns>
        T Get<T>(Guid key) where T : class, IIdentifiable;

        /// <summary>
        /// Stores or updates data with the specified key and automatically notifies subscribers of the change.
        /// </summary>
        /// <typeparam name="T">The type of data to store. Must be a reference type.</typeparam>
        /// <param name="key">The unique identifier for the data.</param>
        /// <param name="data">The data instance to store. Cannot be null.</param>
        void Add<T>(Guid key, T data) where T : class, IIdentifiable;

        /// <summary>
        /// Removes data associated with the specified key and clears all subscriptions for that key.
        /// </summary>
        /// <param name="key">The unique identifier of the data to remove.</param>
        void RemoveData(Guid key);

        /// <summary>
        /// Checks whether data exists for the specified key, regardless of type.
        /// </summary>
        /// <param name="key">The unique identifier to check.</param>
        /// <returns>True if data exists for the key; otherwise, false.</returns>
        bool HasData(Guid key);

        /// <summary>
        /// Checks whether data of the specified type exists for the given key.
        /// </summary>
        /// <typeparam name="T">The expected type of the data. Must be a reference type.</typeparam>
        /// <param name="key">The unique identifier to check.</param>
        /// <returns>True if data exists for the key and can be cast to type T; otherwise, false.</returns>
        bool HasData<T>(Guid key);

        /// <summary>
        /// Gets all GUID keys currently stored in the data provider.
        /// </summary>
        /// <returns>An enumerable collection of all stored GUIDs.</returns>
        IEnumerable<Guid> GetAllKeys();

        /// <summary>
        /// Manually triggers change notifications for subscribers of the specified key and type.
        /// Useful when the data object's internal state has changed without replacing the reference.
        /// </summary>
        /// <typeparam name="T">The type of data that changed. Must be a reference type.</typeparam>
        /// <param name="key">The unique identifier of the data that changed.</param>
        void NotifyDataChanged<T>(Guid key) where T : class, IIdentifiable;

        /// <summary>
        /// Registers a callback to be invoked when data associated with the specified key changes.
        /// </summary>
        /// <typeparam name="T">The expected type of the data. Must be a reference type.</typeparam>
        /// <param name="key">The unique identifier of the data to monitor.</param>
        /// <param name="callback">The action to invoke when the data changes. Receives the updated data as parameter.</param>
        void Subscribe<T>(Guid key, Action<T> callback) where T : class, IIdentifiable;

        /// <summary>
        /// Removes a previously registered callback from change notifications for the specified key.
        /// </summary>
        /// <typeparam name="T">The expected type of the data. Must be a reference type.</typeparam>
        /// <param name="key">The unique identifier of the data to stop monitoring.</param>
        /// <param name="callback">The action to remove from notifications.</param>
        void Unsubscribe<T>(Guid key, Action<T> callback) where T : class, IIdentifiable;
    }
}
