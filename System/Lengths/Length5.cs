﻿namespace System
{
    /// <summary>
    /// 5D array length
    /// </summary>
    [Serializable]
    public readonly struct Length5 : IEquatableReadOnlyStruct<Length5>, IComparableReadOnlyStruct<Length5>
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;
        public readonly int D;
        public readonly int E;

        public Length5(int a, int b, int c, int d, int e)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
            this.E = e;
        }

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.A;
            hashCode = hashCode * -1521134295 + this.B;
            hashCode = hashCode * -1521134295 + this.C;
            hashCode = hashCode * -1521134295 + this.D;
            hashCode = hashCode * -1521134295 + this.E;
            return hashCode;
        }

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

        public void Deconstruct(out int a, out int b, out int c, out int d, out int e)
        {
            a = this.A;
            b = this.B;
            c = this.C;
            d = this.D;
            e = this.E;
        }

        public override string ToString()
            => $"({this.A}, {this.B}, {this.C}, {this.D}, {this.E})";

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