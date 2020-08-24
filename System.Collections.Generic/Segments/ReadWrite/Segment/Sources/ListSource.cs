using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct ListSource : ISegmentSource<T>, IEquatableReadOnlyStruct<ListSource>
        {
            private readonly List<T> source;

            public int Count
                => GetSource().Count;

            public T this[int index]
                => GetSource()[index];

            public ListSource(List<T> source)
            {
                this.source = source ?? ReadList<T>._empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal List<T> GetSource()
                => this.source ?? ReadList<T>._empty;

            public override int GetHashCode()
                => GetSource().GetHashCode();

            public override bool Equals(object obj)
                => obj is ListSource other && Equals(in other);

            public bool Equals(ListSource other)
                => GetSource().Equals(other.GetSource());

            public bool Equals(in ListSource other)
                => GetSource().Equals(other.GetSource());

            public bool Equals(ISegmentSource<T> obj)
                => obj is ListSource other && Equals(in other);

            public static ListSource Empty { get; } = new ListSource(ReadList<T>._empty);
        }
    }
}