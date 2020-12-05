using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly struct ReadCollection<T> : IReadOnlyCollection<T>, IEquatableReadOnlyStruct<ReadCollection<T>>
    {
        private readonly ICollection<T> source;
        private readonly bool hasSource;

        public int Count => GetSource().Count;

        public ReadCollection(ICollection<T> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ICollection<T> GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadCollection<T> other && Equals(in other);

        public bool Equals(ReadCollection<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Equals(in ReadCollection<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Contains(T item)
            => GetSource().Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator()
            => GetSource().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static ICollection<T> _empty { get; } = new T[0];

        public static ReadCollection<T> Empty { get; } = new ReadCollection<T>(_empty);

        public static bool operator ==(in ReadCollection<T> a, in ReadCollection<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadCollection<T> a, in ReadCollection<T> b)
            => !a.Equals(in b);
    }
}