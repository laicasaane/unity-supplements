using System.Collections.Generic;

namespace System.Grid
{
    public partial struct ClampedGridSize
    {
        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(int rangeSize, ICollection<GridIndexRange> output)
            => Partition(rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, ICollection<GridIndexRange> output)
            => Partition(rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(int rangeSize, int step, ICollection<GridIndexRange> output)
            => Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(int rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
            => Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, int step, ICollection<GridIndexRange> output)
            => Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndex rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
            => Partition(rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, ICollection<GridIndexRange> output)
            => Partition(slice, rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, ICollection<GridIndexRange> output)
            => Partition(slice, rangeSize, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, int step, ICollection<GridIndexRange> output)
            => Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, int rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
            => Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, int step, ICollection<GridIndexRange> output)
            => Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public void IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, in GridIndex step, ICollection<GridIndexRange> output)
            => Partition(slice, rangeSize, step, output);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(int rangeSize)
            => Partition(rangeSize);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(in GridIndex rangeSize)
            => Partition(rangeSize);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(int rangeSize, int step)
            => Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(int rangeSize, in GridIndex step)
            => Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(in GridIndex rangeSize, int step)
            => Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(GridIndex rangeSize, GridIndex step)
            => Partition(rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(in GridIndexRange slice, int rangeSize)
            => Partition(slice, rangeSize);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(in GridIndexRange slice, in GridIndex rangeSize)
            => Partition(slice, rangeSize);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(in GridIndexRange slice, int rangeSize, int step)
            => Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(in GridIndexRange slice, int rangeSize, in GridIndex step)
            => Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(in GridIndexRange slice, in GridIndex rangeSize, int step)
            => Partition(slice, rangeSize, step);

        [Obsolete("This method has been deprecated. Use Partition instead.")]
        public IEnumerable<GridIndexRange> IndexRanges(GridIndexRange slice, GridIndex rangeSize, GridIndex step)
            => Partition(slice, rangeSize, step);
    }
}