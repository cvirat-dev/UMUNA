using System;

namespace Umuna.Core.Serialization.Serialization
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SerializerOfTypeAttribute : Attribute
    {
        public SerializerType Type { get; private set; }
        public SerializerOfTypeAttribute(SerializerType type)
        {
            Type = type;
        }
    }
}
