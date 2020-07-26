using System.Collections.Generic;

namespace Unity.Collections
{
    public readonly partial struct NativeSliceSegment<T>
    {
        private readonly struct NativeSliceSource : ISegmentSource<T>
        {
            private readonly ReadNativeSlice<T> source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public NativeSliceSource(in ReadNativeSlice<T> source)
            {
                this.source = source;
            }
        }
    }
}