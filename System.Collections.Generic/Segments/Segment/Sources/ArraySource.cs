namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct ArraySource : ISegmentSource<T>
        {
            private readonly T[] source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public ArraySource(T[] source)
            {
                this.source = source;
            }
        }
    }
}