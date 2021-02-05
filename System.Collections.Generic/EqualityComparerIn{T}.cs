namespace System.Collections.Generic
{
    public class EqualityComparerIn<T> : IEqualityComparerIn<T>
    {
        public bool Equals(in T x, in T y)
            => x.Equals(y);

        public bool Equals(T x, T y)
            => x.Equals(y);

        public int GetHashCode(in T obj)
            => obj.GetHashCode();

        public int GetHashCode(T obj)
            => obj.GetHashCode();

        public static EqualityComparerIn<T> Default { get; } = new EqualityComparerIn<T>();
    }
}
