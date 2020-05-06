namespace System.Collections.Generic
{
    public readonly struct ListSource<T> : ISegmentSource<T>
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