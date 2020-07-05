namespace System.Collections.Generic
{
    public interface IReadOnlyStructEqualityComparer<T> : IEqualityComparer<T> where T : struct
    {
        bool Equals(in T x, in T y);

        int GetHashCode(in T obj);
    }
}