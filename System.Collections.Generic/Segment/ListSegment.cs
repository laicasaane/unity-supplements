namespace System.Collections.Generic
{
    public readonly partial struct ListSegment<T> : ISegment<T>, IEquatable<ListSegment<T>>, IEqualityComparer<ListSegment<T>>
    {
        private readonly List<T> source;

        public bool HasSource { get; }

        public int Offset { get; }

        public int Count { get; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return this.source[this.Offset + index];
            }
        }

        public ListSegment(List<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.source = source;
            this.HasSource = true;
            this.Offset = 0;
            this.Count = source.Count;
        }

        public ListSegment(List<T> source, int offset, int count)
        {
            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if (source == null || (uint)offset > (uint)source.Count || (uint)count > (uint)(source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(source, offset, count);

            this.source = source;
            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        public ListSegment<T> Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ListSegment<T>(this.source, this.Offset + index, this.Count - index);
        }

        public ListSegment<T> Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ListSegment<T>(this.source, this.Offset + index, count);
        }

        public ListSegment<T> Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ListSegment<T>(this.source, this.Offset + count, this.Count - count);
        }

        public ListSegment<T> Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ListSegment<T>(this.source, this.Offset, count);
        }

        public ListSegment<T> TakeLast(int count)
            => Skip(this.Count - count);

        public ListSegment<T> SkipLast(int count)
            => Take(this.Count - count);

        ISegment<T> ISegment<T>.Slice(int index)
            => Slice(index);

        ISegment<T> ISegment<T>.Slice(int index, int count)
            => Slice(index, count);

        ISegment<T> ISegment<T>.Skip(int count)
            => Skip(count);

        ISegment<T> ISegment<T>.Take(int count)
            => Take(count);

        ISegment<T> ISegment<T>.TakeLast(int count)
            => TakeLast(count);

        ISegment<T> ISegment<T>.SkipLast(int count)
            => SkipLast(count);

        public T[] ToArray()
        {
            if (this.HasSource || this.Count == 0)
                return _empty;

            var array = new T[this.Count];
            var count = this.Count + this.Offset;

            for (int i = this.Offset, j = 0; i < count; i++, j++)
            {
                array[j] = this.source[i];
            }

            return array;
        }

        public int IndexOf(T item)
        {
            var index = -1;

            if (!this.HasSource)
                return index;

            var count = this.Count + this.Offset;

            for (var i = this.Offset; i < count; i++)
            {
                if (this.source[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }

            return index >= 0 ? index - this.Offset : -1;
        }

        public bool Contains(T item)
        {
            if (!this.HasSource)
                return false;

            var count = this.Count + this.Offset;

            for (var i = this.Offset; i < count; i++)
            {
                if (this.source[i].Equals(item))
                    return true;
            }

            return false;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this.HasSource ? this : Empty);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ this.Offset;
                hash = (hash * 16777619) ^ this.Count;

                // The array hash is expected to be an evenly-distributed mixture of bits,
                // so rather than adding the cost of another rotation we just xor it.
                hash ^= this.HasSource ? this.source.GetHashCode() : _empty.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
            => obj is ListSegment<T> other && Equals(other);

        public bool Equals(ListSegment<T> other)
        {
            if (!this.HasSource)
                return !other.HasSource || ReferenceEquals(other.source, _empty);

            return ReferenceEquals(this.source, other.source) &&
                   other.Offset == this.Offset &&
                   other.Count == this.Count;
        }

        public bool Equals(ListSegment<T> x, ListSegment<T> y)
            => x.Equals(y);

        public int GetHashCode(ListSegment<T> obj)
            => obj.GetHashCode();

        private static T[] _empty { get; } = new T[0];

        public static ListSegment<T> Empty { get; } = new ListSegment<T>();

        public static implicit operator ListSegment<T>(List<T> source)
            => source == null ? Empty : new ListSegment<T>(source);

        public static implicit operator Segment<T>(in ListSegment<T> segment)
            => new Segment<T>(segment.source, segment.Offset, segment.Count);

        public static bool operator ==(in ListSegment<T> a, in ListSegment<T> b)
            => a.Equals(b);

        public static bool operator !=(in ListSegment<T> a, in ListSegment<T> b)
            => !(a == b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly List<T> source;
            private readonly int start;
            private readonly int end; // cache Offset + Count, since it's a little slow
            private int current;

            internal Enumerator(in ListSegment<T> segment)
            {
                this.source = segment.source;

                if (!segment.HasSource)
                {
                    this.start = 0;
                    this.end = 0;
                    this.current = 0;
                    return;
                }

                this.start = segment.Offset;
                this.end = segment.Offset + segment.Count;
                this.current = this.start - 1;
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
                    if (this.current < this.start)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.current >= this.end)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.source[this.current];
                }
            }

            object IEnumerator.Current
                => this.Current;

            public void Reset()
            {
                this.current = this.start - 1;
            }

            public void Dispose()
            {
            }
        }
    }
}