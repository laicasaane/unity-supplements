namespace System.Collections.Generic
{
    public interface IComparerIn<T> : IComparer<T>
    {
        int Compare(in T x, in T y);
    }
}