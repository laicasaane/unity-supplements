// Based on
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Dictionaries/FasterDictionary.cs
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Dictionaries/FasterDictionaryNode.cs

using System.Collections.Generic;
using System.Helpers;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    /// <summary>
    /// Array Dictionary allows iterating over its value collection directly as an array, without using an enumerator.
    /// For value iteration, Array Dictionary is N times faster than the standard dictionary.
    /// For most of the operations, it is also faster, but the difference is negligible.
    /// The only slower operation is resizing the memory on Add, as this implementation needs to use two separate arrays
    /// compared to the standard dictionary.
    /// </summary>
    public partial class ArrayDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly IEqualityComparerIn<TKey> comparer;

        private Node[] keys;
        private TValue[] values;
        private int[] buckets;
        private uint freeValueCellIndex;
        private uint collisions;

        public TValue this[TKey key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.values[GetIndex(key)];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => AddValue(key, value, out _);
        }

        public TValue this[in TKey key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.values[GetIndex(in key)];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => AddValue(in key, value, out _);
        }

        public KeyValuePair<TKey, TValue> this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= this.freeValueCellIndex)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return new KeyValuePair<TKey, TValue>(this.keys[index], this.values[index]);
            }
        }

        public IEqualityComparerIn<TKey> Comparer => this.comparer;

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.freeValueCellIndex;
        }

        public Node[] UnsafeKeys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.keys;
        }

        public TValue[] UnsafeValues
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.values;
        }

        public ReadArray1<Node> Keys
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new ReadArray1<Node>(this.keys, this.Count);
        }

        public ReadArray1<TValue> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new ReadArray1<TValue>(this.values, this.Count);
        }

        public ArrayDictionary()
            : this(EqualityComparerIn<TKey>.Default)
        {
        }

        public ArrayDictionary(int capacity)
            : this((uint)capacity)
        { }

        public ArrayDictionary(uint capacity)
            : this(capacity, EqualityComparerIn<TKey>.Default)
        {
        }

        public ArrayDictionary(IEqualityComparerIn<TKey> comparer)
        {
            this.comparer = comparer ?? EqualityComparerIn<TKey>.Default;
            this.keys = new Node[1];
            this.values = new TValue[1];
            this.buckets = new int[3];
        }

        public ArrayDictionary(int capacity, IEqualityComparerIn<TKey> comparer)
            : this((uint)capacity, comparer)
        { }

        public ArrayDictionary(uint capacity, IEqualityComparerIn<TKey> comparer)
        {
            this.comparer = comparer ?? EqualityComparerIn<TKey>.Default;
            this.keys = new Node[capacity];
            this.values = new TValue[capacity];
            this.buckets = new int[HashHelpers.GetPrime((int)capacity)];
        }

        public ArrayDictionary(ArrayDictionary<TKey, TValue> source, IEqualityComparerIn<TKey> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.comparer = comparer ?? source.comparer ?? EqualityComparerIn<TKey>.Default;

            this.values = new TValue[source.values.Length];
            Array.Copy(source.values, this.values, this.values.Length);

            this.keys = new Node[source.keys.Length];
            Array.Copy(source.keys, this.keys, this.keys.Length);

            this.buckets = new int[source.buckets.Length];
            Array.Copy(source.buckets, this.buckets, this.buckets.Length);

            this.freeValueCellIndex = source.freeValueCellIndex;
            this.collisions = source.collisions;
        }

        public ArrayDictionary(in ReadArrayDictionary<TKey, TValue> source, IEqualityComparerIn<TKey> comparer = null)
            : this(source.GetSource(), comparer)
        { }

        public ArrayDictionary(ICollection<KeyValuePair<TKey, TValue>> source, IEqualityComparerIn<TKey> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.comparer = comparer ?? EqualityComparerIn<TKey>.Default;
            this.keys = new Node[source.Count];
            this.values = new TValue[source.Count];
            this.buckets = new int[HashHelpers.GetPrime(source.Count)];

            foreach (var kv in source)
            {
                AddValue(kv.Key, kv.Value, out _);
            }
        }

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

            for (var i = 0u; i < this.freeValueCellIndex; i++)
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

            for (var i = 0u; i < this.freeValueCellIndex; i++)
            {
                array[arrayIndex++] = this.keys[i].Key;
            }
        }

        public void CopyValuesTo(TValue[] array, int arrayIndex)
            => Array.Copy(this.values, 0, array, arrayIndex, this.Count);

        public void CopyValuesTo(TValue[] array, uint arrayIndex)
            => Array.Copy(this.values, 0, array, arrayIndex, this.Count);

        public Node[] GetKeysArray(out uint count)
        {
            count = this.freeValueCellIndex;

            return this.keys;
        }

        public TValue[] GetValuesArray(out uint count)
        {
            count = this.freeValueCellIndex;

            return this.values;
        }

        public void Add(TKey key, TValue value)
        {
            if (AddValue(key, value, out _) == false)
                throw new ArrayDictionaryException("Key already present");
        }

        public void Add(TKey key, in TValue value)
        {
            if (AddValue(key, in value, out _) == false)
                throw new ArrayDictionaryException("Key already present");
        }

        public void Add(in TKey key, TValue value)
        {
            if (AddValue(in key, value, out _) == false)
                throw new ArrayDictionaryException("Key already present");
        }

        public void Add(in TKey key, in TValue value)
        {
            if (AddValue(in key, in value, out _) == false)
                throw new ArrayDictionaryException("Key already present");
        }

        public void Set(TKey key, TValue value)
            => AddValue(key, value, out _);

        public void Set(TKey key, in TValue value)
            => AddValue(key, in value, out _);

        public void Set(in TKey key, TValue value)
            => AddValue(in key, value, out _);

        public void Set(in TKey key, in TValue value)
            => AddValue(in key, in value, out _);

        public void Clear()
        {
            if (this.freeValueCellIndex == 0) return;

            this.freeValueCellIndex = 0;

            Array.Clear(this.buckets, 0, this.buckets.Length);
            Array.Clear(this.values, 0, this.values.Length);
            Array.Clear(this.keys, 0, this.keys.Length);
        }

        public void ShallowClear()
        {
            if (this.freeValueCellIndex == 0) return;

            this.freeValueCellIndex = 0;

            Array.Clear(this.buckets, 0, this.buckets.Length);
            Array.Clear(this.keys, 0, this.keys.Length);
        }

        public bool ContainsValue(TValue value)
        {
            if (value == null)
                return false;

            var comparer = EqualityComparerIn<TValue>.Default;

            for (var i = 0u; i < this.freeValueCellIndex; i++)
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

            for (var i = 0u; i < this.freeValueCellIndex; i++)
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

            var comparer = EqualityComparerIn<TValue>.Default;

            for (var i = 0u; i < this.freeValueCellIndex; i++)
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

            for (var i = 0u; i < this.freeValueCellIndex; i++)
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
            if (TryGetIndex(key, out var findIndex) == true)
            {
                result = this.values[findIndex];
                return true;
            }

            result = default;
            return false;
        }

        public bool TryGetValue(in TKey key, out TValue result)
        {
            if (TryGetIndex(in key, out var findIndex) == true)
            {
                result = this.values[findIndex];
                return true;
            }

            result = default;
            return false;
        }

        public void GetAt(uint index, out TKey key, out TValue value)
        {
            if (index >= this.freeValueCellIndex)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            key = this.keys[index];
            value = this.values[index];
        }

        public bool TryGetAt(uint index, out TKey key, out TValue value)
        {
            if (index >= this.freeValueCellIndex)
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
            => new Enumerator(this);

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        // TODO: can be optimized
        public ref TValue GetOrCreate(TKey key)
        {
            if (TryGetIndex(key, out var findIndex) == true)
            {
                return ref this.values[findIndex];
            }

            AddValue(key, default, out findIndex);

            return ref this.values[findIndex];
        }

        public ref TValue GetOrCreate(TKey key, Func<TValue> builder)
        {
            if (TryGetIndex(key, out var findIndex) == true)
            {
                return ref this.values[findIndex];
            }

            AddValue(key, builder(), out findIndex);

            return ref this.values[findIndex];
        }

        public ref TValue GetOrCreate<T>(TKey key, RefFunc<T, TValue> builder, ref T parameter)
        {
            if (TryGetIndex(key, out var findIndex) == true)
            {
                return ref this.values[findIndex];
            }

            AddValue(key, builder(ref parameter), out findIndex);

            return ref this.values[findIndex];
        }

        public ref TValue GetOrCreate(in TKey key)
        {
            if (TryGetIndex(key, out var findIndex) == true)
            {
                return ref this.values[findIndex];
            }

            AddValue(in key, default, out findIndex);

            return ref this.values[findIndex];
        }

        public ref TValue GetOrCreate(in TKey key, Func<TValue> builder)
        {
            if (TryGetIndex(key, out var findIndex) == true)
            {
                return ref this.values[findIndex];
            }

            AddValue(in key, builder(), out findIndex);

            return ref this.values[findIndex];
        }

        public ref TValue GetOrCreate<T>(in TKey key, RefFunc<T, TValue> builder, ref T parameter)
        {
            if (TryGetIndex(key, out var findIndex) == true)
            {
                return ref this.values[findIndex];
            }

            AddValue(in key, builder(ref parameter), out findIndex);

            return ref this.values[findIndex];
        }

        public ref TValue GetValueByRef(TKey key)
        {
            if (TryGetIndex(key, out var findIndex) == true)
            {
                return ref this.values[findIndex];
            }

            throw new ArrayDictionaryException("Key not found");
        }

        public ref TValue GetValueByRef(in TKey key)
        {
            if (TryGetIndex(in key, out var findIndex) == true)
            {
                return ref this.values[findIndex];
            }

            throw new ArrayDictionaryException("Key not found");
        }

        public void SetCapacity(int size)
        {
            if (this.values.Length < size)
            {
                Array.Resize(ref this.values, size);
                Array.Resize(ref this.keys, size);
            }
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

        public void Trim()
        {
            Array.Resize(ref this.values, (int)Math.Max(this.freeValueCellIndex, 1));
            Array.Resize(ref this.keys, (int)Math.Max(this.freeValueCellIndex, 1));
        }

        private bool AddValue(TKey key, TValue value, out uint indexSet)
        {
            var hash = key.GetHashCode();
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            if (this.freeValueCellIndex == this.values.Length)
            {
                var expandPrime = HashHelpers.ExpandPrime((int)this.freeValueCellIndex);

                Array.Resize(ref this.values, expandPrime);
                Array.Resize(ref this.keys, expandPrime);
            }

            //buckets value -1 means it's empty
            var valueIndex = this.buckets[bucketIndex] - 1;

            if (valueIndex == -1)
                //create the info node at the last position and fill it with the relevant information
                this.keys[this.freeValueCellIndex] = new Node(key, hash);
            else //collision or already exists
            {
                var currentValueIndex = valueIndex;
                do
                {
                    //must check if the key already exists in the dictionary
                    //for some reason this is faster than using Comparer<TKey>.default, should investigate
                    ref var node = ref this.keys[currentValueIndex];
                    if (node.Hash == hash && this.comparer.Equals(node.Key, key) == true)
                    {
                        //the key already exists, simply replace the value!
                        this.values[currentValueIndex] = value;
                        indexSet = (uint)currentValueIndex;
                        return false;
                    }

                    currentValueIndex = node.Previous;
                } while (currentValueIndex != -1); //-1 means no more values with key with the same hash

                //oops collision!
                this.collisions++;
                //create a new node which previous index points to node currently pointed in the bucket
                this.keys[this.freeValueCellIndex] = new Node(key, hash, valueIndex);
                //update the next of the existing cell to point to the new one
                //old one -> new one | old one <- next one
                this.keys[valueIndex].Next = (int)this.freeValueCellIndex;
                //Important: the new node is always the one that will be pointed by the bucket cell
                //so I can assume that the one pointed by the bucket is always the last value added
                //(next = -1)
            }

            //item with this bucketIndex will point to the last value created
            //TODO: if instead I assume that the original one is the one in the bucket
            //I wouldn't need to update the bucket here. Small optimization but important
            this.buckets[bucketIndex] = (int)(this.freeValueCellIndex + 1);

            this.values[this.freeValueCellIndex] = value;
            indexSet = this.freeValueCellIndex;

            this.freeValueCellIndex++;

            //too many collisions?
            if (this.collisions > this.buckets.Length)
            {
                //we need more space and less collisions
                this.buckets = new int[HashHelpers.ExpandPrime((int)this.collisions)];

                this.collisions = 0;

                //we need to get all the hash code of all the values stored so far and spread them over the new bucket
                //length
                for (var newValueIndex = 0; newValueIndex < this.freeValueCellIndex; newValueIndex++)
                {
                    //get the original hash code and find the new bucketIndex due to the new length
                    ref var node = ref this.keys[newValueIndex];
                    bucketIndex = Reduce((uint)node.Hash, (uint)this.buckets.Length);
                    //bucketsIndex can be -1 or a next value. If it's -1 means no collisions. If there is collision,
                    //we create a new node which prev points to the old one. Old one next points to the new one.
                    //the bucket will now points to the new one
                    //In this way we can rebuild the linkedlist.
                    //get the current valueIndex, it's -1 if no collision happens
                    var existingValueIndex = this.buckets[bucketIndex] - 1;
                    //update the bucket index to the index of the current item that share the bucketIndex
                    //(last found is always the one in the bucket)
                    this.buckets[bucketIndex] = newValueIndex + 1;
                    if (existingValueIndex != -1)
                    {
                        //oops a value was already being pointed by this cell in the new bucket list,
                        //it means there is a collision, problem
                        this.collisions++;
                        //the bucket will point to this value, so
                        //the previous index will be used as previous for the new value.
                        node.Previous = existingValueIndex;
                        node.Next = -1;
                        //and update the previous next index to the new one
                        this.keys[existingValueIndex].Next = newValueIndex;
                    }
                    else
                    {
                        //ok nothing was indexed, the bucket was empty. We need to update the previous
                        //values of next and previous
                        node.Next = -1;
                        node.Previous = -1;
                    }
                }
            }

            return true;
        }

        private bool AddValue(in TKey key, TValue value, out uint indexSet)
        {
            var hash = key.GetHashCode();
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            if (this.freeValueCellIndex == this.values.Length)
            {
                var expandPrime = HashHelpers.ExpandPrime((int)this.freeValueCellIndex);

                Array.Resize(ref this.values, expandPrime);
                Array.Resize(ref this.keys, expandPrime);
            }

            //buckets value -1 means it's empty
            var valueIndex = this.buckets[bucketIndex] - 1;

            if (valueIndex == -1)
                //create the info node at the last position and fill it with the relevant information
                this.keys[this.freeValueCellIndex] = new Node(in key, hash);
            else //collision or already exists
            {
                var currentValueIndex = valueIndex;
                do
                {
                    //must check if the key already exists in the dictionary
                    //for some reason this is faster than using Comparer<TKey>.default, should investigate
                    ref var node = ref this.keys[currentValueIndex];
                    if (node.Hash == hash && this.comparer.Equals(in node.Key, in key) == true)
                    {
                        //the key already exists, simply replace the value!
                        this.values[currentValueIndex] = value;
                        indexSet = (uint)currentValueIndex;
                        return false;
                    }

                    currentValueIndex = node.Previous;
                } while (currentValueIndex != -1); //-1 means no more values with key with the same hash

                //oops collision!
                this.collisions++;
                //create a new node which previous index points to node currently pointed in the bucket
                this.keys[this.freeValueCellIndex] = new Node(in key, hash, valueIndex);
                //update the next of the existing cell to point to the new one
                //old one -> new one | old one <- next one
                this.keys[valueIndex].Next = (int)this.freeValueCellIndex;
                //Important: the new node is always the one that will be pointed by the bucket cell
                //so I can assume that the one pointed by the bucket is always the last value added
                //(next = -1)
            }

            //item with this bucketIndex will point to the last value created
            //TODO: if instead I assume that the original one is the one in the bucket
            //I wouldn't need to update the bucket here. Small optimization but important
            this.buckets[bucketIndex] = (int)(this.freeValueCellIndex + 1);

            this.values[this.freeValueCellIndex] = value;
            indexSet = this.freeValueCellIndex;

            this.freeValueCellIndex++;

            //too many collisions?
            if (this.collisions > this.buckets.Length)
            {
                //we need more space and less collisions
                this.buckets = new int[HashHelpers.ExpandPrime((int)this.collisions)];

                this.collisions = 0;

                //we need to get all the hash code of all the values stored so far and spread them over the new bucket
                //length
                for (var newValueIndex = 0; newValueIndex < this.freeValueCellIndex; newValueIndex++)
                {
                    //get the original hash code and find the new bucketIndex due to the new length
                    ref var node = ref this.keys[newValueIndex];
                    bucketIndex = Reduce((uint)node.Hash, (uint)this.buckets.Length);
                    //bucketsIndex can be -1 or a next value. If it's -1 means no collisions. If there is collision,
                    //we create a new node which prev points to the old one. Old one next points to the new one.
                    //the bucket will now points to the new one
                    //In this way we can rebuild the linkedlist.
                    //get the current valueIndex, it's -1 if no collision happens
                    var existingValueIndex = this.buckets[bucketIndex] - 1;
                    //update the bucket index to the index of the current item that share the bucketIndex
                    //(last found is always the one in the bucket)
                    this.buckets[bucketIndex] = newValueIndex + 1;
                    if (existingValueIndex != -1)
                    {
                        //oops a value was already being pointed by this cell in the new bucket list,
                        //it means there is a collision, problem
                        this.collisions++;
                        //the bucket will point to this value, so
                        //the previous index will be used as previous for the new value.
                        node.Previous = existingValueIndex;
                        node.Next = -1;
                        //and update the previous next index to the new one
                        this.keys[existingValueIndex].Next = newValueIndex;
                    }
                    else
                    {
                        //ok nothing was indexed, the bucket was empty. We need to update the previous
                        //values of next and previous
                        node.Next = -1;
                        node.Previous = -1;
                    }
                }
            }

            return true;
        }

        private bool AddValue(TKey key, in TValue value, out uint indexSet)
        {
            var hash = key.GetHashCode();
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            if (this.freeValueCellIndex == this.values.Length)
            {
                var expandPrime = HashHelpers.ExpandPrime((int)this.freeValueCellIndex);

                Array.Resize(ref this.values, expandPrime);
                Array.Resize(ref this.keys, expandPrime);
            }

            //buckets value -1 means it's empty
            var valueIndex = this.buckets[bucketIndex] - 1;

            if (valueIndex == -1)
                //create the info node at the last position and fill it with the relevant information
                this.keys[this.freeValueCellIndex] = new Node(key, hash);
            else //collision or already exists
            {
                var currentValueIndex = valueIndex;
                do
                {
                    //must check if the key already exists in the dictionary
                    //for some reason this is faster than using Comparer<TKey>.default, should investigate
                    ref var node = ref this.keys[currentValueIndex];
                    if (node.Hash == hash && this.comparer.Equals(node.Key, key) == true)
                    {
                        //the key already exists, simply replace the value!
                        this.values[currentValueIndex] = value;
                        indexSet = (uint)currentValueIndex;
                        return false;
                    }

                    currentValueIndex = node.Previous;
                } while (currentValueIndex != -1); //-1 means no more values with key with the same hash

                //oops collision!
                this.collisions++;
                //create a new node which previous index points to node currently pointed in the bucket
                this.keys[this.freeValueCellIndex] = new Node(key, hash, valueIndex);
                //update the next of the existing cell to point to the new one
                //old one -> new one | old one <- next one
                this.keys[valueIndex].Next = (int)this.freeValueCellIndex;
                //Important: the new node is always the one that will be pointed by the bucket cell
                //so I can assume that the one pointed by the bucket is always the last value added
                //(next = -1)
            }

            //item with this bucketIndex will point to the last value created
            //TODO: if instead I assume that the original one is the one in the bucket
            //I wouldn't need to update the bucket here. Small optimization but important
            this.buckets[bucketIndex] = (int)(this.freeValueCellIndex + 1);

            this.values[this.freeValueCellIndex] = value;
            indexSet = this.freeValueCellIndex;

            this.freeValueCellIndex++;

            //too many collisions?
            if (this.collisions > this.buckets.Length)
            {
                //we need more space and less collisions
                this.buckets = new int[HashHelpers.ExpandPrime((int)this.collisions)];

                this.collisions = 0;

                //we need to get all the hash code of all the values stored so far and spread them over the new bucket
                //length
                for (var newValueIndex = 0; newValueIndex < this.freeValueCellIndex; newValueIndex++)
                {
                    //get the original hash code and find the new bucketIndex due to the new length
                    ref var node = ref this.keys[newValueIndex];
                    bucketIndex = Reduce((uint)node.Hash, (uint)this.buckets.Length);
                    //bucketsIndex can be -1 or a next value. If it's -1 means no collisions. If there is collision,
                    //we create a new node which prev points to the old one. Old one next points to the new one.
                    //the bucket will now points to the new one
                    //In this way we can rebuild the linkedlist.
                    //get the current valueIndex, it's -1 if no collision happens
                    var existingValueIndex = this.buckets[bucketIndex] - 1;
                    //update the bucket index to the index of the current item that share the bucketIndex
                    //(last found is always the one in the bucket)
                    this.buckets[bucketIndex] = newValueIndex + 1;
                    if (existingValueIndex != -1)
                    {
                        //oops a value was already being pointed by this cell in the new bucket list,
                        //it means there is a collision, problem
                        this.collisions++;
                        //the bucket will point to this value, so
                        //the previous index will be used as previous for the new value.
                        node.Previous = existingValueIndex;
                        node.Next = -1;
                        //and update the previous next index to the new one
                        this.keys[existingValueIndex].Next = newValueIndex;
                    }
                    else
                    {
                        //ok nothing was indexed, the bucket was empty. We need to update the previous
                        //values of next and previous
                        node.Next = -1;
                        node.Previous = -1;
                    }
                }
            }

            return true;
        }

        private bool AddValue(in TKey key, in TValue value, out uint indexSet)
        {
            var hash = key.GetHashCode();
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            if (this.freeValueCellIndex == this.values.Length)
            {
                var expandPrime = HashHelpers.ExpandPrime((int)this.freeValueCellIndex);

                Array.Resize(ref this.values, expandPrime);
                Array.Resize(ref this.keys, expandPrime);
            }

            //buckets value -1 means it's empty
            var valueIndex = this.buckets[bucketIndex] - 1;

            if (valueIndex == -1)
                //create the info node at the last position and fill it with the relevant information
                this.keys[this.freeValueCellIndex] = new Node(in key, hash);
            else //collision or already exists
            {
                var currentValueIndex = valueIndex;
                do
                {
                    //must check if the key already exists in the dictionary
                    //for some reason this is faster than using Comparer<TKey>.default, should investigate
                    ref var node = ref this.keys[currentValueIndex];
                    if (node.Hash == hash && this.comparer.Equals(in node.Key, in key) == true)
                    {
                        //the key already exists, simply replace the value!
                        this.values[currentValueIndex] = value;
                        indexSet = (uint)currentValueIndex;
                        return false;
                    }

                    currentValueIndex = node.Previous;
                } while (currentValueIndex != -1); //-1 means no more values with key with the same hash

                //oops collision!
                this.collisions++;
                //create a new node which previous index points to node currently pointed in the bucket
                this.keys[this.freeValueCellIndex] = new Node(in key, hash, valueIndex);
                //update the next of the existing cell to point to the new one
                //old one -> new one | old one <- next one
                this.keys[valueIndex].Next = (int)this.freeValueCellIndex;
                //Important: the new node is always the one that will be pointed by the bucket cell
                //so I can assume that the one pointed by the bucket is always the last value added
                //(next = -1)
            }

            //item with this bucketIndex will point to the last value created
            //TODO: if instead I assume that the original one is the one in the bucket
            //I wouldn't need to update the bucket here. Small optimization but important
            this.buckets[bucketIndex] = (int)(this.freeValueCellIndex + 1);

            this.values[this.freeValueCellIndex] = value;
            indexSet = this.freeValueCellIndex;

            this.freeValueCellIndex++;

            //too many collisions?
            if (this.collisions > this.buckets.Length)
            {
                //we need more space and less collisions
                this.buckets = new int[HashHelpers.ExpandPrime((int)this.collisions)];

                this.collisions = 0;

                //we need to get all the hash code of all the values stored so far and spread them over the new bucket
                //length
                for (var newValueIndex = 0; newValueIndex < this.freeValueCellIndex; newValueIndex++)
                {
                    //get the original hash code and find the new bucketIndex due to the new length
                    ref var node = ref this.keys[newValueIndex];
                    bucketIndex = Reduce((uint)node.Hash, (uint)this.buckets.Length);
                    //bucketsIndex can be -1 or a next value. If it's -1 means no collisions. If there is collision,
                    //we create a new node which prev points to the old one. Old one next points to the new one.
                    //the bucket will now points to the new one
                    //In this way we can rebuild the linkedlist.
                    //get the current valueIndex, it's -1 if no collision happens
                    var existingValueIndex = this.buckets[bucketIndex] - 1;
                    //update the bucket index to the index of the current item that share the bucketIndex
                    //(last found is always the one in the bucket)
                    this.buckets[bucketIndex] = newValueIndex + 1;
                    if (existingValueIndex != -1)
                    {
                        //oops a value was already being pointed by this cell in the new bucket list,
                        //it means there is a collision, problem
                        this.collisions++;
                        //the bucket will point to this value, so
                        //the previous index will be used as previous for the new value.
                        node.Previous = existingValueIndex;
                        node.Next = -1;
                        //and update the previous next index to the new one
                        this.keys[existingValueIndex].Next = newValueIndex;
                    }
                    else
                    {
                        //ok nothing was indexed, the bucket was empty. We need to update the previous
                        //values of next and previous
                        node.Next = -1;
                        node.Previous = -1;
                    }
                }
            }

            return true;
        }

        public bool Remove(TKey key)
        {
            var hash = key.GetHashCode();
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            //find the bucket
            var indexToValueToRemove = this.buckets[bucketIndex] - 1;

            //Part one: look for the actual key in the bucket list if found I update the bucket list so that it doesn't
            //point anymore to the cell to remove
            while (indexToValueToRemove != -1)
            {
                ref var node = ref this.keys[indexToValueToRemove];
                if (node.Hash == hash && this.comparer.Equals(node.Key, key) == true)
                {
                    //if the key is found and the bucket points directly to the node to remove
                    if (this.buckets[bucketIndex] - 1 == indexToValueToRemove)
                    {
                        if (node.Next != -1)
                            throw new InvalidOperationException("if the bucket points to the cell, next MUST NOT exists");

                        //the bucket will point to the previous cell. if a previous cell exists
                        //its next pointer must be updated!
                        //<--- iteration order
                        //                      B(ucket points always to the last one)
                        //   ------- ------- -------
                        //   |  1  | |  2  | |  3  | //bucket cannot have next, only previous
                        //   ------- ------- -------
                        //--> insert order
                        var value = node.Previous;
                        this.buckets[bucketIndex] = value + 1;
                    }
                    else if (node.Next == -1)
                        throw new InvalidOperationException("if the bucket points to another cell, next MUST exists");

                    UpdateLinkedList(indexToValueToRemove, this.keys);

                    break;
                }

                indexToValueToRemove = node.Previous;
            }

            if (indexToValueToRemove == -1)
                return false; //not found!

            this.freeValueCellIndex--; //one less value to iterate

            //Part two:
            //At this point nodes pointers and buckets are updated, but the _values array
            //still has got the value to delete. Remember the goal of this dictionary is to be able
            //to iterate over the values like an array, so the values array must always be up to date

            //if the cell to remove is the last one in the list, we can perform less operations (no swapping needed)
            //otherwise we want to move the last value cell over the value to remove
            if (indexToValueToRemove != this.freeValueCellIndex)
            {
                //we can move the last value of both arrays in place of the one to delete.
                //in order to do so, we need to be sure that the bucket pointer is updated.
                //first we find the index in the bucket list of the pointer that points to the cell
                //to move
                var movingBucketIndex =
                    Reduce((uint)this.keys[this.freeValueCellIndex].Hash, (uint)this.buckets.Length);

                //if the key is found and the bucket points directly to the node to remove
                //it must now point to the cell where it's going to be moved
                if (this.buckets[movingBucketIndex] - 1 == this.freeValueCellIndex)
                    this.buckets[movingBucketIndex] = indexToValueToRemove + 1;

                //otherwise it means that there was more than one key with the same hash (collision), so
                //we need to update the linked list and its pointers
                var next = this.keys[this.freeValueCellIndex].Next;
                var previous = this.keys[this.freeValueCellIndex].Previous;

                //they now point to the cell where the last value is moved into
                if (next != -1)
                    this.keys[next].Previous = indexToValueToRemove;
                if (previous != -1)
                    this.keys[previous].Next = indexToValueToRemove;

                //finally, actually move the values
                this.keys[indexToValueToRemove] = this.keys[this.freeValueCellIndex];
                this.values[indexToValueToRemove] = this.values[this.freeValueCellIndex];
            }

            return true;
        }

        public bool Remove(in TKey key)
        {
            var hash = key.GetHashCode();
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            //find the bucket
            var indexToValueToRemove = this.buckets[bucketIndex] - 1;

            //Part one: look for the actual key in the bucket list if found I update the bucket list so that it doesn't
            //point anymore to the cell to remove
            while (indexToValueToRemove != -1)
            {
                ref var node = ref this.keys[indexToValueToRemove];
                if (node.Hash == hash && this.comparer.Equals(in node.Key, in key) == true)
                {
                    //if the key is found and the bucket points directly to the node to remove
                    if (this.buckets[bucketIndex] - 1 == indexToValueToRemove)
                    {
                        if (node.Next != -1)
                            throw new InvalidOperationException("if the bucket points to the cell, next MUST NOT exists");

                        //the bucket will point to the previous cell. if a previous cell exists
                        //its next pointer must be updated!
                        //<--- iteration order
                        //                      B(ucket points always to the last one)
                        //   ------- ------- -------
                        //   |  1  | |  2  | |  3  | //bucket cannot have next, only previous
                        //   ------- ------- -------
                        //--> insert order
                        var value = node.Previous;
                        this.buckets[bucketIndex] = value + 1;
                    }
                    else if (node.Next == -1)
                        throw new InvalidOperationException("if the bucket points to another cell, next MUST exists");

                    UpdateLinkedList(indexToValueToRemove, this.keys);

                    break;
                }

                indexToValueToRemove = node.Previous;
            }

            if (indexToValueToRemove == -1)
                return false; //not found!

            this.freeValueCellIndex--; //one less value to iterate

            //Part two:
            //At this point nodes pointers and buckets are updated, but the _values array
            //still has got the value to delete. Remember the goal of this dictionary is to be able
            //to iterate over the values like an array, so the values array must always be up to date

            //if the cell to remove is the last one in the list, we can perform less operations (no swapping needed)
            //otherwise we want to move the last value cell over the value to remove
            if (indexToValueToRemove != this.freeValueCellIndex)
            {
                //we can move the last value of both arrays in place of the one to delete.
                //in order to do so, we need to be sure that the bucket pointer is updated.
                //first we find the index in the bucket list of the pointer that points to the cell
                //to move
                var movingBucketIndex =
                    Reduce((uint)this.keys[this.freeValueCellIndex].Hash, (uint)this.buckets.Length);

                //if the key is found and the bucket points directly to the node to remove
                //it must now point to the cell where it's going to be moved
                if (this.buckets[movingBucketIndex] - 1 == this.freeValueCellIndex)
                    this.buckets[movingBucketIndex] = indexToValueToRemove + 1;

                //otherwise it means that there was more than one key with the same hash (collision), so
                //we need to update the linked list and its pointers
                var next = this.keys[this.freeValueCellIndex].Next;
                var previous = this.keys[this.freeValueCellIndex].Previous;

                //they now point to the cell where the last value is moved into
                if (next != -1)
                    this.keys[next].Previous = indexToValueToRemove;
                if (previous != -1)
                    this.keys[previous].Next = indexToValueToRemove;

                //finally, actually move the values
                this.keys[indexToValueToRemove] = this.keys[this.freeValueCellIndex];
                this.values[indexToValueToRemove] = this.values[this.freeValueCellIndex];
            }

            return true;
        }

        //I store all the index with an offset + 1, so that in the bucket list 0 means actually not existing.
        //When read the offset must be offset by -1 again to be the real one. In this way
        //I avoid to initialize the array to -1
        public bool TryGetIndex(TKey key, out uint findIndex)
        {
            var hash = key.GetHashCode();
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            var valueIndex = this.buckets[bucketIndex] - 1;

            //even if we found an existing value we need to be sure it's the one we requested
            while (valueIndex != -1)
            {
                //for some reason this is way faster than using Comparer<TKey>.default, should investigate
                ref var node = ref this.keys[valueIndex];

                if (node.Hash == hash && this.comparer.Equals(node.Key, key) == true)
                {
                    //this is the one
                    findIndex = (uint)valueIndex;
                    return true;
                }

                valueIndex = node.Previous;
            }

            findIndex = 0;
            return false;
        }

        //I store all the index with an offset + 1, so that in the bucket list 0 means actually not existing.
        //When read the offset must be offset by -1 again to be the real one. In this way
        //I avoid to initialize the array to -1
        public bool TryGetIndex(in TKey key, out uint findIndex)
        {
            var hash = key.GetHashCode();
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            var valueIndex = this.buckets[bucketIndex] - 1;

            //even if we found an existing value we need to be sure it's the one we requested
            while (valueIndex != -1)
            {
                //for some reason this is way faster than using Comparer<TKey>.default, should investigate
                ref var node = ref this.keys[valueIndex];

                if (node.Hash == hash && this.comparer.Equals(in node.Key, in key) == true)
                {
                    //this is the one
                    findIndex = (uint)valueIndex;
                    return true;
                }

                valueIndex = node.Previous;
            }

            findIndex = 0;
            return false;
        }

        internal static readonly ArrayDictionary<TKey, TValue> Empty = new ArrayDictionary<TKey, TValue>();

        private static uint Reduce(uint x, uint N)
        {
            if (x >= N)
                return x % N;

            return x;
        }

        private static void UpdateLinkedList(int index, Node[] valuesInfo)
        {
            var next = valuesInfo[index].Next;
            var previous = valuesInfo[index].Previous;

            if (next != -1)
                valuesInfo[next].Previous = previous;
            if (previous != -1)
                valuesInfo[previous].Next = next;
        }

        public struct Node
        {
            public readonly TKey Key;
            public readonly int Hash;
            public int Previous;
            public int Next;

            public Node(TKey key, int hash, int previousNode)
            {
                this.Key = key;
                this.Hash = hash;
                this.Previous = previousNode;
                this.Next = -1;
            }

            public Node(TKey key, int hash)
            {
                this.Key = key;
                this.Hash = hash;
                this.Previous = -1;
                this.Next = -1;
            }

            public Node(in TKey key, int hash, int previousNode)
            {
                this.Key = key;
                this.Hash = hash;
                this.Previous = previousNode;
                this.Next = -1;
            }

            public Node(in TKey key, int hash)
            {
                this.Key = key;
                this.Hash = hash;
                this.Previous = -1;
                this.Next = -1;
            }

            public static implicit operator TKey(in Node node)
                => node.Key;
        }

        public readonly ref struct KeyValuePair
        {
            private readonly TKey key;
            private readonly TValue[] values;
            private readonly uint index;

            public TKey Key => this.key;

            public ref TValue Value => ref this.values[this.index];

            public KeyValuePair(TKey key, TValue[] values, uint index)
            {
                this.key = key;
                this.values = values;
                this.index = index;
            }

            public void Deconstruct(out TKey key, out TValue value)
            {
                key = this.key;
                value = this.values[this.index];
            }

            public static implicit operator KeyValuePair<TKey, TValue>(in KeyValuePair kv)
                => new KeyValuePair<TKey, TValue>(kv.Key, kv.Value);
        }

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

            public KeyValuePair Current
                => new KeyValuePair(this.source.UnsafeKeys[this.index], this.source.UnsafeValues, this.index);

            KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
                => new KeyValuePair<TKey, TValue>(this.source.UnsafeKeys[this.index], this.source.UnsafeValues[this.index]);

            object IEnumerator.Current
                => new KeyValuePair<TKey, TValue>(this.source.UnsafeKeys[this.index], this.source.UnsafeValues[this.index]);
        }
    }

    public class ArrayDictionaryException : Exception
    {
        public ArrayDictionaryException(string keyAlreadyExisting) : base(keyAlreadyExisting) { }
    }
}