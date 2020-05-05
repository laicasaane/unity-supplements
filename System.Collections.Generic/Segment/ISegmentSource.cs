namespace System.Collections.Generic
{
    public interface ISegmentSource<T>
    {
        int Count { get; }

        T this[int index] { get; }
    }
}