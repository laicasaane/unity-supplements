namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct ArraySource : ISegmentSource<T>, IEquatable<ArraySource>, IEquatableReadOnlyStruct<ArraySource>
        {
            private readonly ReadArray<T> source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public ArraySource(in ReadArray<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is ArraySource other && Equals(in other);

            public bool Equals(ArraySource other)
                => this.source.Equals(in other.source);

            public bool Equals(in ArraySource other)
                => this.source.Equals(in other.source);

            public bool Equals(ISegmentSource<T> obj)
                => obj is ArraySource other && Equals(in other);
        }
    }
}