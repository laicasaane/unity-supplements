// Based on
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterReadOnlyList.cs

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public readonly struct ReadArrayHashSet<T> : IEnumerable<T>
    {
        public ref readonly T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.source == null || (uint)index >= this.Count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.buffer[(uint)index].Value;
            }
        }

        public ref readonly T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.source == null || index >= this.Count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.buffer[index].Value;
            }
        }

        public readonly IEqualityComparerIn<T> Comparer;
        public readonly uint Count;
        public readonly uint Capacity;

        private readonly ArrayHashSet<T> source;
        private readonly ArrayHashSet<T>.Node[] buffer;
        private readonly int[] buckets;

        public ReadArrayHashSet(ArrayHashSet<T> source)
        {
            this.source = source ?? ArrayHashSet<T>.Empty;
            this.Comparer = this.source.Comparer;
            this.buffer = this.source.UnsafeBuffer;
            this.buckets = this.source.UnsafeBuckets;
            this.Count = this.source.Count;
            this.Capacity = this.source.Capacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ArrayHashSet<T> GetSource()
            => this.source ?? ArrayHashSet<T>.Empty;

        public void CopyTo(T[] array, int arrayIndex)
            => CopyTo(array, (uint)arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, uint arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex >= (uint)array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("The number of elements in the source is greater than the available space from index to the end of the destination array.");

            for (var i = 0u; i < this.Count; i++)
            {
                array[arrayIndex++] = this.buffer[i].Value;
            }
        }

        public bool Contains(T item)
            => TryGetIndex(item, out _);

        public bool Contains(in T item)
            => TryGetIndex(in item, out _);

        public void GetAt(uint index, out T item)
        {
            if (index >= this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            item = this.buffer[index];
        }

        public bool TryGetAt(uint index, out T item)
        {
            if (index >= this.Count)
            {
                item = default;
                return false;
            }

            item = this.buffer[index];
            return true;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(GetSource());

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public uint GetIndex(T item)
        {
            if (TryGetIndex(item, out var findIndex)) return findIndex;

            throw new ArrayHashSetException("Key not found");
        }

        public uint GetIndex(in T item)
        {
            if (TryGetIndex(in item, out var findIndex)) return findIndex;

            throw new ArrayHashSetException("Key not found");
        }

        //I store all the index with an offset + 1, so that in the bucket list 0 means actually not existing.
        //When read the offset must be offset by -1 again to be the real one. In this way
        //I avoid to initialize the array to -1
        public bool TryGetIndex(T item, out uint findIndex)
        {
            if (this.source == null)
            {
                findIndex = 0;
                return false;
            }

            var hash = this.Comparer.GetHashCode(item);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);
            var bufferIndex = this.buckets[bucketIndex] - 1;

            //even if we found an existing value we need to be sure it's the one we requested
            while (bufferIndex != -1)
            {
                //for some reason this is way faster than using Comparer<T>.default, should investigate
                ref var node = ref this.buffer[bufferIndex];

                if (node.Hash == hash && this.Comparer.Equals(node.Value, item))
                {
                    //this is the one
                    findIndex = (uint)bufferIndex;
                    return true;
                }

                bufferIndex = node.Previous;
            }

            findIndex = 0;
            return false;
        }

        //I store all the index with an offset + 1, so that in the bucket list 0 means actually not existing.
        //When read the offset must be offset by -1 again to be the real one. In this way
        //I avoid to initialize the array to -1
        public bool TryGetIndex(in T item, out uint findIndex)
        {
            if (this.source == null)
            {
                findIndex = 0;
                return false;
            }

            var hash = this.Comparer.GetHashCode(in item);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);
            var bufferIndex = this.buckets[bucketIndex] - 1;

            //even if we found an existing value we need to be sure it's the one we requested
            while (bufferIndex != -1)
            {
                //for some reason this is way faster than using Comparer<T>.default, should investigate
                ref var node = ref this.buffer[bufferIndex];

                if (node.Hash == hash && this.Comparer.Equals(node.Value, item))
                {
                    //this is the one
                    findIndex = (uint)bufferIndex;
                    return true;
                }

                bufferIndex = node.Previous;
            }

            findIndex = 0;
            return false;
        }

        private static uint Reduce(uint x, uint N)
        {
            if (x >= N)
                return x % N;

            return x;
        }

        public static ReadArrayHashSet<T> Empty { get; } = new ReadArrayHashSet<T>(ArrayHashSet<T>.Empty);

        public static implicit operator ReadArrayHashSet<T>(ArrayHashSet<T> list)
            => new ReadArrayHashSet<T>(list);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly ArrayHashSet<T> source;
            private readonly uint count;

            private uint index;
            private bool first;

            public Enumerator(ArrayHashSet<T> source)
            {
                this.source = source;
                this.count = source.Count;
                this.index = 0;
                this.first = true;
            }

            public bool MoveNext()
            {
#if DEBUG
                if (this.count != this.source.Count)
                    throw new ArrayHashSetException("Cannot modify a dictionary during its iteration");
#endif

                if (this.count == 0)
                    return false;

                if (this.first)
                {
                    this.first = false;
                    return true;
                }

                if (this.index < this.count - 1)
                {
                    this.index += 1;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                this.index = 0;
                this.first = true;
            }

            public void Dispose()
            {
            }

            public T Current
                => this.source[this.index];

            T IEnumerator<T>.Current
                => this.source[this.index];

            object IEnumerator.Current
                => this.source[this.index];
        }
    }
}