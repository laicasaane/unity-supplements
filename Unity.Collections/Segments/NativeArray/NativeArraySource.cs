using System.Collections.Generic;

namespace Unity.Collections
{
    public readonly partial struct NativeArraySegment<T>
    {
        private readonly struct NativeArraySource : ISegmentSource<T>
        {
            private readonly ReadNativeArray<T> source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public NativeArraySource(in ReadNativeArray<T> source)
            {
                this.source = source;
            }
        }
    }
}