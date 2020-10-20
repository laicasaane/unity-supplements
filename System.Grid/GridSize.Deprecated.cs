using System.Collections.Generic;

namespace System.Grid
{
    public partial struct GridSize
    {
        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(int rangeSize, ICollection<GridRange> output)
            => Partition(rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, ICollection<GridRange> output)
            => Partition(rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(int rangeSize, int step, ICollection<GridRange> output)
            => Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(int rangeSize, in GridIndex step, ICollection<GridRange> output)
            => Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, int step, ICollection<GridRange> output)
            => Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, in GridIndex step, ICollection<GridRange> output)
            => Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, ICollection<GridRange> output)
            => Partition(slice, rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, ICollection<GridRange> output)
            => Partition(slice, rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, int step, ICollection<GridRange> output)
            => Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, in GridIndex step, ICollection<GridRange> output)
            => Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, int step, ICollection<GridRange> output)
            => Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, in GridIndex step, ICollection<GridRange> output)
            => Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(int rangeSize)
            => Partition(rangeSize);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndex rangeSize)
            => Partition(rangeSize);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(int rangeSize, int step)
            => Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(int rangeSize, in GridIndex step)
            => Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndex rangeSize, int step)
            => Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(GridIndex rangeSize, GridIndex step)
            => Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, int rangeSize)
            => Partition(slice, rangeSize);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, in GridIndex rangeSize)
            => Partition(slice, rangeSize);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, int rangeSize, int step)
            => Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, int rangeSize, in GridIndex step)
            => Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, int step)
            => Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridRange> IndexRanges(GridIndexRange slice, GridIndex rangeSize, GridIndex step)
            => Partition(slice, rangeSize, step);
    }
}