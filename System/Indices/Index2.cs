namespace System
{
    /// <summary>
    /// 2D array index
    /// </summary>
    [Serializable]
    public readonly struct Index2 : IEquatable<Index2>, IComparable<Index2>
    {
        public readonly int X;
        public readonly int Y;

        public Index2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Converts to 1D array index.
        /// </summary>
        /// <param name="xLength">The length of dimension <see cref="X"/> of the 2D array.</param>
        /// <returns>
        /// <para>If length is zero, returns zero.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public int ToIndex1(int xLength)
            => xLength == 0 ? 0 : this.X + this.Y * xLength;

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.X;
            hashCode = hashCode * -1521134295 + this.Y;
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is Index2 other &&
               this.X == other.X &&
               this.Y == other.Y;

        public int CompareTo(Index2 other)
        {
            var comp = this.X.CompareTo(other.X);

            if (comp == 0)
                return this.Y.CompareTo(other.Y);

            return comp;
        }

        public bool Equals(Index2 other)
            => this.X == other.X && this.Y == other.Y;

        public void Deconstruct(out int row, out int column)
        {
            row = this.X;
            column = this.Y;
        }

        public override string ToString()
            => $"({this.X}, {this.Y})";

        /// <summary>
        /// Shorthand for writing <see cref="Index2"/>(0, 0).
        /// </summary>
        public static Index2 Zero { get; } = new Index2(0, 0);

        public static implicit operator Index2(in (int, int) value)
            => new Index2(value.Item1, value.Item2);

        public static Index2 operator +(in Index2 lhs, in Index2 rhs)
            => new Index2(lhs.X + rhs.X, lhs.Y + rhs.Y);

        public static Index2 operator -(in Index2 lhs, in Index2 rhs)
            => new Index2(lhs.X - rhs.X, lhs.Y - rhs.Y);

        public static Index2 operator -(in Index2 a)
            => new Index2(-a.X, -a.Y);

        public static Index2 operator *(in Index2 lhs, int rhs)
            => new Index2(lhs.X * rhs, lhs.Y * rhs);

        public static Index2 operator *(int lhs, in Index2 rhs)
            => new Index2(lhs * rhs.X, lhs * rhs.Y);

        public static Index2 operator /(in Index2 lhs, int rhs)
            => new Index2(lhs.X / rhs, lhs.Y / rhs);

        public static bool operator ==(in Index2 lhs, in Index2 rhs)
            => lhs.X == rhs.X && lhs.Y == rhs.Y;

        public static bool operator !=(in Index2 lhs, in Index2 rhs)
            => lhs.X != rhs.X || lhs.Y != rhs.Y;

        /// <summary>
        /// Converts 1D array index to 2D array index.
        /// </summary>
        /// <param name="index1">Index in the 1D array.</param>
        /// <param name="xLength">The length of dimension <see cref="X"/> of the 2D array.</param>
        /// <returns>
        /// <para>If length is zero, returns <see cref="Zero"/>.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public static Index2 Convert(int index1, int xLength)
            => xLength == 0 ? Zero: new Index2(index1 % xLength, index1 / xLength);
    }
}
