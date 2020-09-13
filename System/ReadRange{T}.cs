namespace System
{
    [Serializable]
    public readonly struct ReadRange<T> : IEquatableReadOnlyStruct<ReadRange<T>> where T : struct, IEquatable<T>
    {
        public readonly T Start;
        public readonly T End;

        public ReadRange(T start, T end)
        {
            this.Start = start;
            this.End = end;
        }

        public void Deconstruct(out T start, out T end)
        {
            start = this.Start;
            end = this.End;
        }

        public ReadRange<T> With(in T? Start = null, in T? End = null)
            => new ReadRange<T>(
                Start ?? this.Start,
                End ?? this.End
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

        public static implicit operator ReadRange<T>(in (T start, T end) value)
            => new ReadRange<T>(value.start, value.end);
    }
}
