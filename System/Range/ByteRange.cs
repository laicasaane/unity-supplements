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
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than or equal to <see cref="End"/>.
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

        public static implicit operator ReadRange<byte, Enumerator>(in ByteRange value)
            => new ReadRange<byte, Enumerator>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<byte>(in ByteRange value)
            => new ReadRange<byte>(value.Start, value.End, value.IsFromEnd, new Enumerator());

        public static implicit operator ByteRange(in ReadRange<byte> value)
            => new ByteRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ByteRange(in ReadRange<byte, Enumerator> value)
            => new ByteRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in ByteRange lhs, in ByteRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in ByteRange lhs, in ByteRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<byte>, IRangeEnumerator<byte>
        {
            private readonly byte start;
            private readonly byte end;
            private readonly sbyte sign;

            private byte current;
            private sbyte flag;

            public Enumerator(in ByteRange range)
            {
                var increasing = range.Start <= range.End;

                if (range.IsFromEnd)
                {
                    this.start = range.End;
                    this.end = range.Start;
                }
                else
                {
                    this.start = range.Start;
                    this.end = range.End;
                }

                this.sign = (sbyte)(increasing
                            ? (range.IsFromEnd ? -1 : 1)
                            : (range.IsFromEnd ? 1 : -1));

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

            public IEnumerator<byte> Enumerate(byte start, byte end, bool fromEnd)
            {
                var increasing = start <= end;

                return increasing
                       ? (fromEnd ? EnumerateDecreasing(end, start) : EnumerateIncreasing(start, end))
                       : (fromEnd ? EnumerateIncreasing(end, start) : EnumerateDecreasing(start, end));
            }

            private IEnumerator<byte> EnumerateIncreasing(byte start, byte end)
            {
                for (var i = start;; i++)
                {
                    yield return i;

                    if (i == end)
                        yield break;
                }
            }

            private IEnumerator<byte> EnumerateDecreasing(byte start, byte end)
            {
                for (var i = start;; i--)
                {
                    yield return i;

                    if (i == end)
                        yield break;
                }
            }
        }
    }
}
