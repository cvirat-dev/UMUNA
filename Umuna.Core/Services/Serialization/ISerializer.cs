namespace Umuna.Core.Services.Serialization
{
    public interface ISerializer<T> where T : class
    {
        string Serialize(T obj);
        T? Deserialize(string data);
        string GetExtension();
    }
}
