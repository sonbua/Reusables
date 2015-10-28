namespace Reusables.Serialization
{
    public abstract class XmlSerializer<T> : ISerializer<T>
    {
        public abstract string Serialize(T source);

        public abstract T Deserialize(string source);
    }
}
