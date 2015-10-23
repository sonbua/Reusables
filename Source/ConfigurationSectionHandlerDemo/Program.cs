using System;
using System.Configuration;
using System.Xml.Serialization;

namespace ConfigurationSectionHandlerDemo
{
    internal class Program
    {
        private static void Main()
        {
            TestGenericConfigurationSectionHandler();

            TestGenericCollectionConfigurationSectionHandler();

            Console.ReadLine();
        }

        private static void TestGenericConfigurationSectionHandler()
        {
            Console.WriteLine("Test section with single config\n");

            var singlePageSection = (SinglePage) ConfigurationManager.GetSection("page");

            Console.WriteLine("Page name: {0}", singlePageSection.Name);
            Console.WriteLine("Body: {0}", singlePageSection.Body);
            Console.WriteLine("Properties count: {0}", singlePageSection.Properties.Length);

            Console.WriteLine("====================\n");
        }

        private static void TestGenericCollectionConfigurationSectionHandler()
        {
            Console.WriteLine("Test section with collection of configs");

            var pageSections = (Page[]) ConfigurationManager.GetSection("site");

            Console.WriteLine("Found {0} articles\n", pageSections.Length);

            foreach (var indexing in pageSections)
            {
                Console.WriteLine("Page name: {0}", indexing.Name);
                Console.WriteLine("Searchable: {0}\n", indexing.Property.Searchable);
            }
        }
    }

    [XmlRoot("page")]
    public class Page
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("property")]
        public PageProperty Property { get; set; }
    }

    public class PageProperty
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("searchable")]
        public bool Searchable { get; set; }
    }

    [XmlRoot("page")]
    public class SinglePage
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("body")]
        public string Body { get; set; }

        [XmlArray("properties")]
        [XmlArrayItem("property")]
        public PageProperty[] Properties { get; set; }
    }
}