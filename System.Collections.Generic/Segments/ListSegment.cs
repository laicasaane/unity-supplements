namespace System.Collections.Generic
{
    public readonly partial struct ListSegment<T> : ISegment<T>, IEquatableReadOnlyStruct<ListSegment<T>>
    {
        private readonly ReadList<T> source;
        private readonly bool hasSource;
        private readonly int count;
        private readonly int offset;

        public bool HasSource => this.hasSource;

        public int Offset => this.offset;

        public int Count => this.count;

        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)this.count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return this.source[this.offset + index];
            }
        }

        public ListSegment(in ReadList<T> source)
        {
            this.source = source;
            this.hasSource = true;
            this.offset = 0;
            this.count = this.source.Count;
        }

        public ListSegment(in ReadList<T> source, int offset, int count)
        {
            this.source = source;

            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if ((uint)offset > (uint)this.source.Count || (uint)count > (uint)(this.source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(this.source, offset, count);

            this.hasSource = true;
            this.offset = offset;
            this.count = count;
        }

        public ListSegment<T> Slice(int index)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)index > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ListSegment<T>(this.source, this.offset + index, this.count - index);
        }

        public ListSegment<T> Slice(int index, int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)index > (uint)this.count || (uint)count > (uint)(this.count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ListSegment<T>(this.source, this.offset + index, count);
        }

        public ListSegment<T> Skip(int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ListSegment<T>(this.source, this.offset + count, this.count - count);
        }

        public ListSegment<T> Take(int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ListSegment<T>(this.source, this.offset, count);
        }

        public ListSegment<T> TakeLast(int count)
            => Skip(this.count - count);

        public ListSegment<T> SkipLast(int count)
            => Take(this.count - count);

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
            if (!this.hasSource || this.count == 0)
                return new T[0];

            var array = new T[this.count];
            var count = this.count + this.offset;

            for (int i = this.offset, j = 0; i < count; i++, j++)
            {
                array[j] = this.source[i];
            }

            return array;
        }

        public int IndexOf(T item)
        {
            var index = -1;

            if (!this.hasSource)
                return index;

            var count = this.count + this.offset;

            for (var i = this.offset; i < count; i++)
            {
                if (this.source[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }

            return index >= 0 ? index - this.offset : -1;
        }

        public bool Contains(T item)
        {
            if (!this.hasSource)
                return false;

            var count = this.count + this.offset;

            for (var i = this.offset; i < count; i++)
            {
                if (this.source[i].Equals(item))
                    return true;
            }

            return false;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this.hasSource ? this : Empty);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public override bool Equals(object obj)
            => obj is ListSegment<T> other && Equals(in other);

        public bool Equals(ListSegment<T> other)
            => this.hasSource == other.HasSource && this.source.Equals(in other.source) &&
               other.Count == this.count && other.Offset == this.offset;

        public bool Equals(in ListSegment<T> other)
            => this.hasSource == other.HasSource && this.source.Equals(in other.source) &&
               other.Count == this.count && other.Offset == this.offset;

        public override int GetHashCode()
        {
            var hashCode = 1328453276;
            hashCode = hashCode * -1521134295 + this.hasSource.GetHashCode();
            hashCode = hashCode * -1521134295 + this.source.GetHashCode();
            hashCode = hashCode * -1521134295 + this.offset.GetHashCode();
            hashCode = hashCode * -1521134295 + this.count.GetHashCode();
            return hashCode;
        }

        public static ListSegment<T> Empty { get; } = new ListSegment<T>();

        public static implicit operator ListSegment<T>(List<T> source)
            => new ListSegment<T>(source.AsReadList());

        public static implicit operator ListSegment<T>(in ReadList<T> source)
            => new ListSegment<T>(source);

        public static implicit operator Segment<T>(in ListSegment<T> segment)
            => new Segment<T>(segment.source, segment.Offset, segment.Count);

        public static bool operator ==(in ListSegment<T> a, in ListSegment<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ListSegment<T> a, in ListSegment<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReadList<T> source;
            private readonly int start;
            private readonly int end;
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

            void IEnumerator.Reset()
            {
                this.current = this.start - 1;
            }

            public void Dispose()
            {
            }
        }
    }
}