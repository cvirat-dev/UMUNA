using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Umuna.Core.Services.Serialization.Xml
{
    [SerializerOfType(SerializerType.Xml)]
    public class XmlSerializer<T> : SerializerBase, ISerializer<T> where T : class
    {
        #region Fields
        XmlWriterSettings? _xmlWriterSettings;
        XmlSerializer? _xmlSerializer;
        #endregion

        #region Properties
        public XmlWriterSettings? Settings
        {
            get => _xmlWriterSettings;
            set
            {
                _xmlWriterSettings = value;
            }
        }

        public XmlSerializer Serializer
        {
            get
            {
                _xmlSerializer ??= new XmlSerializer(typeof(T));
                return _xmlSerializer;
            }
            set
            {
                _xmlSerializer = value;
            }
        }
        #endregion

        #region Constructors
        public XmlSerializer()
        {
            Settings = new XmlWriterSettings
            {
                Indent = false,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };
            Serializer = new XmlSerializer(typeof(T));
        }

        public XmlSerializer(XmlWriterSettings? xmlWriterSettings)
        {
            Settings = xmlWriterSettings ?? new XmlWriterSettings
            {
                Indent = false,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };
            Serializer = new XmlSerializer(typeof(T));
        }
        #endregion

        #region Public Methods
        public string Serialize(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, Settings);

            Serializer.Serialize(xmlWriter, obj);
            return stringWriter.ToString();
        }

        public T? Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            using var stringReader = new StringReader(data);
            using var xmlReader = XmlReader.Create(stringReader);

            return (T?)Serializer.Deserialize(xmlReader);
        }
        #endregion
    }
}