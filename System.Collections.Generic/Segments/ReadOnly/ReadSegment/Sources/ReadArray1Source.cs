namespace System.Collections.Generic
{
    public readonly partial struct ReadSegment<T>
    {
        private readonly struct ReadArray1Source : IReadSegmentSource<T>, IEquatableReadOnlyStruct<ReadArray1Source>
        {
            private readonly ReadArray1<T> source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public ReadArray1Source(in ReadArray1<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is ReadArray1Source other && Equals(in other);

            public bool Equals(ReadArray1Source other)
                => this.source.Equals(in other.source);

            public bool Equals(in ReadArray1Source other)
                => this.source.Equals(in other.source);

            public bool Equals(IReadSegmentSource<T> obj)
                => obj is ReadArray1Source other && Equals(in other);
        }
    }
}