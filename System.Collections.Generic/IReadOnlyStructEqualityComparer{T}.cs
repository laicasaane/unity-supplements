namespace System.Collections.Generic
{
    public interface IReadOnlyStructEqualityComparer<T> : IEqualityComparerIn<T> where T : struct
    {
    }
}