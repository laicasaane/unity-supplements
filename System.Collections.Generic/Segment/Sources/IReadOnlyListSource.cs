namespace System.Collections.Generic
{
    public readonly struct IReadOnlyListSource<T> : ISegmentSource<T>
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