namespace System.Collections.Generic
{
    public readonly struct ReadSegment1<T> : IReadSegment<T>, IEquatableReadOnlyStruct<ReadSegment1<T>>
    {
        public bool HasSource { get; }

        private readonly T source;

        public ReadSegment1(T source)
        {
            this.source = source;
            this.HasSource = true;
            this.Count = 1;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException(nameof(index));

                return this.source;
            }
        }

        public int Count { get; }

        public override int GetHashCode()
        {
            var hashCode = -989046110;
            hashCode = hashCode * -1521134295 + this.HasSource.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.source);
            hashCode = hashCode * -1521134295 + this.Count.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is ReadSegment1<T> other && Equals(in other);

        public bool Equals(ReadSegment1<T> other)
            => this.HasSource == other.HasSource && Equals(this.source, other.source) &&
               this.Count == other.Count;

        public bool Equals(in ReadSegment1<T> other)
            => this.HasSource == other.HasSource && Equals(this.source, other.source) &&
               this.Count == other.Count;

        public bool Contains(T item)
            => this.HasSource && Equals(this.source, item);

        public ReadSegment1<T> Slice(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new IndexOutOfRangeException(nameof(index));

            if (this.Count == 0)
                return new ReadSegment1<T>();

            return new ReadSegment1<T>(this.source);
        }

        public ReadSegment1<T> Slice(int index, int count)
        {
            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.Count == 0)
                return new ReadSegment1<T>();

            return new ReadSegment1<T>(this.source);
        }

        public ReadSegment1<T> Skip(int count)
        {
            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == this.Count)
                return new ReadSegment1<T>();

            return new ReadSegment1<T>(this.source);
        }

        public ReadSegment1<T> SkipLast(int count)
        {
            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == this.Count)
                return new ReadSegment1<T>();

            return new ReadSegment1<T>(this.source);
        }

        public ReadSegment1<T> Take(int count)
        {
            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == 0)
                return new ReadSegment1<T>();

            return new ReadSegment1<T>(this.source);
        }

        public ReadSegment1<T> TakeLast(int count)
        {
            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == 0)
                return new ReadSegment1<T>();

            return new ReadSegment1<T>(this.source);
        }

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

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static bool operator ==(in ReadSegment1<T> a, in ReadSegment1<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadSegment1<T> a, in ReadSegment1<T> b)
            => !a.Equals(in b);

        public static implicit operator ReadSegment1<T>(T source)
            => new ReadSegment1<T>(source);

        public static implicit operator ReadSegment<T>(in ReadSegment1<T> segment)
            => new ReadSegment<T>(segment);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReadSegment1<T> segment;
            private int current;

            public Enumerator(in ReadSegment1<T> segment)
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