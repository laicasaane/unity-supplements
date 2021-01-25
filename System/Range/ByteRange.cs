using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct ByteRange : IRange<byte, ByteRange.Enumerator>, IEquatableReadOnlyStruct<ByteRange>, ISerializable
    {
        public byte Start { get; }

        public byte End { get; }

        public bool IsFromEnd { get; }

        public ByteRange(byte start, byte end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public ByteRange(byte start, byte end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private ByteRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetByteOrDefault(nameof(this.Start));
            this.End = info.GetByteOrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out byte start, out byte end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out byte start, out byte end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public ByteRange With(in byte? Start = null, in byte? End = null, bool? IsFromEnd = null)
            => new ByteRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public ByteRange FromStart()
            => new ByteRange(this.Start, this.End, false);

        public ByteRange FromEnd()
            => new ByteRange(this.Start, this.End, true);

        IRange<byte> IRange<byte>.FromStart()
            => FromStart();

        IRange<byte> IRange<byte>.FromEnd()
            => FromEnd();

        public byte Count()
        {
            if (this.End > this.Start)
                return (byte)(this.End - this.Start + 1);

            return (byte)(this.Start - this.End + 1);
        }

        public bool Contains(byte value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is ByteRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in ByteRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(ByteRange other)
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

        IEnumerator<byte> IRange<byte>.Range()
            => GetEnumerator();

        public ByteRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static ByteRange Normal(byte a, byte b)
            => a > b ? new ByteRange(b, a) : new ByteRange(a, b);

        public static ByteRange FromSize(byte value, bool fromEnd = false)
            => new ByteRange(0, (byte)(value > 0 ? value - 1 : value), fromEnd);

        public static ByteRange FromStart(byte start, byte end)
            => new ByteRange(start, end, false);

        public static ByteRange FromEnd(byte start, byte end)
            => new ByteRange(start, end, true);

        public static implicit operator ByteRange(in (byte start, byte end) value)
            => new ByteRange(value.start, value.end);

        public static implicit operator ByteRange(in (byte start, byte end, bool fromEnd) value)
            => new ByteRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<byte, Enumerator.Default>(in ByteRange value)
            => new ReadRange<byte, Enumerator.Default>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<byte>(in ByteRange value)
            => new ReadRange<byte>(value.Start, value.End, value.IsFromEnd, new Enumerator.Default());

        public static implicit operator ByteRange(in ReadRange<byte> value)
            => new ByteRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ByteRange(in ReadRange<byte, Enumerator.Default> value)
            => new ByteRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in ByteRange lhs, in ByteRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in ByteRange lhs, in ByteRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<byte>
        {
            private readonly byte start;
            private readonly byte end;
            private readonly sbyte sign;

            private byte current;
            private sbyte flag;

            public Enumerator(in ByteRange range)
                : this(range.Start, range.End, range.IsFromEnd)
            { }

            public Enumerator(byte start, byte end, bool fromEnd)
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

                    this.current += (byte)this.sign;
                    return true;
                }

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
            }

            public byte Current
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

            public readonly struct Default : IRangeEnumerator<byte>
            {
                public IEnumerator<byte> Enumerate(byte start, byte end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd);
            }
        }
    }
}
