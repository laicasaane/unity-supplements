using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct ShortRange : IRange<short, ShortRange.Enumerator>, IEquatableReadOnlyStruct<ShortRange>, ISerializable
    {
        public short Start { get; }

        public short End { get; }

        public bool IsFromEnd { get; }

        public ShortRange(short start, short end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public ShortRange(short start, short end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private ShortRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetInt16OrDefault(nameof(this.Start));
            this.End = info.GetInt16OrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out short start, out short end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out short start, out short end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public ShortRange With(in short? Start = null, in short? End = null, bool? IsFromEnd = null)
            => new ShortRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public ShortRange FromStart()
            => new ShortRange(this.Start, this.End, false);

        public ShortRange FromEnd()
            => new ShortRange(this.Start, this.End, true);

        IRange<short> IRange<short>.FromStart()
            => FromStart();

        IRange<short> IRange<short>.FromEnd()
            => FromEnd();

        public short Count()
        {
            if (this.End > this.Start)
                return (short)(this.End - this.Start + 1);

            return (short)(this.Start - this.End + 1);
        }

        public bool Contains(short value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is ShortRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in ShortRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(ShortRange other)
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

        IEnumerator<short> IRange<short>.Range()
            => GetEnumerator();

        public ShortRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static ShortRange Normal(short a, short b)
            => a > b ? new ShortRange(b, a) : new ShortRange(a, b);

        /// <summary>
        /// Create a range from a size which is greater than 0
        /// </summary>
        /// <exception cref="InvalidOperationException">Size must be greater than 0</exception>
        public static ShortRange FromSize(short value, bool fromEnd = false)
        {
            if (value <= 0)
                throw new InvalidOperationException("Size must be greater than 0");

            return new ShortRange(0, (short)Math.Max(value - 1, 0), fromEnd);
        }

        public static ShortRange FromStart(short start, short end)
            => new ShortRange(start, end, false);

        public static ShortRange FromEnd(short start, short end)
            => new ShortRange(start, end, true);

        public static implicit operator ShortRange(in (short start, short end) value)
            => new ShortRange(value.start, value.end);

        public static implicit operator ShortRange(in (short start, short end, bool fromEnd) value)
            => new ShortRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<short, Enumerator.Default>(in ShortRange value)
            => new ReadRange<short, Enumerator.Default>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<short>(in ShortRange value)
            => new ReadRange<short>(value.Start, value.End, value.IsFromEnd, new Enumerator.Default());

        public static implicit operator ShortRange(in ReadRange<short> value)
            => new ShortRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ShortRange(in ReadRange<short, Enumerator.Default> value)
            => new ShortRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in ShortRange lhs, in ShortRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in ShortRange lhs, in ShortRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<short>
        {
            private readonly short start;
            private readonly short end;
            private readonly sbyte sign;

            private short current;
            private sbyte flag;

            public Enumerator(in ShortRange range)
                : this(range.Start, range.End, range.IsFromEnd)
            { }

            public Enumerator(short start, short end, bool fromEnd)
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

            public short Current
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

            public readonly struct Default : IRangeEnumerator<short>
            {
                public IEnumerator<short> Enumerate(short start, short end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd);
            }
        }
    }
}
