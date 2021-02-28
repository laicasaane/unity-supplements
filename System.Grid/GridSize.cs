using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly partial struct GridSize : IEquatableReadOnlyStruct<GridSize>, ISerializable
    {
        public int Row => this.value.Row;

        public int Column => this.value.Column;

        public int Count => this.value.Row * this.value.Column;

        public int this[int index]
        {
            get
            {
                if (index == 0) return this.value.Row;
                if (index == 1) return this.value.Column;
                throw new IndexOutOfRangeException();
            }
        }

        private readonly GridIndex value;

        public GridSize(int row, int column)
            => this.value = new GridIndex(row, column);

        public GridSize(in GridIndex value)
            => this.value = value;

        public GridSize(in GridIndexRange range)
        {
            var normal = range.Normalize();

            this.value = new GridIndex(
                normal.End.Row - normal.Start.Row + 1,
                normal.End.Column - normal.Start.Column + 1
            );
        }

        private GridSize(SerializationInfo info, StreamingContext context)
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
            => obj is GridSize other && this.value.Equals(in other.value);

        public bool Equals(in GridSize other)
            => this.value.Equals(in other.value);

        public bool Equals(GridSize other)
            => this.value.Equals(in other.value);

        public bool Contains(in GridIndex value)
            => value.Row <= this.value.Row && value.Column <= this.value.Column;

        public bool Contains(in GridSize other)
            => other.value.Row <= this.value.Row && other.value.Column <= this.value.Column;

        public bool ValidateIndex(in GridIndex value)
            => value.Row < this.value.Row && value.Column < this.value.Column;

        public int Index1Of(in GridIndex value)
            => value.ToIndex1(this.value.Column);

        public GridIndex LastIndex()
            => this.value - GridIndex.One;

        public GridIndex ClampIndex(in GridIndex value)
            => new GridIndex(
                value.Row >= this.value.Row ? this.value.Row - 1 : value.Row,
                value.Column >= this.value.Column ? this.value.Column - 1 : value.Column
            );

        public GridRange ClampIndexRange(in GridIndex start, in GridIndex end)
            => new GridRange(
                this.value,
                ClampIndex(start),
                ClampIndex(end)
            );

        public GridRange ClampIndexRange(in GridIndexRange range)
            => new GridRange(
                this.value,
                true,
                ClampIndex(range.Start),
                ClampIndex(range.End),
                range.IsFromEnd,
                range.Direction
            );

        public GridRange ClampIndexRange(in GridRange range)
            => new GridRange(
                this.value,
                range.Clamped,
                ClampIndex(range.Start),
                ClampIndex(range.End),
                range.IsFromEnd,
                range.Direction
            );

        public GridRange IndexRange(in GridIndex pivot, int extend)
            => IndexRange(pivot, GridIndex.One * extend);

        public GridRange IndexRange(in GridIndex pivot, int lowerExtend, int upperExtend)
            => IndexRange(pivot, GridIndex.One * lowerExtend, GridIndex.One * upperExtend);

        public GridRange IndexRange(in GridIndex pivot, in GridIndex extend)
            => IndexRange(pivot, extend, extend);

        public GridRange IndexRange(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => new GridRange(
                this.value,
                ClampIndex(pivot - lowerExtend),
                ClampIndex(pivot + upperExtend)
            );

        public GridRange IndexRange(in GridIndex pivot, bool row)
            => new GridRange(
                this.value,
                new GridIndex(row ? pivot.Row : 0, row ? 0 : pivot.Column),
                new GridIndex(row ? pivot.Row : this.value.Row - 1, row ? this.value.Column - 1 : pivot.Column)
            );

        public GridRange IndexRange()
            => new GridRange(
                this.value,
                GridIndex.Zero,
                this.value - GridIndex.One
            );

        public GridRange IndexRange(in GridRange pivot, int extend)
            => IndexRange(pivot, GridIndex.One * extend);

        public GridRange IndexRange(in GridRange pivot, int lowerExtend, int upperExtend)
            => IndexRange(pivot, GridIndex.One * lowerExtend, GridIndex.One * upperExtend);

        public GridRange IndexRange(in GridRange pivot, bool row)
        {
            var normal = pivot.Normalize();

            return new GridRange(
                this.value,
                new GridIndex(row ? normal.Start.Row : 0, row ? 0 : normal.Start.Column),
                new GridIndex(row ? normal.End.Row : this.value.Row - 1, row ? this.value.Column - 1 : normal.End.Column)
            );
        }

        public GridRange IndexRange(in GridRange pivot, in GridIndex extend)
            => IndexRange(pivot, extend, extend);

        public GridRange IndexRange(in GridRange pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
        {
            var normal = pivot.Normalize();

            return new GridRange(
                this.value,
                ClampIndex(normal.Start - lowerExtend),
                ClampIndex(normal.End + upperExtend)
            );
        }

        public GridPartitioner Partitioner(bool fromEnd = false, GridDirection direction = default)
            => new GridPartitioner(this, fromEnd, direction);

        public static GridSize Zero { get; } = new GridSize(GridIndex.Zero);

        public static implicit operator GridSize(in (int row, int column) value)
            => new GridSize(value.row, value.column);

        public static implicit operator GridSize(in GridIndex value)
            => new GridSize(value);

        public static implicit operator GridIndex(in GridSize value)
            => value.value;

        public static bool operator ==(in GridSize lhs, in GridSize rhs)
            => lhs.value.Equals(in rhs.value);

        public static bool operator !=(in GridSize lhs, in GridSize rhs)
            => !lhs.value.Equals(in rhs.value);

        public static GridSize operator +(in GridSize lhs, in GridSize rhs)
            => new GridSize(lhs.value + rhs.value);

        public static GridSize operator -(in GridSize lhs, in GridSize rhs)
            => new GridSize(lhs.value - rhs.value);

        public static GridSize operator *(in GridSize lhs, int rhs)
            => new GridSize(lhs.value * rhs);

        public static GridSize operator *(int lhs, in GridSize rhs)
            => new GridSize(rhs.value * lhs);

        public static GridSize operator *(in GridSize lhs, in GridIndex rhs)
            => new GridSize(lhs.value * rhs);

        public static GridSize operator *(in GridIndex lhs, in GridSize rhs)
            => new GridSize(lhs * rhs.value);

        public static GridSize operator /(in GridSize lhs, int rhs)
            => new GridSize(lhs.value / rhs);

        public static GridSize operator /(in GridSize lhs, in GridIndex rhs)
            => new GridSize(lhs.value / rhs);

        public static GridSize operator %(in GridSize lhs, int rhs)
            => new GridSize(lhs.value % rhs);

        public static GridSize operator %(in GridSize lhs, in GridIndex rhs)
            => new GridSize(lhs.value % rhs);
    }
}