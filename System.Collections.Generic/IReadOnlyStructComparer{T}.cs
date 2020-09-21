namespace System.Collections.Generic
{
    public interface IReadOnlyStructComparer<T> : IComparer<T> where T : struct
    {
        int Compare(in T x, in T y);
    }
}