using System;
using System.Collections.Generic;

namespace Unity.Collections
{
    public readonly partial struct NativeArraySegment<T>
    {
        private readonly struct NativeArraySource : ISegmentSource<T>,
            IEquatable<NativeArraySource>, IEquatableReadOnlyStruct<NativeArraySource>
        {
            private readonly ReadNativeArray<T> source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public NativeArraySource(in ReadNativeArray<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is NativeArraySource other && Equals(in other);

            public bool Equals(NativeArraySource other)
                => this.source.Equals(in other.source);

            public bool Equals(in NativeArraySource other)
                => this.source.Equals(in other.source);

            public bool Equals(ISegmentSource<T> obj)
                => obj is NativeArraySource other && Equals(in other);
        }
    }
}