namespace System.Collections.Generic
{
    public interface ISegmentSource<T> : IEquatable<ISegmentSource<T>>
    {
        int Count { get; }

        T this[int index] { get; }

        int GetHashCode();
    }
}