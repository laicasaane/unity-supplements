namespace System
{
    public interface IEquatableIn<T> : IEquatable<T>
    {
        bool Equals(in T other);
    }
}