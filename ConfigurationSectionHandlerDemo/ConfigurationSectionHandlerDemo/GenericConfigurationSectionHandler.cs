using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace ConfigurationSectionHandlerDemo
{
    public sealed class GenericConfigurationSectionHandler<T> : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var xmlSerializer = new XmlSerializer(typeof (T));

            var xmlNodeReader = new XmlNodeReader(section);

            return xmlSerializer.Deserialize(xmlNodeReader);
        }
    }
}