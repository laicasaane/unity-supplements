using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct LongRange : IRange<long, LongRange.Enumerator>, IEquatableReadOnlyStruct<LongRange>, ISerializable
    {
        public long Start { get; }

        public long End { get; }

        public bool IsFromEnd { get; }

        public LongRange(long start, long end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public LongRange(long start, long end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private LongRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetInt64OrDefault(nameof(this.Start));
            this.End = info.GetInt64OrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out long start, out long end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out long start, out long end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public LongRange With(in long? Start = null, in long? End = null, bool? IsFromEnd = null)
            => new LongRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public LongRange FromStart()
            => new LongRange(this.Start, this.End, false);

        public LongRange FromEnd()
            => new LongRange(this.Start, this.End, true);

        IRange<long> IRange<long>.FromStart()
            => FromStart();

        IRange<long> IRange<long>.FromEnd()
            => FromEnd();

        public long Count()
        {
            if (this.End > this.Start)
                return this.End - this.Start + 1;

            return this.Start - this.End + 1;
        }

        public bool Contains(long value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is LongRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in LongRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(LongRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public override int GetHashCode()
        {
            var hashCode = -1418356749;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"{{ {nameof(this.Start)}={this.Start}, {nameof(this.End)}={this.End}, {nameof(this.IsFromEnd)}={this.IsFromEnd} }}";

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        public Enumerator Range()
            => GetEnumerator();

        IEnumerator<long> IRange<long>.Range()
            => GetEnumerator();

        public LongRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static LongRange Normal(long a, long b)
            => a > b ? new LongRange(b, a) : new LongRange(a, b);

        public static LongRange FromSize(long value, bool fromEnd = false)
            => new LongRange(0, Math.Max(value - 1, 0), fromEnd);

        public static LongRange FromStart(long start, long end)
            => new LongRange(start, end, false);

        public static LongRange FromEnd(long start, long end)
            => new LongRange(start, end, true);

        public static implicit operator LongRange(in (long start, long end) value)
            => new LongRange(value.start, value.end);

        public static implicit operator LongRange(in (long start, long end, bool fromEnd) value)
            => new LongRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<long, Enumerator.Default>(in LongRange value)
            => new ReadRange<long, Enumerator.Default>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<long>(in LongRange value)
            => new ReadRange<long>(value.Start, value.End, value.IsFromEnd, new Enumerator.Default());

        public static implicit operator LongRange(in ReadRange<long> value)
            => new LongRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator LongRange(in ReadRange<long, Enumerator.Default> value)
            => new LongRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in LongRange lhs, in LongRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in LongRange lhs, in LongRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<long>
        {
            private readonly long start;
            private readonly long end;
            private readonly sbyte sign;

            private long current;
            private sbyte flag;

            public Enumerator(in LongRange range)
                : this(range.Start, range.End, range.IsFromEnd)
            { }

            public Enumerator(long start, long end, bool fromEnd)
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
                this.flag = (sbyte)(this.current == this.end ? 1 : -1);
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

            public long Current
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

            public readonly struct Default : IRangeEnumerator<long>
            {
                public IEnumerator<long> Enumerate(long start, long end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd);
            }
        }
    }
}
