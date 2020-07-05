namespace System.Collections.Generic
{
    public static partial class SegmentExtensions
    {
        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static Segment<T> AsSegment<T>(this IReadOnlyList<T> source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static Segment<T> AsSegment<T>(this IList<T> source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ListSegment<T> AsSegment<T>(this List<T> source, int offset, int count)
            => source == null ? ListSegment<T>.Empty : new ListSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static Segment<T> AsSegment<T>(this ISegmentSource<T> source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ArraySegment<T> AsSegment<T>(this T[] source, int offset, int count)
            => source == null ? ArraySegment<T>.Empty : new ArraySegment<T>(source, offset, count);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Segment<T> AsSegment<T>(this IReadOnlyList<T> source, int offset = 0)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Segment<T> AsSegment<T>(this IList<T> source, int offset = 0)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ListSegment<T> AsSegment<T>(this List<T> source, int offset = 0)
            => source == null ? ListSegment<T>.Empty : new ListSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Segment<T> AsSegment<T>(this ISegmentSource<T> source, int offset = 0)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ArraySegment<T> AsSegment<T>(this T[] source, int offset = 0)
            => source == null ? ArraySegment<T>.Empty : new ArraySegment<T>(source, offset, source.Length - offset);
    }
}
