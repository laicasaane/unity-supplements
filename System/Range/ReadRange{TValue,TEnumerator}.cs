using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct ReadRange<TValue, TEnumerator> : IRange<TValue>,
                                                            IEquatableReadOnlyStruct<ReadRange<TValue, TEnumerator>>,
                                                            ISerializable
        where TValue : unmanaged, IEquatable<TValue>
        where TEnumerator : unmanaged, IRangeEnumerator<TValue>
    {
        public TValue Start { get; }

        public TValue End { get; }

        public bool IsFromEnd { get; }

        private readonly TEnumerator enumerator;

        public ReadRange(TValue start, TValue end)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = false;
            this.enumerator = default;
        }

        public ReadRange(TValue start, TValue end, bool fromEnd)
        {
            this.Start = start;
            this.End = end;
            this.IsFromEnd = fromEnd;
            this.enumerator = default;
        }

        private ReadRange(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Start = (TValue)info.GetValue(nameof(this.Start), typeof(TValue));
            }
            catch
            {
                this.Start = default;
            }

            try
            {
                this.End = (TValue)info.GetValue(nameof(this.End), typeof(TValue));
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

            this.enumerator = default;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
        }

        public void Deconstruct(out TValue start, out TValue end)
        {
            start = this.Start;
            end = this.End;
        }

        public void Deconstruct(out TValue start, out TValue end, out bool fromEnd)
        {
            start = this.Start;
            end = this.End;
            fromEnd = this.IsFromEnd;
        }

        public ReadRange<TValue> With(TValue? Start = null, TValue? End = null, bool? IsFromEnd = null)
            => new ReadRange<TValue>(
                Start ?? this.Start,
                End ?? this.End,
                IsFromEnd ?? this.IsFromEnd
            );

        public ReadRange<TValue, TEnumerator> FromStart()
            => new ReadRange<TValue, TEnumerator>(this.Start, this.End, false);

        public ReadRange<TValue, TEnumerator> FromEnd()
            => new ReadRange<TValue, TEnumerator>(this.Start, this.End, true);

        IRange<TValue> IRange<TValue>.FromStart()
            => FromStart();

        IRange<TValue> IRange<TValue>.FromEnd()
            => FromEnd();

        public override bool Equals(object obj)
            => obj is ReadRange<TValue, TEnumerator> other &&
               this.Start.Equals(other.Start) &&
               this.End.Equals(other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(in ReadRange<TValue, TEnumerator> other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End) &&
               this.IsFromEnd == other.IsFromEnd;

        public bool Equals(ReadRange<TValue, TEnumerator> other)
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

        public IEnumerator<TValue> GetEnumerator()
            => this.enumerator.Enumerate(this.Start, this.End, this.IsFromEnd);

        public IEnumerator<TValue> Range()
            => GetEnumerator();

        /// <summary>
        /// Automatically create a range from (a, b).
        /// If a <= b, then a is the start value, and b is the end value.
        /// Otherwise, they are swapped.
        /// </summary>
        public static ReadRange<TValue, TEnumerator> Auto(TValue a, TValue b, IComparer<TValue> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return comparer.Compare(a, b) > 0 ? new ReadRange<TValue, TEnumerator>(b, a) : new ReadRange<TValue, TEnumerator>(a, b);
        }

        public static ReadRange<TValue, TEnumerator> FromStart(in TValue start, in TValue end)
            => new ReadRange<TValue, TEnumerator>(start, end, false);

        public static ReadRange<TValue, TEnumerator> FromEnd(in TValue start, in TValue end)
            => new ReadRange<TValue, TEnumerator>(start, end, true);

        public static implicit operator ReadRange<TValue, TEnumerator>(in (TValue start, TValue end) value)
            => new ReadRange<TValue, TEnumerator>(value.start, value.end);

        public static implicit operator ReadRange<TValue, TEnumerator>(in (TValue start, TValue end, bool fromEnd) value)
            => new ReadRange<TValue, TEnumerator>(value.start, value.end, value.fromEnd);

        public static implicit operator ReadRange<TValue, TEnumerator>(in ReadRange<TValue> value)
            => new ReadRange<TValue, TEnumerator>(value.Start, value.End, value.IsFromEnd);

        public static implicit operator ReadRange<TValue>(in ReadRange<TValue, TEnumerator> value)
            => new ReadRange<TValue>(value.Start, value.End, value.IsFromEnd, value.enumerator);

        public static bool operator ==(in ReadRange<TValue, TEnumerator> lhs, in ReadRange<TValue, TEnumerator> rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in ReadRange<TValue, TEnumerator> lhs, in ReadRange<TValue, TEnumerator> rhs)
            => !lhs.Equals(in rhs);
    }
}
