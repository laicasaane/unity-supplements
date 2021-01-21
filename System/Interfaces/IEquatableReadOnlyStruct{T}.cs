namespace System
{
    public interface IEquatableReadOnlyStruct<T> : IEquatableIn<T> where T : struct
    {
    }
}