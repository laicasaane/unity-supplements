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
            var hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            return hashCode;
        }

        public IEnumerator<TValue> GetEnumerator()
            => this.enumerator.Enumerate(this.Start, this.End);

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
