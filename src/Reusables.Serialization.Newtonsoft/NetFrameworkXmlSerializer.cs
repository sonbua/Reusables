using System.IO;
using System.Xml.Serialization;
using Reusables.Util.Extensions;

namespace Reusables.Serialization.Newtonsoft
{
    /// <summary>
    /// Uses <see cref="XmlSerializer"/> to support serialization/deserialization.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NetFrameworkXmlSerializer<T> : XmlSerializer<T>
    {
        public override string Serialize(T source)
        {
            using (var sourceStream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(T));

                serializer.Serialize(sourceStream, source);
                sourceStream.TryToRewind();

                using (var streamReader = new StreamReader(sourceStream))
                    return streamReader.ReadToEnd();
            }
        }

        public override T Deserialize(string source)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(source))
                return (T) serializer.Deserialize(reader);
        }
    }
}