namespace System.Collections.Generic
{
    public static partial class ReadSegmentExtensions
    {
        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ReadSegment<T> AsReadSegment<T>(this IReadOnlyList<T> source, int offset, int count)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ReadSegment<T> AsReadSegment<T>(this IList<T> source, int offset, int count)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ReadListSegment<T> AsReadSegment<T>(this List<T> source, int offset, int count)
            => source == null ? ReadListSegment<T>.Empty : new ReadListSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ReadListSegment<T> AsReadSegment<T>(in this ReadList<T> source, int offset, int count)
            => new ReadListSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ReadSegment<T> AsReadSegment<T>(this IReadSegmentSource<T> source, int offset, int count)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ReadArray1Segment<T> AsReadSegment<T>(this T[] source, int offset, int count)
            => source == null ? ReadArray1Segment<T>.Empty : new ReadArray1Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static ReadArray1Segment<T> AsReadSegment<T>(in this ReadArray1<T> source, int offset, int count)
            => new ReadArray1Segment<T>(source, offset, count);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadSegment<T> AsReadSegment<T>(this IReadOnlyList<T> source, int offset = 0)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadSegment<T> AsReadSegment<T>(this IList<T> source, int offset = 0)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadListSegment<T> AsReadSegment<T>(this List<T> source, int offset = 0)
            => source == null ? ReadListSegment<T>.Empty : new ReadListSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadListSegment<T> AsReadSegment<T>(in this ReadList<T> source, int offset = 0)
            => new ReadListSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadSegment<T> AsReadSegment<T>(this IReadSegmentSource<T> source, int offset = 0)
            => source == null ? ReadSegment<T>.Empty : new ReadSegment<T>(source, offset, source.Count - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadArray1Segment<T> AsReadSegment<T>(this T[] source, int offset = 0)
            => source == null ? ReadArray1Segment<T>.Empty : new ReadArray1Segment<T>(source, offset, source.Length - offset);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static ReadArray1Segment<T> AsReadSegment<T>(in this ReadArray1<T> source, int offset = 0)
            => new ReadArray1Segment<T>(source, offset, source.Length - offset);
    }
}
