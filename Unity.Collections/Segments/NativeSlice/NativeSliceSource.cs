using System;
using System.Collections.Generic;

namespace Unity.Collections
{
    public readonly partial struct NativeSliceSegment<T>
    {
        private readonly struct NativeSliceSource : ISegmentSource<T>,
            IEquatable<NativeSliceSource>, IEquatableReadOnlyStruct<NativeSliceSource>
        {
            private readonly ReadNativeSlice<T> source;

            public int Count
                => this.source.Length;

            public T this[int index]
                => this.source[index];

            public NativeSliceSource(in ReadNativeSlice<T> source)
            {
                this.source = source;
            }

            public override int GetHashCode()
                => this.source.GetHashCode();

            public override bool Equals(object obj)
                => obj is NativeSliceSource other && Equals(in other);

            public bool Equals(NativeSliceSource other)
                => this.source.Equals(in other.source);

            public bool Equals(in NativeSliceSource other)
                => this.source.Equals(in other.source);

            public bool Equals(ISegmentSource<T> obj)
                => obj is NativeSliceSource other && Equals(in other);
        }
    }
}