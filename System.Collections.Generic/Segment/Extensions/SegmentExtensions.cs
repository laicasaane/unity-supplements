namespace System.Collections.Generic
{
    /// <summary>
    /// Provides extension methods for <see cref="Segment{T}"/>.
    /// </summary>
    public static partial class SegmentExtensions
    {
        public static bool ValidateIndex<T>(in this Segment<T> self, int index)
            => self.HasSource && index >= 0 && index < self.Count;

        /// <summary>
        /// Creates an <see cref="SegmentReader{T}"/> over this segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>A new <see cref="SegmentReader{T}"/>.</returns>
        public static SegmentReader<Segment<T>, T> CreateReader<T>(in this Segment<T> segment)
            => new SegmentReader<Segment<T>, T>(segment);
    }
}
