using System;
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
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return source.FromXml<T>();
        }
    }
}
