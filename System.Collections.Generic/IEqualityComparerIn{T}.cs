namespace System.Collections.Generic
{
    public interface IEqualityComparerIn<T> : IEqualityComparer<T>
    {
        bool Equals(in T x, in T y);

        int GetHashCode(in T obj);
    }
}