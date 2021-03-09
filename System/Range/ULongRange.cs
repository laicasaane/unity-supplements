using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct ULongRange : IRange<ulong, ULongRange.Enumerator>, IEquatableReadOnlyStruct<ULongRange>, ISerializable
    {
        public ulong Start { get; }

        public ulong End { get; }

        public bool IsFromEnd { get; }

        public ULongRange(ulong start, ulong end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public ULongRange(ulong start, ulong end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private ULongRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetUInt64OrDefault(nameof(this.Start));
            this.End = info.GetUInt64OrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out ulong start, out ulong end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out ulong start, out ulong end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public ULongRange With(in ulong? Start = null, in ulong? End = null, bool? IsFromEnd = null)
            => new ULongRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public ULongRange FromStart()
            => new ULongRange(this.Start, this.End, false);

        public ULongRange FromEnd()
            => new ULongRange(this.Start, this.End, true);

        IRange<ulong> IRange<ulong>.FromStart()
            => FromStart();

        IRange<ulong> IRange<ulong>.FromEnd()
            => FromEnd();

        public ulong Count()
        {
            if (this.End > this.Start)
                return this.End - this.Start + 1;

            return this.Start - this.End + 1;
        }

        public bool Contains(ulong value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is ULongRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in ULongRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(ULongRange other)
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

        IEnumerator<ulong> IRange<ulong>.Range()
            => GetEnumerator();

        public ULongRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static ULongRange Normal(ulong a, ulong b)
            => a > b ? new ULongRange(b, a) : new ULongRange(a, b);

        /// <summary>
        /// Create a range from a size which is greater than 0
        /// </summary>
        /// <exception cref="InvalidOperationException">Size must be greater than 0</exception>
        public static ULongRange FromSize(ulong value, bool fromEnd = false)
        {
            if (value == 0)
                throw new InvalidOperationException("Size must be greater than 0");

            return new ULongRange(0, value > 0 ? value - 1 : value, fromEnd);
        }

        public static ULongRange FromStart(ulong start, ulong end)
            => new ULongRange(start, end, false);

        public static ULongRange FromEnd(ulong start, ulong end)
            => new ULongRange(start, end, true);

        public static implicit operator ULongRange(in (ulong start, ulong end) value)
            => new ULongRange(value.start, value.end);

        public static implicit operator ULongRange(in (ulong start, ulong end, bool fromEnd) value)
            => new ULongRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<ulong, Enumerator.Default>(in ULongRange value)
            => new ReadRange<ulong, Enumerator.Default>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<ulong>(in ULongRange value)
            => new ReadRange<ulong>(value.Start, value.End, value.IsFromEnd, new Enumerator.Default());

        public static implicit operator ULongRange(in ReadRange<ulong> value)
            => new ULongRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ULongRange(in ReadRange<ulong, Enumerator.Default> value)
            => new ULongRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in ULongRange lhs, in ULongRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in ULongRange lhs, in ULongRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<ulong>
        {
            private readonly ulong start;
            private readonly ulong end;
            private readonly sbyte sign;

            private ulong current;
            private sbyte flag;

            public Enumerator(in ULongRange range)
                 : this(range.Start, range.End, range.IsFromEnd)
            { }

            public Enumerator(ulong start, ulong end, bool fromEnd)
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

                    this.current += (ulong)this.sign;
                    return true;
                }

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
            }

            public ulong Current
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

            public readonly struct Default : IRangeEnumerator<ulong>
            {
                public IEnumerator<ulong> Enumerate(ulong start, ulong end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd);
            }
        }
    }
}
