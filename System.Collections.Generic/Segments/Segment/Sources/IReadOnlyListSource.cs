namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct IReadOnlyListSource : ISegmentSource<T>
        {
            private readonly IReadOnlyList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public IReadOnlyListSource(IReadOnlyList<T> source)
            {
                this.source = source;
            }
        }
    }
}