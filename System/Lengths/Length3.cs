namespace System
{
    /// <summary>
    /// 3D array length
    /// </summary>
    [Serializable]
    public readonly struct Length3 : IEquatableReadOnlyStruct<Length3>, IComparableReadOnlyStruct<Length3>
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;

        public Length3(int a, int b, int c)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.A;
            hashCode = hashCode * -1521134295 + this.B;
            hashCode = hashCode * -1521134295 + this.C;
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is Length3 other &&
               this.A == other.A &&
               this.B == other.B &&
               this.C == other.C;

        public int CompareTo(Length3 other)
        {
            var comp = this.A.CompareTo(other.A);

            if (comp != 0)
                return comp;

            var comp1 = this.B.CompareTo(other.B);

            if (comp1 == 0)
                return this.C.CompareTo(other.C);

            return comp1;
        }

        public int CompareTo(in Length3 other)
        {
            var comp = this.A.CompareTo(other.A);

            if (comp != 0)
                return comp;

            var comp1 = this.B.CompareTo(other.B);

            if (comp1 == 0)
                return this.C.CompareTo(other.C);

            return comp1;
        }

        public bool Equals(Length3 other)
            => this.A == other.A && this.B == other.B && this.C == other.C;

        public bool Equals(in Length3 other)
            => this.A == other.A && this.B == other.B && this.C == other.C;

        public void Deconstruct(out int a, out int b, out int c)
        {
            a = this.A;
            b = this.B;
            c = this.C;
        }

        public override string ToString()
            => $"({this.A}, {this.B}, {this.C})";

        /// <summary>
        /// Shorthand for writing <see cref="Length3"/>(0, 0, 0).
        /// </summary>
        public static Length3 Zero { get; } = new Length3(0, 0, 0);

        public static implicit operator Length3(in (int, int, int) value)
            => new Length3(value.Item1, value.Item2, value.Item3);

        public static Length3 operator +(in Length3 lhs, in Length3 rhs)
            => new Length3(lhs.A + rhs.A, lhs.B + rhs.B, lhs.C + rhs.C);

        public static Length3 operator -(in Length3 lhs, in Length3 rhs)
            => new Length3(lhs.A - rhs.A, lhs.B - rhs.B, lhs.C - rhs.C);

        public static Length3 operator -(in Length3 a)
            => new Length3(-a.A, -a.B, -a.C);

        public static Length3 operator *(in Length3 lhs, int rhs)
            => new Length3(lhs.A * rhs, lhs.B * rhs, lhs.C * rhs);

        public static Length3 operator *(int lhs, in Length3 rhs)
            => new Length3(lhs * rhs.A, lhs * rhs.B, lhs * rhs.C);

        public static Length3 operator /(in Length3 lhs, int rhs)
            => new Length3(lhs.A / rhs, lhs.B / rhs, lhs.C / rhs);

        public static bool operator ==(in Length3 lhs, in Length3 rhs)
            => lhs.A == rhs.A && lhs.B == rhs.B && lhs.C == rhs.C;

        public static bool operator !=(in Length3 lhs, in Length3 rhs)
            => lhs.A != rhs.A || lhs.B != rhs.B || lhs.C != rhs.C;
    }
}
