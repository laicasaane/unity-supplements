namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        public readonly struct StringSource : ISegmentSource<char>
        {
            private readonly string source;

            public int Count
                => this.source.Length;

            public char this[int index]
                => this.source[index];

            public StringSource(string source)
            {
                this.source = source;
            }
        }
    }
}