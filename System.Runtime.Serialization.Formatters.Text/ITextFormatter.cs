using System.IO;

namespace System.Runtime.Serialization.Formatters.Text
{
    public interface ITextFormatter
    {
        void Serialize<T>(TextWriter writer, T @object);

        T Deserialize<T>(TextReader reader);
    }
}