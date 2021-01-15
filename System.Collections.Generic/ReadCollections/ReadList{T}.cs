using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly struct ReadList<T> : IReadOnlyList<T>, IEquatableReadOnlyStruct<ReadList<T>>
    {
        private readonly List<T> source;

        public int Count => GetSource().Count;

        public T this[int index]
            => GetSource()[index];

        public ReadList(List<T> source)
        {
            this.source = source ?? _empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal List<T> GetSource()
            => this.source ?? _empty;

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

        public int BinarySearch(T item)
            => GetSource().BinarySearch(item);

        public int BinarySearch(T item, IComparer<T> comparer)
            => GetSource().BinarySearch(item, comparer);

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
            => GetSource().BinarySearch(index, count, item, comparer);

        public bool Contains(T item)
            => GetSource().Contains(item);

        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
            => GetSource().ConvertAll(converter);

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
            => GetSource().CopyTo(index, array, arrayIndex, count);

        public void CopyTo(T[] array, int arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        public void CopyTo(T[] array)
            => GetSource().CopyTo(array);

        public bool Exists(Predicate<T> match)
            => GetSource().Exists(match);

        public T Find(Predicate<T> match)
            => GetSource().Find(match);

        public List<T> FindAll(Predicate<T> match)
            => GetSource().FindAll(match);

        public int FindIndex(int startIndex, int count, Predicate<T> match)
            => GetSource().FindIndex(startIndex, count, match);

        public int FindIndex(int startIndex, Predicate<T> match)
            => GetSource().FindIndex(startIndex, match);

        public int FindIndex(Predicate<T> match)
            => GetSource().FindIndex(match);

        public T FindLast(Predicate<T> match)
            => GetSource().FindLast(match);

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
            => GetSource().FindLastIndex(startIndex, count, match);

        public int FindLastIndex(int startIndex, Predicate<T> match)
            => GetSource().FindLastIndex(startIndex, match);

        public int FindLastIndex(Predicate<T> match)
            => GetSource().FindLastIndex(match);

        public void ForEach(Action<T> action)
            => GetSource().ForEach(action);

        public List<T> GetRange(int index, int count)
            => GetSource().GetRange(index, count);

        public int IndexOf(T item, int index, int count)
            => GetSource().IndexOf(item, index, count);

        public int IndexOf(T item, int index)
            => GetSource().IndexOf(item, index);

        public int IndexOf(T item)
            => GetSource().IndexOf(item);

        public int LastIndexOf(T item)
            => GetSource().LastIndexOf(item);

        public int LastIndexOf(T item, int index)
            => GetSource().LastIndexOf(item, index);

        public int LastIndexOf(T item, int index, int count)
            => GetSource().LastIndexOf(item, index, count);
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

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

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