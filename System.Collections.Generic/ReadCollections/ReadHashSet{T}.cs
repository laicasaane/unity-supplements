using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly struct ReadHashSet<T> : IReadOnlyCollection<T>, IEquatableReadOnlyStruct<ReadHashSet<T>>
    {
        private readonly HashSet<T> source;
        private readonly bool hasSource;

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Count;
        }

        public ReadHashSet(HashSet<T> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal HashSet<T> GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
            => GetSource().Contains(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex, int count)
            => GetSource().CopyTo(array, arrayIndex, count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array)
            => GetSource().CopyTo(array);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsProperSubsetOf(IEnumerable<T> other)
            => GetSource().IsProperSubsetOf(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsProperSupersetOf(IEnumerable<T> other)
            => GetSource().IsProperSupersetOf(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSubsetOf(IEnumerable<T> other)
            => GetSource().IsSubsetOf(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsSupersetOf(IEnumerable<T> other)
            => GetSource().IsSupersetOf(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Overlaps(IEnumerable<T> other)
            => GetSource().Overlaps(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool SetEquals(IEnumerable<T> other)
            => GetSource().SetEquals(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HashSet<T>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static HashSet<T> _empty { get; } = new HashSet<T>();

        public static ReadHashSet<T> Empty { get; } = new ReadHashSet<T>(_empty);

        public static implicit operator ReadHashSet<T>(HashSet<T> source)
            => source == null ? Empty : new ReadHashSet<T>(source);

        public static implicit operator ReadCollection<T>(in ReadHashSet<T> source)
            => new ReadCollection<T>(source.GetSource());

        public static bool operator ==(in ReadHashSet<T> a, in ReadHashSet<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadHashSet<T> a, in ReadHashSet<T> b)
            => !a.Equals(in b);
    }
}