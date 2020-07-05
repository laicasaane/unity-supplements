namespace System.Collections.Generic
{
    public readonly struct Segment1<T> : ISegment<T>, IEquatableReadOnlyStruct<Segment1<T>>
    {
        public static bool operator ==(in Segment1<T> a, in Segment1<T> b)
            => a.Equals(in b);

        public static bool operator !=(in Segment1<T> a, in Segment1<T> b)
            => !a.Equals(in b);

        public static implicit operator Segment1<T>(T source)
            => new Segment1<T>(source);

        public static implicit operator Segment<T>(in Segment1<T> segment)
            => new Segment<T>(segment);

        public Segment1(T item)
        {
            this.item = item;
            this.Count = 1;
        }

        private readonly T item;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw new IndexOutOfRangeException(nameof(index));

                return this.item;
            }
        }

        public int Count { get; }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash ^= this.item == null ? 0 : this.item.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
            => obj is Segment1<T> other && Equals(in other);

        public bool Equals(Segment1<T> other)
        {
            if (this.item == null && other.item == null)
                return true;

            if (this.item == null || other.item == null)
                return false;

            return this.item.Equals(other.item);
        }

        public bool Equals(in Segment1<T> other)
        {
            if (this.item == null && other.item == null)
                return true;

            if (this.item == null || other.item == null)
                return false;

            return this.item.Equals(other.item);
        }

        public Segment1<T> Slice(int index)
        {
            if (index < 0 || index >= this.Count)
                throw new IndexOutOfRangeException(nameof(index));

            if (this.Count == 0)
                return new Segment1<T>();

            return new Segment1<T>(this.item);
        }

        public Segment1<T> Slice(int index, int count)
        {
            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.Count == 0)
                return new Segment1<T>();

            return new Segment1<T>(this.item);
        }

        public Segment1<T> Skip(int count)
        {
            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == this.Count)
                return new Segment1<T>();

            return new Segment1<T>(this.item);
        }

        public Segment1<T> SkipLast(int count)
        {
            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == this.Count)
                return new Segment1<T>();

            return new Segment1<T>(this.item);
        }

        public Segment1<T> Take(int count)
        {
            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == 0)
                return new Segment1<T>();

            return new Segment1<T>(this.item);
        }

        public Segment1<T> TakeLast(int count)
        {
            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            if (count == 0)
                return new Segment1<T>();

            return new Segment1<T>(this.item);
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

                    return this.segment.item;
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