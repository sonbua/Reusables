namespace Reusables.Serialization
{
    public interface ISerializer<T>
    {
        string Serialize(T source);

        T Deserialize(string source);
    }
}
