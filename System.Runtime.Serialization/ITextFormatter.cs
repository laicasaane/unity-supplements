using System.IO;

namespace System.Runtime.Serialization
{
    public interface ITextFormatter
    {
        void Serialize<T>(TextWriter writer, T @object);

        T Deserialize<T>(TextReader reader);
    }
}