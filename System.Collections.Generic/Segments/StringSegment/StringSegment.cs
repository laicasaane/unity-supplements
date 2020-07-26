using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly partial struct StringSegment : ISegment<char>, IEquatableReadOnlyStruct<StringSegment>
    {
        private readonly string source;

        public bool HasSource { get; }

        public int Offset { get; }

        public int Count { get; }

        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return this.source[this.Offset + index];
            }
        }

        public StringSegment(string source)
        {
            this.source = source ?? _empty;
            this.HasSource = true;
            this.Offset = 0;
            this.Count = this.source.Length;
        }

        public StringSegment(string source, int offset, int count)
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
        private string GetSource()
            => this.HasSource ? this.source : _empty;

        public StringSegment Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new StringSegment(this.source, this.Offset + index, this.Count - index);
        }

        public StringSegment Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            return new StringSegment(this.source, this.Offset + index, count);
        }

        public StringSegment Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new StringSegment(this.source, this.Offset + count, this.Count - count);
        }

        public StringSegment Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return new StringSegment(this.source, this.Offset, count);
        }

        public StringSegment TakeLast(int count)
            => Skip(this.Count - count);

        public StringSegment SkipLast(int count)
            => Take(this.Count - count);

        ISegment<char> ISegment<char>.Slice(int index)
            => Slice(index);

        ISegment<char> ISegment<char>.Slice(int index, int count)
            => Slice(index, count);

        ISegment<char> ISegment<char>.Skip(int count)
            => Skip(count);

        ISegment<char> ISegment<char>.Take(int count)
            => Take(count);

        ISegment<char> ISegment<char>.TakeLast(int count)
            => TakeLast(count);

        ISegment<char> ISegment<char>.SkipLast(int count)
            => SkipLast(count);

        public char[] ToArray()
        {
            if (!this.HasSource || this.Count == 0)
                return new char[0];

            var array = new char[this.Count];
            var count = this.Count + this.Offset;

            for (int i = this.Offset, j = 0; i < count; i++, j++)
            {
                array[j] = this.source[i];
            }

            return array;
        }

        public int IndexOf(char item)
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

        public bool Contains(char item)
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

        IEnumerator<char> IEnumerable<char>.GetEnumerator()
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
                hash ^= GetSource().GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
            => obj is StringSegment other && Equals(in other);

        public bool Equals(StringSegment other)
        {
            return GetSource().Equals(other.source) &&
                   other.Offset == this.Offset &&
                   other.Count == this.Count;
        }

        public bool Equals(in StringSegment other)
        {
            return GetSource().Equals(other.source) &&
                   other.Offset == this.Offset &&
                   other.Count == this.Count;
        }

        public override string ToString()
        {
            if (!this.HasSource || this.Count == 0)
                return new string(new char[0]);

            unsafe
            {
                fixed (char* p = this.source)
                {
                    return new string(p, this.Offset, this.Count);
                }
            }
        }

        private static string _empty { get; } = new string(new char[0]);

        public static StringSegment Empty { get; } = new StringSegment(_empty);

        public static implicit operator StringSegment(string source)
            => source == null ? Empty : new StringSegment(source);

        public static implicit operator Segment<char>(in StringSegment segment)
            => new Segment<char>(new CharSource(segment.GetSource()), segment.Offset, segment.Count);

        public static implicit operator string(in StringSegment segment)
            => segment.ToString();

        public static bool operator ==(in StringSegment a, in StringSegment b)
            => a.Equals(in b);

        public static bool operator !=(in StringSegment a, in StringSegment b)
            => !a.Equals(in b);

        public struct Enumerator : IEnumerator<char>
        {
            private readonly string source;
            private readonly int start;
            private readonly int end;
            private int current;

            internal Enumerator(in StringSegment segment)
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

            public char Current
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