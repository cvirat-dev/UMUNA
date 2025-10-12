using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Umuna.Core.SharedData.Communication
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MessageType
    {
        Info
    }
}