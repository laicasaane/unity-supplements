using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct IntRange : IEquatableReadOnlyStruct<IntRange>, ISerializable
    {
        public readonly int Start;
        public readonly int End;

        public IntRange(int start, int end)
        {
            if (end < start)
                throw new InvalidOperationException($"{nameof(end)} must be greater than or equal to {nameof(start)}");

            this.Start = start;
            this.End = end;
        }

        private IntRange(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Start = info.GetInt32(nameof(this.Start));
            }
            catch
            {
                this.Start = default;
            }

            try
            {
                this.End = info.GetInt32(nameof(this.End));
            }
            catch
            {
                this.End = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
        }

        public void Deconstruct(out int start, out int end)
        {
            start = this.Start;
            end = this.End;
        }

        public IntRange With(in int? Start = null, in int? End = null)
            => new IntRange(
                Start ?? this.Start,
                End ?? this.End
            );

        public override bool Equals(object obj)
            => obj is IntRange other &&
               this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(in IntRange other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(IntRange other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public override int GetHashCode()
        {
            var hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            return hashCode;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        /// <summary>
        /// Automatically create a range from (a, b).
        /// If a <= b, then a is the start value, and b is the end value.
        /// Otherwise, they are swapped.
        /// </summary>
        public static IntRange Auto(int a, int b)
            => a > b ? new IntRange(b, a) : new IntRange(a, b);

        public static IntRange Size(int size)
            => new IntRange(0, Math.Abs(size - 1));

        public static implicit operator IntRange(in (int start, int end) value)
            => new IntRange(value.start, value.end);

        public static implicit operator ReadRange<int, Enumerator>(in IntRange value)
            => new ReadRange<int, Enumerator>(value.Start, value.End);

        public static implicit operator ReadRange<int>(in IntRange value)
            => new ReadRange<int>(value.Start, value.End, new Enumerator());

        public static implicit operator IntRange(in ReadRange<int> value)
            => new IntRange(value.Start, value.End);

        public static implicit operator IntRange(in ReadRange<int, Enumerator> value)
            => new IntRange(value.Start, value.End);

        public static bool operator ==(in IntRange lhs, in IntRange rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in IntRange lhs, in IntRange rhs)
            => !lhs.Equals(in rhs);

        public struct Enumerator : IEnumerator<int>, IRangeEnumerator<int>
        {
            private readonly IntRange range;

            private int current;

            public Enumerator(in IntRange range)
            {
                this.range = range;
                this.current = range.Start - 1;
            }

            public bool MoveNext()
            {
                if (this.current <= this.range.End)
                {
                    this.current++;
                    return this.current <= this.range.End;
                }

                return false;
            }

            public int Current
            {
                get
                {
                    if (this.current < this.range.Start)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.current > this.range.End)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.current;
                }
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current
                => this.Current;

            void IEnumerator.Reset()
            {
                this.current = this.range.Start - 1;
            }

            public IEnumerator<int> Enumerate(int start, int end)
            {
                for (var i = start; i <= end; i++)
                {
                    yield return i;
                }
            }
        }
    }
}
