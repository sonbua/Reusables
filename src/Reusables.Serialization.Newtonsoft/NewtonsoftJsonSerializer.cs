using System;
using Reusables.Serialization.Newtonsoft.Extensions;

namespace Reusables.Serialization.Newtonsoft
{
    public class NewtonsoftJsonSerializer<T> : JsonSerializer<T>
    {
        public override string Serialize(T source)
        {
            return source.ToJson();
        }

        public override T Deserialize(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.FromJson<T>();
        }
    }
}
