using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct ReadRange<TValue, TEnumerator> : IEquatableReadOnlyStruct<ReadRange<TValue, TEnumerator>>, ISerializable
        where TValue : unmanaged, IEquatable<TValue>
        where TEnumerator : unmanaged, IRangeEnumerator<TValue>
    {
        public readonly TValue Start;
        public readonly TValue End;

        private readonly TEnumerator enumerator;

        public ReadRange(TValue start, TValue end)
        {
            this.Start = start;
            this.End = end;
            this.enumerator = default;
        }

        public ReadRange(TValue start, TValue end, IComparer<TValue> comparer)
        {
            if (comparer.Compare(end, start) < 0)
                throw new InvalidOperationException($"{nameof(end)} must be greater than or equal to {nameof(start)}");

            this.Start = start;
            this.End = end;
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

            this.enumerator = default;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
        }

        public void Deconstruct(out TValue start, out TValue end)
        {
            start = this.Start;
            end = this.End;
        }

        public ReadRange<TValue> With(in TValue? Start = null, in TValue? End = null)
            => new ReadRange<TValue>(
                Start ?? this.Start,
                End ?? this.End
            );

        public override bool Equals(object obj)
            => obj is ReadRange<TValue, TEnumerator> other &&
               this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(in ReadRange<TValue, TEnumerator> other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(ReadRange<TValue, TEnumerator> other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public override int GetHashCode()
        {
            var hashCode = 1005511336;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            hashCode = hashCode * -1521134295 + this.enumerator.GetHashCode();
            return hashCode;
        }

        public IEnumerator<TValue> GetEnumerator()
            => this.enumerator.Enumerate(this.Start, this.End);

        /// <summary>
        /// Automatically create a range from (a, b).
        /// If a <= b, then a is the start value, and b is the end value.
        /// Otherwise, they are swapped.
        /// </summary>
        public static ReadRange<TValue, TEnumerator> Auto(TValue a, TValue b, IComparer<TValue> comparer)
            => comparer.Compare(a, b) > 0 ? new ReadRange<TValue, TEnumerator>(b, a) : new ReadRange<TValue, TEnumerator>(a, b);

        public static implicit operator ReadRange<TValue, TEnumerator>(in (TValue start, TValue end) value)
            => new ReadRange<TValue, TEnumerator>(value.start, value.end);

        public static implicit operator ReadRange<TValue, TEnumerator>(in ReadRange<TValue> value)
            => new ReadRange<TValue, TEnumerator>(value.Start, value.End);

        public static implicit operator ReadRange<TValue>(in ReadRange<TValue, TEnumerator> value)
            => new ReadRange<TValue>(value.Start, value.End, value.enumerator);

        public static bool operator ==(in ReadRange<TValue, TEnumerator> lhs, in ReadRange<TValue, TEnumerator> rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in ReadRange<TValue, TEnumerator> lhs, in ReadRange<TValue, TEnumerator> rhs)
            => !lhs.Equals(in rhs);
    }
}
