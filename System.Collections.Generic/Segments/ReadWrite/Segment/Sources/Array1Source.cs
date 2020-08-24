using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    public readonly partial struct Segment<T>
    {
        private readonly struct Array1Source : ISegmentSource<T>, IEquatableReadOnlyStruct<Array1Source>
        {
            private readonly T[] source;

            public int Count
                => GetSource().Length;

            public T this[int index]
                => GetSource()[index];

            public Array1Source(T[]source)
            {
                this.source = source ?? ReadArray1<T>._empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal T[] GetSource()
                => this.source ?? ReadArray1<T>._empty;

            public override int GetHashCode()
                => GetSource().GetHashCode();

            public override bool Equals(object obj)
                => obj is Array1Source other && Equals(in other);

            public bool Equals(Array1Source other)
                => GetSource().Equals(other.GetSource());

            public bool Equals(in Array1Source other)
                => GetSource().Equals(other.GetSource());

            public bool Equals(ISegmentSource<T> obj)
                => obj is Array1Source other && Equals(in other);

            public static Array1Source Empty { get; } = new Array1Source(ReadArray1<T>._empty);
        }
    }
}