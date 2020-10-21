using System.Collections.Generic;

namespace System.Grid
{
    public partial struct GridSize
    {
        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(int rangeSize, ICollection<GridRange> output)
            => Partitioner().Partition(rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, ICollection<GridRange> output)
            => Partitioner().Partition(rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(int rangeSize, int step, ICollection<GridRange> output)
            => Partitioner().Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(int rangeSize, in GridIndex step, ICollection<GridRange> output)
            => Partitioner().Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, int step, ICollection<GridRange> output)
            => Partitioner().Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, in GridIndex step, ICollection<GridRange> output)
            => Partitioner().Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, ICollection<GridRange> output)
            => Partitioner().Partition(slice, rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, ICollection<GridRange> output)
            => Partitioner().Partition(slice, rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, int step, ICollection<GridRange> output)
            => Partitioner().Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, in GridIndex step, ICollection<GridRange> output)
            => Partitioner().Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, int step, ICollection<GridRange> output)
            => Partitioner().Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, in GridIndex step, ICollection<GridRange> output)
            => Partitioner().Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(int rangeSize)
            => Partitioner().Partition(rangeSize);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndex rangeSize)
            => Partitioner().Partition(rangeSize);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(int rangeSize, int step)
            => Partitioner().Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(int rangeSize, in GridIndex step)
            => Partitioner().Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndex rangeSize, int step)
            => Partitioner().Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(GridIndex rangeSize, GridIndex step)
            => Partitioner().Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, int rangeSize)
            => Partitioner().Partition(slice, rangeSize);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, in GridIndex rangeSize)
            => Partitioner().Partition(slice, rangeSize);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, int rangeSize, int step)
            => Partitioner().Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, int rangeSize, in GridIndex step)
            => Partitioner().Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, int step)
            => Partitioner().Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partitioner().Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(GridIndexRange slice, GridIndex rangeSize, GridIndex step)
            => Partitioner().Partition(slice, rangeSize, step);
    }
}