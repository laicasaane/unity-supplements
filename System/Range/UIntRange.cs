using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct UIntRange : IEquatableReadOnlyStruct<UIntRange>, ISerializable
    {
        public readonly uint Start;
        public readonly uint End;
        public readonly bool IsFromEnd;

        public UIntRange(uint start, uint end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public UIntRange(uint start, uint end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private UIntRange(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Start = info.GetUInt32(nameof(this.Start));
            }
            catch
            {
                this.Start = default;
            }

            try
            {
                this.End = info.GetUInt32(nameof(this.End));
            }
            catch
            {
                this.End = default;
            }

            try
            {
                this.IsFromEnd = info.GetBoolean(nameof(this.IsFromEnd));
            }
            catch
            {
                this.IsFromEnd = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out uint start, out uint end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out uint start, out uint end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public UIntRange With(in uint? Start = null, in uint? End = null, in bool? IsFromEnd = null)
            => new UIntRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public override bool Equals(object obj)
            => obj is UIntRange other &&
               this.Start.Equals(other.Start) &&
               this.End.Equals(other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in UIntRange other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(UIntRange other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End) &&
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

        /// <summary>
        /// Automatically create a range from (a, b).
        /// If a <= b, then a is the start value, and b is the end value.
        /// Otherwise, they are swapped.
        /// </summary>
        public static UIntRange Auto(uint a, uint b)
            => a > b ? new UIntRange(b, a) : new UIntRange(a, b);

        public static UIntRange Count(uint value)
            => new UIntRange(0, value > 0 ? value - 1 : value);

        public static UIntRange FromStart(uint start, uint end)
            => new UIntRange(start, end, false);

        public static UIntRange FromEnd(uint start, uint end)
            => new UIntRange(start, end, true);

        public static implicit operator UIntRange(in (uint start, uint end) value)
            => new UIntRange(value.start, value.end);

        public static implicit operator UIntRange(in (uint start, uint end, bool fromEnd) value)
            => new UIntRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<uint, Enumerator>(in UIntRange value)
            => new ReadRange<uint, Enumerator>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<uint>(in UIntRange value)
            => new ReadRange<uint>(value.Start, value.End, value.IsFromEnd, new Enumerator());

        public static implicit operator UIntRange(in ReadRange<uint> value)
            => new UIntRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator UIntRange(in ReadRange<uint, Enumerator> value)
            => new UIntRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in UIntRange lhs, in UIntRange rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in UIntRange lhs, in UIntRange rhs)
            => !lhs.Equals(in rhs);

        public struct Enumerator : IEnumerator<uint>, IRangeEnumerator<uint>
        {
            private readonly uint start;
            private readonly uint end;
            private readonly bool fromEnd;

            private uint current;
            private sbyte flag;

            public Enumerator(in UIntRange range)
            {
                var isIncreasing = range.Start.CompareTo(range.End) <= 0;
                this.start = isIncreasing ? range.Start : range.End;
                this.end = isIncreasing ? range.End : range.Start;
                this.fromEnd = range.IsFromEnd;

                this.current = this.fromEnd ? this.end : this.start;
                this.flag = -1;
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

            public uint Current
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

            public IEnumerator<uint> Enumerate(uint start, uint end, bool fromEnd)
            {
                var increasing = start <= end;
                var newStart = increasing ? start : end;
                var newEnd = increasing ? end : start;

                return fromEnd ? EnumerateFromEnd(newStart, newEnd) : EnumerateFromStart(newStart, newEnd);
            }

            private IEnumerator<uint> EnumerateFromStart(uint start, uint end)
            {
                for (var i = start; i <= end; i++)
                {
                    yield return i;
                }
            }

            private IEnumerator<uint> EnumerateFromEnd(uint start, uint end)
            {
                for (var i = end; i >= start; i--)
                {
                    yield return i;
                }
            }
        }
    }
}
