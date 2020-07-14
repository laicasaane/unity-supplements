using System.IO;

namespace System.Runtime.Serialization
{
    public interface IStringFormatter<T>
    {
        string Serialize(TextWriter writer, T @object);

        T Deserialize(TextReader reader);
    }
}