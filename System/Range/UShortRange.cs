using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct UShortRange : IRange<ushort, UShortRange.Enumerator>, IEquatableReadOnlyStruct<UShortRange>, ISerializable
    {
        public ushort Start { get; }

        public ushort End { get; }

        public bool IsFromEnd { get; }

        public UShortRange(ushort start, ushort end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public UShortRange(ushort start, ushort end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private UShortRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetUInt16OrDefault(nameof(this.Start));
            this.End = info.GetUInt16OrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out ushort start, out ushort end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out ushort start, out ushort end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public UShortRange With(in ushort? Start = null, in ushort? End = null, bool? IsFromEnd = null)
            => new UShortRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public UShortRange FromStart()
            => new UShortRange(this.Start, this.End, false);

        public UShortRange FromEnd()
            => new UShortRange(this.Start, this.End, true);

        IRange<ushort> IRange<ushort>.FromStart()
            => FromStart();

        IRange<ushort> IRange<ushort>.FromEnd()
            => FromEnd();

        public ushort Count()
        {
            if (this.End > this.Start)
                return (ushort)(this.End - this.Start + 1);

            return (ushort)(this.Start - this.End + 1);
        }

        public bool Contains(ushort value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is UShortRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in UShortRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(UShortRange other)
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

        IEnumerator<ushort> IRange<ushort>.Range()
            => GetEnumerator();

        public UShortRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than or equal to <see cref="End"/>.
        /// </summary>
        public static UShortRange Normal(ushort a, ushort b)
            => a > b ? new UShortRange(b, a) : new UShortRange(a, b);

        public static UShortRange FromSize(ushort value, bool fromEnd = false)
            => new UShortRange(0, (ushort)(value > 0 ? value - 1 : value), fromEnd);

        public static UShortRange FromStart(ushort start, ushort end)
            => new UShortRange(start, end, false);

        public static UShortRange FromEnd(ushort start, ushort end)
            => new UShortRange(start, end, true);

        public static implicit operator UShortRange(in (ushort start, ushort end) value)
            => new UShortRange(value.start, value.end);

        public static implicit operator UShortRange(in (ushort start, ushort end, bool fromEnd) value)
            => new UShortRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<ushort, Enumerator>(in UShortRange value)
            => new ReadRange<ushort, Enumerator>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<ushort>(in UShortRange value)
            => new ReadRange<ushort>(value.Start, value.End, value.IsFromEnd, new Enumerator());

        public static implicit operator UShortRange(in ReadRange<ushort> value)
            => new UShortRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator UShortRange(in ReadRange<ushort, Enumerator> value)
            => new UShortRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in UShortRange lhs, in UShortRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in UShortRange lhs, in UShortRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<ushort>, IRangeEnumerator<ushort>
        {
            private readonly ushort start;
            private readonly ushort end;
            private readonly bool fromEnd;

            private ushort current;
            private sbyte flag;

            public Enumerator(in UShortRange range)
            {
                var isIncreasing = range.Start.CompareTo(range.End) <= 0;
                this.start = isIncreasing ? range.Start : range.End;
                this.end = isIncreasing ? range.End : range.Start;
                this.fromEnd = range.IsFromEnd;

                if (this.fromEnd)
                {
                    this.current = this.end;
                    this.flag = (sbyte)(this.current == this.start ? 1 : -1);
                }
                else
                {
                    this.current = this.start;
                    this.flag = (sbyte)(this.current == this.end ? 1 : -1);
                }
            }

            public bool MoveNext()
            {
                if (this.flag == 0)
                    return this.fromEnd ? MoveNextFromEnd() : MoveNextFromStart();

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
            }

            private bool MoveNextFromStart()
            {
                if (this.current == this.end)
                {
                    this.flag = 1;
                    return false;
                }

                this.current++;
                return true;
            }

            private bool MoveNextFromEnd()
            {
                if (this.current == this.start)
                {
                    this.flag = 1;
                    return false;
                }

                this.current--;
                return true;
            }

            public ushort Current
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
                this.current = this.fromEnd ? this.end : this.start;
                this.flag = -1;
            }

            public IEnumerator<ushort> Enumerate(ushort start, ushort end, bool fromEnd)
            {
                var increasing = start <= end;
                var newStart = increasing ? start : end;
                var newEnd = increasing ? end : start;

                return fromEnd ? EnumerateFromEnd(newStart, newEnd) : EnumerateFromStart(newStart, newEnd);
            }

            private IEnumerator<ushort> EnumerateFromStart(ushort start, ushort end)
            {
                for (var i = start; i <= end; i++)
                {
                    yield return i;
                }
            }

            private IEnumerator<ushort> EnumerateFromEnd(ushort start, ushort end)
            {
                for (var i = end; i >= start; i--)
                {
                    yield return i;
                }
            }
        }
    }
}
