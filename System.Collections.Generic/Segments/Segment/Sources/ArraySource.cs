namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        public readonly struct ArraySource : ISegmentSource<T>
        {
            private readonly ReadArray<T> source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public ArraySource(in ReadArray<T> source)
            {
                this.source = source;
            }
        }
    }
}