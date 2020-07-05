namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        public readonly struct ListSource : ISegmentSource<T>
        {
            private readonly ReadList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public ListSource(in ReadList<T> source)
            {
                this.source = source;
            }
        }
    }
}