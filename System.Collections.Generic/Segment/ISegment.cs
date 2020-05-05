namespace System.Collections.Generic
{
    public interface ISegment<out T> : IReadOnlyList<T>
    {
        ISegment<T> Slice(int index);

        ISegment<T> Slice(int index, int count);

        ISegment<T> Skip(int count);

        ISegment<T> Take(int count);

        ISegment<T> TakeLast(int count);

        ISegment<T> SkipLast(int count);
    }
}