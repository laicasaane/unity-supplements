// Based on
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterReadOnlyList.cs

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public readonly struct ReadArrayList<T> : IEnumerable<T>
    {
        public ref readonly T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.buffer == null || (uint)index >= this.Count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.buffer[(uint)index];
            }
        }

        public ref readonly T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.buffer == null || index >= this.Count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.buffer[index];
            }
        }

        public readonly IEqualityComparerIn<T> Comparer;
        public readonly uint Count;
        public readonly uint Capacity;

        private readonly ArrayList<T> source;
        private readonly T[] buffer;

        public ReadArrayList(ArrayList<T> source)
        {
            this.source = source ?? ArrayList<T>.Empty;
            this.Comparer = this.source.Comparer;
            this.buffer = this.source.UnsafeBuffer;
            this.Count = this.source.Count;
            this.Capacity = this.source.Capacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ArrayList<T> GetSource()
            => this.source ?? ArrayList<T>.Empty;

        public bool Contains(T item)
        {
            if (this.buffer == null)
                return false;

            for (var index = 0u; index < this.Count; index++)
                if (this.Comparer.Equals(this.buffer[index], item))
                    return true;

            return false;
        }

        public bool Contains(in T item)
        {
            if (this.buffer == null)
                return false;

            for (var index = 0u; index < this.Count; index++)
                if (this.Comparer.Equals(in this.buffer[index], in item))
                    return true;

            return false;
        }

        public uint? IndexOf(T item)
        {
            if (this.buffer == null)
                return null;

            for (var index = 0u; index < this.Count; index++)
                if (this.Comparer.Equals(this.buffer[index], item))
                    return index;

            return null;
        }

        public uint? IndexOf(in T item)
        {
            if (this.buffer == null)
                return null;

            for (var index = 0u; index < this.Count; index++)
                if (this.Comparer.Equals(in this.buffer[index], in item))
                    return index;

            return null;
        }

        public T[] ToArray()
        {
            if (this.buffer == null)
                return Array.Empty<T>();

            var destinationArray = new T[this.Count];

            Array.Copy(this.buffer, 0, destinationArray, 0, this.Count);

            return destinationArray;
        }

        public ref readonly T Peek()
        {
            if (this.buffer == null || this.Count == 0)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return ref this.buffer[this.Count - 1];
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (this.buffer == null)
                return;

            Array.Copy(this.buffer, 0, array, arrayIndex, this.Count);
        }

        public void CopyTo(T[] array, uint arrayIndex)
        {
            if (this.buffer == null)
                return;

            Array.Copy(this.buffer, 0, array, arrayIndex, this.Count);
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this.buffer, this.Count);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static ReadArrayList<T> Empty { get; } = new ReadArrayList<T>(ArrayList<T>.Empty);

        public static implicit operator ReadArrayList<T>(ArrayList<T> list)
            => new ReadArrayList<T>(list);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly T[] buffer;
            private readonly uint count;

            private uint index;

            public T Current => this.buffer[this.index - 1];

            object IEnumerator.Current => this.Current;

            public Enumerator(T[] buffer, uint count)
            {
                if (buffer == null)
                {
                    this.buffer = ArrayList<T>.Empty.UnsafeBuffer;
                    this.count = 0;
                    this.index = 0;
                }
                else
                {
                    this.buffer = buffer;
                    this.count = count;
                    this.index = 0;
                }
            }

            public bool MoveNext()
            {
                if (this.index < this.count)
                {
                    this.index += 1;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                this.index = 0;
            }

            public void Dispose()
            {
            }
        }
    }
}