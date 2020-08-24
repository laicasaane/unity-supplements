﻿namespace System.Collections.Generic
{
    public readonly partial struct ReadSegment<T>
    {
        private readonly struct IReadOnlyListSource : IReadSegmentSource<T>, IEquatableReadOnlyStruct<IReadOnlyListSource>
        {
            private readonly IReadOnlyList<T> source;

            public int Count
                => this.source.Count;

            public T this[int index]
                => this.source[index];

            public IReadOnlyListSource(IReadOnlyList<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source == null ? base.GetHashCode() : this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is IReadOnlyListSource other && Equals(in other);

            public bool Equals(IReadOnlyListSource other)
                => this.source == other.source;

            public bool Equals(in IReadOnlyListSource other)
                => this.source == other.source;

            public bool Equals(IReadSegmentSource<T> obj)
                => obj is IReadOnlyListSource other && Equals(in other);
        }
    }
}