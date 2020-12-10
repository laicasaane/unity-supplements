using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly partial struct ClampedGridSize : IEquatableReadOnlyStruct<ClampedGridSize>, ISerializable
    {
        public int Row => this.value.Row;

        public int Column => this.value.Column;

        private readonly GridIndex value;

        public ClampedGridSize(int row, int column)
            => this.value = new GridIndex(row, column);

        public ClampedGridSize(in GridIndex value)
            => this.value = value;

        public ClampedGridSize(in GridIndexRange range)
        {
            var normal = range.Normalize();

            this.value = new GridIndex(
                normal.End.Row - normal.Start.Row + 1,
                normal.End.Column - normal.Start.Column + 1
            );
        }

        private ClampedGridSize(SerializationInfo info, StreamingContext context)
        {
            var row = info.GetInt32OrDefault(nameof(GridIndex.Row));
            var col = info.GetInt32OrDefault(nameof(GridIndex.Column));

            this.value = new GridIndex(row, col);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(GridIndex.Row), this.value.Row);
            info.AddValue(nameof(GridIndex.Column), this.value.Column);
        }

        public void Deconstruct(out int row, out int column)
        {
            row = this.Row;
            column = this.Column;
        }

        public override int GetHashCode()
            => -1937169414 + this.value.GetHashCode();

        public override bool Equals(object obj)
            => obj is ClampedGridSize other && this.value.Equals(in other.value);

        public bool Equals(in ClampedGridSize other)
            => this.value.Equals(in other.value);

        public bool Equals(ClampedGridSize other)
            => this.value.Equals(in other.value);

        public bool Contains(in GridIndex value)
            => value.Row <= this.value.Row && value.Column <= this.value.Column;

        public bool Contains(in ClampedGridSize other)
            => other.value.Row <= this.value.Row && other.value.Column <= this.value.Column;

        public bool ValidateIndex(in GridIndex value)
            => value.Row < this.value.Row && value.Column < this.value.Column;

        public int Index1Of(in GridIndex value)
            => value.ToIndex1(this.value);

        public GridIndex LastIndex()
            => this.value - GridIndex.One;

        public GridIndex ClampIndex(in GridIndex value)
            => new GridIndex(
                value.Row >= this.value.Row ? this.value.Row - 1 : value.Row,
                value.Column >= this.value.Column ? this.value.Column - 1 : value.Column
            );

        public GridIndexRange ClampIndexRange(in GridIndex start, in GridIndex end)
            => new GridIndexRange(
                ClampIndex(start),
                ClampIndex(end)
            );

        public GridIndexRange ClampIndexRange(in GridIndexRange range)
            => new GridIndexRange(
                ClampIndex(range.Start),
                ClampIndex(range.End),
                range.IsFromEnd,
                range.Direction
            );

        public GridIndexRange IndexRange(in GridIndex pivot, int extend)
            => IndexRange(pivot, GridIndex.One * extend);

        public GridIndexRange IndexRange(in GridIndex pivot, int lowerExtend, int upperExtend)
            => IndexRange(pivot, GridIndex.One * lowerExtend, GridIndex.One * upperExtend);

        public GridIndexRange IndexRange(in GridIndex pivot, in GridIndex extend)
            => IndexRange(pivot, extend, extend);

        public GridIndexRange IndexRange(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => new GridIndexRange(
                ClampIndex(pivot - lowerExtend),
                ClampIndex(pivot + upperExtend)
            );

        public GridIndexRange IndexRange(in GridIndex pivot, bool row)
            => new GridIndexRange(
                new GridIndex(row ? pivot.Row : 0, row ? 0 : pivot.Column),
                new GridIndex(row ? pivot.Row : this.value.Row - 1, row ? this.value.Column - 1 : pivot.Column)
            );

        public GridIndexRange IndexRange()
            => new GridIndexRange(
                GridIndex.Zero,
                this.value - GridIndex.One
            );

        public GridIndexRange IndexRange(in GridIndexRange pivot, int extend)
            => IndexRange(pivot, GridIndex.One * extend);

        public GridIndexRange IndexRange(in GridIndexRange pivot, int lowerExtend, int upperExtend)
            => IndexRange(pivot, GridIndex.One * lowerExtend, GridIndex.One * upperExtend);

        public GridIndexRange IndexRange(in GridIndexRange pivot, bool row)
        {
            var normal = pivot.Normalize();

            return new GridIndexRange(
                new GridIndex(row ? normal.Start.Row : 0, row ? 0 : normal.Start.Column),
                new GridIndex(row ? normal.End.Row : this.value.Row - 1, row ? this.value.Column - 1 : normal.End.Column)
            );
        }

        public GridIndexRange IndexRange(in GridIndexRange pivot, in GridIndex extend)
            => IndexRange(pivot, extend, extend);

        public GridIndexRange IndexRange(in GridIndexRange pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
        {
            var normal = pivot.Normalize();

            return new GridIndexRange(
                ClampIndex(normal.Start - lowerExtend),
                ClampIndex(normal.End + upperExtend)
            );
        }

        public ClampedGridPartitioner Partitioner(bool fromEnd = false, GridDirection direction = default)
            => new ClampedGridPartitioner(this, fromEnd, direction);

        public static ClampedGridSize Zero { get; } = new ClampedGridSize(GridIndex.Zero);

        public static implicit operator ClampedGridSize(in (int row, int column) value)
            => new ClampedGridSize(value.row, value.column);

        public static implicit operator ClampedGridSize(in GridIndex value)
            => new ClampedGridSize(value);

        public static implicit operator GridIndex(in ClampedGridSize value)
            => value.value;

        public static implicit operator ClampedGridSize(in GridSize value)
            => new ClampedGridSize(value.Row, value.Column);

        public static implicit operator GridSize(in ClampedGridSize value)
            => new GridSize(value.value);

        public static bool operator ==(in ClampedGridSize lhs, in ClampedGridSize rhs)
            => lhs.value.Equals(in rhs.value);

        public static bool operator !=(in ClampedGridSize lhs, in ClampedGridSize rhs)
            => !lhs.value.Equals(in rhs.value);

        public static ClampedGridSize operator +(in ClampedGridSize lhs, in ClampedGridSize rhs)
            => new ClampedGridSize(lhs.value + rhs.value);

        public static ClampedGridSize operator -(in ClampedGridSize lhs, in ClampedGridSize rhs)
            => new ClampedGridSize(lhs.value - rhs.value);

        public static ClampedGridSize operator *(in ClampedGridSize lhs, int rhs)
            => new ClampedGridSize(lhs.value * rhs);

        public static ClampedGridSize operator *(int lhs, in ClampedGridSize rhs)
            => new ClampedGridSize(rhs.value * lhs);

        public static ClampedGridSize operator *(in ClampedGridSize lhs, in GridIndex rhs)
            => new ClampedGridSize(lhs.value * rhs);

        public static ClampedGridSize operator *(in GridIndex lhs, in ClampedGridSize rhs)
            => new ClampedGridSize(lhs * rhs.value);

        public static ClampedGridSize operator /(in ClampedGridSize lhs, int rhs)
            => new ClampedGridSize(lhs.value / rhs);

        public static ClampedGridSize operator /(in ClampedGridSize lhs, in GridIndex rhs)
            => new ClampedGridSize(lhs.value / rhs);

        public static ClampedGridSize operator %(in ClampedGridSize lhs, int rhs)
            => new ClampedGridSize(lhs.value % rhs);

        public static ClampedGridSize operator %(in ClampedGridSize lhs, in GridIndex rhs)
            => new ClampedGridSize(lhs.value % rhs);
    }
}