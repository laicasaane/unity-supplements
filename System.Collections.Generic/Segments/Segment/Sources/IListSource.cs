namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        public readonly struct IListSource : ISegmentSource<T>
        {
            private readonly IList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public IListSource(IList<T> source)
            {
                this.source = source;
            }
        }
    }
}