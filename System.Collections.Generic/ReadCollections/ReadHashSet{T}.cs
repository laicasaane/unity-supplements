using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly struct ReadHashSet<T> : IReadOnlyCollection<T>, IEquatableReadOnlyStruct<ReadHashSet<T>>
    {
        private readonly HashSet<T> source;
        private readonly bool hasSource;

        public int Count => GetSource().Count;

        public ReadHashSet(HashSet<T> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal HashSet<T> GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadHashSet<T> other && Equals(in other);

        public bool Equals(ReadHashSet<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Equals(in ReadHashSet<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Contains(T item)
            => GetSource().Contains(item);

        public void CopyTo(T[] array, int arrayIndex, int count)
            => GetSource().CopyTo(array, arrayIndex, count);

        public void CopyTo(T[] array, int arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        public void CopyTo(T[] array)
            => GetSource().CopyTo(array);

        public bool IsProperSubsetOf(IEnumerable<T> other)
            => GetSource().IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other)
            => GetSource().IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other)
            => GetSource().IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other)
            => GetSource().IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other)
            => GetSource().Overlaps(other);

        public bool SetEquals(IEnumerable<T> other)
            => GetSource().SetEquals(other);

        public HashSet<T>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static HashSet<T> _empty { get; } = new HashSet<T>();

        public static ReadHashSet<T> Empty { get; } = new ReadHashSet<T>(_empty);

        public static implicit operator ReadHashSet<T>(HashSet<T> source)
            => source == null ? Empty : new ReadHashSet<T>(source);

        public static bool operator ==(in ReadHashSet<T> a, in ReadHashSet<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadHashSet<T> a, in ReadHashSet<T> b)
            => !a.Equals(in b);
    }
}