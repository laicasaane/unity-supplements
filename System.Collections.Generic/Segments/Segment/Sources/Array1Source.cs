namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct Array1Source : ISegmentSource<T>, IEquatable<Array1Source>, IEquatableReadOnlyStruct<Array1Source>
        {
            private readonly ReadArray1<T> source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public Array1Source(in ReadArray1<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is Array1Source other && Equals(in other);

            public bool Equals(Array1Source other)
                => this.source.Equals(in other.source);

            public bool Equals(in Array1Source other)
                => this.source.Equals(in other.source);

            public bool Equals(ISegmentSource<T> obj)
                => obj is Array1Source other && Equals(in other);
        }
    }
}