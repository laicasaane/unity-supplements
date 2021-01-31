using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

        public KeyValuePair<TKey, TValue> this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource()[index];
        }

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Count;
        }

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
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyKeysTo(TKey[] array, int arrayIndex)
            => GetSource().CopyKeysTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyValuesTo(TValue[] array, int arrayIndex)
            => GetSource().CopyValuesTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, uint arrayIndex)
            => GetSource().CopyTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyKeysTo(TKey[] array, uint arrayIndex)
            => GetSource().CopyKeysTo(array, arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyValuesTo(TValue[] array, uint arrayIndex)
            => GetSource().CopyValuesTo(array, arrayIndex);

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
        public ref readonly TValue GetValueByRef(TKey key)
            => ref GetSource().GetValueByRef(key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly TValue GetValueByRef(in TKey key)
            => ref GetSource().GetValueByRef(in key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayDictionary<TKey, TValue>.Node[] GetKeysArray(out uint count)
            => GetSource().GetKeysArray(out count);

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

        [Obsolete("This method has been deprecated. Us GetAt instead.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetKeyValueAt(uint index, out TKey key, out TValue value)
            => GetSource().GetKeyValueAt(index, out key, out value);

        [Obsolete("This method has been deprecated. Us TryGetAt instead.")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetKeyValueAt(uint index, out TKey key, out TValue value)
            => GetSource().TryGetKeyValueAt(index, out key, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetAt(uint index, out TKey key, out TValue value)
            => GetSource().GetAt(index, out key, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetAt(uint index, out TKey key, out TValue value)
            => GetSource().TryGetAt(index, out key, out value);

        public static ReadArrayDictionary<TKey, TValue> Empty { get; }
            = new ReadArrayDictionary<TKey, TValue>(ArrayDictionary<TKey, TValue>.Empty);

        public static implicit operator ReadArrayDictionary<TKey, TValue>(ArrayDictionary<TKey, TValue> dict)
            => new ReadArrayDictionary<TKey, TValue>(dict);
    }

}