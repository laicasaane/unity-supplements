using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly partial struct Segment<T> : ISegment<T>, IEquatableReadOnlyStruct<Segment<T>>
    {
        private readonly ISegmentSource<T> source;
        private readonly bool hasSource;
        private readonly int count;
        private readonly int offset;

        public bool HasSource => this.hasSource;

        public int Offset => this.offset;

        public int Count => this.count;

        public T this[int index]
            => GetSource()[this.offset + index];

        public Segment(in ReadArray1<T> source)
        {
            this.source = source == null ? _empty : new Array1Source(source);
            this.hasSource = true;
            this.offset = 0;
            this.count = this.source.Count;
        }

        public Segment(in ReadArray1<T> source, int offset, int count)
        {
            this.source = source == null ? _empty : new Array1Source(source);

            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if ((uint)offset > (uint)this.source.Count || (uint)count > (uint)(this.source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(this.source, offset, count);

            this.hasSource = true;
            this.offset = offset;
            this.count = count;
        }

        public Segment(ISegmentSource<T> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
            this.offset = 0;
            this.count = source.Count;
        }

        public Segment(ISegmentSource<T> source, int offset, int count)
        {
            if (source == null || (uint)offset > (uint)source.Count || (uint)count > (uint)(source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(source, offset, count);

            this.source = source;
            this.hasSource = true;
            this.offset = offset;
            this.count = count;
        }

        public Segment<T> Slice(int index)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)index > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new Segment<T>(this.source, this.offset + index, this.count - index);
        }

        public Segment<T> Slice(int index, int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)index > (uint)this.count || (uint)count > (uint)(this.count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new Segment<T>(this.source, this.offset + index, count);
        }

        public Segment<T> Skip(int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new Segment<T>(this.source, this.offset + count, this.count - count);
        }

        public Segment<T> Take(int count)
        {
            if (!this.hasSource)
                return Empty;

            if ((uint)count > (uint)this.count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new Segment<T>(this.source, this.offset, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ISegmentSource<T> GetSource()
            => this.hasSource ? this.source : _empty;

        public Segment<T> TakeLast(int count)
            => Skip(this.count - count);

        public Segment<T> SkipLast(int count)
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
            var source = GetSource();
            var array = new T[this.count];
            var count = this.count + this.offset;

            for (int i = this.offset, j = 0; i < count; i++, j++)
            {
                array[j] = source[i];
            }

            return array;
        }

        public int IndexOf(T item)
        {
            var index = -1;

            var source = GetSource();

            if (source.Count <= 0)
                return index;

            var count = this.count + this.offset;

            for (var i = this.offset; i < count; i++)
            {
                if (source[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }

            return index >= 0 ? index - this.offset : -1;
        }

        public bool Contains(T item)
        {
            var source = GetSource();
            var count = this.count + this.offset;

            for (var i = this.offset; i < count; i++)
            {
                if (source[i].Equals(item))
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
            => obj is Segment<T> other && Equals(in other);

        public bool Equals(Segment<T> other)
            => this.hasSource == other.HasSource && this.source.Equals(other) &&
               this.count == other.Count && this.offset == other.Offset;

        public bool Equals(in Segment<T> other)
            => this.hasSource == other.HasSource && this.source.Equals(other) &&
               this.count == other.Count && this.offset == other.Offset;

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.hasSource, GetSource(), this.offset, this.count);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            var hashCode = 1328453276;
            hashCode = hashCode * -1521134295 + this.hasSource.GetHashCode();
            hashCode = hashCode * -1521134295 + GetSource().GetHashCode();
            hashCode = hashCode * -1521134295 + this.offset.GetHashCode();
            hashCode = hashCode * -1521134295 + this.count.GetHashCode();
            return hashCode;
#pragma warning restore CS0162 // Unreachable code detected
        }

        private static Array1Source _empty { get; } = new Array1Source(new T[0]);

        public static Segment<T> Empty { get; } = new Segment<T>(_empty);

        public static implicit operator Segment<T>(T[] source)
            => new Segment<T>(source.AsReadArray());

        public static implicit operator Segment<T>(in ReadArray1<T> source)
            => new Segment<T>(source);

        public static bool operator ==(in Segment<T> a, in Segment<T> b)
            => a.Equals(in b);

        public static bool operator !=(in Segment<T> a, in Segment<T> b)
            => !a.Equals(in b);
    }
}