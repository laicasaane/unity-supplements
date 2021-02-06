namespace System.Collections.Generic
{
    public abstract partial  class EqualityComparerIn<T> : IEqualityComparerIn<T>
    {
        public abstract bool Equals(in T x, in T y);

        public abstract bool Equals(T x, T y);

        public abstract int GetHashCode(in T obj);

        public abstract int GetHashCode(T obj);

        public static EqualityComparerIn<T> Default { get; } = CreateComparer();
    }
}
