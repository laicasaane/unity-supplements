namespace System.Collections.Generic
{
    public readonly partial struct ReadSegment<T>
    {
        private readonly struct ReadListSource : IReadSegmentSource<T>, IEquatableReadOnlyStruct<ReadListSource>
        {
            private readonly ReadList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public ReadListSource(in ReadList<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is ReadListSource other && Equals(in other);

            public bool Equals(ReadListSource other)
                => this.source.Equals(in other.source);

            public bool Equals(in ReadListSource other)
                => this.source.Equals(in other.source);

            public bool Equals(IReadSegmentSource<T> obj)
                => obj is ReadListSource other && Equals(in other);
        }
    }
}