using System.IO;

namespace System.Collections.Generic
{
    /// <summary>
    /// A reader that divides a source <see cref="Segment{T}"/> into multiple <see cref="Segment{T}"/> instances.
    /// </summary>
    /// <typeparam name="TSegment">The type of elements contained in the source.</typeparam>
    public sealed class SegmentReader<TSegment, TValue> : ISegmentReader<TSegment>
        where TSegment : ISegment<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentReader&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="source">The source segment.</param>
        public SegmentReader(TSegment source)
        {
            this.Source = source;
            this.Position = 0;
        }

        /// <summary>
        /// Gets the source segment.
        /// </summary>
        public TSegment Source { get; }

        /// <summary>
        /// Gets or sets the position of this reader.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Creates a new segment which starts at the current position and covers the specified number of elements.
        /// </summary>
        /// <param name="count">The number of elements in the new segment.</param>
        /// <returns>The new segment.</returns>
        public TSegment Read(int count)
        {
            var ret = this.Source.Slice(this.Position, count);
            this.Position += count;

            return (TSegment)ret;
        }

        /// <summary>
        /// Sets the position of this reader. Returns the new position.
        /// </summary>
        /// <param name="offset">The offset from the origin.</param>
        /// <param name="origin">The origin to use when setting the position.</param>
        /// <returns>The new position.</returns>
        public int Seek(int offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.Position = offset;
                    break;
                case SeekOrigin.Current:
                    this.Position += offset;
                    break;
                case SeekOrigin.End:
                    this.Position = this.Source.Count + offset;
                    break;
            }

            return this.Position;
        }
    }
}
