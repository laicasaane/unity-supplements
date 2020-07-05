namespace System.Collections.Generic
{
    public interface ISegmentReader<T>
    {
        T Source { get; }

        int Position { get; }

        T Read(int count);
    }
}