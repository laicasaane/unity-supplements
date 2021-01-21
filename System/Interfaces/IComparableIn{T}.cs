namespace System
{
    public interface IComparableIn<T> : IComparable<T>
    {
        int CompareTo(in T other);
    }
}