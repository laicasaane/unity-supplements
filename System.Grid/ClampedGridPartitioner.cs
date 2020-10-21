using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public readonly struct ClampedGridPartitioner : IEquatableReadOnlyStruct<ClampedGridPartitioner>, ISerializable
    {
        public readonly ClampedGridSize Size;
        public readonly bool IsFromEnd;
        public readonly GridDirection Direction;

        public ClampedGridPartitioner(in ClampedGridSize size, bool fromEnd = false, GridDirection direction = default)
        {
            this.Size = size;
            this.IsFromEnd = fromEnd;
            this.Direction = direction;
        }

        private ClampedGridPartitioner(SerializationInfo info, StreamingContext context)
        {
            this.Size = info.GetValueOrDefault<ClampedGridSize>(nameof(this.Size));
            this.IsFromEnd = info.GetBooleanOrDefault(nameof(this.IsFromEnd));
            this.Direction = info.GetValueOrDefault<GridDirection>(nameof(this.Direction));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Size), this.Size);
            info.AddValue(nameof(this.IsFromEnd), this.IsFromEnd);
            info.AddValue(nameof(this.Direction), this.Direction);
        }

        public override bool Equals(object obj)
            => obj is ClampedGridPartitioner other &&
                this.Size.Equals(other.Size) &&
                this.IsFromEnd == other.IsFromEnd &&
                this.Direction == other.Direction;

        public bool Equals(ClampedGridPartitioner other)
            => this.Size.Equals(other.Size) &&
                this.IsFromEnd == other.IsFromEnd &&
                this.Direction == other.Direction;

        public bool Equals(in ClampedGridPartitioner other)
            => this.Size.Equals(other.Size) &&
                this.IsFromEnd == other.IsFromEnd &&
                this.Direction == other.Direction;

        public override int GetHashCode()
        {
            var hashCode = -2040884224;
            hashCode = hashCode * -1521134295 + this.Size.GetHashCode();
            hashCode = hashCode * -1521134295 + this.IsFromEnd.GetHashCode();
            hashCode = hashCode * -1521134295 + this.Direction.GetHashCode();
            return hashCode;
        }

        public void Deconstruct(out ClampedGridSize size, out bool fromEnd, out GridDirection direction)
        {
            size = this.Size;
            fromEnd = this.IsFromEnd;
            direction = this.Direction;
        }

        public ClampedGridPartitioner With(in ClampedGridSize? Size = null, bool? IsFromEnd = null, in GridDirection? Direction = null)
            => new ClampedGridPartitioner(
                Size ?? this.Size,
                IsFromEnd ?? this.IsFromEnd,
                Direction ?? this.Direction
            );

        public ClampedGridPartitioner FromStart()
            => new ClampedGridPartitioner(this.Size, false, this.Direction);

        public ClampedGridPartitioner FromEnd()
            => new ClampedGridPartitioner(this.Size, true, this.Direction);

        public ClampedGridPartitioner ByRow()
            => new ClampedGridPartitioner(this.Size, this.IsFromEnd, GridDirection.Row);

        public ClampedGridPartitioner ByColumn()
            => new ClampedGridPartitioner(this.Size, this.IsFromEnd, GridDirection.Column);

        public void Partition(int rangeSize, ICollection<GridIndexRange> output)
            => Partition(rangeSize, rangeSize, output);

        public void Partition(in GridIndex rangeSize, ICollection<GridIndexRange> output)
            => Partition(rangeSize, rangeSize, output);

        public void Partition(int rangeSize, int step, ICollection<GridIndexRange> output)
            => Partition(GridIndex.One * rangeSize, step, output);

        public void Partition(int rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
            => Partition(GridIndex.One * rangeSize, step, output);

        public void Partition(in GridIndex rangeSize, int step, ICollection<GridIndexRange> output)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            Partition(rangeSize, GridIndex.One * step, output);
        }

        private static void Validate(in GridIndex step, ICollection<GridIndexRange> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (step.Row < 1 || step.Column < 1)
                throw new ArgumentException($"Must be greater than or equal to {nameof(GridIndex)}.{nameof(GridIndex.One)}",
                                            nameof(step));
        }

        public void Partition(in GridIndex rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
        {
            Validate(step, output);

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > this.Size.Row || step.Column > this.Size.Column)
                return;

            var slice = this.Size.IndexRange();
            var extend = rangeSize - GridIndex.One;
            var count = this.Size / step;

            switch (this.Direction)
            {
                case GridDirection.Column:
                    if (this.IsFromEnd)
                        PartitionByColumnFromEnd(step, slice, extend, count, output);
                    else
                        PartitionByColumnFromStart(step, slice, extend, count, output);
                    break;

                case GridDirection.Row:
                    if (this.IsFromEnd)
                        PartitionByRowFromEnd(step, slice, extend, count, output);
                    else
                        PartitionByRowFromStart(step, slice, extend, count, output);
                    break;
            }
        }

        public void Partition(in GridIndexRange slice, int rangeSize, ICollection<GridIndexRange> output)
            => Partition(slice, rangeSize, rangeSize, output);

        public void Partition(in GridIndexRange slice, in GridIndex rangeSize, ICollection<GridIndexRange> output)
            => Partition(slice, rangeSize, rangeSize, output);

        public void Partition(in GridIndexRange slice, int rangeSize, int step, ICollection<GridIndexRange> output)
            => Partition(slice, GridIndex.One * rangeSize, step, output);

        public void Partition(in GridIndexRange slice, int rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
            => Partition(slice, GridIndex.One * rangeSize, step, output);

        public void Partition(in GridIndexRange slice, in GridIndex rangeSize, int step, ICollection<GridIndexRange> output)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            Partition(slice, rangeSize, GridIndex.One * step, output);
        }

        public void Partition(in GridIndexRange slice, in GridIndex rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
        {
            Validate(step, output);

            var normalSlice = slice.Normalize();
            var sliceSize = new ClampedGridSize(
                normalSlice.End.Row - normalSlice.Start.Row + 1,
                normalSlice.End.Column - normalSlice.Start.Column + 1
            );

            if (sliceSize.Row > this.Size.Row || sliceSize.Column > this.Size.Column)
                throw new ArgumentOutOfRangeException($"Must be within the grid of size {this.Size}", nameof(slice));

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > sliceSize.Row || step.Column > sliceSize.Column)
                return;

            var extend = rangeSize - GridIndex.One;
            var count = this.Size / step;

            switch (this.Direction)
            {
                case GridDirection.Column:
                    if (this.IsFromEnd)
                        PartitionByColumnFromEnd(step, normalSlice, extend, count, output);
                    else
                        PartitionByColumnFromStart(step, normalSlice, extend, count, output);
                    break;

                case GridDirection.Row:
                    if (this.IsFromEnd)
                        PartitionByRowFromEnd(step, normalSlice, extend, count, output);
                    else
                        PartitionByRowFromStart(step, normalSlice, extend, count, output);
                    break;
            }
        }

        private void PartitionByColumnFromStart(in GridIndex step, in GridIndexRange slice, in GridIndex extend, in ClampedGridSize count,
                                                ICollection<GridIndexRange> output)
        {
            for (var r = 0; r < count.Row; r++)
            {
                for (var c = 0; c < count.Column; c++)
                {
                    var pivot = new GridIndex(
                        r * step.Row + slice.Start.Row,
                        c * step.Column + slice.Start.Column
                    );

                    var upperExtend = pivot + extend;

                    if (upperExtend.Row > slice.End.Row ||
                        upperExtend.Column > slice.End.Column)
                        break;

                    output.Add(new GridIndexRange(pivot, upperExtend));
                }
            }
        }

        private void PartitionByRowFromStart(in GridIndex step, in GridIndexRange slice, in GridIndex extend, in ClampedGridSize count,
                                             ICollection<GridIndexRange> output)
        {
            for (var c = 0; c < count.Column; c++)
            {
                for (var r = 0; r < count.Row; r++)
                {
                    var pivot = new GridIndex(
                        r * step.Row + slice.Start.Row,
                        c * step.Column + slice.Start.Column
                    );

                    var upperExtend = pivot + extend;

                    if (upperExtend.Row > this.Size.Row ||
                        upperExtend.Column > this.Size.Column)
                        break;

                    output.Add(new GridIndexRange(pivot, upperExtend));
                }
            }
        }

        private void PartitionByColumnFromEnd(in GridIndex step, in GridIndexRange slice, in GridIndex extend, in ClampedGridSize count,
                                                ICollection<GridIndexRange> output)
        {
            for (var r = 0; r < count.Row; r++)
            {
                for (var c = 0; c < count.Column; c++)
                {
                    var pivot = new GridIndex(
                        slice.End.Row - r * step.Row,
                        slice.End.Column - c * step.Column
                    );

                    var lowerRow = pivot.Row - extend.Row;
                    var lowerCol = pivot.Column - extend.Column;

                    if (lowerRow < slice.Start.Row ||
                        lowerCol < slice.Start.Column)
                        break;

                    output.Add(new GridIndexRange(new GridIndex(lowerRow, lowerCol), pivot));
                }
            }
        }

        private void PartitionByRowFromEnd(in GridIndex step, in GridIndexRange slice, in GridIndex extend, in ClampedGridSize count,
                                             ICollection<GridIndexRange> output)
        {
            for (var c = 0; c < count.Column; c++)
            {
                for (var r = 0; r < count.Row; r++)
                {
                    var pivot = new GridIndex(
                        slice.End.Row - r * step.Row,
                        slice.End.Column - c * step.Column
                    );

                    var lowerRow = pivot.Row - extend.Row;
                    var lowerCol = pivot.Column - extend.Column;

                    if (lowerRow < slice.Start.Row ||
                        lowerCol < slice.Start.Column)
                        break;

                    output.Add(new GridIndexRange(new GridIndex(lowerRow, lowerCol), pivot));
                }
            }
        }

        public IEnumerable<GridIndexRange> Partition(int rangeSize)
           => Partition(rangeSize, rangeSize);

        public IEnumerable<GridIndexRange> Partition(in GridIndex rangeSize)
            => Partition(rangeSize, rangeSize);

        public IEnumerable<GridIndexRange> Partition(int rangeSize, int step)
            => Partition(GridIndex.One * rangeSize, step);

        public IEnumerable<GridIndexRange> Partition(int rangeSize, in GridIndex step)
            => Partition(GridIndex.One * rangeSize, step);

        public IEnumerable<GridIndexRange> Partition(in GridIndex rangeSize, int step)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            return Partition(rangeSize, GridIndex.One * step);
        }

        public IEnumerable<GridIndexRange> Partition(GridIndex rangeSize, GridIndex step)
        {
            if (step.Row < 1 || step.Column < 1)
                throw new ArgumentException($"Must be greater than or equal to {nameof(GridIndex)}.{nameof(GridIndex.One)}",
                                            nameof(step));

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > this.Size.Row || step.Column > this.Size.Column)
                return _empty;

            var slice = this.Size.IndexRange();
            var extend = rangeSize - GridIndex.One;
            var count = this.Size / step;

            switch (this.Direction)
            {
                case GridDirection.Column:
                    return this.IsFromEnd
                            ? PartitionByColumnFromEnd(step, slice, extend, count)
                            : PartitionByColumnFromStart(step, slice, extend, count);

                case GridDirection.Row:
                    return this.IsFromEnd
                            ? PartitionByRowFromEnd(step, slice, extend, count)
                            : PartitionByRowFromStart(step, slice, extend, count);

                default:
                    return _empty;
            }
        }

        public IEnumerable<GridIndexRange> Partition(in GridIndexRange slice, int rangeSize)
            => Partition(slice, rangeSize, rangeSize);

        public IEnumerable<GridIndexRange> Partition(in GridIndexRange slice, in GridIndex rangeSize)
            => Partition(slice, rangeSize, rangeSize);

        public IEnumerable<GridIndexRange> Partition(in GridIndexRange slice, int rangeSize, int step)
            => Partition(slice, GridIndex.One * rangeSize, step);

        public IEnumerable<GridIndexRange> Partition(in GridIndexRange slice, int rangeSize, in GridIndex step)
            => Partition(slice, GridIndex.One * rangeSize, step);

        public IEnumerable<GridIndexRange> Partition(in GridIndexRange slice, in GridIndex rangeSize, int step)
        {
            if (step < 1)
                throw new ArgumentException($"Must be greater than or equal to 1", nameof(step));

            return Partition(slice, rangeSize, GridIndex.One * step);
        }

        public IEnumerable<GridIndexRange> Partition(GridIndexRange slice, GridIndex rangeSize, GridIndex step)
        {
            if (step.Row < 1 || step.Column < 1)
                throw new ArgumentException($"Must be greater than or equal to {nameof(GridIndex)}.{nameof(GridIndex.One)}",
                                            nameof(step));

            var normalSlice = slice.Normalize();
            var sliceSize = new ClampedGridSize(
                normalSlice.End.Row - normalSlice.Start.Row + 1,
                normalSlice.End.Column - normalSlice.Start.Column + 1
            );

            if (sliceSize.Row > this.Size.Row || sliceSize.Column > this.Size.Column)
                throw new ArgumentOutOfRangeException($"Must be within the grid of size {this.Size}", nameof(slice));

            if (rangeSize.Row < 1 || rangeSize.Column < 1 ||
                step.Row > sliceSize.Row || step.Column > sliceSize.Column)
                return _empty;

            var extend = rangeSize - GridIndex.One;
            var count = this.Size / step;

            switch (this.Direction)
            {
                case GridDirection.Column:
                    return this.IsFromEnd
                            ? PartitionByColumnFromEnd(step, normalSlice, extend, count)
                            : PartitionByColumnFromStart(step, normalSlice, extend, count);

                case GridDirection.Row:
                    return this.IsFromEnd
                            ? PartitionByRowFromEnd(step, normalSlice, extend, count)
                            : PartitionByRowFromStart(step, normalSlice, extend, count);

                default:
                    return _empty;
            }
        }

        private IEnumerable<GridIndexRange> PartitionByColumnFromStart(GridIndex step, GridIndexRange slice, GridIndex extend, ClampedGridSize count)
        {
            for (var r = 0; r < count.Row; r++)
            {
                for (var c = 0; c < count.Column; c++)
                {
                    var pivot = new GridIndex(
                        r * step.Row + slice.Start.Row,
                        c * step.Column + slice.Start.Column
                    );

                    var upperExtend = pivot + extend;

                    if (upperExtend.Row > slice.End.Row ||
                        upperExtend.Column > slice.End.Column)
                        break;

                    yield return new GridIndexRange(pivot, upperExtend);
                }
            }
        }

        private IEnumerable<GridIndexRange> PartitionByRowFromStart(GridIndex step, GridIndexRange slice, GridIndex extend, ClampedGridSize count)
        {
            for (var c = 0; c < count.Column; c++)
            {
                for (var r = 0; r < count.Row; r++)
                {
                    var pivot = new GridIndex(
                        r * step.Row + slice.Start.Row,
                        c * step.Column + slice.Start.Column
                    );

                    var upperExtend = pivot + extend;

                    if (upperExtend.Row > slice.End.Row ||
                        upperExtend.Column > slice.End.Column)
                        break;

                    yield return new GridIndexRange(pivot, upperExtend);
                }
            }
        }

        private IEnumerable<GridIndexRange> PartitionByColumnFromEnd(GridIndex step, GridIndexRange slice, GridIndex extend, ClampedGridSize count)
        {
            for (var r = 0; r < count.Row; r++)
            {
                for (var c = 0; c < count.Column; c++)
                {
                    var pivot = new GridIndex(
                        slice.End.Row - r * step.Row,
                        slice.End.Column - c * step.Column
                    );

                    var lowerRow = pivot.Row - extend.Row;
                    var lowerCol = pivot.Column - extend.Column;

                    if (lowerRow < slice.Start.Row ||
                        lowerCol < slice.Start.Column)
                        break;

                    yield return new GridIndexRange(new GridIndex(lowerRow, lowerCol), pivot);
                }
            }
        }

        private IEnumerable<GridIndexRange> PartitionByRowFromEnd(GridIndex step, GridIndexRange slice, GridIndex extend, ClampedGridSize count)
        {
            for (var c = 0; c < count.Column; c++)
            {
                for (var r = 0; r < count.Row; r++)
                {
                    var pivot = new GridIndex(
                        slice.End.Row - r * step.Row,
                        slice.End.Column - c * step.Column
                    );

                    var lowerRow = pivot.Row - extend.Row;
                    var lowerCol = pivot.Column - extend.Column;

                    if (lowerRow < slice.Start.Row ||
                        lowerCol < slice.Start.Column)
                        break;

                    yield return new GridIndexRange(new GridIndex(lowerRow, lowerCol), pivot);
                }
            }
        }

        private static readonly GridIndexRange[] _empty = new GridIndexRange[0];

        public static bool operator ==(in ClampedGridPartitioner lhs, in ClampedGridPartitioner rhs)
            => lhs.Equals(in rhs);

        public static bool operator !=(in ClampedGridPartitioner lhs, in ClampedGridPartitioner rhs)
            => !lhs.Equals(in rhs);
    }
}