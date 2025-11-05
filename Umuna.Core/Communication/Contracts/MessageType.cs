using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Umuna.Core.Communication.Contracts
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MessageType
    {
        Info
    }
}