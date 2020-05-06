namespace System.Collections.Generic
{
    public readonly struct ArraySource<T> : ISegmentSource<T>
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