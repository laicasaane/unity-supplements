namespace System
{
    public interface IComparableReadOnlyStruct<T> : IComparableIn<T> where T : struct
    {
    }
}