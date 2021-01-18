// Based on
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterReadOnlyList.cs

using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public readonly struct ReadArrayList<T>
    {
        private readonly ArrayList<T> source;

        public uint Count => GetSource().Count;

        public uint Capacity => GetSource().Capacity;

        public ReadArrayList(ArrayList<T> list)
        {
            this.source = list ?? ArrayList<T>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ArrayList<T> GetSource()
            => this.source ?? ArrayList<T>.Empty;

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref GetSource()[index];
        }

        public ref T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref GetSource()[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArrayFast(out uint count)
            => GetSource().GetBufferArray(out count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayList<T>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        public static ReadArrayList<T> Empty { get; } = new ReadArrayList<T>(ArrayList<T>.Empty);

        public static implicit operator ReadArrayList<T>(ArrayList<T> list)
            => new ReadArrayList<T>(list);

        public static implicit operator RefReadArrayList<T>(in ReadArrayList<T> list)
            => new RefReadArrayList<T>(list.source);
    }
}