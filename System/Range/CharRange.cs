﻿using System.Collections;
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
        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than or equal to <see cref="End"/>.
        /// </summary>
        public static CharRange Normal(char a, char b)
            => a > b ? new CharRange(b, a) : new CharRange(a, b);

        public static CharRange FromSize(char value, bool fromEnd = false)
            => new CharRange((char)0, (char)(value > 0 ? value - 1 : value), fromEnd);

        public static CharRange FromStart(char start, char end)
            => new CharRange(start, end, false);

        public static CharRange FromEnd(char start, char end)
            => new CharRange(start, end, true);

        public static implicit operator CharRange(in (char start, char end) value)
            => new CharRange(value.start, value.end);

        public static implicit operator CharRange(in (char start, char end, bool fromEnd) value)
            => new CharRange(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<char, Enumerator>(in CharRange value)
            => new ReadRange<char, Enumerator>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<char>(in CharRange value)
            => new ReadRange<char>(value.Start, value.End, value.IsFromEnd, new Enumerator());

        public static implicit operator CharRange(in ReadRange<char> value)
            => new CharRange(value.Start, value.End, value.IsFromEnd);

        public static implicit operator CharRange(in ReadRange<char, Enumerator> value)
            => new CharRange(value.Start, value.End, value.IsFromEnd);

        public static bool operator ==(in CharRange lhs, in CharRange rhs)
            => lhs.Start == rhs.Start && lhs.End == rhs.End &&
               lhs.IsFromEnd == rhs.IsFromEnd;

        public static bool operator !=(in CharRange lhs, in CharRange rhs)
            => lhs.Start != rhs.Start || lhs.End != rhs.End ||
               lhs.IsFromEnd != rhs.IsFromEnd;

        public struct Enumerator : IEnumerator<char>, IRangeEnumerator<char>
        {
            private readonly char start;
            private readonly char end;
            private readonly bool fromEnd;

            private char current;
            private sbyte flag;

            public Enumerator(in CharRange range)
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
                this.current = this.fromEnd ? this.end : this.start;
                this.flag = -1;
            }

            public IEnumerator<char> Enumerate(char start, char end, bool fromEnd)
            {
                var increasing = start <= end;
                var newStart = increasing ? start : end;
                var newEnd = increasing ? end : start;

                return fromEnd ? EnumerateFromEnd(newStart, newEnd) : EnumerateFromStart(newStart, newEnd);
            }

            private IEnumerator<char> EnumerateFromStart(char start, char end)
            {
                for (var i = start; i <= end; i++)
                {
                    yield return i;
                }
            }

            private IEnumerator<char> EnumerateFromEnd(char start, char end)
            {
                for (var i = end; i >= start; i--)
                {
                    yield return i;
                }
            }
        }
    }
}