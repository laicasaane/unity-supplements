namespace System.Grid
{
    [Serializable]
    public readonly struct GridIndex : IEquatableReadOnlyStruct<GridIndex>, IComparableReadOnlyStruct<GridIndex>
    {
        public readonly int Row;
        public readonly int Column;

        public GridIndex(int row, int column)
        {
            this.Row = Math.Max(row, 0);
            this.Column = Math.Max(column, 0);
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

        public int CompareTo(GridIndex other)
        {
            var comp = this.Row.CompareTo(other.Row);
            return comp != 0 ? comp : this.Column.CompareTo(other.Column);
        }

        public int CompareTo(in GridIndex other)
        {
            var comp = this.Row.CompareTo(other.Row);
            return comp != 0 ? comp : this.Column.CompareTo(other.Column);
        }

        public override int GetHashCode()
        {
            var hashCode = -1663278630;
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

        public static bool operator >(in GridIndex lhs, in GridIndex rhs)
            => lhs.CompareTo(in rhs) > 0;

        public static bool operator <(in GridIndex lhs, in GridIndex rhs)
            => lhs.CompareTo(in rhs) < 0;

        public static GridIndex operator +(in GridIndex lhs, in GridIndex rhs)
            => new GridIndex(lhs.Row + rhs.Row, lhs.Column + rhs.Column);

        public static GridIndex operator -(in GridIndex lhs, in GridIndex rhs)
            => new GridIndex(lhs.Row - rhs.Row, lhs.Column - rhs.Column);

        public static GridIndex operator *(in GridIndex lhs, int scale)
            => new GridIndex(lhs.Row * scale, lhs.Column * scale);

        public static GridIndex operator *(int scale, in GridIndex rhs)
            => new GridIndex(rhs.Row * scale, rhs.Column * scale);

        public static GridIndex operator /(in GridIndex lhs, int scale)
            => new GridIndex(lhs.Row / scale, lhs.Column / scale);

        public static GridIndex Clamp(in GridIndex value, in GridIndex min, in GridIndex max)
            => new GridIndex(
                value.Row < min.Row ? min.Row : (value.Row > max.Row ? max.Row : value.Row),
                value.Column < min.Column ? min.Column : (value.Column > max.Column ? max.Column : value.Column)
            );

        public static ReadRange<GridIndex> Range(in GridIndex pivot, in GridIndex extend, in GridIndex gridSize)
        {
            var lastIndex = gridSize - One;

            return new ReadRange<GridIndex>(
                Clamp(pivot - extend, Zero, lastIndex),
                Clamp(pivot + extend, Zero, lastIndex)
            );
        }

        public static ReadRange<GridIndex> Range(in GridIndex pivot, bool byRow, in GridIndex gridSize)
        {
            var lastIndex = gridSize - One;

            return new ReadRange<GridIndex>(
                new GridIndex(byRow ? pivot.Row : 0, byRow ? 0 : pivot.Column),
                new GridIndex(byRow ? pivot.Row : lastIndex.Row, byRow ? lastIndex.Column : pivot.Column)
            );
        }
    }
}