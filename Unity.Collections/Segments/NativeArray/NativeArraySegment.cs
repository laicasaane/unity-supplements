﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.Collections
{
    public readonly partial struct NativeArraySegment<T> : ISegment<T>, IEquatableReadOnlyStruct<NativeArraySegment<T>>
        where T : struct
    {
        private readonly ReadNativeArray<T> source;

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

        public NativeArraySegment(in ReadNativeArray<T> source)
        {
            this.source = source;
            this.HasSource = true;
            this.Offset = 0;
            this.Count = this.source.Length;
        }

        public NativeArraySegment(in ReadNativeArray<T> source, int offset, int count)
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

        public NativeArraySegment<T> Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new NativeArraySegment<T>(this.source, this.Offset + index, this.Count - index);
        }

        public NativeArraySegment<T> Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new NativeArraySegment<T>(this.source, this.Offset + index, count);
        }

        public NativeArraySegment<T> Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new NativeArraySegment<T>(this.source, this.Offset + count, this.Count - count);
        }

        public NativeArraySegment<T> Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new NativeArraySegment<T>(this.source, this.Offset, count);
        }

        public NativeArraySegment<T> TakeLast(int count)
            => Skip(this.Count - count);

        public NativeArraySegment<T> SkipLast(int count)
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

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.Offset, this.Count, this.source);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ this.Offset;
                hash = (hash * 16777619) ^ this.Count;
                hash ^= this.source.GetHashCode();

                return hash;
            }
#pragma warning restore CS0162 // Unreachable code detected
        }

        public override bool Equals(object obj)
            => obj is NativeArraySegment<T> other && Equals(in other);

        public bool Equals(NativeArraySegment<T> other)
        {
            return this.source.Equals(in other.source) &&
                   other.Offset == this.Offset &&
                   other.Count == this.Count;
        }

        public bool Equals(in NativeArraySegment<T> other)
        {
            return this.source.Equals(in other.source) &&
                   other.Offset == this.Offset &&
                   other.Count == this.Count;
        }

        public static NativeArraySegment<T> Empty { get; } = new NativeArraySegment<T>(ReadNativeArray<T>.Empty);

        public static implicit operator NativeArraySegment<T>(in NativeArray<T> source)
            => source.IsCreated ? new NativeArraySegment<T>(source) : Empty;

        public static implicit operator NativeArraySegment<T>(in ReadNativeArray<T> source)
            => new NativeArraySegment<T>(source);

        public static implicit operator Segment<T>(in NativeArraySegment<T> segment)
            => new Segment<T>(new NativeArraySource(segment.source.GetSource()), segment.Offset, segment.Count);

        public static bool operator ==(in NativeArraySegment<T> a, in NativeArraySegment<T> b)
            => a.Equals(in b);

        public static bool operator !=(in NativeArraySegment<T> a, in NativeArraySegment<T> b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReadNativeArray<T> source;
            private readonly int start;
            private readonly int end;
            private int current;

            internal Enumerator(in NativeArraySegment<T> segment)
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