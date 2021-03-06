﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct ReadRange<T> : IRange<T>, IEquatableReadOnlyStruct<ReadRange<T>>, ISerializable
        where T : struct
    {
        public T Start { get; }

        public T End { get; }

        public bool IsFromEnd { get; }

        private readonly IRangeEnumerator<T> enumerator;

        public ReadRange(T start, T end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
            this.enumerator = null;
        }

        public ReadRange(T start, T end, IRangeEnumerator<T> enumerator)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
            this.enumerator = enumerator;
        }

        public ReadRange(T start, T end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.enumerator = null;
        }

        public ReadRange(T start, T end, bool fromEnd, IRangeEnumerator<T> enumerator)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.enumerator = enumerator;
        }

        private ReadRange(SerializationInfo info, StreamingContext context)
        {
            this.Start = info.GetValueOrDefault<T>(nameof(this.Start));
            this.End = info.GetValueOrDefault<T>(nameof(this.End));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
            this.enumerator = null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
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

        public ReadRange<T> With(T? Start = null, T? End = null, bool? IsFromEnd = null, IRangeEnumerator<T> Enumerator = null)
            => new ReadRange<T>(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd,
                Enumerator ?? this.enumerator
            );

        public ReadRange<T> FromStart()
            => new ReadRange<T>(this.Start, this.End, false, this.enumerator);

        public ReadRange<T> FromEnd()
            => new ReadRange<T>(this.Start, this.End, true, this.enumerator);

        IRange<T> IRange<T>.FromStart()
            => FromStart();

        IRange<T> IRange<T>.FromEnd()
            => FromEnd();

        public override bool Equals(object obj)
            => obj is ReadRange<T> other &&
               this.Start.Equals(other.Start) &&
               this.End.Equals(other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in ReadRange<T> other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(ReadRange<T> other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End) &&
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

        public IEnumerator<T> GetEnumerator()
            => (this.enumerator ?? new Enumerator()).Enumerate(this.Start, this.End, this.IsFromEnd);

        public IEnumerator<T> Range()
            => GetEnumerator();

        public ReadRange<T> Normalize(IComparer<T> comparer)
            => Normal(this.Start, this.End, this.enumerator, comparer);

        /// <summary>
        /// Create a normal range from (a, b) where <see cref="Start"/> is lesser than <see cref="End"/>.
        /// </summary>
        public static ReadRange<T> Normal(T a, T b, IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return comparer.Compare(a, b) > 0 ? new ReadRange<T>(b, a) : new ReadRange<T>(a, b);
        }

        public static ReadRange<T> Normal(T a, T b, IRangeEnumerator<T> enumerator, IComparer<T> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return comparer.Compare(a, b) > 0 ? new ReadRange<T>(b, a, enumerator) : new ReadRange<T>(a, b, enumerator);
        }

        public static ReadRange<T> FromStart(in T start, in T end)
            => new ReadRange<T>(start, end, false);

        public static ReadRange<T> FromEnd(in T start, in T end)
            => new ReadRange<T>(start, end, true);

        public static implicit operator ReadRange<T>(in (T start, T end) value)
            => new ReadRange<T>(value.start, value.end);

        public static implicit operator ReadRange<T>(in (T start, T end, bool fromEnd) value)
            => new ReadRange<T>(value.start, value.end, value.fromEnd);

        public static bool operator ==(in ReadRange<T> lhs, in ReadRange<T> rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in ReadRange<T> lhs, in ReadRange<T> rhs)
            => !lhs.Equals(in rhs);

        private struct Enumerator : IRangeEnumerator<T>
        {
            public IEnumerator<T> Enumerate(T start, T end, bool fromEnd)
            {
                if (start.Equals(end))
                {
                    yield return start;
                }
                else if (fromEnd)
                {
                    yield return end;
                    yield return start;
                }
                else
                {
                    yield return start;
                    yield return end;
                }
            }
        }
    }
}
