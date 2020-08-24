namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct IListSource : ISegmentSource<T>, IEquatableReadOnlyStruct<IListSource>
        {
            private readonly IList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public IListSource(IList<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source == null ? base.GetHashCode() : this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is IListSource other && Equals(in other);

            public bool Equals(in IListSource other)
                => this.source == other.source;

            public bool Equals(IListSource other)
                => this.source == other.source;

            public bool Equals(ISegmentSource<T> obj)
                => obj is IListSource other && Equals(in other);
        }
    }
}