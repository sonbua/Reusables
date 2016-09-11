using System.Runtime.Serialization;
using Reusables.Diagnostics.Contracts;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace Reusables.Serialization.Newtonsoft
{
    /// <summary>
    /// Uses <see cref="DataContractSerializer"/> to support serialization/deserialization.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataContractXmlSerializer<T> : XmlSerializer<T>
    {
        public override string Serialize(T source)
        {
            return source.ToXml();
        }

        public override T Deserialize(string source)
        {
            Requires.IsNotNull(source, nameof(source));

            return source.FromXml<T>();
        }
    }
}