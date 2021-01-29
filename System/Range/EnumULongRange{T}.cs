using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct EnumULongRange<T> : IRange<T, EnumULongRange<T>.Enumerator>, IEquatableReadOnlyStruct<EnumULongRange<T>>, ISerializable
        where T : unmanaged, Enum
    {
        public T Start { get; }

        public T End { get; }

        public bool IsFromEnd { get; }

        public EnumULongRange(T start, T end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public EnumULongRange(T start, T end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private EnumULongRange(SerializationInfo info, StreamingContext context)
        {
            if (!Enum<T>.TryParse(info.GetStringOrDefault(nameof(this.Start)), out var start))
                start = default;

            if (!Enum<T>.TryParse(info.GetStringOrDefault(nameof(this.End)), out var end))
                end = default;

            this.Start = start;
            this.End = end;
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start.ToString());
            info.AddValue(nameof(this.End), this.End.ToString());
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out T start, out T end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out T start, out T end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public EnumULongRange<T> With(in T? Start = null, in T? End = null, bool? IsFromEnd = null)
            => new EnumULongRange<T>(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public EnumULongRange<T> FromStart()
            => new EnumULongRange<T>(this.Start, this.End, false);

        public EnumULongRange<T> FromEnd()
            => new EnumULongRange<T>(this.Start, this.End, true);

        IRange<T> IRange<T>.FromStart()
            => FromStart();

        IRange<T> IRange<T>.FromEnd()
            => FromEnd();

        public ulong Count()
        {
            var startVal = Enum<T>.ToULong(this.Start);
            var endVal = Enum<T>.ToULong(this.End);

            if (endVal > startVal)
                return endVal - startVal + 1;

            return startVal - endVal + 1;
        }

        public bool Contains(T value)
        {
            var startVal = Enum<T>.ToULong(value);
            var endVal = Enum<T>.ToULong(value);
            var val = Enum<T>.ToULong(value);

            return startVal < endVal
                   ? val >= startVal && val <= endVal
                   : val >= endVal && val <= startVal;
        }

        public override bool Equals(object obj)
            => obj is EnumULongRange<T> other &&
               this.Start.Equals(other.Start) && this.End.Equals(other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in EnumULongRange<T> other)
            => this.Start.Equals(other.Start) && this.End.Equals(other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(EnumULongRange<T> other)
            => this.Start.Equals(other.Start) && this.End.Equals(other.End) &&
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

        IEnumerator<T> IRange<T>.Range()
            => GetEnumerator();

        public EnumULongRange<T> Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static EnumULongRange<T> Normal(T a, T b)
        {
            var aVal = Enum<T>.ToULong(a);
            var bVal = Enum<T>.ToULong(b);

            return aVal > bVal ? new EnumULongRange<T>(b, a) : new EnumULongRange<T>(a, b);
        }

        public static EnumULongRange<T> FromSize(long value, bool fromEnd = false)
        {
            var values = Enum<T>.Values;
            var size = Math.Min(value, values.LongLength);
            var start = default(T);
            var end = default(T);

            if (size > 0)
            {
                var last = size - 1;

                for (var i = 0u; i < size; i++)
                {
                    if (i == 0u)
                        start = values[i];
                    else if (i == last)
                        end = values[i];
                }
            }

            return new EnumULongRange<T>(start, end, fromEnd);
        }

        public static EnumULongRange<T> FromStart(T start, T end)
            => new EnumULongRange<T>(start, end, false);

        public static EnumULongRange<T> FromEnd(T start, T end)
            => new EnumULongRange<T>(start, end, true);

        public static implicit operator EnumULongRange<T>(in (T start, T end) value)
            => new EnumULongRange<T>(value.start, value.end);

        public static implicit operator EnumULongRange<T>(in (T start, T end, bool fromEnd) value)
            => new EnumULongRange<T>(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<T, Enumerator.Default>(in EnumULongRange<T> value)
            => new ReadRange<T, Enumerator.Default>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<T>(in EnumULongRange<T> value)
            => new ReadRange<T>(value.Start, value.End, value.IsFromEnd, new Enumerator.Default());

        public static implicit operator EnumULongRange<T>(in ReadRange<T> value)
            => new EnumULongRange<T>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator EnumULongRange<T>(in ReadRange<T, Enumerator.Default> value)
            => new EnumULongRange<T>(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in EnumULongRange<T> lhs, in EnumULongRange<T> rhs)
            => lhs.Start.Equals(rhs.Start) && lhs.End.Equals(rhs.End) &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in EnumULongRange<T> lhs, in EnumULongRange<T> rhs)
            => !lhs.Start.Equals(rhs.Start) || !lhs.End.Equals(rhs.End) ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ulong start;
            private readonly ulong end;
            private readonly sbyte sign;

            private ulong current;
            private sbyte flag;

            public Enumerator(in EnumULongRange<T> range)
                : this(range.Start, range.End, range.IsFromEnd)
            { }

            public Enumerator(T start, T end, bool fromEnd)
            {
                var startVal = Enum<T>.ToULong(start);
                var endVal = Enum<T>.ToULong(end);
                var increasing = startVal <= endVal;

                if (fromEnd)
                {
                    this.start = endVal;
                    this.end = startVal;
                }
                else
                {
                    this.start = startVal;
                    this.end = endVal;
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

            public T Current
            {
                get
                {
                    if (this.flag < 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.flag > 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return Enum<T>.From(this.current);
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

            public readonly struct Default : IRangeEnumerator<T>
            {
                public IEnumerator<T> Enumerate(T start, T end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd);
            }
        }
    }
}
