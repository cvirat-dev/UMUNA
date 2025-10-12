using System;
using Newtonsoft.Json;

[Serializable]
public class AppConfiguration
{
    [JsonProperty("ApiBaseUrl")]
    public string ApiBaseUrl { get; set; } = "https://api.umuna.com";

    [JsonProperty("FileSystemConfiguration")]
    public FileSystemConfiguration FileSystemConfiguration { get; set; } = new();

    [JsonProperty("NetworkConfiguration")]
    public NetworkConfiguration NetworkConfiguration { get; set; } = new();
}

[Serializable]
public class FileSystemConfiguration
{
    [JsonProperty("SerializationFormat")]
    public SerializationFormat SerializationFormat { get; set; } = new();
}

[Serializable]
public class NetworkConfiguration
{
    [JsonProperty("TcpConfig")]
    public Tcpconfig TcpConfig { get; set; } = new Tcpconfig();

    [JsonProperty("SerializationFormat")]
    public SerializationFormat SerializationFormat { get; set; } = new();

}

[Serializable]
public class Tcpconfig
{
    [JsonProperty("Port")]
    public int Port { get; set; } = 5000;

    [JsonProperty("ServerIp")]
    public string ServerIp { get; set; } = "127.0.0.1";

    [JsonProperty("MaxConnections")]
    public int MaxConnections { get; set; } = 100;
}


[Serializable]
public class SerializationFormat
{
    [JsonProperty("Value")]
    public string Value { get; set; } = "json";

    [JsonProperty("Allowed")]
    public string[] Allowed { get; set; } = new[] { "json", "xml" };
}
