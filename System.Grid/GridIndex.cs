using System.Runtime.Serialization;

namespace System.Grid
{
    /// <summary>
    /// Represent the coordinates of the 2D grid. The value of each component is greater than or equal to 0.
    /// </summary>
    [Serializable]
    public readonly struct GridIndex : IEquatableReadOnlyStruct<GridIndex>, ISerializable
    {
        public readonly int Row;
        public readonly int Column;

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return this.Row;
                    case 1: return this.Column;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public GridIndex(int row, int column)
        {
            this.Row = Math.Max(row, 0);
            this.Column = Math.Max(column, 0);
        }

        private GridIndex(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Row = Math.Max(info.GetInt32(nameof(this.Row)), 0);
            }
            catch
            {
                this.Row = default;
            }

            try
            {
                this.Column = Math.Max(info.GetInt32(nameof(this.Column)), 0);
            }
            catch
            {
                this.Column = default;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Row), this.Row);
            info.AddValue(nameof(this.Column), this.Column);
        }

        public void Deconstruct(out int row, out int column)
        {
            row = this.Row;
            column = this.Column;
        }

        public GridIndex With(int? Row = null, int? Column = null)
            => new GridIndex(
                Row ?? this.Row,
                Column ?? this.Column
            );

        public int ToIndex1(int columnCount)
            => columnCount <= 0 ? 0 : this.Column + this.Row * columnCount;

        public int ToIndex1(in GridIndex size)
            => size.Column <= 0 ? 0 : this.Column + this.Row * size.Column;

        public override bool Equals(object obj)
            => obj is GridIndex other && this.Row == other.Row && this.Column == other.Column;

        public bool Equals(GridIndex other)
            => this.Row == other.Row && this.Column == other.Column;

        public bool Equals(in GridIndex other)
            => this.Row == other.Row && this.Column == other.Column;

        public override int GetHashCode()
        {
            var hashCode = 240067226;
            hashCode = hashCode * -1521134295 + this.Row.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Column.GetHashCode();
            return hashCode;
        }

        public override string ToString()
            => $"({this.Row}, {this.Column})";

        /// <summary>
        /// Shorthand for writing GridIndex(0, 0)
        /// </summary>
        public static GridIndex Zero { get; } = new GridIndex(0, 0);

        /// <summary>
        /// Shorthand for writing GridIndex(1, 1)
        /// </summary>
        public static GridIndex One { get; } = new GridIndex(1, 1);

        /// <summary>
        /// Shorthand for writing GridIndex(0, 1)
        /// </summary>
        public static GridIndex Right { get; } = new GridIndex(0, 1);

        /// <summary>
        /// Shorthand for writing GridIndex(1, 0)
        /// </summary>
        public static GridIndex Up { get; } = new GridIndex(1, 0);

        public static implicit operator GridIndex(in (int row, int column) value)
            => new GridIndex(value.row, value.column);

        public static bool operator ==(in GridIndex lhs, in GridIndex rhs)
            => lhs.Row == rhs.Row && lhs.Column == rhs.Column;

        public static bool operator !=(in GridIndex lhs, in GridIndex rhs)
            => lhs.Row != rhs.Row || lhs.Column != rhs.Column;

        public static GridIndex operator +(in GridIndex lhs, in GridIndex rhs)
            => new GridIndex(lhs.Row + rhs.Row, lhs.Column + rhs.Column);

        public static GridIndex operator -(in GridIndex lhs, in GridIndex rhs)
            => new GridIndex(lhs.Row - rhs.Row, lhs.Column - rhs.Column);

        public static GridIndex operator *(in GridIndex lhs, int rhs)
            => new GridIndex(lhs.Row * rhs, lhs.Column * rhs);

        public static GridIndex operator *(int lhs, in GridIndex rhs)
            => new GridIndex(rhs.Row * lhs, rhs.Column * lhs);

        public static GridIndex operator *(in GridIndex lhs, in GridIndex rhs)
            => new GridIndex(lhs.Row * rhs.Row, lhs.Column * rhs.Column);

        public static GridIndex operator /(in GridIndex lhs, int rhs)
            => new GridIndex(lhs.Row / rhs, lhs.Column / rhs);

        public static GridIndex operator /(in GridIndex lhs, in GridIndex rhs)
            => new GridIndex(lhs.Row / rhs.Row, lhs.Column / rhs.Column);

        public static GridIndex Clamp(in GridIndex value, in GridIndex min, in GridIndex max)
            => new GridIndex(
                value.Row < min.Row ? min.Row : (value.Row > max.Row ? max.Row : value.Row),
                value.Column < min.Column ? min.Column : (value.Column > max.Column ? max.Column : value.Column)
            );

        public static GridIndex Convert(int index1, int columnCount)
            => columnCount <= 0 ? Zero : new GridIndex(index1 / columnCount, index1 % columnCount);

        public static GridIndex Convert(int index1, in GridIndex size)
            => size.Column <= 0 ? Zero : new GridIndex(index1 / size.Column, index1 % size.Column);
    }
}