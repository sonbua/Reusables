using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace Reusables.Configuration
{
    /// <summary>
    /// References: http://theburningmonk.com/2011/06/generic-config-section-handlers/
    /// </summary>
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
