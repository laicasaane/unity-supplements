using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct ClampedGridSize : IEquatableReadOnlyStruct<ClampedGridSize>, ISerializable
    {
        public int Row => this.value.Row;

        public int Column => this.value.Column;

        private readonly GridIndex value;

        public ClampedGridSize(int row, int column)
            => this.value = new GridIndex(row, column);

        public ClampedGridSize(in GridIndex value)
            => this.value = value;

        private ClampedGridSize(SerializationInfo info, StreamingContext context)
        {
            int row;
            int col;

            try
            {
                row = info.GetInt32(nameof(GridIndex.Row));
            }
            catch
            {
                row = default;
            }

            try
            {
                col = info.GetInt32(nameof(GridIndex.Column));
            }
            catch
            {
                col = default;
            }

            this.value = new GridIndex(row, col);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(GridIndex.Row), this.value.Row);
            info.AddValue(nameof(GridIndex.Column), this.value.Column);
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
                range.IsFromEnd
            );

        public GridIndexRange IndexRange(in GridIndex pivot, int extend)
            => IndexRange(pivot, GridIndex.One * extend);

        public GridIndexRange IndexRange(in GridIndex pivot, int lowerExtend, int upperExtend)
            => IndexRange(pivot, GridIndex.One * lowerExtend, GridIndex.One * upperExtend);

        public GridIndexRange IndexRange(in GridIndex pivot, in GridIndex extend)
            => new GridIndexRange(
                ClampIndex(pivot - extend),
                ClampIndex(pivot + extend)
            );

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

        public void IndexRanges(int rangeSize, ICollection<GridIndexRange> output)
            => IndexRanges(rangeSize, rangeSize, output);

        public void IndexRanges(in GridIndex rangeSize, ICollection<GridIndexRange> output)
            => IndexRanges(rangeSize, rangeSize, output);

        public void IndexRanges(int rangeSize, int step, ICollection<GridIndexRange> output)
            => IndexRanges(GridIndex.One * rangeSize, step, output);

        public void IndexRanges(int rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
            => IndexRanges(GridIndex.One * rangeSize, step, output);

        public void IndexRanges(in GridIndex rangeSize, int step, ICollection<GridIndexRange> output)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            IndexRanges(rangeSize, GridIndex.One * step, output);
        }

        public void IndexRanges(in GridIndex rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (step.Row < 1 || step.Column < 1)
                throw new ArgumentException($"Must be greater than or equal to {nameof(GridIndex)}.{nameof(GridIndex.One)}",
                                            nameof(step));

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > this.value.Row || step.Column > this.value.Column)
                return;

            var (rowCount, colCount) = this.value / step;
            var extend = rangeSize - GridIndex.One;

            if ((rowCount - 1) * step.Row + extend.Row >= this.value.Row)
                rowCount -= 1;

            if ((colCount - 1) * step.Column + extend.Column >= this.value.Column)
                colCount -= 1;

            for (var r = 0; r < rowCount; r++)
            {
                for (var c = 0; c < colCount; c++)
                {
                    var pivot = new GridIndex(r, c) * step;
                    output.Add(IndexRange(pivot, GridIndex.Zero, extend));
                }
            }
        }

        public IEnumerable<GridIndexRange> IndexRanges(int rangeSize)
           => IndexRanges(rangeSize, rangeSize);

        public IEnumerable<GridIndexRange> IndexRanges(in GridIndex rangeSize)
            => IndexRanges(rangeSize, rangeSize);

        public IEnumerable<GridIndexRange> IndexRanges(int rangeSize, int step)
            => IndexRanges(GridIndex.One * rangeSize, step);

        public IEnumerable<GridIndexRange> IndexRanges(int rangeSize, in GridIndex step)
            => IndexRanges(GridIndex.One * rangeSize, step);

        public IEnumerable<GridIndexRange> IndexRanges(in GridIndex rangeSize, int step)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            return IndexRanges(rangeSize, GridIndex.One * step);
        }

        public IEnumerable<GridIndexRange> IndexRanges(GridIndex rangeSize, GridIndex step)
        {
            if (step.Row < 1 || step.Column < 1)
                throw new ArgumentException($"Must be greater than or equal to {nameof(GridIndex)}.{nameof(GridIndex.One)}",
                                            nameof(step));

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > this.value.Row || step.Column > this.value.Column)
                yield break;

            var (rowCount, colCount) = this.value / step;
            var extend = rangeSize - GridIndex.One;

            if ((rowCount - 1) * step.Row + extend.Row >= this.value.Row)
                rowCount -= 1;

            if ((colCount - 1) * step.Column + extend.Column >= this.value.Column)
                colCount -= 1;

            for (var r = 0; r < rowCount; r++)
            {
                for (var c = 0; c < colCount; c++)
                {
                    var pivot = new GridIndex(r, c) * step;
                    yield return IndexRange(pivot, GridIndex.Zero, extend);
                }
            }
        }

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
    }
}