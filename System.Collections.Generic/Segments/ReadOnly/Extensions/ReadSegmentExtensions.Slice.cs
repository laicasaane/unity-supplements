namespace System.Collections.Generic
{
    public static partial class ReadSegmentExtensions
    {
        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ReadSegment<T> Slice<T>(this IReadOnlyList<T> source, int offset, int count)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ReadSegment<T> Slice<T>(this IList<T> source, int offset, int count)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ReadListSegment<T> Slice<T>(this List<T> source, int offset, int count)
            => source == null ? ReadListSegment<T>.Empty : new ReadListSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ReadListSegment<T> Slice<T>(in this ReadList<T> source, int offset, int count)
            => new ReadListSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ReadSegment<T> Slice<T>(this IReadSegmentSource<T> source, int offset, int count)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ReadArray1Segment<T> Slice<T>(this T[] source, int offset, int count)
            => source == null ? ReadArray1Segment<T>.Empty : new ReadArray1Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ReadArray1Segment<T> Slice<T>(in this ReadArray1<T> source, int offset, int count)
            => new ReadArray1Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadSegment<T> Slice<T>(this IReadOnlyList<T> source, int offset)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadSegment<T> Slice<T>(this IList<T> source, int offset)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadListSegment<T> Slice<T>(this List<T> source, int offset)
            => source == null ? ReadListSegment<T>.Empty : new ReadListSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadListSegment<T> Slice<T>(in this ReadList<T> source, int offset)
            => new ReadListSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadSegment<T> Slice<T>(this IReadSegmentSource<T> source, int offset)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadArray1Segment<T> Slice<T>(this T[] source, int offset)
            => source == null ? ReadArray1Segment<T>.Empty : new ReadArray1Segment<T>(source, offset, source.Length - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadArray1Segment<T> Slice<T>(in this ReadArray1<T> source, int offset)
            => new ReadArray1Segment<T>(source, offset, source.Length - offset);
    }
}