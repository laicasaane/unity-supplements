// Based on
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterReadOnlyList.cs

using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public readonly ref struct RefReadArrayList<T>
    {
        private readonly ArrayList<T> source;

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.source.Count;
        }

        public uint Capacity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.source.Capacity;
        }

        public RefReadArrayList(ArrayList<T> list)
        {
            this.source = list;
        }

        public ref readonly T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref this.source[index];
        }

        public ref readonly T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref this.source[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayList<T>.Enumerator GetEnumerator()
            => this.source.GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArrayFast(out uint count)
            => this.source.GetBufferArray(out count);

        public static implicit operator RefReadArrayList<T>(ArrayList<T> list)
            => new RefReadArrayList<T>(list);
    }
}