namespace System
{
    /// <summary>
    /// Represents an index of the 5D variable size array
    /// </summary>
    [Serializable]
    public readonly struct Index5 : IEquatableReadOnlyStruct<Index5>, IComparableReadOnlyStruct<Index5>
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

        public Index5(int a, int b, int c, int d, int e)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
            this.E = e;
        }

        public void Deconstruct(out int a, out int b, out int c, out int d, out int e)
        {
            a = this.A;
            b = this.B;
            c = this.C;
            d = this.D;
            e = this.E;
        }

        public Index5 With(int? A = null, int? B = null, int? C = null, int? D = null, int? E = null)
            => new Index5(
                A ?? this.A,
                B ?? this.B,
                C ?? this.C,
                D ?? this.D,
                E ?? this.E
            );

        /// <summary>
        /// Converts to 1D array index.
        /// </summary>
        /// <param name="lengthA">Length of the dimension <see cref="A"/> of the 5D array.</param>
        /// <param name="lengthB">Length of the dimension <see cref="B"/> of the 5D array.</param>
        /// <param name="lengthC">Length of the dimension <see cref="C"/> of the 5D array.</param>
        /// <param name="lengthD">Length of the dimension <see cref="D"/> of the 5D array.</param>
        /// <returns>
        /// <para>If any length is less than or equal to zero, returns zero.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public int ToIndex1(int lengthA, int lengthB, int lengthC, int lengthD)
        {
            if (lengthA == 0 || lengthB == 0 || lengthC == 0 || lengthD == 0)
                return 0;

            var ab = lengthA * lengthB;
            var abc = ab * lengthC;

            return this.A + (this.B * lengthA) + (this.C * ab) + (this.D * abc) + (this.E * abc * lengthD);
        }

        public int ToIndex1(in Length4 length)
        {
            if (length.A == 0 || length.B == 0 || length.C == 0 || length.D == 0)
                return 0;

            var ab = length.A * length.B;
            var abc = ab * length.C;

            return this.A + (this.B * length.A) + (this.C * ab) + (this.D * abc) + (this.E * abc * length.D);
        }

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

        public override bool Equals(object obj)
            => obj is Index5 other &&
               this.A == other.A &&
               this.B == other.B &&
               this.C == other.C &&
               this.D == other.D &&
               this.E == other.E;

        public int CompareTo(Index5 other)
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

        public int CompareTo(in Index5 other)
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

        public bool Equals(Index5 other)
            => this.A == other.A && this.B == other.B &&
               this.C == other.C && this.D == other.D &&
               this.E == other.E;

        public bool Equals(in Index5 other)
            => this.A == other.A && this.B == other.B &&
               this.C == other.C && this.D == other.D &&
               this.E == other.E;

        public override string ToString()
            => $"({this.A}, {this.B}, {this.C}, {this.D}, {this.E})";

        /// <summary>
        /// Shorthand for writing <see cref="Index5"/>(0, 0, 0, 0, 0).
        /// </summary>
        public static Index5 Zero { get; } = new Index5(0, 0, 0, 0, 0);

        public static implicit operator Index5(in (int, int, int, int, int) value)
            => new Index5(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);

        public static Index5 operator +(in Index5 lhs, in Index5 rhs)
            => new Index5(lhs.A + rhs.A, lhs.B + rhs.B, lhs.C + rhs.C, lhs.D + rhs.D, lhs.E + rhs.E);

        public static Index5 operator -(in Index5 lhs, in Index5 rhs)
            => new Index5(lhs.A - rhs.A, lhs.B - rhs.B, lhs.C - rhs.C, lhs.D - rhs.D, lhs.E - rhs.E);

        public static Index5 operator -(in Index5 a)
            => new Index5(-a.A, -a.B, -a.C, -a.D, -a.E);

        public static Index5 operator *(in Index5 lhs, int rhs)
            => new Index5(lhs.A * rhs, lhs.B * rhs, lhs.C * rhs, lhs.D * rhs, lhs.E * rhs);

        public static Index5 operator *(int lhs, in Index5 rhs)
            => new Index5(lhs * rhs.A, lhs * rhs.B, lhs * rhs.C, lhs * rhs.D, lhs * rhs.E);

        public static Index5 operator /(in Index5 lhs, int rhs)
            => new Index5(lhs.A / rhs, lhs.B / rhs, lhs.C / rhs, lhs.D / rhs, lhs.E / rhs);

        public static bool operator ==(in Index5 lhs, in Index5 rhs)
            => lhs.A == rhs.A && lhs.B == rhs.B && lhs.C == rhs.C && lhs.D == rhs.D && lhs.E == rhs.E;

        public static bool operator !=(in Index5 lhs, in Index5 rhs)
            => lhs.A != rhs.A || lhs.B != rhs.B || lhs.C != rhs.C || lhs.D != rhs.D || lhs.E != rhs.E;

        public static implicit operator Index2(in Index5 value)
            => new Index2(value.A, value.B);

        public static implicit operator Index3(in Index5 value)
            => new Index3(value.A, value.B, value.C);

        public static implicit operator Index4(in Index5 value)
            => new Index4(value.A, value.B, value.C, value.D);

        /// <summary>
        /// Converts 1D index to 5D index.
        /// </summary>
        /// <param name="index1">Index in the 1D array.</param>
        /// <param name="lengthA">Length of the dimension <see cref="A"/> of the 5D array.</param>
        /// <param name="lengthB">Length of the dimension <see cref="B"/> of the 5D array.</param>
        /// <param name="lengthC">Length of the dimension <see cref="C"/> of the 5D array.</param>
        /// <param name="lengthD">Length of the dimension <see cref="D"/> of the 5D array.</param>
        /// <returns>
        /// <para>If any length is less than or equal to zero, returns <see cref="Zero"/>.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public static Index5 Convert(int index1, int lengthA, int lengthB, int lengthC, int lengthD)
        {
            if (lengthA <= 0 || lengthB <= 0 || lengthC <= 0 || lengthD <= 0)
                return Zero;

            var alengthB = lengthA * lengthB;
            var ablengthC = alengthB * lengthC;
            var abclengthD = ablengthC * lengthD;
            var e = index1 / abclengthD;
            var i_abcd = index1 - (e * abclengthD);
            var d = i_abcd / ablengthC;
            var i_abc = i_abcd - (d * ablengthC);
            var c = i_abc / alengthB;
            var i_ab = i_abc - (c * alengthB);
            var b = i_ab / lengthA;
            var a = i_ab % lengthA;

            return new Index5(a, b, c, d, e);
        }

        public static Index5 Convert(int index1, in Length4 length)
        {
            if (length.A <= 0 || length.B <= 0 || length.C <= 0 || length.D <= 0)
                return Zero;

            var alengthB = length.A * length.B;
            var ablengthC = alengthB * length.C;
            var abclengthD = ablengthC * length.D;
            var e = index1 / abclengthD;
            var i_abcd = index1 - (e * abclengthD);
            var d = i_abcd / ablengthC;
            var i_abc = i_abcd - (d * ablengthC);
            var c = i_abc / alengthB;
            var i_ab = i_abc - (c * alengthB);
            var b = i_ab / length.A;
            var a = i_ab % length.A;

            return new Index5(a, b, c, d, e);
        }
    }
}
