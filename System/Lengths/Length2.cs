﻿namespace System
{
    /// <summary>
    /// 2D array length
    /// </summary>
    [Serializable]
    public readonly struct Length2 : IEquatableReadOnlyStruct<Length2>, IComparableReadOnlyStruct<Length2>
    {
        public readonly int A;
        public readonly int B;

        public Length2(int a, int b)
        {
            this.A = a;
            this.B = b;
        }

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.A;
            hashCode = hashCode * -1521134295 + this.B;
            return hashCode;
        }

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

        public void Deconstruct(out int a, out int b)
        {
            a = this.A;
            b = this.B;
        }

        public override string ToString()
            => $"({this.A}, {this.B})";

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

        public static Length2 operator /(in Length2 lhs, int rhs)
            => new Length2(lhs.A / rhs, lhs.B / rhs);

        public static bool operator ==(in Length2 lhs, in Length2 rhs)
            => lhs.A == rhs.A && lhs.B == rhs.B;

        public static bool operator !=(in Length2 lhs, in Length2 rhs)
            => lhs.A != rhs.A || lhs.B != rhs.B;
    }
}