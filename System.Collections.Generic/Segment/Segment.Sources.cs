namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct SourceReadOnlyList : ISegmentSource<T>
        {
            private readonly IReadOnlyList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public SourceReadOnlyList(IReadOnlyList<T> source)
            {
                this.source = source;
            }
        }

        private readonly struct SourceList : ISegmentSource<T>
        {
            private readonly IList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public SourceList(IList<T> source)
            {
                this.source = source;
            }
        }
    }
}