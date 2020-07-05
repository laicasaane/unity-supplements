namespace System.Collections.Generic
{
    public readonly partial struct Segment<T> :
        ISegment<T>,
        IEquatableReadOnlyStruct<Segment<T>>,
        IReadOnlyStructEqualityComparer<Segment<T>>
    {
        private readonly ISegmentSource<T> source;

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

        public Segment(T[] source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.source = new ArraySource(source);
            this.HasSource = true;
            this.Offset = 0;
            this.Count = this.source.Count;
        }

        public Segment(T[] source, int offset, int count)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.source = new ArraySource(source);

            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if ((uint)offset > (uint)this.source.Count || (uint)count > (uint)(this.source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(this.source, offset, count);

            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        public Segment(ISegmentSource<T> source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.HasSource = true;
            this.Offset = 0;
            this.Count = source.Count;
        }

        public Segment(ISegmentSource<T> source, int offset, int count)
        {
            if (source == null || (uint)offset > (uint)source.Count || (uint)count > (uint)(source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(source, offset, count);

            this.source = source;
            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        public Segment<T> Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new Segment<T>(this.source, this.Offset + index, this.Count - index);
        }

        public Segment<T> Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new Segment<T>(this.source, this.Offset + index, count);
        }

        public Segment<T> Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new Segment<T>(this.source, this.Offset + count, this.Count - count);
        }

        public Segment<T> Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new Segment<T>(this.source, this.Offset, count);
        }

        public Segment<T> TakeLast(int count)
            => Skip(this.Count - count);

        public Segment<T> SkipLast(int count)
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
            => obj is Segment<T> other && Equals(in other);

        public bool Equals(Segment<T> other)
        {
            if (!this.HasSource)
                return !other.HasSource || ReferenceEquals(other.source, _empty);

            return ReferenceEquals(this.source, other.source) &&
                   other.Offset == this.Offset &&
                   other.Count == this.Count;
        }

        public bool Equals(in Segment<T> other)
        {
            if (!this.HasSource)
                return !other.HasSource || ReferenceEquals(other.source, _empty);

            return ReferenceEquals(this.source, other.source) &&
                   other.Offset == this.Offset &&
                   other.Count == this.Count;
        }

        public bool Equals(Segment<T> x, Segment<T> y)
            => x.Equals(in y);

        public int GetHashCode(Segment<T> obj)
            => obj.GetHashCode();

        public bool Equals(in Segment<T> x, in Segment<T> y)
            => x.Equals(in y);

        public int GetHashCode(in Segment<T> obj)
            => obj.GetHashCode();

        private static T[] _empty { get; } = new T[0];

        public static Segment<T> Empty { get; } = new Segment<T>(_empty);

        public static implicit operator Segment<T>(T[] source)
            => source == null ? Empty : new Segment<T>(source);

        public static bool operator ==(in Segment<T> a, in Segment<T> b)
            => a.Equals(in b);

        public static bool operator !=(in Segment<T> a, in Segment<T> b)
            => !a.Equals(in b);
    }
}