using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct IntRange : IRange<int, IntRange.Enumerator>, IEquatableReadOnlyStruct<IntRange>, ISerializable
    {
        public int Start { get; }

        public int End { get; }

        public bool IsFromEnd { get; }

        public IntRange(int start, int end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public IntRange(int start, int end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private IntRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetInt32OrDefault(nameof(this.Start));
            this.End = info.GetInt32OrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out int start, out int end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out int start, out int end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public IntRange With(in int? Start = null, in int? End = null, bool? IsFromEnd = null)
            => new IntRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public IntRange FromStart()
            => new IntRange(this.Start, this.End, false);

        public IntRange FromEnd()
            => new IntRange(this.Start, this.End, true);

        IRange<int> IRange<int>.FromStart()
            => FromStart();

        IRange<int> IRange<int>.FromEnd()
            => FromEnd();

        public int Count()
        {
            if (this.End > this.Start)
                return this.End - this.Start + 1;

            return this.Start - this.End + 1;
        }

        public bool Contains(int value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is IntRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in IntRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(IntRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.Start, this.End, this.IsFromEnd);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            var hashCode = -1418356749;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
            return hashCode;
#pragma warning restore CS0162 // Unreachable code detected
        }

        public override string ToString()
            => $"{{ {nameof(this.Start)}={this.Start}, {nameof(this.End)}={this.End}, {nameof(this.IsFromEnd)}={this.IsFromEnd} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public Enumerator Range()
            => GetEnumerator();

        IEnumerator<int> IRange<int>.Range()
            => GetEnumerator();

        public IntRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static IntRange Normal(int a, int b)
            => a > b ? new IntRange(b, a) : new IntRange(a, b);

        /// <summary>
        /// Create a range from a size which is greater than 0
        /// </summary>
        /// <exception cref="InvalidOperationException">Size must be greater than 0</exception>
        public static IntRange FromSize(int value, bool fromEnd = false)
        {
            if (value <= 0)
                throw new InvalidOperationException("Size must be greater than 0");

            return new IntRange(0, Math.Max(value - 1, 0), fromEnd);
        }

        public static IntRange FromStart(int start, int end)
            => new IntRange(start, end, false);

        public static IntRange FromEnd(int start, int end)
            => new IntRange(start, end, true);

        public static implicit operator IntRange(in (int start, int end) value)
            => new IntRange(value.start, value.end);

        public static implicit operator IntRange(in (int start, int end, bool fromEnd) value)
            => new IntRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<int, Enumerator.Default>(in IntRange value)
            => new ReadRange<int, Enumerator.Default>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<int>(in IntRange value)
            => new ReadRange<int>(value.Start, value.End, value.IsFromEnd, new Enumerator.Default());

        public static implicit operator IntRange(in ReadRange<int> value)
            => new IntRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator IntRange(in ReadRange<int, Enumerator.Default> value)
            => new IntRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in IntRange lhs, in IntRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in IntRange lhs, in IntRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<int>
        {
            private readonly int start;
            private readonly int end;
            private readonly sbyte sign;

            private int current;
            private sbyte flag;

            public Enumerator(in IntRange range)
                : this(range.Start, range.End, range.IsFromEnd)
            { }

            public Enumerator(int start, int end, bool fromEnd)
            {
                var increasing = start <= end;

                if (fromEnd)
                {
                    this.start = end;
                    this.end = start;
                }
                else
                {
                    this.start = start;
                    this.end = end;
                }

                this.sign = (sbyte)(increasing
                            ? (fromEnd ? -1 : 1)
                            : (fromEnd ? 1 : -1));

                this.current = this.start;
                this.flag = -1;
            }

            public bool MoveNext()
            {
                if (this.flag == 0)
                {
                    if (this.current == this.end)
                    {
                        this.flag = 1;
                        return false;
                    }

                    this.current += this.sign;
                    return true;
                }

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
            }

            public int Current
            {
                get
                {
                    if (this.flag < 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.flag > 0)
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
                this.current = this.start;
                this.flag = -1;
            }

            public readonly struct Default : IRangeEnumerator<int>
            {
                public IEnumerator<int> Enumerate(int start, int end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd);
            }
        }
    }
}
