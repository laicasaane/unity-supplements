namespace System.Collections.Generic
{
    public readonly partial struct ReadSegment<T>
    {
        public readonly struct CharSource : IReadSegmentSource<char>, IEquatableReadOnlyStruct<CharSource>
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

            public override int GetHashCode()
                => this.source == null ? base.GetHashCode() : this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is CharSource other && Equals(in other);

            public bool Equals(in CharSource other)
                => string.Equals(this.source, other.source);

            public bool Equals(CharSource other)
                => string.Equals(this.source, other.source);

            public bool Equals(IReadSegmentSource<char> obj)
                => obj is CharSource other && Equals(in other);
        }
    }
}