namespace System
{
    public interface IEquatableReadOnlyStruct<T> : IEquatable<T> where T : struct
    {
        bool Equals(in T other);
    }
}