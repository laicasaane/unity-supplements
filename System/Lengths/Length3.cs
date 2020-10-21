using System.Runtime.Serialization;

namespace System
{
    /// <summary>
    /// Represent the lengths of the 3D array. The value of each component is greater than or equal to 0.
    /// </summary>
    [Serializable]
    public readonly struct Length3 : IEquatableReadOnlyStruct<Length3>, IComparableReadOnlyStruct<Length3>, ISerializable
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.A;
                    case 1: return this.B;
                    case 2: return this.C;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public Length3(int a, int b, int c)
        {
            this.A = Math.Max(a, 0);
            this.B = Math.Max(b, 0);
            this.C = Math.Max(c, 0);
        }

        public void Deconstruct(out int a, out int b, out int c)
        {
            a = this.A;
            b = this.B;
            c = this.C;
        }

        public Length3 With(int? A = null, int? B = null, int? C = null)
            => new Length3(
                A ?? this.A,
                B ?? this.B,
                C ?? this.C
            );

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

        public override string ToString()
            => $"({this.A}, {this.B}, {this.C})";

        public override int GetHashCode()
        {
            var hashCode = 793064651;
            hashCode = hashCode * -1521134295 + this.A.GetHashCode();
            hashCode = hashCode * -1521134295 + this.B.GetHashCode();
            hashCode = hashCode * -1521134295 + this.C.GetHashCode();
            return hashCode;
        }

        private Length3(SerializationInfo info, StreamingContext context)
        {
            this.A = info.GetInt32OrDefault(nameof(this.A));
            this.B = info.GetInt32OrDefault(nameof(this.B));
            this.C = info.GetInt32OrDefault(nameof(this.C));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.A), this.A);
            info.AddValue(nameof(this.B), this.B);
            info.AddValue(nameof(this.C), this.C);
        }

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

        public static Length3 operator *(in Length3 lhs, in Length3 rhs)
            => new Length3(lhs.A * rhs.A, lhs.B * rhs.B, lhs.C * rhs.C);

        public static Length3 operator /(in Length3 lhs, int rhs)
            => new Length3(lhs.A / rhs, lhs.B / rhs, lhs.C / rhs);

        public static Length3 operator /(in Length3 lhs, in Length3 rhs)
            => new Length3(lhs.A / rhs.A, lhs.B / rhs.B, lhs.C / rhs.C);

        public static Length3 operator %(in Length3 lhs, int rhs)
            => new Length3(lhs.A % rhs, lhs.B % rhs, lhs.C % rhs);

        public static Length3 operator %(in Length3 lhs, in Length3 rhs)
            => new Length3(lhs.A % rhs.A, lhs.B % rhs.B, lhs.C % rhs.C);

        public static bool operator ==(in Length3 lhs, in Length3 rhs)
            => lhs.A == rhs.A && lhs.B == rhs.B && lhs.C == rhs.C;

        public static bool operator !=(in Length3 lhs, in Length3 rhs)
            => lhs.A != rhs.A || lhs.B != rhs.B || lhs.C != rhs.C;

        public static implicit operator Length2(in Length3 value)
            => new Length2(value.A, value.B);
    }
}
