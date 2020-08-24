using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly partial struct ReadSegment<T> : IReadSegment<T>, IEquatableReadOnlyStruct<ReadSegment<T>>
    {
        private readonly IReadSegmentSource<T> source;

        public bool HasSource { get; }

        public int Offset { get; }

        public int Count { get; }

        public T this[int index]
            => GetSource()[this.Offset + index];

        public ReadSegment(in ReadArray1<T> source)
        {
            this.source = source == null ? _empty : new ReadArray1Source(source);
            this.HasSource = true;
            this.Offset = 0;
            this.Count = this.source.Count;
        }

        public ReadSegment(in ReadArray1<T> source, int offset, int count)
        {
            this.source = source == null ? _empty : new ReadArray1Source(source);

            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if ((uint)offset > (uint)this.source.Count || (uint)count > (uint)(this.source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(this.source, offset, count);

            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        public ReadSegment(IReadSegmentSource<T> source)
        {
            this.source = source ?? _empty;
            this.HasSource = true;
            this.Offset = 0;
            this.Count = source.Count;
        }

        public ReadSegment(IReadSegmentSource<T> source, int offset, int count)
        {
            if (source == null || (uint)offset > (uint)source.Count || (uint)count > (uint)(source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(source, offset, count);

            this.source = source;
            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal IReadSegmentSource<T> GetSource()
            => this.HasSource ? (this.source ?? _empty) : _empty;

        public ReadSegment<T> Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ReadSegment<T>(this.source, this.Offset + index, this.Count - index);
        }

        public ReadSegment<T> Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new ReadSegment<T>(this.source, this.Offset + index, count);
        }

        public ReadSegment<T> Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ReadSegment<T>(this.source, this.Offset + count, this.Count - count);
        }

        public ReadSegment<T> Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new ReadSegment<T>(this.source, this.Offset, count);
        }

        public ReadSegment<T> TakeLast(int count)
            => Skip(this.Count - count);

        public ReadSegment<T> SkipLast(int count)
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
            var source = GetSource();
            var array = new T[this.Count];
            var count = this.Count + this.Offset;

            for (int i = this.Offset, j = 0; i < count; i++, j++)
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

            var count = this.Count + this.Offset;

            for (var i = this.Offset; i < count; i++)
            {
                if (source[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }

            return index >= 0 ? index - this.Offset : -1;
        }

        public bool Contains(T item)
        {
            var source = GetSource();
            var count = this.Count + this.Offset;

            for (var i = this.Offset; i < count; i++)
            {
                if (source[i].Equals(item))
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
            => obj is ReadSegment<T> other && Equals(in other);

        public bool Equals(ReadSegment<T> other)
            => this.HasSource == other.HasSource && this.source.Equals(other) &&
               this.Count == other.Count && this.Offset == other.Offset;

        public bool Equals(in ReadSegment<T> other)
            => this.HasSource == other.HasSource && this.source.Equals(other) &&
               this.Count == other.Count && this.Offset == other.Offset;

        public override int GetHashCode()
        {
            var hashCode = 1328453276;
            hashCode = hashCode * -1521134295 + this.HasSource.GetHashCode();
            hashCode = hashCode * -1521134295 + GetSource().GetHashCode();
            hashCode = hashCode * -1521134295 + this.Offset.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Count.GetHashCode();
            return hashCode;
        }

        private static ReadArray1Source _empty { get; } = new ReadArray1Source(ReadArray1<T>.Empty);

        public static ReadSegment<T> Empty { get; } = new ReadSegment<T>(_empty);

        public static implicit operator ReadSegment<T>(T[] source)
            => new ReadSegment<T>(source.AsReadArray());

        public static implicit operator ReadSegment<T>(in ReadArray1<T> source)
            => new ReadSegment<T>(source);

        public static bool operator ==(in ReadSegment<T> a, in ReadSegment<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadSegment<T> a, in ReadSegment<T> b)
            => !a.Equals(in b);
    }
}