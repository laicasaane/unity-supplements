using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly partial struct GridSize : IEquatableReadOnlyStruct<GridSize>, ISerializable
    {
        public int Row => this.value.Row;

        public int Column => this.value.Column;

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
            => value.ToIndex1(this.value);

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
                range.IsFromEnd
            );

        public GridRange ClampIndexRange(in GridRange range)
            => new GridRange(
                this.value,
                range.Clamped,
                ClampIndex(range.Start),
                ClampIndex(range.End),
                range.IsFromEnd
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

        public void Partition(int rangeSize, ICollection<GridRange> output, GridDirection direction = default)
            => Partition(rangeSize, rangeSize, output, direction);

        public void Partition(in GridIndex rangeSize, ICollection<GridRange> output, GridDirection direction = default)
            => Partition(rangeSize, rangeSize, output, direction);

        public void Partition(int rangeSize, int step, ICollection<GridRange> output, GridDirection direction = default)
            => Partition(GridIndex.One* rangeSize, step, output, direction);

        public void Partition(int rangeSize, in GridIndex step, ICollection<GridRange> output, GridDirection direction = default)
            => Partition(GridIndex.One * rangeSize, step, output, direction);

        public void Partition(in GridIndex rangeSize, int step, ICollection<GridRange> output, GridDirection direction = default)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            Partition(rangeSize, GridIndex.One * step, output, direction);
        }

        private void ValidatePartition(in GridIndex step, ICollection<GridRange> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (step.Row < 1 || step.Column < 1)
                throw new ArgumentException($"Must be greater than or equal to {nameof(GridIndex)}.{nameof(GridIndex.One)}",
                                            nameof(step));
        }

        public void Partition(in GridIndex rangeSize, in GridIndex step, ICollection<GridRange> output, GridDirection direction = default)
        {
            ValidatePartition(step, output);

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > this.value.Row || step.Column > this.value.Column)
                return;

            var (rowCount, colCount) = this.value / step;
            var extend = rangeSize - GridIndex.One;

            if ((rowCount - 1) * step.Row + extend.Row >= this.value.Row)
                rowCount -= 1;

            if ((colCount - 1) * step.Column + extend.Column >= this.value.Column)
                colCount -= 1;

            switch (direction)
            {
                case GridDirection.Column:
                    {
                        for (var r = 0; r < rowCount; r++)
                        {
                            for (var c = 0; c < colCount; c++)
                            {
                                var pivot = new GridIndex(r * step.Row, c * step.Column);
                                output.Add(IndexRange(pivot, GridIndex.Zero, extend));
                            }
                        }

                        break;
                    }

                case GridDirection.Row:
                    {
                        for (var c = 0; c < colCount; c++)
                        {
                            for (var r = 0; r < rowCount; r++)
                            {
                                var pivot = new GridIndex(r * step.Row, c * step.Column);
                                output.Add(IndexRange(pivot, GridIndex.Zero, extend));
                            }
                        }

                        break;
                    }
            }
        }

        public void Partition(in GridIndexRange slice, int rangeSize, ICollection<GridRange> output, GridDirection direction = default)
            => Partition(slice, rangeSize, rangeSize, output, direction);

        public void Partition(in GridIndexRange slice, in GridIndex rangeSize, ICollection<GridRange> output, GridDirection direction = default)
            => Partition(slice, rangeSize, rangeSize, output, direction);

        public void Partition(in GridIndexRange slice, int rangeSize, int step, ICollection<GridRange> output, GridDirection direction = default)
            => Partition(slice, GridIndex.One * rangeSize, step, output, direction);

        public void Partition(in GridIndexRange slice, int rangeSize, in GridIndex step, ICollection<GridRange> output, GridDirection direction = default)
            => Partition(slice, GridIndex.One * rangeSize, step, output, direction);

        public void Partition(in GridIndexRange slice, in GridIndex rangeSize, int step, ICollection<GridRange> output, GridDirection direction = default)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            Partition(slice, rangeSize, GridIndex.One * step, output, direction);
        }

        public void Partition(in GridIndexRange slice, in GridIndex rangeSize, in GridIndex step, ICollection<GridRange> output, GridDirection direction = default)
        {
            ValidatePartition(step, output);

            var normalSlice = slice.Normalize();
            var sliceSize = new GridSize(
                normalSlice.End.Row - normalSlice.Start.Row + 1,
                normalSlice.End.Column - normalSlice.Start.Column + 1
            );

            if (sliceSize.Row > this.value.Row || sliceSize.Column > this.value.Column)
                throw new ArgumentOutOfRangeException($"Must be lesser than or equal to the grid size {this.value}", nameof(slice));

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > sliceSize.value.Row || step.Column > sliceSize.value.Column)
                return;

            var (rowCount, colCount) = sliceSize.value / step;
            var extend = rangeSize - GridIndex.One;

            if ((rowCount - 1) * step.Row + extend.Row >= sliceSize.value.Row)
                rowCount -= 1;

            if ((colCount - 1) * step.Column + extend.Column >= sliceSize.value.Column)
                colCount -= 1;

            switch (direction)
            {
                case GridDirection.Column:
                    {
                        for (var r = 0; r < rowCount; r++)
                        {
                            for (var c = 0; c < colCount; c++)
                            {
                                var row = r * step.Row + normalSlice.Start.Row;
                                var col = c * step.Column + normalSlice.Start.Column;

                                output.Add(IndexRange(new GridIndex(row, col), GridIndex.Zero, extend));
                            }
                        }

                        break;
                    }

                case GridDirection.Row:
                    {
                        for (var c = 0; c < colCount; c++)
                        {
                            for (var r = 0; r < rowCount; r++)
                            {
                                var row = r * step.Row + normalSlice.Start.Row;
                                var col = c * step.Column + normalSlice.Start.Column;

                                output.Add(IndexRange(new GridIndex(row, col), GridIndex.Zero, extend));
                            }
                        }

                        break;
                    }
            }
        }

        public IEnumerable<GridRange> Partition(int rangeSize, GridDirection direction = default)
           => Partition(rangeSize, rangeSize, direction);

        public IEnumerable<GridRange> Partition(in GridIndex rangeSize, GridDirection direction = default)
            => Partition(rangeSize, rangeSize, direction);

        public IEnumerable<GridRange> Partition(int rangeSize, int step, GridDirection direction = default)
            => Partition(GridIndex.One * rangeSize, step, direction);

        public IEnumerable<GridRange> Partition(int rangeSize, in GridIndex step, GridDirection direction = default)
            => Partition(GridIndex.One * rangeSize, step, direction);

        public IEnumerable<GridRange> Partition(in GridIndex rangeSize, int step, GridDirection direction = default)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            return Partition(rangeSize, GridIndex.One * step, direction);
        }

        public IEnumerable<GridRange> Partition(GridIndex rangeSize, GridIndex step, GridDirection direction = default)
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

            switch (direction)
            {
                case GridDirection.Column:
                    {
                        for (var r = 0; r < rowCount; r++)
                        {
                            for (var c = 0; c < colCount; c++)
                            {
                                var pivot = new GridIndex(r * step.Row, c * step.Column);
                                yield return IndexRange(pivot, GridIndex.Zero, extend);
                            }
                        }

                        break;
                    }

                case GridDirection.Row:
                    {
                        for (var c = 0; c < colCount; c++)
                        {
                            for (var r = 0; r < rowCount; r++)
                            {
                                var pivot = new GridIndex(r * step.Row, c * step.Column);
                                yield return IndexRange(pivot, GridIndex.Zero, extend);
                            }
                        }

                        break;
                    }
            }
        }

        public IEnumerable<GridRange> Partition(in GridIndexRange slice, int rangeSize, GridDirection direction = default)
            => Partition(slice, rangeSize, rangeSize, direction);

        public IEnumerable<GridRange> Partition(in GridIndexRange slice, in GridIndex rangeSize, GridDirection direction = default)
            => Partition(slice, rangeSize, rangeSize, direction);

        public IEnumerable<GridRange> Partition(in GridIndexRange slice, int rangeSize, int step, GridDirection direction = default)
            => Partition(slice, GridIndex.One * rangeSize, step, direction);

        public IEnumerable<GridRange> Partition(in GridIndexRange slice, int rangeSize, in GridIndex step, GridDirection direction = default)
            => Partition(slice, GridIndex.One * rangeSize, step, direction);

        public IEnumerable<GridRange> Partition(in GridIndexRange slice, in GridIndex rangeSize, int step, GridDirection direction = default)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            return Partition(slice, rangeSize, GridIndex.One * step, direction);
        }

        public IEnumerable<GridRange> Partition(GridIndexRange slice, GridIndex rangeSize, GridIndex step, GridDirection direction = default)
        {
            if (step.Row < 1 || step.Column < 1)
                throw new ArgumentException($"Must be greater than or equal to {nameof(GridIndex)}.{nameof(GridIndex.One)}",
                                            nameof(step));

            var normalSlice = slice.Normalize();
            var sliceSize = new GridSize(
                normalSlice.End.Row - normalSlice.Start.Row + 1,
                normalSlice.End.Column - normalSlice.Start.Column + 1
            );

            if (sliceSize.Row > this.value.Row || sliceSize.Column > this.value.Column)
                throw new ArgumentOutOfRangeException($"Must be lesser than or equal to the grid size {this.value}", nameof(slice));

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > sliceSize.value.Row || step.Column > sliceSize.value.Column)
                yield break;

            var (rowCount, colCount) = sliceSize.value / step;
            var extend = rangeSize - GridIndex.One;

            if ((rowCount - 1) * step.Row + extend.Row >= sliceSize.value.Row)
                rowCount -= 1;

            if ((colCount - 1) * step.Column + extend.Column >= sliceSize.value.Column)
                colCount -= 1;

            switch (direction)
            {
                case GridDirection.Column:
                    {
                        for (var r = 0; r < rowCount; r++)
                        {
                            for (var c = 0; c < colCount; c++)
                            {
                                var row = r * step.Row + normalSlice.Start.Row;
                                var col = c * step.Column + normalSlice.Start.Column;

                                yield return IndexRange(new GridIndex(row, col), GridIndex.Zero, extend);
                            }
                        }

                        break;
                    }

                case GridDirection.Row:
                    {
                        for (var c = 0; c < colCount; c++)
                        {
                            for (var r = 0; r < rowCount; r++)
                            {
                                var row = r * step.Row + normalSlice.Start.Row;
                                var col = c * step.Column + normalSlice.Start.Column;

                                yield return IndexRange(new GridIndex(row, col), GridIndex.Zero, extend);
                            }
                        }

                        break;
                    }
            }
        }

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
    }
}