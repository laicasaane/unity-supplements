namespace System.Collections.Generic
{
    public static partial class SegmentExtensions
    {
        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static Segment<T> Slice<T>(this IReadOnlyList<T> source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static Segment<T> Slice<T>(this IList<T> source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ListSegment<T> Slice<T>(this List<T> source, int offset, int count)
            => source == null ? ListSegment<T>.Empty : new ListSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static ListSegment<T> Slice<T>(in this ReadList<T> source, int offset, int count)
            => new ListSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static Segment<T> Slice<T>(this ISegmentSource<T> source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static Array1Segment<T> Slice<T>(this T[] source, int offset, int count)
            => source == null ? Array1Segment<T>.Empty : new Array1Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static Array1Segment<T> Slice<T>(in this ReadArray1<T> source, int offset, int count)
            => new Array1Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Segment<T> Slice<T>(this IReadOnlyList<T> source, int offset)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Segment<T> Slice<T>(this IList<T> source, int offset)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ListSegment<T> Slice<T>(this List<T> source, int offset)
            => source == null ? ListSegment<T>.Empty : new ListSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ListSegment<T> Slice<T>(in this ReadList<T> source, int offset)
            => new ListSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Segment<T> Slice<T>(this ISegmentSource<T> source, int offset)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Array1Segment<T> Slice<T>(this T[] source, int offset)
            => source == null ? Array1Segment<T>.Empty : new Array1Segment<T>(source, offset, source.Length - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Array1Segment<T> Slice<T>(in this ReadArray1<T> source, int offset)
            => new Array1Segment<T>(source, offset, source.Length - offset);
    }
}