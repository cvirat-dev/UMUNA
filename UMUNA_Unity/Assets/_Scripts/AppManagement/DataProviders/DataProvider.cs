using System;
using System.Collections.Generic;

namespace UMUNA.AppManagement.DataProviders
{
    public class DataProvider : IDataProvider
    {
        private readonly Dictionary<Guid, object> _dataStore = new();
        private readonly Dictionary<Guid, List<Action<object>>> _subscribers = new();

        public T Get<T>(Guid key) where T : class, IIdentifiable
        {
            return _dataStore.TryGetValue(key, out var data) ? data as T : null;
        }

        public void Add<T>(Guid key, T data) where T : class, IIdentifiable
        {
            if (data == null) return;
            
            _dataStore[key] = data;
            NotifyDataChanged<T>(key);
        }

        public void RemoveData(Guid key)
        {
            _dataStore.Remove(key);
            _subscribers.Remove(key);
        }

        public bool HasData(Guid key)
        {
            return _dataStore.ContainsKey(key);
        }

        public bool HasData<T>(Guid key)
        {
            if (_dataStore.TryGetValue(key, out var data))
            {
                return data is T;
            }
            return false;
        }

        public IEnumerable<Guid> GetAllKeys()
        {
            return _dataStore.Keys;
        }

        public void NotifyDataChanged<T>(Guid key) where T : class, IIdentifiable
        {
            if (_subscribers.TryGetValue(key, out var callbacks))
            {
                var data = Get<T>(key);
                foreach (var callback in callbacks)
                {
                    callback?.Invoke(data);
                }
            }
        }

        public void Subscribe<T>(Guid key, Action<T> callback) where T : class, IIdentifiable
        {
            if (!_subscribers.ContainsKey(key))
                _subscribers[key] = new List<Action<object>>();

            _subscribers[key].Add(data => callback?.Invoke(data as T));
        }

        public void Unsubscribe<T>(Guid key, Action<T> callback) where T : class, IIdentifiable
        {
            if (_subscribers.TryGetValue(key, out var callbacks))
            {
                callbacks.RemoveAll(c => c.Target == callback.Target && c.Method == callback.Method);
            }
        }
    }
}