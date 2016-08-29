using System.Configuration;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Reusables.Configuration
{
    public sealed class GenericCollectionConfigurationSectionHandler<T> : IConfigurationSectionHandler
    {
        private string _rootElementName;

        private string RootElementName
        {
            get
            {
                if (_rootElementName != null)
                    return _rootElementName;

                var xmlRootAttributes = typeof (T).GetCustomAttributes(typeof (XmlRootAttribute), false)
                                                  .Cast<XmlRootAttribute>()
                                                  .ToList();

                return _rootElementName = xmlRootAttributes.Any() ? xmlRootAttributes.First().ElementName : typeof (T).Name;
            }
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            var xmlSerializer = new XmlSerializer(typeof (T));

            var xmlNodeReader = new XmlNodeReader(section);

            return XDocument.Load(xmlNodeReader)
                            .Descendants(RootElementName)
                            .Select(e => (T) xmlSerializer.Deserialize(e.CreateReader()))
                            .ToArray();
        }
    }
}
