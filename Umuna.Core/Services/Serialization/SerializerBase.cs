using System.Reflection;

namespace Umuna.Core.Services.Serialization
{
    public abstract class SerializerBase
    {
        public string GetExtension()
        {
            var attribute = GetType().GetCustomAttribute<SerializerOfTypeAttribute>();
            if (attribute == null)
            {
                throw new System.Exception($"{nameof(SerializerOfTypeAttribute)} not found");
            }

            return attribute.Type.ToString().ToLower();
        }
    }
}
