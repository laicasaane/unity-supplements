using System.IO;

namespace System.Runtime.Serialization.Formatters.Text
{
    public interface ITextFormatter<T>
    {
        void Serialize(TextWriter writer, T @object);

        T Deserialize(TextReader reader);
    }
}