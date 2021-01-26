using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly struct ReadList<T> : IReadOnlyList<T>, IEquatableReadOnlyStruct<ReadList<T>>
    {
        private readonly List<T> source;

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Count;
        }

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource()[index];
        }

        public ReadList(List<T> source)
        {
            this.source = source ?? _empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal List<T> GetSource()
            => this.source ?? _empty;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadList<T> other && Equals(in other);

        public bool Equals(ReadList<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Equals(in ReadList<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int BinarySearch(T item)
            => GetSource().BinarySearch(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int BinarySearch(T item, IComparer<T> comparer)
            => GetSource().BinarySearch(item, comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
            => GetSource().BinarySearch(index, count, item, comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
            => GetSource().Contains(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
            => GetSource().ConvertAll(converter);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
            => GetSource().CopyTo(index, array, arrayIndex, count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array)
            => GetSource().CopyTo(array);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Exists(Predicate<T> match)
            => GetSource().Exists(match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Find(Predicate<T> match)
            => GetSource().Find(match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> FindAll(Predicate<T> match)
            => GetSource().FindAll(match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FindIndex(int startIndex, int count, Predicate<T> match)
            => GetSource().FindIndex(startIndex, count, match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FindIndex(int startIndex, Predicate<T> match)
            => GetSource().FindIndex(startIndex, match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FindIndex(Predicate<T> match)
            => GetSource().FindIndex(match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T FindLast(Predicate<T> match)
            => GetSource().FindLast(match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
            => GetSource().FindLastIndex(startIndex, count, match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FindLastIndex(int startIndex, Predicate<T> match)
            => GetSource().FindLastIndex(startIndex, match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FindLastIndex(Predicate<T> match)
            => GetSource().FindLastIndex(match);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ForEach(Action<T> action)
            => GetSource().ForEach(action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public List<T> GetRange(int index, int count)
            => GetSource().GetRange(index, count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item, int index, int count)
            => GetSource().IndexOf(item, index, count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item, int index)
            => GetSource().IndexOf(item, index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item)
            => GetSource().IndexOf(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LastIndexOf(T item)
            => GetSource().LastIndexOf(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LastIndexOf(T item, int index)
            => GetSource().LastIndexOf(item, index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int LastIndexOf(T item, int index, int count)
            => GetSource().LastIndexOf(item, index, count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TrueForAll(Predicate<T> match)
            => GetSource().TrueForAll(match);

        public T[] ToArray()
        {
            var source = GetSource();
            var array = new T[source.Count];

            for (var i = 0; i < source.Count; i++)
            {
                array[i] = source[i];
            }

            return array;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
            => new Enumerator(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        private static List<T> _empty { get; } = new List<T>(0);

        public static ReadList<T> Empty { get; } = new ReadList<T>(_empty);

        public static implicit operator ReadList<T>(List<T> source)
            => source == null ? Empty : new ReadList<T>(source);

        public static implicit operator ReadCollection<T>(in ReadList<T> source)
            => new ReadCollection<T>(source.GetSource());

        public static bool operator ==(in ReadList<T> a, in ReadList<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadList<T> a, in ReadList<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly List<T> source;
            private readonly int end;
            private int current;

            internal Enumerator(in ReadList<T> list)
            {
                this.source = list.GetSource();
                this.end = this.source.Count;
                this.current = -1;
            }

            public bool MoveNext()
            {
                if (this.current < this.end)
                {
                    this.current++;
                    return (this.current < this.end);
                }

                return false;
            }

            public T Current
            {
                get
                {
                    if (this.current < 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.current >= this.end)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.source[this.current];
                }
            }

            object IEnumerator.Current
                => this.Current;

            void IEnumerator.Reset()
            {
                this.current = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}