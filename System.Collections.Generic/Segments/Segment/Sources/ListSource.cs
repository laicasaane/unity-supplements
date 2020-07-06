namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct ListSource : ISegmentSource<T>
        {
            private readonly List<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public ListSource(List<T> source)
            {
                this.source = source;
            }
        }
    }
}