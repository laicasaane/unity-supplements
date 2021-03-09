using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct CharRange : IRange<char, CharRange.Enumerator>, IEquatableReadOnlyStruct<CharRange>, ISerializable
    {
        public char Start { get; }

        public char End { get; }

        public bool IsFromEnd { get; }

        public CharRange(char start, char end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
        }

        public CharRange(char start, char end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
        }

        private CharRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetCharOrDefault(nameof(this.Start));
            this.End = info.GetCharOrDefault(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out char start, out char end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out char start, out char end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public CharRange With(in char? Start = null, in char? End = null, bool? IsFromEnd = null)
            => new CharRange(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public CharRange FromStart()
            => new CharRange(this.Start, this.End, false);

        public CharRange FromEnd()
            => new CharRange(this.Start, this.End, true);

        IRange<char> IRange<char>.FromStart()
            => FromStart();

        IRange<char> IRange<char>.FromEnd()
            => FromEnd();

        public char Count()
        {
            if (this.End > this.Start)
                return (char)(this.End - this.Start + 1);

            return (char)(this.Start - this.End + 1);
        }

        public bool Contains(char value)
            => this.Start < this.End
               ? value >= this.Start && value <= this.End
               : value >= this.End && value <= this.Start;

        public override bool Equals(object obj)
            => obj is CharRange other &&
               this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in CharRange other)
            => this.Start == other.Start && this.End == other.End &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(CharRange other)
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

        IEnumerator<char> IRange<char>.Range()
            => GetEnumerator();

        public CharRange Normalize()
            => Normal(this.Start, this.End);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static CharRange Normal(char a, char b)
            => a > b ? new CharRange(b, a) : new CharRange(a, b);

        /// <summary>
        /// Create a range from a size which is greater than 0
        /// </summary>
        /// <exception cref="InvalidOperationException">Size must be greater than 0</exception>
        public static CharRange FromSize(char value, bool fromEnd = false)
        {
            if (value == 0)
                throw new InvalidOperationException("Size must be greater than 0");

            return new CharRange((char)0, (char)(value > 0 ? value - 1 : value), fromEnd);
        }

        public static CharRange FromStart(char start, char end)
            => new CharRange(start, end, false);

        public static CharRange FromEnd(char start, char end)
            => new CharRange(start, end, true);

        public static implicit operator CharRange(in (char start, char end) value)
            => new CharRange(value.start, value.end);

        public static implicit operator CharRange(in (char start, char end, bool fromEnd) value)
            => new CharRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<char, Enumerator.Default>(in CharRange value)
            => new ReadRange<char, Enumerator.Default>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<char>(in CharRange value)
            => new ReadRange<char>(value.Start, value.End, value.IsFromEnd, new Enumerator.Default());

        public static implicit operator CharRange(in ReadRange<char> value)
            => new CharRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator CharRange(in ReadRange<char, Enumerator.Default> value)
            => new CharRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in CharRange lhs, in CharRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in CharRange lhs, in CharRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<char>
        {
            private readonly char start;
            private readonly char end;
            private readonly sbyte sign;

            private char current;
            private sbyte flag;

            public Enumerator(in CharRange range)
                : this(range.Start, range.End, range.IsFromEnd)
            { }

            public Enumerator(char start, char end, bool fromEnd)
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

                    this.current += (char)this.sign;
                    return true;
                }

                if (this.flag < 0)
                {
                    this.flag = 0;
                    return true;
                }

                return false;
            }

            public char Current
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

            public readonly struct Default : IRangeEnumerator<char>
            {
                public IEnumerator<char> Enumerate(char start, char end, bool fromEnd)
                    => new Enumerator(start, end, fromEnd);
            }
        }
    }
}
