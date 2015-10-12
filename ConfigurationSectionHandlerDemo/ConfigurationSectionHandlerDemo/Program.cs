using System;
using System.Configuration;
using System.Xml.Serialization;

namespace ConfigurationSectionHandlerDemo
{
    internal class Program
    {
        private static void Main()
        {
            TestGenericCollectionConfigurationSectionHandler();

            Console.ReadLine();
        }

        private static void TestGenericCollectionConfigurationSectionHandler()
        {
            var pageSections = (Page[]) ConfigurationManager.GetSection("page");

            Console.WriteLine("Found {0} pages\n", pageSections.Length);

            foreach (var indexing in pageSections)
            {
                Console.WriteLine("Page name: {0}", indexing.Name);
                Console.WriteLine("Searchable: {0}\n", indexing.Property.Searchable);
            }
        }
    }

    [XmlRoot("article")]
    public class Page
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("property")]
        public PageTypeProperty Property { get; set; }
    }

    public class PageTypeProperty
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("searchable")]
        public bool Searchable { get; set; }
    }
}