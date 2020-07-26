using System.Collections.Generic;

namespace Unity.Collections
{
    public static class NativeSegmentExtensions
    {
        public static bool ValidateIndex<T>(in this NativeArraySegment<T> self, int index) where T : struct
            => self.HasSource && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(in this NativeSliceSegment<T> self, int index) where T : struct
            => self.HasSource && index >= 0 && index < self.Count;

        /// <summary>
        /// Creates an <see cref="SegmentReader{T}"/> over this segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>A new <see cref="SegmentReader{T}"/>.</returns>
        public static SegmentReader<NativeArraySegment<T>, T> CreateReader<T>(in this NativeArraySegment<T> segment) where T : struct
            => new SegmentReader<NativeArraySegment<T>, T>(segment);

        /// <summary>
        /// Creates an <see cref="SegmentReader{T}"/> over this segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>A new <see cref="SegmentReader{T}"/>.</returns>
        public static SegmentReader<NativeSliceSegment<T>, T> CreateReader<T>(in this NativeSliceSegment<T> segment) where T : struct
            => new SegmentReader<NativeSliceSegment<T>, T>(segment);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static NativeArraySegment<T> Slice<T>(in this ReadNativeArray<T> source, int offset, int count) where T : struct
            => new NativeArraySegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static NativeArraySegment<T> Slice<T>(in this ReadNativeArray<T> source, int offset) where T : struct
            => new NativeArraySegment<T>(source, offset, source.Length - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static NativeSliceSegment<T> Slice<T>(in this ReadNativeSlice<T> source, int offset, int count) where T : struct
            => new NativeSliceSegment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static NativeSliceSegment<T> Slice<T>(in this ReadNativeSlice<T> source, int offset) where T : struct
            => new NativeSliceSegment<T>(source, offset, source.Length - offset);
    }
}