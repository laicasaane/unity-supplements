namespace System
{
    /// <summary>
    /// 3D array index
    /// </summary>
    [Serializable]
    public readonly struct Index3 : IEquatable<Index3>, IComparable<Index3>
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public Index3(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Converts to 1D array index.
        /// </summary>
        /// <param name="xLength">The length of dimension <see cref="X"/> of the 3D array.</param>
        /// <param name="yLength">The length of dimension <see cref="Y"/> of the 3D array.</param>
        /// <returns>
        /// <para>If any length is zero, returns zero.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public int ToIndex1(int xLength, int yLength)
        {
            if (xLength == 0 || yLength == 0)
                return 0;

            return this.X + (this.Y * xLength) + (this.Z * xLength * yLength);
        }

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.X;
            hashCode = hashCode * -1521134295 + this.Y;
            hashCode = hashCode * -1521134295 + this.Z;
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is Index3 other &&
               this.X == other.X &&
               this.Y == other.Y &&
               this.Z == other.Z;

        public int CompareTo(Index3 other)
        {
            var comp = this.X.CompareTo(other.X);

            if (comp == 0)
            {
                var comp1 = this.Y.CompareTo(other.Y);

                if (comp1 == 0)
                    return this.Z.CompareTo(other.Z);

                return comp1;
            }

            return comp;
        }

        public bool Equals(Index3 other)
            => this.X == other.X && this.Y == other.Y && this.Z == other.Z;

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = this.X;
            y = this.Y;
            z = this.Z;
        }

        public override string ToString()
            => $"({this.X}, {this.Y}, {this.Z})";

        /// <summary>
        /// Shorthand for writing <see cref="Index3"/>(0, 0, 0).
        /// </summary>
        public static Index3 Zero { get; } = new Index3(0, 0, 0);

        public static implicit operator Index3(in (int, int, int) value)
            => new Index3(value.Item1, value.Item2, value.Item3);

        public static Index3 operator +(in Index3 lhs, in Index3 rhs)
            => new Index3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);

        public static Index3 operator -(in Index3 lhs, in Index3 rhs)
            => new Index3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);

        public static Index3 operator -(in Index3 a)
            => new Index3(-a.X, -a.Y, -a.Z);

        public static Index3 operator *(in Index3 lhs, int rhs)
            => new Index3(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);

        public static Index3 operator *(int lhs, in Index3 rhs)
            => new Index3(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z);

        public static Index3 operator /(in Index3 lhs, int rhs)
            => new Index3(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);

        public static bool operator ==(in Index3 lhs, in Index3 rhs)
            => lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;

        public static bool operator !=(in Index3 lhs, in Index3 rhs)
            => lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;

        /// <summary>
        /// Converts 1D array index to 3D array index.
        /// </summary>
        /// <param name="index1">Index in the 1D array.</param>
        /// <param name="xLength">The length of dimension <see cref="X"/> of the 3D array.</param>
        /// <param name="yLength">The length of dimension <see cref="Y"/> of the 3D array.</param>
        /// <returns>
        /// <para>If any length is zero, returns <see cref="Zero"/>.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public static Index3 Convert(int index1, int xLength, int yLength)
        {
            if (xLength == 0 || yLength == 0)
                return Zero;

            var xyLength = xLength * yLength;
            var z = index1 / xyLength;
            var iXY = index1 - (z * xyLength);
            var y = iXY / xLength;
            var x = iXY % xLength;

            return new Index3(x, y, z);
        }
    }
}
