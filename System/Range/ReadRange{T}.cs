using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public readonly struct ReadRange<T> : IEquatableReadOnlyStruct<ReadRange<T>>, ISerializable
        where T : unmanaged, IEquatable<T>
    {
        public readonly T Start;
        public readonly T End;

        private readonly IRangeEnumerator<T> enumerator;

        public ReadRange(T start, T end)
        {
            this.Start = start;
            this.End = end;
            this.enumerator = null;
        }

        public ReadRange(T start, T end, IRangeEnumerator<T> enumerator)
        {
            this.Start = start;
            this.End = end;
            this.enumerator = enumerator;
        }

        public void Deconstruct(out T start, out T end)
        {
            start = this.Start;
            end = this.End;
        }

        public ReadRange<T> With(in T? Start = null, in T? End = null, IRangeEnumerator<T> Enumerator = null)
            => new ReadRange<T>(
                Start ?? this.Start,
                End ?? this.End,
                Enumerator ?? this.enumerator
            );

        public override bool Equals(object obj)
            => obj is ReadRange<T> other &&
               this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(in ReadRange<T> other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public bool Equals(ReadRange<T> other)
            => this.Start.Equals(other.Start) &&
               this.End.Equals(other.End);

        public override int GetHashCode()
        {
            var hashCode = -1676728671;
            hashCode = hashCode * -1521134295 + this.Start.GetHashCode();
            hashCode = hashCode * -1521134295 + this.End.GetHashCode();
            return hashCode;
        }

        public IEnumerator<T> GetEnumerator()
            => (this.enumerator ?? new Enumerator()).Enumerate(this.Start, this.End);

        public static implicit operator ReadRange<T>(in (T start, T end) value)
            => new ReadRange<T>(value.start, value.end);

        public static bool operator ==(in ReadRange<T> lhs, in ReadRange<T> rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in ReadRange<T> lhs, in ReadRange<T> rhs)
            => !lhs.Equals(in rhs);

        private ReadRange(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Start = (T)info.GetValue(nameof(this.Start), typeof(T));
            }
            catch
            {
                this.Start = default;
            }

            try
            {
                this.End = (T)info.GetValue(nameof(this.End), typeof(T));
            }
            catch
            {
                this.End = default;
            }

            this.enumerator = null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Start), this.Start);
            info.AddValue(nameof(this.End), this.End);
        }

        private struct Enumerator : IRangeEnumerator<T>
        {
            public IEnumerator<T> Enumerate(T start, T end)
            {
                yield return start;
                yield return end;
            }
        }
    }
}
