using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly partial struct Array1Segment<T> : ISegment<T>, IEquatableReadOnlyStruct<Array1Segment<T>>
    {
        private readonly T[] source;

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

        public Array1Segment(T[] source)
        {
            this.source = source ?? _empty;
            this.HasSource = true;
            this.Offset = 0;
            this.Count = this.source.Length;
        }

        public Array1Segment(T[] source, int offset, int count)
        {
            this.source = source ?? _empty;

            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if ((uint)offset > (uint)this.source.Length || (uint)count > (uint)(this.source.Length - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(this.source, offset, count);

            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal T[] GetSource()
            => this.HasSource ? (this.source ?? _empty) : _empty;

        public Array1Segment<T> Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new Array1Segment<T>(this.source, this.Offset + index, this.Count - index);
        }

        public Array1Segment<T> Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new Array1Segment<T>(this.source, this.Offset + index, count);
        }

        public Array1Segment<T> Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new Array1Segment<T>(this.source, this.Offset + count, this.Count - count);
        }

        public Array1Segment<T> Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new Array1Segment<T>(this.source, this.Offset, count);
        }

        public Array1Segment<T> TakeLast(int count)
            => Skip(this.Count - count);

        public Array1Segment<T> SkipLast(int count)
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
            => obj is Array1Segment<T> other && Equals(in other);

        public bool Equals(Array1Segment<T> other)
            => this.HasSource == other.HasSource && GetSource().Equals(other.GetSource()) &&
               other.Count == this.Count && other.Offset == this.Offset;

        public bool Equals(in Array1Segment<T> other)
            => this.HasSource == other.HasSource && GetSource().Equals(other.GetSource()) &&
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

        private static readonly T[] _empty = ReadArray1<T>.Empty.GetSource();

        public static Array1Segment<T> Empty { get; } = new Array1Segment<T>(_empty);

        public static implicit operator Array1Segment<T>(T[] source)
            => new Array1Segment<T>(source);

        public static implicit operator Segment<T>(in Array1Segment<T> segment)
            => new Segment<T>(segment.GetSource(), segment.Offset, segment.Count);

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