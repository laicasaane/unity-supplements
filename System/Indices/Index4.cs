namespace System
{
    /// <summary>
    /// Represents an index of the 4D variable size array
    /// </summary>
    [Serializable]
    public readonly struct Index4 : IEquatableReadOnlyStruct<Index4>, IComparableReadOnlyStruct<Index4>
    {
        public readonly int A;
        public readonly int B;
        public readonly int C;
        public readonly int D;

        [Obsolete("This property has been deprecated. Use A instead.")]
        public int X => this.A;

        [Obsolete("This property has been deprecated. Use B instead.")]
        public int Y => this.B;

        [Obsolete("This property has been deprecated. Use C instead.")]
        public int Z => this.C;

        [Obsolete("This property has been deprecated. Use D instead.")]
        public int W => this.D;

        public Index4(int a, int b, int c, int d)
        {
            this.A = a;
            this.B = b;
            this.C = c;
            this.D = d;
        }

        /// <summary>
        /// Converts to 1D index.
        /// </summary>
        /// <param name="aLength">Length of the dimension <see cref="A"/> of the 4D array.</param>
        /// <param name="bLength">Length of the dimension <see cref="B"/> of the 4D array.</param>
        /// <param name="cLength">Length of the dimension <see cref="C"/> of the 4D array.</param>
        /// <returns>
        /// <para>If any length is less than or equal to zero, returns zero.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public int ToIndex1(int aLength, int bLength, int cLength)
        {
            if (aLength <= 0 || bLength <= 0 || cLength <= 0)
                return 0;

            var ab = aLength * bLength;
            return this.A + (this.B * aLength) + (this.C * ab) + (this.D * ab * cLength);
        }

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.A;
            hashCode = hashCode * -1521134295 + this.B;
            hashCode = hashCode * -1521134295 + this.C;
            hashCode = hashCode * -1521134295 + this.D;
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is Index4 other &&
               this.A == other.A &&
               this.B == other.B &&
               this.C == other.C &&
               this.D == other.D;

        public int CompareTo(Index4 other)
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

        public int CompareTo(in Index4 other)
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

        public bool Equals(Index4 other)
            => this.A == other.A && this.B == other.B &&
               this.C == other.C && this.D == other.D;

        public bool Equals(in Index4 other)
            => this.A == other.A && this.B == other.B &&
               this.C == other.C && this.D == other.D;

        public void Deconstruct(out int a, out int b, out int c, out int d)
        {
            a = this.A;
            b = this.B;
            c = this.C;
            d = this.D;
        }

        public override string ToString()
            => $"({this.A}, {this.B}, {this.C}, {this.D})";

        /// <summary>
        /// Shorthand for writing <see cref="Index4"/>(0, 0, 0, 0).
        /// </summary>
        public static Index4 Zero { get; } = new Index4(0, 0, 0, 0);

        public static implicit operator Index4(in (int, int, int, int) value)
            => new Index4(value.Item1, value.Item2, value.Item3, value.Item4);

        public static Index4 operator +(in Index4 lhs, in Index4 rhs)
            => new Index4(lhs.A + rhs.A, lhs.B + rhs.B, lhs.C + rhs.C, lhs.D + rhs.D);

        public static Index4 operator -(in Index4 lhs, in Index4 rhs)
            => new Index4(lhs.A - rhs.A, lhs.B - rhs.B, lhs.C - rhs.C, lhs.D - rhs.D);

        public static Index4 operator -(in Index4 a)
            => new Index4(-a.A, -a.B, -a.C, -a.D);

        public static Index4 operator *(in Index4 lhs, int rhs)
            => new Index4(lhs.A * rhs, lhs.B * rhs, lhs.C * rhs, lhs.D * rhs);

        public static Index4 operator *(int lhs, in Index4 rhs)
            => new Index4(lhs * rhs.A, lhs * rhs.B, lhs * rhs.C, lhs * rhs.D);

        public static Index4 operator /(in Index4 lhs, int rhs)
            => new Index4(lhs.A / rhs, lhs.B / rhs, lhs.C / rhs, lhs.D / rhs);

        public static bool operator ==(in Index4 lhs, in Index4 rhs)
            => lhs.A == rhs.A && lhs.B == rhs.B && lhs.C == rhs.C && lhs.D == rhs.D;

        public static bool operator !=(in Index4 lhs, in Index4 rhs)
            => lhs.A != rhs.A || lhs.B != rhs.B || lhs.C != rhs.C || lhs.D != rhs.D;

        /// <summary>
        /// Converts 1D index to 4D index.
        /// </summary>
        /// <param name="index1">Index in the 1D array.</param>
        /// <param name="aLength">Length of the dimension <see cref="A"/> of the 4D array.</param>
        /// <param name="bLength">Length of the dimension <see cref="B"/> of the 4D array.</param>
        /// <param name="cLength">Length of the dimension <see cref="C"/> of the 4D array.</param>
        /// <returns>
        /// <para>If any length is less than or equal to zero, returns <see cref="Zero"/>.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public static Index4 Convert(int index1, int aLength, int bLength, int cLength)
        {
            if (aLength <= 0 || bLength <= 0 || cLength <= 0)
                return Zero;

            var abLength = aLength * bLength;
            var abcLength = abLength * cLength;
            var d = index1 / abcLength;
            var i_abc = index1 - (d * abcLength);
            var c = i_abc / abLength;
            var i_ab = i_abc - (c * abLength);
            var b = i_ab / aLength;
            var a = i_ab % aLength;

            return new Index4(a, b, c, d);
        }
    }
}
