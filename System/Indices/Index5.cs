namespace System
{
    /// <summary>
    /// 5D array index
    /// </summary>
    [Serializable]
    public readonly struct Index5 : IEquatableReadOnlyStruct<Index5>, IComparableReadOnlyStruct<Index5>
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;
        public readonly int D;
        public readonly int E;

        public Index5(int a, int b, int c, int d, int e)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
            this.E = e;
        }

        /// <summary>
        /// Converts to 1D array index.
        /// </summary>
        /// <param name="aLength">The length of dimension <see cref="A"/> of the 5D array.</param>
        /// <param name="bLength">The length of dimension <see cref="B"/> of the 5D array.</param>
        /// <param name="cLength">The length of dimension <see cref="C"/> of the 5D array.</param>
        /// <param name="dLength">The length of dimension <see cref="D"/> of the 5D array.</param>
        /// <returns>
        /// <para>If any length is zero, returns zero.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public int ToIndex1(int aLength, int bLength, int cLength, int dLength)
        {
            if (aLength == 0 || bLength == 0 || cLength == 0 || dLength == 0)
                return 0;

            var ab = aLength * bLength;
            var abc = ab * cLength;

            return this.A + (this.B * aLength) + (this.C * ab) + (this.D * abc) + (this.E * abc * dLength);
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

        /// <summary>
        /// Converts 1D array index to 5D array index.
        /// </summary>
        /// <param name="index1">Index in the 1D array.</param>
        /// <param name="aLength">The length of dimension <see cref="A"/> of the 5D array.</param>
        /// <param name="bLength">The length of dimension <see cref="B"/> of the 5D array.</param>
        /// <param name="cLength">The length of dimension <see cref="C"/> of the 5D array.</param>
        /// <param name="dLength">The length of dimension <see cref="D"/> of the 5D array.</param>
        /// <returns>
        /// <para>If any length is zero, returns <see cref="Zero"/>.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public static Index5 Convert(int index1, int aLength, int bLength, int cLength, int dLength)
        {
            if (aLength == 0 || bLength == 0 || cLength == 0 || dLength == 0)
                return Zero;

            var abLength = aLength * bLength;
            var abcLength = abLength * cLength;
            var abcdLength = abcLength * dLength;
            var e = index1 / abcdLength;
            var i_abcd = index1 - (e * abcdLength);
            var d = i_abcd / abcLength;
            var i_abc = i_abcd - (d * abcLength);
            var c = i_abc / abLength;
            var i_ab = i_abc - (c * abLength);
            var b = i_ab / aLength;
            var a = i_ab % aLength;

            return new Index5(a, b, c, d, e);
        }
    }
}
