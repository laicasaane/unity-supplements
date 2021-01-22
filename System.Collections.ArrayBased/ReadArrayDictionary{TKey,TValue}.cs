﻿using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public readonly struct ReadArrayDictionary<TKey, TValue>
    {
        private readonly ArrayDictionary<TKey, TValue> source;

        public ReadArrayDictionary(ArrayDictionary<TKey, TValue> source)
        {
            this.source = source ?? ArrayDictionary<TKey, TValue>.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ArrayDictionary<TKey, TValue> GetSource()
            => this.source ?? ArrayDictionary<TKey, TValue>.Empty;

        public TValue this[TKey key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource()[key];
        }

        public TValue this[in TKey key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource()[in key];
        }

        public uint Count => GetSource().Count;

        public ReadArray1<ArrayDictionary<TKey, TValue>.Node> Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Keys;
        }

        public ReadArray1<TValue> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Values;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(TKey key)
            => GetSource().ContainsKey(key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(in TKey key)
            => GetSource().ContainsKey(in key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyValuesTo(TValue[] tasks, uint index)
            => GetSource().CopyValuesTo(tasks, index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayDictionary<TKey, TValue>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetIndex(TKey key)
            => GetSource().GetIndex(key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetIndex(in TKey key)
            => GetSource().GetIndex(in key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TValue GetValueByRef(TKey key)
            => ref GetSource().GetValueByRef(key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref TValue GetValueByRef(in TKey key)
            => ref GetSource().GetValueByRef(in key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TValue[] GetValuesArray(out uint count)
            => GetSource().GetValuesArray(out count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetIndex(TKey key, out uint foundIndex)
            => GetSource().TryGetIndex(key, out foundIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetIndex(in TKey key, out uint foundIndex)
            => GetSource().TryGetIndex(in key, out foundIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(TKey key, out TValue result)
            => GetSource().TryGetValue(key, out result);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(in TKey key, out TValue result)
            => GetSource().TryGetValue(in key, out result);

        public static ReadArrayDictionary<TKey, TValue> Empty { get; }
            = new ReadArrayDictionary<TKey, TValue>(ArrayDictionary<TKey, TValue>.Empty);

        public static implicit operator ReadArrayDictionary<TKey, TValue>(ArrayDictionary<TKey, TValue> dict)
            => new ReadArrayDictionary<TKey, TValue>(dict);
    }

}