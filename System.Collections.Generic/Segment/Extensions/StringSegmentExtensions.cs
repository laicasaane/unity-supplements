namespace System.Collections.Generic
{
    public static class StringSegmentExtensions
    {
        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static StringSegment AsSegment(this string source, int offset, int count)
            => source == null ? StringSegment.Empty :
               new StringSegment(source, offset, count);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static StringSegment AsSegment(this string source, int offset = 0)
            => source == null ? StringSegment.Empty :
               new StringSegment(source, offset, source.Length - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static StringSegment Slice(this string source, int offset, int count)
            => source == null ? StringSegment.Empty :
               new StringSegment(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static StringSegment Slice(this string source, int offset)
            => source == null ? StringSegment.Empty :
               new StringSegment(source, offset, source.Length - offset);
    }
}