using System.Runtime.Serialization;

namespace System
{
    /// <summary>
    /// Represent the lengths of the 2D array. The value of each component is greater than or equal to 0.
    /// </summary>
    [Serializable]
    public readonly struct Length2 : IEquatableReadOnlyStruct<Length2>, IComparableReadOnlyStruct<Length2>, ISerializable
    {
        public readonly int A;
        public readonly int B;

        public int this[int index]
        {
            get
            {
                if (index == 0) return this.A;
                if (index == 1) return this.B;
                throw new IndexOutOfRangeException();
            }
        }

        public Length2(int a, int b)
        {
            this.A = Math.Max(a, 0);
            this.B = Math.Max(b, 0);
        }

        public void Deconstruct(out int a, out int b)
        {
            a = this.A;
            b = this.B;
        }

        public Length2 With(int? A = null, int? B = null)
            => new Length2(
                A ?? this.A,
                B ?? this.B
            );

        public override bool Equals(object obj)
            => obj is Length2 other &&
               this.A == other.A &&
               this.B == other.B;

        public int CompareTo(Length2 other)
        {
            var comp = this.A.CompareTo(other.A);

            if (comp == 0)
                return this.B.CompareTo(other.B);

            return comp;
        }

        public int CompareTo(in Length2 other)
        {
            var comp = this.A.CompareTo(other.A);

            if (comp == 0)
                return this.B.CompareTo(other.B);

            return comp;
        }

        public bool Equals(Length2 other)
            => this.A == other.A && this.B == other.B;

        public bool Equals(in Length2 other)
            => this.A == other.A && this.B == other.B;

        public override string ToString()
            => $"({this.A}, {this.B})";

        public override int GetHashCode()
        {
#if USE_SYSTEM_HASHCODE
            return HashCode.Combine(this.A, this.B);
#endif

#pragma warning disable CS0162 // Unreachable code detected
            var hashCode = -1817952719;
            hashCode = hashCode * -1521134295 + this.A.GetHashCode();
            hashCode = hashCode * -1521134295 + this.B.GetHashCode();
            return hashCode;
#pragma warning restore CS0162 // Unreachable code detected
        }

        private Length2(SerializationInfo info, StreamingContext context)
        {
            this.A = info.GetInt32OrDefault(nameof(this.A));
            this.B = info.GetInt32OrDefault(nameof(this.B));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.A), this.A);
            info.AddValue(nameof(this.B), this.B);
        }

        /// <summary>
        /// Shorthand for writing <see cref="Length2"/>(0, 0).
        /// </summary>
        public static Length2 Zero { get; } = new Length2(0, 0);

        public static implicit operator Length2(in (int, int) value)
            => new Length2(value.Item1, value.Item2);

        public static Length2 operator +(in Length2 lhs, in Length2 rhs)
            => new Length2(lhs.A + rhs.A, lhs.B + rhs.B);

        public static Length2 operator -(in Length2 lhs, in Length2 rhs)
            => new Length2(lhs.A - rhs.A, lhs.B - rhs.B);

        public static Length2 operator -(in Length2 a)
            => new Length2(-a.A, -a.B);

        public static Length2 operator *(in Length2 lhs, int rhs)
            => new Length2(lhs.A * rhs, lhs.B * rhs);

        public static Length2 operator *(int lhs, in Length2 rhs)
            => new Length2(lhs * rhs.A, lhs * rhs.B);

        public static Length2 operator *(in Length2 lhs, in Length2 rhs)
            => new Length2(lhs.A * rhs.A, lhs.B * rhs.B);

        public static Length2 operator /(in Length2 lhs, int rhs)
            => new Length2(lhs.A / rhs, lhs.B / rhs);

        public static Length2 operator /(in Length2 lhs, in Length2 rhs)
            => new Length2(lhs.A / rhs.A, lhs.B / rhs.B);

        public static Length2 operator %(in Length2 lhs, int rhs)
            => new Length2(lhs.A % rhs, lhs.B % rhs);

        public static Length2 operator %(in Length2 lhs, in Length2 rhs)
            => new Length2(lhs.A % rhs.A, lhs.B % rhs.B);

        public static bool operator ==(in Length2 lhs, in Length2 rhs)
            => lhs.A == rhs.A && lhs.B == rhs.B;

        public static bool operator !=(in Length2 lhs, in Length2 rhs)
            => lhs.A != rhs.A || lhs.B != rhs.B;
    }
}
