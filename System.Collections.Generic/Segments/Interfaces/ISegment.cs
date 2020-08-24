namespace System.Collections.Generic
{
    public interface ISegment<T> : IReadOnlyList<T>
    {
        //new T this[int index] { get; set; }

        bool Contains(T item);

        ISegment<T> Slice(int index);

        ISegment<T> Slice(int index, int count);

        ISegment<T> Skip(int count);

        ISegment<T> Take(int count);

        ISegment<T> TakeLast(int count);

        ISegment<T> SkipLast(int count);
    }

    public interface IReadSegment<T> : IReadOnlyList<T>
    {
        bool Contains(T item);

        IReadSegment<T> Slice(int index);

        IReadSegment<T> Slice(int index, int count);

        IReadSegment<T> Skip(int count);

        IReadSegment<T> Take(int count);

        IReadSegment<T> TakeLast(int count);

        IReadSegment<T> SkipLast(int count);
    }
}