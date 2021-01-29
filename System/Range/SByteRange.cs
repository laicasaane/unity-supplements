using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct SByteRange : IRange<sbyte, SByteRange.Enumerator>, IEquatableReadOnlyStruct<SByteRange>, ISerializable
    {
        public sbyte Start { get; }

        public sbyte End { get; }

        public bool IsFromEnd { get; }

        public SByteRange(sbyte start, sbyte end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public SByteRange(sbyte start, sbyte end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private SByteRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetSByteOrDefault(nameof(this.Start));
            this.End = info.GetSByteOrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out sbyte start, out sbyte end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out sbyte start, out sbyte end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public SByteRange With(in sbyte? Start = null, in sbyte? End = null, bool? IsFromEnd = null)
            => new SByteRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public SByteRange FromStart()
            => new SByteRange(this.Start, this.End, false);

        public SByteRange FromEnd()
            => new SByteRange(this.Start, this.End, true);

        IRange<sbyte> IRange<sbyte>.FromStart()
            => FromStart();

        IRange<sbyte> IRange<sbyte>.FromEnd()
            => FromEnd();

        public sbyte Count()
        {
            if (this.End > this.Start)
                return (sbyte)(this.End - this.Start + 1);

            return (sbyte)(this.Start - this.End + 1);
        }

        public bool Contains(sbyte value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is SByteRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in SByteRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(SByteRange other)
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

        IEnumerator<sbyte> IRange<sbyte>.Range()
            => GetEnumerator();

        public SByteRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static SByteRange Normal(sbyte a, sbyte b)
            => a > b ? new SByteRange(b, a) : new SByteRange(a, b);

        public static SByteRange FromSize(sbyte value, bool fromEnd = false)
            => new SByteRange(0, (sbyte)Math.Max(value - 1, 0), fromEnd);

        public static SByteRange FromStart(sbyte start, sbyte end)
            => new SByteRange(start, end, false);

        public static SByteRange FromEnd(sbyte start, sbyte end)
            => new SByteRange(start, end, true);

        public static implicit operator SByteRange(in (sbyte start, sbyte end) value)
            => new SByteRange(value.start, value.end);

        public static implicit operator SByteRange(in (sbyte start, sbyte end, bool fromEnd) value)
            => new SByteRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<sbyte, Enumerator.Default>(in SByteRange value)
            => new ReadRange<sbyte, Enumerator.Default>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<sbyte>(in SByteRange value)
            => new ReadRange<sbyte>(value.Start, value.End, value.IsFromEnd, new Enumerator.Default());

        public static implicit operator SByteRange(in ReadRange<sbyte> value)
            => new SByteRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator SByteRange(in ReadRange<sbyte, Enumerator.Default> value)
            => new SByteRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in SByteRange lhs, in SByteRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in SByteRange lhs, in SByteRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<sbyte>
        {
            private readonly sbyte start;
            private readonly sbyte end;
            private readonly sbyte sign;

            private sbyte current;
            private sbyte flag;

            public Enumerator(in SByteRange range)
                : this(range.Start, range.End, range.IsFromEnd)
            { }

            public Enumerator(sbyte start, sbyte end, bool fromEnd)
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

            public sbyte Current
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

            public readonly struct Default : IRangeEnumerator<sbyte>
            {
                public IEnumerator<sbyte> Enumerate(sbyte start, sbyte end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd);
            }
        }
    }
}
