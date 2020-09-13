using System.Runtime.Serialization;

namespace System
{
    /// <summary>
    /// Represent the lengths of the 5D array. The value of each component is greater than or equal to 0.
    /// </summary>
    [Serializable]
    public readonly struct Length5 : IEquatableReadOnlyStruct<Length5>, IComparableReadOnlyStruct<Length5>, ISerializable
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;
        public readonly int D;
        public readonly int E;

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.A;
                    case 1: return this.B;
                    case 2: return this.C;
                    case 3: return this.D;
                    case 4: return this.E;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public Length5(int a, int b, int c, int d, int e)
        {
            this.A = Math.Max(a, 0);
            this.B = Math.Max(b, 0);
            this.C = Math.Max(c, 0);
            this.D = Math.Max(d, 0);
            this.E = Math.Max(e, 0);
        }

        public void Deconstruct(out int a, out int b, out int c, out int d, out int e)
        {
            a = this.A;
            b = this.B;
            c = this.C;
            d = this.D;
            e = this.E;
        }

        public Length5 With(int? A = null, int? B = null, int? C = null, int? D = null, int? E = null)
            => new Length5(
                A ?? this.A,
                B ?? this.B,
                C ?? this.C,
                D ?? this.D,
                E ?? this.E
            );

        public override bool Equals(object obj)
            => obj is Length5 other &&
               this.A == other.A &&
               this.B == other.B &&
               this.C == other.C &&
               this.D == other.D &&
               this.E == other.E;

        public int CompareTo(Length5 other)
        {
            var comp = this.A.CompareTo(other.A);

            if (comp != 0)
                return comp;

            var comp1 = this.B.CompareTo(other.B);

            if (comp1 != 0)
                return comp1;

            var comp2 = this.C.CompareTo(other.C);

            if (comp2 != 0)
                return comp2;

            var comp3 = this.D.CompareTo(other.D);

            if (comp3 == 0)
                return this.E.CompareTo(other.E);

            return comp3;
        }

        public int CompareTo(in Length5 other)
        {
            var comp = this.A.CompareTo(other.A);

            if (comp != 0)
                return comp;

            var comp1 = this.B.CompareTo(other.B);

            if (comp1 != 0)
                return comp1;

            var comp2 = this.C.CompareTo(other.C);

            if (comp2 != 0)
                return comp2;

            var comp3 = this.D.CompareTo(other.D);

            if (comp3 == 0)
                return this.E.CompareTo(other.E);

            return comp3;
        }

        public bool Equals(Length5 other)
            => this.A == other.A && this.B == other.B &&
               this.C == other.C && this.D == other.D &&
               this.E == other.E;

        public bool Equals(in Length5 other)
            => this.A == other.A && this.B == other.B &&
               this.C == other.C && this.D == other.D &&
               this.E == other.E;

        public override string ToString()
            => $"({this.A}, {this.B}, {this.C}, {this.D}, {this.E})";

        public override int GetHashCode()
        {
            var hashCode = 2124721030;
            hashCode = hashCode * -1521134295 + this.A.GetHashCode();
            hashCode = hashCode * -1521134295 + this.B.GetHashCode();
            hashCode = hashCode * -1521134295 + this.C.GetHashCode();
            hashCode = hashCode * -1521134295 + this.D.GetHashCode();
            hashCode = hashCode * -1521134295 + this.E.GetHashCode();
            return hashCode;
        }

        private Length5(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.A = info.GetInt32(nameof(this.A));
            }
            catch
            {
                this.A = default;
            }

            try
            {
                this.B = info.GetInt32(nameof(this.B));
            }
            catch
            {
                this.B = default;
            }

            try
            {
                this.C = info.GetInt32(nameof(this.C));
            }
            catch
            {
                this.C = default;
            }

            try
            {
                this.D = info.GetInt32(nameof(this.D));
            }
            catch
            {
                this.D = default;
            }

            try
            {
                this.E = info.GetInt32(nameof(this.E));
            }
            catch
            {
                this.E = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.A), this.A);
            info.AddValue(nameof(this.B), this.B);
            info.AddValue(nameof(this.C), this.C);
            info.AddValue(nameof(this.D), this.D);
            info.AddValue(nameof(this.E), this.E);
        }

        /// <summary>
        /// Shorthand for writing <see cref="Length5"/>(0, 0, 0, 0, 0).
        /// </summary>
        public static Length5 Zero { get; } = new Length5(0, 0, 0, 0, 0);

        public static implicit operator Length5(in (int, int, int, int, int) value)
            => new Length5(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);

        public static Length5 operator +(in Length5 lhs, in Length5 rhs)
            => new Length5(lhs.A + rhs.A, lhs.B + rhs.B, lhs.C + rhs.C, lhs.D + rhs.D, lhs.E + rhs.E);

        public static Length5 operator -(in Length5 lhs, in Length5 rhs)
            => new Length5(lhs.A - rhs.A, lhs.B - rhs.B, lhs.C - rhs.C, lhs.D - rhs.D, lhs.E - rhs.E);

        public static Length5 operator -(in Length5 a)
            => new Length5(-a.A, -a.B, -a.C, -a.D, -a.E);

        public static Length5 operator *(in Length5 lhs, int rhs)
            => new Length5(lhs.A * rhs, lhs.B * rhs, lhs.C * rhs, lhs.D * rhs, lhs.E * rhs);

        public static Length5 operator *(int lhs, in Length5 rhs)
            => new Length5(lhs * rhs.A, lhs * rhs.B, lhs * rhs.C, lhs * rhs.D, lhs * rhs.E);

        public static Length5 operator /(in Length5 lhs, int rhs)
            => new Length5(lhs.A / rhs, lhs.B / rhs, lhs.C / rhs, lhs.D / rhs, lhs.E / rhs);

        public static bool operator ==(in Length5 lhs, in Length5 rhs)
            => lhs.A == rhs.A && lhs.B == rhs.B && lhs.C == rhs.C && lhs.D == rhs.D && lhs.E == rhs.E;

        public static bool operator !=(in Length5 lhs, in Length5 rhs)
            => lhs.A != rhs.A || lhs.B != rhs.B || lhs.C != rhs.C || lhs.D != rhs.D || lhs.E != rhs.E;

        public static implicit operator Length2(in Length5 value)
            => new Length2(value.A, value.B);

        public static implicit operator Length3(in Length5 value)
            => new Length3(value.A, value.B, value.C);

        public static implicit operator Length4(in Length5 value)
            => new Length4(value.A, value.B, value.C, value.D);
    }
}
