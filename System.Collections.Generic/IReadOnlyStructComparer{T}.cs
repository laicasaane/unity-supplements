namespace System.Collections.Generic
{
    public interface IReadOnlyStructComparer<T> : IComparerIn<T> where T : struct
    {
    }
}