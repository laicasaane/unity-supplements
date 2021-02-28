using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public readonly struct ReadArrayDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public ref readonly TValue this[TKey key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.source == null)
                    throw ThrowHelper.GetArgumentOutOfRange_KeyException();

                return ref this.values[GetIndex(key)];
            }
        }

        public ref readonly TValue this[in TKey key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.source == null)
                    throw ThrowHelper.GetArgumentOutOfRange_KeyException();

                return ref this.values[GetIndex(in key)];
            }
        }

        public KeyValuePair<TKey, TValue> this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.source == null || index >= this.Count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return new KeyValuePair<TKey, TValue>(this.keys[index], this.values[index]);
            }
        }

        public ReadArray1<ArrayDictionary<TKey, TValue>.Node> Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new ReadArray1<ArrayDictionary<TKey, TValue>.Node>(GetSource().UnsafeKeys, this.Count);
        }

        public ReadArray1<TValue> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new ReadArray1<TValue>(GetSource().UnsafeValues, this.Count);
        }

        public readonly IEqualityComparerIn<TKey> Comparer;
        public readonly uint Count;
        public readonly uint Capacity;

        private readonly ArrayDictionary<TKey, TValue> source;
        private readonly ArrayDictionary<TKey, TValue>.Node[] keys;
        private readonly TValue[] values;
        private readonly int[] buckets;

        public ReadArrayDictionary(ArrayDictionary<TKey, TValue> source)
        {
            this.source = source ?? ArrayDictionary<TKey, TValue>.Empty;
            this.Comparer = this.source.Comparer;
            this.keys = this.source.UnsafeKeys;
            this.values = this.source.UnsafeValues;
            this.buckets = this.source.UnsafeBuckets;
            this.Count = this.source.Count;
            this.Capacity = this.source.Capacity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ArrayDictionary<TKey, TValue> GetSource()
            => this.source ?? ArrayDictionary<TKey, TValue>.Empty;

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
            => CopyTo(array, (uint)arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, uint arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex >= (uint)array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("The number of elements in the source is greater than the available space from index to the end of the destination array.");

            if (this.source == null)
                return;

            for (var i = 0u; i < this.Count; i++)
            {
                array[arrayIndex++] = new KeyValuePair<TKey, TValue>(this.keys[i].Key, this.values[i]);
            }
        }

        public void CopyKeysTo(TKey[] array, int arrayIndex)
            => CopyKeysTo(array, (uint)arrayIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyKeysTo(TKey[] array, uint arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex >= (uint)array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("The number of elements in the source is greater than the available space from index to the end of the destination array.");

            if (this.source == null)
                return;

            for (var i = 0u; i < this.Count; i++)
            {
                array[arrayIndex++] = this.keys[i].Key;
            }
        }

        public void CopyValuesTo(TValue[] array, int arrayIndex)
        {
            if (this.source == null)
                return;

            Array.Copy(this.values, 0, array, arrayIndex, this.Count);
        }

        public void CopyValuesTo(TValue[] array, uint arrayIndex)
        {
            if (this.source == null)
                return;

            Array.Copy(this.values, 0, array, arrayIndex, this.Count);
        }

        public bool ContainsValue(TValue value)
        {
            if (value == null)
                return false;

            if (this.source == null)
                return false;

            var comparer = EqualityComparerIn<TValue>.Default;

            for (var i = 0u; i < this.Count; i++)
            {
                if (comparer.Equals(this.values[i], value))
                    return true;
            }

            return false;
        }

        public bool ContainsValue(TValue value, IEqualityComparer<TValue> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            if (value == null)
                return false;

            if (this.source == null)
                return false;

            for (var i = 0u; i < this.Count; i++)
            {
                if (comparer.Equals(this.values[i], value))
                    return true;
            }

            return false;
        }

        public bool ContainsValue(in TValue value)
        {
            if (value == null)
                return false;

            if (this.source == null)
                return false;

            var comparer = EqualityComparerIn<TValue>.Default;

            for (var i = 0u; i < this.Count; i++)
            {
                if (comparer.Equals(in this.values[i], in value))
                    return true;
            }

            return false;
        }

        public bool ContainsValue(in TValue value, IEqualityComparerIn<TValue> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            if (value == null)
                return false;

            if (this.source == null)
                return false;

            for (var i = 0u; i < this.Count; i++)
            {
                if (comparer.Equals(in this.values[i], in value))
                    return true;
            }

            return false;
        }

        public bool ContainsKey(TKey key)
            => TryGetIndex(key, out _);

        public bool ContainsKey(in TKey key)
            => TryGetIndex(in key, out _);

        public bool TryGetValue(TKey key, out TValue result)
        {
            if (TryGetIndex(key, out var findIndex))
            {
                result = this.values[findIndex];
                return true;
            }

            result = default;
            return false;
        }

        public bool TryGetValue(in TKey key, out TValue result)
        {
            if (TryGetIndex(in key, out var findIndex))
            {
                result = this.values[findIndex];
                return true;
            }

            result = default;
            return false;
        }

        public void GetAt(uint index, out TKey key, out TValue value)
        {
            if (index >= this.Count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            key = this.keys[index];
            value = this.values[index];
        }

        public bool TryGetAt(uint index, out TKey key, out TValue value)
        {
            if (index >= this.Count)
            {
                key = default;
                value = default;
                return false;
            }

            key = this.keys[index];
            value = this.values[index];
            return true;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(GetSource());

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public ref TValue GetValueByRef(TKey key)
        {
            if (TryGetIndex(key, out var findIndex))
            {
                return ref this.values[findIndex];
            }

            throw new ArrayDictionaryException("Key not found");
        }

        public ref TValue GetValueByRef(in TKey key)
        {
            if (TryGetIndex(in key, out var findIndex))
            {
                return ref this.values[findIndex];
            }

            throw new ArrayDictionaryException("Key not found");
        }

        public uint GetIndex(TKey key)
        {
            if (TryGetIndex(key, out var findIndex)) return findIndex;

            throw new ArrayDictionaryException("Key not found");
        }

        public uint GetIndex(in TKey key)
        {
            if (TryGetIndex(in key, out var findIndex)) return findIndex;

            throw new ArrayDictionaryException("Key not found");
        }

        //I store all the index with an offset + 1, so that in the bucket list 0 means actually not existing.
        //When read the offset must be offset by -1 again to be the real one. In this way
        //I avoid to initialize the array to -1
        public bool TryGetIndex(TKey key, out uint findIndex)
        {
            if (this.source == null)
            {
                findIndex = 0;
                return false;
            }

            var hash = this.Comparer.GetHashCode(key);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            var kvIndex = this.buckets[bucketIndex] - 1;

            //even if we found an existing value we need to be sure it's the one we requested
            while (kvIndex != -1)
            {
                ref var node = ref this.keys[kvIndex];

                if (node.Hash == hash && this.Comparer.Equals(node.Key, key))
                {
                    //this is the one
                    findIndex = (uint)kvIndex;
                    return true;
                }

                kvIndex = node.Previous;
            }

            findIndex = 0;
            return false;
        }

        //I store all the index with an offset + 1, so that in the bucket list 0 means actually not existing.
        //When read the offset must be offset by -1 again to be the real one. In this way
        //I avoid to initialize the array to -1
        public bool TryGetIndex(in TKey key, out uint findIndex)
        {
            if (this.source == null)
            {
                findIndex = 0;
                return false;
            }

            var hash = this.Comparer.GetHashCode(in key);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            var kvIndex = this.buckets[bucketIndex] - 1;

            //even if we found an existing value we need to be sure it's the one we requested
            while (kvIndex != -1)
            {
                ref var node = ref this.keys[kvIndex];

                if (node.Hash == hash && this.Comparer.Equals(in node.Key, in key))
                {
                    //this is the one
                    findIndex = (uint)kvIndex;
                    return true;
                }

                kvIndex = node.Previous;
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

        public static ReadArrayDictionary<TKey, TValue> Empty { get; }
            = new ReadArrayDictionary<TKey, TValue>(ArrayDictionary<TKey, TValue>.Empty);

        public static implicit operator ReadArrayDictionary<TKey, TValue>(ArrayDictionary<TKey, TValue> dict)
            => new ReadArrayDictionary<TKey, TValue>(dict);

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private readonly ArrayDictionary<TKey, TValue> source;
            private readonly uint count;

            private uint index;
            private bool first;

            public Enumerator(ArrayDictionary<TKey, TValue> source)
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
                    throw new ArrayDictionaryException("Cannot modify a dictionary during its iteration");
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

            public ArrayDictionary<TKey, TValue>.KeyValuePair Current
                => new ArrayDictionary<TKey, TValue>.KeyValuePair(this.source.UnsafeKeys[this.index], this.source.UnsafeValues, this.index);

            KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
                => new KeyValuePair<TKey, TValue>(this.source.UnsafeKeys[this.index], this.source.UnsafeValues[this.index]);

            object IEnumerator.Current
                => new KeyValuePair<TKey, TValue>(this.source.UnsafeKeys[this.index], this.source.UnsafeValues[this.index]);
        }
    }
}