using Reusables.Diagnostics.Contracts;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace Reusables.Serialization.Newtonsoft
{
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
