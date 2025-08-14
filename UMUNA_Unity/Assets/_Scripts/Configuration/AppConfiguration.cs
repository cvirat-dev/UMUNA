
using Newtonsoft.Json;

namespace UMUNA.Configuration
{
    [System.Serializable]
    public class AppConfiguration
    {
        [JsonProperty("ApiBaseUrl")]
        public string ApiBaseUrl = "https://api.umuna.com";

        [JsonProperty("LocalFileFormat")]
        public LocalFileFormat LocalFileFormat = new();
    }

    [System.Serializable]
    public class LocalFileFormat
    {
        [JsonProperty("Value")]
        public string Value { get; set; } = "json";

        [JsonProperty("Allowed")]
        public string[] Allowed { get; set; } = new[] { "json", "xml"};

    }
}
