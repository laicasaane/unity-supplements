namespace System.Collections.Generic
{
    public readonly partial struct ReadListSegment<T> : IReadSegment<T>, IEquatableReadOnlyStruct<ReadListSegment<T>>
    {
        private readonly ReadList<T> source;

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

        public ReadListSegment(in ReadList<T> source)
        {
            this.source = source;
            this.HasSource = true;
            this.Offset = 0;
            this.Count = this.source.Count;
        }

        public ReadListSegment(in ReadList<T> source, int offset, int count)
        {
            this.source = source;

            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if ((uint)offset > (uint)this.source.Count || (uint)count > (uint)(this.source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(this.source, offset, count);

            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        public ReadListSegment<T> Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ReadListSegment<T>(this.source, this.Offset + index, this.Count - index);
        }

        public ReadListSegment<T> Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ReadListSegment<T>(this.source, this.Offset + index, count);
        }

        public ReadListSegment<T> Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ReadListSegment<T>(this.source, this.Offset + count, this.Count - count);
        }

        public ReadListSegment<T> Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ReadListSegment<T>(this.source, this.Offset, count);
        }

        public ReadListSegment<T> TakeLast(int count)
            => Skip(this.Count - count);

        public ReadListSegment<T> SkipLast(int count)
            => Take(this.Count - count);

        IReadSegment<T> IReadSegment<T>.Slice(int index)
            => Slice(index);

        IReadSegment<T> IReadSegment<T>.Slice(int index, int count)
            => Slice(index, count);

        IReadSegment<T> IReadSegment<T>.Skip(int count)
            => Skip(count);

        IReadSegment<T> IReadSegment<T>.Take(int count)
            => Take(count);

        IReadSegment<T> IReadSegment<T>.TakeLast(int count)
            => TakeLast(count);

        IReadSegment<T> IReadSegment<T>.SkipLast(int count)
            => SkipLast(count);

        public T[] ToArray()
        {
            if (!this.HasSource || this.Count == 0)
                return new T[0];

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

        public override bool Equals(object obj)
            => obj is ReadListSegment<T> other && Equals(in other);

        public bool Equals(ReadListSegment<T> other)
            => this.HasSource == other.HasSource && this.source.Equals(in other.source) &&
               other.Count == this.Count && other.Offset == this.Offset;

        public bool Equals(in ReadListSegment<T> other)
            => this.HasSource == other.HasSource && this.source.Equals(in other.source) &&
               other.Count == this.Count && other.Offset == this.Offset;

        public override int GetHashCode()
        {
            var hashCode = 1328453276;
            hashCode = hashCode * -1521134295 + this.HasSource.GetHashCode();
            hashCode = hashCode * -1521134295 + this.source.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Offset.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Count.GetHashCode();
            return hashCode;
        }

        public static ReadListSegment<T> Empty { get; } = new ReadListSegment<T>();

        public static implicit operator ReadListSegment<T>(List<T> source)
            => new ReadListSegment<T>(source.AsReadList());

        public static implicit operator ReadListSegment<T>(in ReadList<T> source)
            => new ReadListSegment<T>(source);

        public static implicit operator ReadSegment<T>(in ReadListSegment<T> segment)
            => new ReadSegment<T>(segment.source, segment.Offset, segment.Count);

        public static bool operator ==(in ReadListSegment<T> a, in ReadListSegment<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadListSegment<T> a, in ReadListSegment<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReadList<T> source;
            private readonly int start;
            private readonly int end;
            private int current;

            internal Enumerator(in ReadListSegment<T> segment)
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