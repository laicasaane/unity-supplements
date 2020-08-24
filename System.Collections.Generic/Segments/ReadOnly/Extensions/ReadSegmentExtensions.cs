namespace System.Collections.Generic
{
    /// <summary>
    /// Provides extension methods for <see cref="ReadSegment{T}"/>.
    /// </summary>
    public static partial class ReadSegmentExtensions
    {
        public static bool ValidateIndex<T>(in this ReadSegment<T> self, int index)
            => self.HasSource && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(in this ReadArray1Segment<T> self, int index)
            => self.HasSource && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(in this ReadListSegment<T> self, int index)
            => self.HasSource && index >= 0 && index < self.Count;

        /// <summary>
        /// Creates an <see cref="SegmentReader{T}"/> over this segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>A new <see cref="SegmentReader{T}"/>.</returns>
        public static SegmentReader<ReadSegment<T>, T> CreateReader<T>(in this ReadSegment<T> segment)
            => new SegmentReader<ReadSegment<T>, T>(segment);

        /// <summary>
        /// Creates an <see cref="SegmentReader{T}"/> over this segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>A new <see cref="SegmentReader{T}"/>.</returns>
        public static SegmentReader<ReadArray1Segment<T>, T> CreateReader<T>(in this ReadArray1Segment<T> segment)
            => new SegmentReader<ReadArray1Segment<T>, T>(segment);

        /// <summary>
        /// Creates an <see cref="SegmentReader{T}"/> over this segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>A new <see cref="SegmentReader{T}"/>.</returns>
        public static SegmentReader<ReadListSegment<T>, T> CreateReader<T>(in this ReadListSegment<T> segment)
            => new SegmentReader<ReadListSegment<T>, T>(segment);
    }
}
