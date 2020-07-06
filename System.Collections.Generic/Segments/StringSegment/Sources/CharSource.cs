namespace System.Collections.Generic
{
    public readonly partial struct StringSegment
    {
        private readonly struct CharSource : ISegmentSource<char>
        {
            private readonly string source;

            public int Count
                => this.source.Length;

            public char this[int index]
                => this.source[index];

            public CharSource(string source)
            {
                this.source = source;
            }
        }
    }
}