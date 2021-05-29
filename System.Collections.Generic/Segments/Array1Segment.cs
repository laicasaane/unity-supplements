namespace System.Collections.Generic
{
    public readonly partial struct Array1Segment<T> : ISegment<T>, IEquatableReadOnlyStruct<Array1Segment<T>>
    {
        private readonly ReadArray1<T> source;
        private readonly bool hasSource;
        private readonly int count;
        private readonly int offset;

        public bool HasSource => this.hasSource;

        public int Offset => this.offset;

        public int Count => this.count;

        public ref readonly T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)this.count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.source[this.offset + index];
            }
        }

        T IReadOnlyList<T>.this[int index]
        {
            get
            {
                if ((uint)index >= (uint)this.count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return this.source[this.offset + index];
            }
        }

        public Array1Segment(in ReadArray1<T> source)
        {
            this.source = source;
            this.hasSource = true;
            this.offset = 0;
            this.count = this.source.Length;
        }

        public Array1Segment(in ReadArray1<T> source, int offset, int count)
        {
            this.source = source;

            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if ((uint)offset > (uint)this.source.Length || (uint)count > (uint)(this.source.Length - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(this.source, offset, count);

            this.hasSource = true;
            this.offset = offset;
            this.count = count;
        }

        public Array1Segment<T> Slice(int index)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)index > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new Array1Segment<T>(this.source, this.offset + index, this.count - index);
        }

        public Array1Segment<T> Slice(int index, int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)index > (uint)this.count || (uint)count > (uint)(this.count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new Array1Segment<T>(this.source, this.offset + index, count);
        }

        public Array1Segment<T> Skip(int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new Array1Segment<T>(this.source, this.offset + count, this.count - count);
        }

        public Array1Segment<T> Take(int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new Array1Segment<T>(this.source, this.offset, count);
        }

        public Array1Segment<T> TakeLast(int count)
            => Skip(this.count - count);

        public Array1Segment<T> SkipLast(int count)
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
            if (this.hasSource || this.count == 0)
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
            => obj is Array1Segment<T> other && Equals(in other);

        public bool Equals(Array1Segment<T> other)
            => this.hasSource == other.HasSource && this.source.Equals(in other.source) &&
               other.Count == this.count && other.Offset == this.offset;

        public bool Equals(in Array1Segment<T> other)
            => this.hasSource == other.HasSource && this.source.Equals(in other.source) &&
               other.Count == this.count && other.Offset == this.offset;

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.hasSource, this.source, this.offset, this.count);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            var hashCode = 1328453276;
            hashCode = hashCode * -1521134295 + this.hasSource.GetHashCode();
            hashCode = hashCode * -1521134295 + this.source.GetHashCode();
            hashCode = hashCode * -1521134295 + this.offset.GetHashCode();
            hashCode = hashCode * -1521134295 + this.count.GetHashCode();
            return hashCode;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public static Array1Segment<T> Empty { get; } = new Array1Segment<T>();

        public static implicit operator Array1Segment<T>(T[] source)
            => new Array1Segment<T>(source.AsReadArray());

        public static implicit operator Array1Segment<T>(in ReadArray1<T> source)
            => new Array1Segment<T>(source);

        public static implicit operator Segment<T>(in Array1Segment<T> segment)
            => new Segment<T>(segment.source, segment.Offset, segment.Count);

        public static bool operator ==(in Array1Segment<T> a, in Array1Segment<T> b)
            => a.Equals(in b);

        public static bool operator !=(in Array1Segment<T> a, in Array1Segment<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReadArray1<T> source;
            private readonly int start;
            private readonly int end;
            private int current;

            internal Enumerator(in Array1Segment<T> segment)
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