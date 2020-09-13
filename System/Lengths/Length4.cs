namespace System
{
    /// <summary>
    /// Represent the lengths of the 4D array. The value of each component is greater than or equal to 0.
    /// </summary>
    [Serializable]
    public readonly struct Length4 : IEquatableReadOnlyStruct<Length4>, IComparableReadOnlyStruct<Length4>
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;
        public readonly int D;

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
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public Length4(int a, int b, int c, int d)
        {
            this.A = Math.Max(a, 0);
            this.B = Math.Max(b, 0);
            this.C = Math.Max(c, 0);
            this.D = Math.Max(d, 0);
        }

        public void Deconstruct(out int a, out int b, out int c, out int d)
        {
            a = this.A;
            b = this.B;
            c = this.C;
            d = this.D;
        }

        public Length4 With(int? A = null, int? B = null, int? C = null, int? D = null)
            => new Length4(
                A ?? this.A,
                B ?? this.B,
                C ?? this.C,
                D ?? this.D
            );

        public override bool Equals(object obj)
            => obj is Length4 other &&
               this.A == other.A &&
               this.B == other.B &&
               this.C == other.C &&
               this.D == other.D;

        public int CompareTo(Length4 other)
        {
            var comp = this.A.CompareTo(other.A);

            if (comp != 0)
                return comp;

            var comp1 = this.B.CompareTo(other.B);

            if (comp1 != 0)
                return comp1;

            var comp2 = this.C.CompareTo(other.C);

            if (comp2 == 0)
                return this.D.CompareTo(other.D);

            return comp2;
        }

        public int CompareTo(in Length4 other)
        {
            var comp = this.A.CompareTo(other.A);

            if (comp != 0)
                return comp;

            var comp1 = this.B.CompareTo(other.B);

            if (comp1 != 0)
                return comp1;

            var comp2 = this.C.CompareTo(other.C);

            if (comp2 == 0)
                return this.D.CompareTo(other.D);

            return comp2;
        }

        public bool Equals(Length4 other)
            => this.A == other.A && this.B == other.B &&
               this.C == other.C && this.D == other.D;

        public bool Equals(in Length4 other)
            => this.A == other.A && this.B == other.B &&
               this.C == other.C && this.D == other.D;

        public override string ToString()
            => $"({this.A}, {this.B}, {this.C}, {this.D})";

        public override int GetHashCode()
        {
            var hashCode = -1408250474;
            hashCode = hashCode * -1521134295 + this.A.GetHashCode();
            hashCode = hashCode * -1521134295 + this.B.GetHashCode();
            hashCode = hashCode * -1521134295 + this.C.GetHashCode();
            hashCode = hashCode * -1521134295 + this.D.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Shorthand for writing <see cref="Length4"/>(0, 0, 0, 0).
        /// </summary>
        public static Length4 Zero { get; } = new Length4(0, 0, 0, 0);

        public static implicit operator Length4(in (int, int, int, int) value)
            => new Length4(value.Item1, value.Item2, value.Item3, value.Item4);

        public static Length4 operator +(in Length4 lhs, in Length4 rhs)
            => new Length4(lhs.A + rhs.A, lhs.B + rhs.B, lhs.C + rhs.C, lhs.D + rhs.D);

        public static Length4 operator -(in Length4 lhs, in Length4 rhs)
            => new Length4(lhs.A - rhs.A, lhs.B - rhs.B, lhs.C - rhs.C, lhs.D - rhs.D);

        public static Length4 operator -(in Length4 a)
            => new Length4(-a.A, -a.B, -a.C, -a.D);

        public static Length4 operator *(in Length4 lhs, int rhs)
            => new Length4(lhs.A * rhs, lhs.B * rhs, lhs.C * rhs, lhs.D * rhs);

        public static Length4 operator *(int lhs, in Length4 rhs)
            => new Length4(lhs * rhs.A, lhs * rhs.B, lhs * rhs.C, lhs * rhs.D);

        public static Length4 operator /(in Length4 lhs, int rhs)
            => new Length4(lhs.A / rhs, lhs.B / rhs, lhs.C / rhs, lhs.D / rhs);

        public static bool operator ==(in Length4 lhs, in Length4 rhs)
            => lhs.A == rhs.A && lhs.B == rhs.B && lhs.C == rhs.C && lhs.D == rhs.D;

        public static bool operator !=(in Length4 lhs, in Length4 rhs)
            => lhs.A != rhs.A || lhs.B != rhs.B || lhs.C != rhs.C || lhs.D != rhs.D;

        public static implicit operator Length2(in Length4 value)
            => new Length2(value.A, value.B);

        public static implicit operator Length3(in Length4 value)
            => new Length3(value.A, value.B, value.C);
    }
}
