namespace System.Collections.Generic
{
    public readonly partial struct ReadArray1Segment<T> : IReadSegment<T>, IEquatableReadOnlyStruct<ReadArray1Segment<T>>
    {
        private readonly ReadArray1<T> source;

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

        public ReadArray1Segment(in ReadArray1<T> source)
        {
            this.source = source;
            this.HasSource = true;
            this.Offset = 0;
            this.Count = this.source.Length;
        }

        public ReadArray1Segment(in ReadArray1<T> source, int offset, int count)
        {
            this.source = source;

            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if ((uint)offset > (uint)this.source.Length || (uint)count > (uint)(this.source.Length - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(this.source, offset, count);

            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        public ReadArray1Segment<T> Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ReadArray1Segment<T>(this.source, this.Offset + index, this.Count - index);
        }

        public ReadArray1Segment<T> Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ReadArray1Segment<T>(this.source, this.Offset + index, count);
        }

        public ReadArray1Segment<T> Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ReadArray1Segment<T>(this.source, this.Offset + count, this.Count - count);
        }

        public ReadArray1Segment<T> Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ReadArray1Segment<T>(this.source, this.Offset, count);
        }

        public ReadArray1Segment<T> TakeLast(int count)
            => Skip(this.Count - count);

        public ReadArray1Segment<T> SkipLast(int count)
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
            if (this.HasSource || this.Count == 0)
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
            => obj is ReadArray1Segment<T> other && Equals(in other);

        public bool Equals(ReadArray1Segment<T> other)
            => this.HasSource == other.HasSource && this.source.Equals(in other.source) &&
               other.Count == this.Count && other.Offset == this.Offset;

        public bool Equals(in ReadArray1Segment<T> other)
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

        public static ReadArray1Segment<T> Empty { get; } = new ReadArray1Segment<T>();

        public static implicit operator ReadArray1Segment<T>(T[] source)
            => new ReadArray1Segment<T>(source.AsReadArray());

        public static implicit operator ReadArray1Segment<T>(in ReadArray1<T> source)
            => new ReadArray1Segment<T>(source);

        public static implicit operator ReadSegment<T>(in ReadArray1Segment<T> segment)
            => new ReadSegment<T>(segment.source, segment.Offset, segment.Count);

        public static bool operator ==(in ReadArray1Segment<T> a, in ReadArray1Segment<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadArray1Segment<T> a, in ReadArray1Segment<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReadArray1<T> source;
            private readonly int start;
            private readonly int end;
            private int current;

            internal Enumerator(in ReadArray1Segment<T> segment)
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