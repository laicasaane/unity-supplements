// Based on
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterReadOnlyList.cs

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public readonly struct ReadArrayList<T>
    {
        private readonly ArrayList<T> source;

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Count;
        }

        public uint Capacity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Capacity;
        }

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
        public T[] ToArray()
            => GetSource().ToArray();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadArray1<T> ToArrayFast()
        {
            var buffer = GetSource().GetBufferArray(out var count);
            var length = buffer.Length;

            if (count < length)
                length = (int)count;

            return new ReadArray1<T>(buffer, length, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayList<T>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in T item)
            => GetSource().Contains(in item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in T item, IEqualityComparerIn<T> comparer)
            => GetSource().Contains(in item, comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
            => GetSource().Contains(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item, IEqualityComparer<T> comparer)
            => GetSource().Contains(item, comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint? IndexOf(in T item)
            => GetSource().IndexOf(in item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint? IndexOf(in T item, IEqualityComparerIn<T> comparer)
            => GetSource().IndexOf(in item, comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint? IndexOf(T item)
            => GetSource().IndexOf(item);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint? IndexOf(T item, IEqualityComparer<T> comparer)
            => GetSource().IndexOf(item, comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly T Peek()
            => ref GetSource().Peek();

        public static ReadArrayList<T> Empty { get; } = new ReadArrayList<T>(ArrayList<T>.Empty);

        public static implicit operator ReadArrayList<T>(ArrayList<T> list)
            => new ReadArrayList<T>(list);

        public static implicit operator RefReadArrayList<T>(in ReadArrayList<T> list)
            => new RefReadArrayList<T>(list.source);
    }
}