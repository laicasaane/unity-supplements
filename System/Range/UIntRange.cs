﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct UIntRange : IRange<uint, UIntRange.Enumerator>, IEquatableReadOnlyStruct<UIntRange>, ISerializable
    {
        public uint Start { get; }

        public uint End { get; }

        public bool IsFromEnd { get; }

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
            this.Start = info.GetUInt32OrDefault(nameof(this.Start));
            this.End = info.GetUInt32OrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
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

        public UIntRange With(in uint? Start = null, in uint? End = null, bool? IsFromEnd = null)
            => new UIntRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public UIntRange FromStart()
            => new UIntRange(this.Start, this.End, false);

        public UIntRange FromEnd()
            => new UIntRange(this.Start, this.End, true);

        IRange<uint> IRange<uint>.FromStart()
            => FromStart();

        IRange<uint> IRange<uint>.FromEnd()
            => FromEnd();

        public uint Count()
        {
            if (this.End > this.Start)
                return this.End - this.Start + 1;

            return this.Start - this.End + 1;
        }

        public bool Contains(uint value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is UIntRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in UIntRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(UIntRange other)
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

        IEnumerator<uint> IRange<uint>.Range()
            => GetEnumerator();

        public UIntRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static UIntRange Normal(uint a, uint b)
            => a > b ? new UIntRange(b, a) : new UIntRange(a, b);

        /// <summary>
        /// Create a range from a size which is greater than 0
        /// </summary>
        /// <exception cref="InvalidOperationException">Size must be greater than 0</exception>
        public static UIntRange FromSize(uint value, bool fromEnd = false)
        {
            if (value == 0)
                throw new InvalidOperationException("Size must be greater than 0");

            return new UIntRange(0, value > 0 ? value - 1 : value, fromEnd);
        }

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
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in UIntRange lhs, in UIntRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<uint>, IRangeEnumerator<uint>
        {
            private readonly uint start;
            private readonly uint end;
            private readonly sbyte sign;

            private uint current;
            private sbyte flag;

            public Enumerator(in UIntRange range)
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

                    this.current += (uint)this.sign;
                    return true;
                }

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
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
                this.current = this.start;
                this.flag = -1;
            }

            public IEnumerator<uint> Enumerate(uint start, uint end, bool fromEnd)
            {
                var increasing = start <= end;

                return increasing
                       ? (fromEnd ? EnumerateDecreasing(end, start) : EnumerateIncreasing(start, end))
                       : (fromEnd ? EnumerateIncreasing(end, start) : EnumerateDecreasing(start, end));
            }

            private IEnumerator<uint> EnumerateIncreasing(uint start, uint end)
            {
                for (var i = start; ; i++)
                {
                    yield return i;

                    if (i == end)
                        yield break;
                }
            }

            private IEnumerator<uint> EnumerateDecreasing(uint start, uint end)
            {
                for (var i = start; ; i--)
                {
                    yield return i;

                    if (i == end)
                        yield break;
                }
            }
        }
    }
}
