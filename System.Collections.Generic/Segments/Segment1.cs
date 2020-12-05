namespace System.Collections.Generic
{
    public readonly struct Segment1<T> : ISegment<T>, IEquatableReadOnlyStruct<Segment1<T>>
    {

        private readonly T source;
        private readonly bool hasSource;
        private readonly int count;

        public bool HasSource => this.hasSource;

        public int Count => this.count;

        public Segment1(T source)
        {
            this.source = source;
            this.hasSource = true;
            this.count = 1;
        }

        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)this.count)
                    throw new IndexOutOfRangeException(nameof(index));

                return this.source;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -989046110;
            hashCode = hashCode * -1521134295 + this.hasSource.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.source);
            hashCode = hashCode * -1521134295 + this.count.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is Segment1<T> other && Equals(in other);

        public bool Equals(Segment1<T> other)
            => this.hasSource == other.HasSource && Equals(this.source, other.source) &&
               this.count == other.Count;

        public bool Equals(in Segment1<T> other)
            => this.hasSource == other.HasSource && Equals(this.source, other.source) &&
               this.count == other.Count;

        public Segment1<T> Slice(int index)
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException(nameof(index));

            if (this.count == 0)
                return new Segment1<T>();

            return new Segment1<T>(this.source);
        }

        public Segment1<T> Slice(int index, int count)
        {
            if ((uint)index > (uint)this.count || (uint)count > (uint)(this.count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == 0)
                return new Segment1<T>();

            return new Segment1<T>(this.source);
        }

        public Segment1<T> Skip(int count)
        {
            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == this.count)
                return new Segment1<T>();

            return new Segment1<T>(this.source);
        }

        public Segment1<T> SkipLast(int count)
        {
            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == this.count)
                return new Segment1<T>();

            return new Segment1<T>(this.source);
        }

        public Segment1<T> Take(int count)
        {
            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == 0)
                return new Segment1<T>();

            return new Segment1<T>(this.source);
        }

        public Segment1<T> TakeLast(int count)
        {
            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == 0)
                return new Segment1<T>();

            return new Segment1<T>(this.source);
        }

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

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static bool operator ==(in Segment1<T> a, in Segment1<T> b)
            => a.Equals(in b);

        public static bool operator !=(in Segment1<T> a, in Segment1<T> b)
            => !a.Equals(in b);

        public static implicit operator Segment1<T>(T source)
            => new Segment1<T>(source);

        public static implicit operator Segment<T>(in Segment1<T> segment)
            => new Segment<T>(segment);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly Segment1<T> segment;
            private int current;

            public Enumerator(in Segment1<T> segment)
            {
                this.segment = segment;
                this.current = -1;
            }

            public bool MoveNext()
            {
                if (this.segment.Count <= 0)
                    return false;

                if (this.current < 0)
                {
                    this.current = 0;
                    return true;
                }

                return false;
            }

            public T Current
            {
                get
                {
                    if (this.current < 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.current > 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.segment.source;
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