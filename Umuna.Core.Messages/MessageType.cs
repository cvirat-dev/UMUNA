using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Umuna.Core.Messages
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MessageType
    {
        Info
    }
}