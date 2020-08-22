namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct ListSource : ISegmentSource<T>, IEquatable<ListSource>, IEquatableReadOnlyStruct<ListSource>
        {
            private readonly ReadList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public ListSource(in ReadList<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is ListSource other && Equals(in other);

            public bool Equals(ListSource other)
                => this.source.Equals(in other.source);

            public bool Equals(in ListSource other)
                => this.source.Equals(in other.source);

            public bool Equals(ISegmentSource<T> obj)
                => obj is ListSource other && Equals(in other);
        }
    }
}