using Newtonsoft.Json;
using System.Collections.Generic;

namespace Umuna.Core.Services.Serialization.Json
{
    [SerializerOfType(SerializerType.Json)]
    public class JsonSerializer<T> : SerializerBase, ISerializer<T> where T : class
    {
        #region Fields
        JsonSerializerSettings? _settings;
        #endregion

        #region Properties
        public JsonSerializerSettings? Settings
        {
            get => _settings;
            set
            {
                _settings = value;
            }
        }
        #endregion

        #region Constructors
        public JsonSerializer()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter>()
            };
        }

        public JsonSerializer(JsonConverter[]? converters)
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                Converters = new List<JsonConverter>(converters)
            };
        }

        public JsonSerializer(JsonSerializerSettings serializerSettings)
        {
            Settings = serializerSettings;
        }
        #endregion

        #region Public Methods
        public void SetReferenceLoopHandling(ReferenceLoopHandling handling)
        {
            if (_settings != null)
            {
                _settings.ReferenceLoopHandling = handling;
            }
        }

        public void SetFormatting(Formatting formatting)
        {
            if (_settings != null)
            {
                _settings.Formatting = formatting;
            }
        }

        public void AddConverter(JsonConverter converter)
        {
            if (_settings != null && converter != null)
            {
                _settings.Converters.Add(converter);
            }
        }

        public void AddConverters(IEnumerable<JsonConverter> converters)
        {
            if (_settings != null && converters != null)
            {
                foreach (var converter in converters)
                {
                    _settings.Converters.Add(converter);
                }
            }
        }

        public void ClearConverters()
        {
            if (_settings != null)
            {
                _settings.Converters.Clear();
            }
        }

        public T? Deserialize(string json) => JsonConvert.DeserializeObject<T>(json, _settings);

        public string Serialize(T obj) => JsonConvert.SerializeObject(obj, _settings);
        #endregion
    }
}
