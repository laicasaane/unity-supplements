namespace System
{
    public interface IComparableReadOnlyStruct<T> : IComparable<T> where T : struct
    {
        int CompareTo(in T other);
    }
}