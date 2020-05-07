namespace System
{
    /// <summary>
    /// 4D array index
    /// </summary>
    [Serializable]
    public readonly struct Index4 : IEquatable<Index4>, IComparable<Index4>
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;
        public readonly int W;

        public Index4(int x, int y, int z, int w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Converts to 1D array index.
        /// </summary>
        /// <param name="xLength">The length of dimension <see cref="X"/> of the 4D array.</param>
        /// <param name="yLength">The length of dimension <see cref="Y"/> of the 4D array.</param>
        /// <param name="zLength">The length of dimension <see cref="Z"/> of the 4D array.</param>
        /// <returns>
        /// <para>If any length is zero, returns zero.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public int ToIndex1(int xLength, int yLength, int zLength)
        {
            if (xLength == 0 || yLength == 0 || zLength == 0)
                return 0;

            var xyArea = xLength * yLength;
            return this.X + (this.Y * xLength) + (this.Z * xyArea) + (this.W * xyArea * zLength);
        }

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.X;
            hashCode = hashCode * -1521134295 + this.Y;
            hashCode = hashCode * -1521134295 + this.Z;
            hashCode = hashCode * -1521134295 + this.W;
            return hashCode;
        }

        public override bool Equals(object obj)
            => obj is Index4 other &&
               this.X == other.X &&
               this.Y == other.Y &&
               this.Z == other.Z &&
               this.W == other.W;

        public int CompareTo(Index4 other)
        {
            var comp = this.X.CompareTo(other.X);

            if (comp == 0)
            {
                var comp1 = this.Y.CompareTo(other.Y);

                if (comp1 == 0)
                {
                    var comp2 = this.Z.CompareTo(other.Z);

                    if (comp2 == 0)
                        return this.W.CompareTo(other.W);

                    return comp2;
                }

                return comp1;
            }

            return comp;
        }

        public bool Equals(Index4 other)
            => this.X == other.X && this.Y == other.Y &&
               this.Z == other.Z && this.W == other.W;

        public void Deconstruct(out int x, out int y, out int z, out int w)
        {
            x = this.X;
            y = this.Y;
            z = this.Z;
            w = this.W;
        }

        public override string ToString()
            => $"({this.X}, {this.Y}, {this.Z}, {this.W})";

        /// <summary>
        /// Shorthand for writing <see cref="Index4"/>(0, 0, 0, 0).
        /// </summary>
        public static Index4 Zero { get; } = new Index4(0, 0, 0, 0);

        public static implicit operator Index4(in (int, int, int, int) value)
            => new Index4(value.Item1, value.Item2, value.Item3, value.Item4);

        public static Index4 operator +(in Index4 lhs, in Index4 rhs)
            => new Index4(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.W + rhs.W);

        public static Index4 operator -(in Index4 lhs, in Index4 rhs)
            => new Index4(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.W - rhs.W);

        public static Index4 operator -(in Index4 a)
            => new Index4(-a.X, -a.Y, -a.Z, -a.W);

        public static Index4 operator *(in Index4 lhs, int rhs)
            => new Index4(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs, lhs.W * rhs);

        public static Index4 operator *(int lhs, in Index4 rhs)
            => new Index4(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z, lhs * rhs.W);

        public static Index4 operator /(in Index4 lhs, int rhs)
            => new Index4(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs, lhs.W / rhs);

        public static bool operator ==(in Index4 lhs, in Index4 rhs)
            => lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z && lhs.W == rhs.W;

        public static bool operator !=(in Index4 lhs, in Index4 rhs)
            => lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z || lhs.W != rhs.W;

        /// <summary>
        /// Converts 1D array index to 4D array index.
        /// </summary>
        /// <param name="index1">Index in the 1D array.</param>
        /// <param name="xLength">The length of dimension <see cref="X"/> of the 4D array.</param>
        /// <param name="yLength">The length of dimension <see cref="Y"/> of the 4D array.</param>
        /// <param name="zLength">The length of dimension <see cref="Z"/> of the 4D array.</param>
        /// <returns>
        /// <para>If any length is zero, returns <see cref="Zero"/>.</para>
        /// <para>Otherwise, returns the converted value.</para>
        /// </returns>
        public static Index4 Convert(int index1, int xLength, int yLength, int zLength)
        {
            if (xLength == 0 || yLength == 0 || zLength == 0)
                return Zero;

            var xyLength = xLength * yLength;
            var xyzLength = xyLength * zLength;
            var w = index1 / xyzLength;
            var iXYZ = index1 - (w * xyzLength);
            var z = iXYZ / xyLength;
            var iXY = iXYZ - (z * xyLength);
            var y = iXY / xLength;
            var x = iXY % xLength;

            return new Index4(x, y, z, w);
        }
    }
}
