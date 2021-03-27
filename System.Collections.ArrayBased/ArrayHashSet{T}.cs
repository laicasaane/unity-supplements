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
    public partial class ArrayHashSet<T> : IEnumerable<T>
    {
        private readonly IEqualityComparerIn<T> comparer;

        private Node[] buffer;
        private int[] buckets;
        private uint freeBufferIndex;
        private uint collisions;

        public IEqualityComparerIn<T> Comparer => this.comparer;

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.freeBufferIndex;
        }

        public uint Capacity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (uint)this.buffer.Length;
        }

        public Node[] UnsafeBuffer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.buffer;
        }

        internal int[] UnsafeBuckets
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.buckets;
        }

        public ArrayHashSet()
            : this(EqualityComparerIn<T>.Default)
        {
        }

        public ArrayHashSet(int capacity)
            : this((uint)capacity)
        { }

        public ArrayHashSet(uint capacity)
            : this(capacity, EqualityComparerIn<T>.Default)
        {
        }

        public ArrayHashSet(IEqualityComparerIn<T> comparer)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new Node[1];
            this.buckets = new int[3];
        }

        public ArrayHashSet(int capacity, IEqualityComparerIn<T> comparer)
            : this((uint)capacity, comparer)
        { }

        public ArrayHashSet(uint capacity, IEqualityComparerIn<T> comparer)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new Node[capacity];
            this.buckets = new int[HashHelpers.GetPrime((int)capacity)];
        }

        public ArrayHashSet(params T[] source)
            : this(EqualityComparerIn<T>.Default, source)
        {
        }

        public ArrayHashSet(IEqualityComparerIn<T> comparer, params T[] source)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new Node[source.Length];

            for (var i = 0; i < source.Length; i++)
            {
                Add(source[i]);
            }

            this.freeBufferIndex = (uint)source.Length;
        }

        public ArrayHashSet(T[] source, uint actualSize, IEqualityComparerIn<T> comparer = null)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new Node[actualSize];

            for (var i = 0; i < source.Length; i++)
            {
                Add(source[i]);
            }

            this.freeBufferIndex = actualSize;
        }

        public ArrayHashSet(ICollection<T> source, IEqualityComparerIn<T> comparer = null)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new Node[source.Count];

            foreach (var item in source)
            {
                Add(item);
            }

            this.freeBufferIndex = (uint)source.Count;
        }

        public ArrayHashSet(ICollection<T> source, int extraCapacity, IEqualityComparerIn<T> comparer = null)
            : this(source, (uint)extraCapacity, comparer)
        { }

        public ArrayHashSet(ICollection<T> source, uint extraCapacity, IEqualityComparerIn<T> comparer = null)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new Node[(uint)source.Count + extraCapacity];

            foreach (var item in source)
            {
                Add(item);
            }

            this.freeBufferIndex = (uint)source.Count;
        }

        public ArrayHashSet(ArrayHashSet<T> source, IEqualityComparerIn<T> comparer = null)
            : this(source, 0, comparer)
        { }

        public ArrayHashSet(ArrayHashSet<T> source, int extraCapacity, IEqualityComparerIn<T> comparer = null)
            : this(source, (uint)extraCapacity, comparer)
        { }

        public ArrayHashSet(ArrayHashSet<T> source, uint extraCapacity, IEqualityComparerIn<T> comparer = null)
        {
            this.comparer = comparer ?? source.comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new Node[source.freeBufferIndex + extraCapacity];

            for (var i = 0u; i < source.freeBufferIndex; i++)
            {
                Add(source[i]);
            }

            this.freeBufferIndex = source.freeBufferIndex;
        }

        public ref readonly T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= this.freeBufferIndex)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.buffer[(uint)index].Value;
            }
        }

        public ref readonly T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= this.freeBufferIndex)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.buffer[index].Value;
            }
        }

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

            for (var i = 0u; i < this.freeBufferIndex; i++)
            {
                array[arrayIndex++] = this.buffer[i].Value;
            }
        }

        public void Add(T item)
        {
            if (AddValue(item, out _) == false)
                throw new ArrayHashSetException("Key already present");
        }

        public void Add(in T item)
        {
            if (AddValue(in item, out _) == false)
                throw new ArrayHashSetException("Key already present");
        }

        public void Clear()
        {
            if (this.freeBufferIndex == 0) return;

            this.freeBufferIndex = 0;

            Array.Clear(this.buckets, 0, this.buckets.Length);
            Array.Clear(this.buffer, 0, this.buffer.Length);
        }

        public void ShallowClear()
        {
            if (this.freeBufferIndex == 0) return;

            this.freeBufferIndex = 0;

            Array.Clear(this.buckets, 0, this.buckets.Length);
        }

        public bool Contains(T item)
            => TryGetIndex(item, out _);

        public bool Contains(in T item)
            => TryGetIndex(in item, out _);

        public void GetAt(uint index, out T item)
        {
            if (index >= this.freeBufferIndex)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            item = this.buffer[index];
        }

        public bool TryGetAt(uint index, out T item)
        {
            if (index >= this.freeBufferIndex)
            {
                item = default;
                return false;
            }

            item = this.buffer[index];
            return true;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public void SetCapacity(uint size)
        {
            if (this.buffer.Length < size)
            {
                Array.Resize(ref this.buffer, (int)size);
            }
        }

        public uint GetIndex(T item)
        {
            if (TryGetIndex(item, out var findIndex)) return findIndex;

            throw new ArrayHashSetException("Item not found");
        }

        public uint GetIndex(in T item)
        {
            if (TryGetIndex(in item, out var findIndex)) return findIndex;

            throw new ArrayHashSetException("Item not found");
        }

        public void Trim()
        {
            Array.Resize(ref this.buffer, (int)Math.Max(this.freeBufferIndex, 1));
        }

        private bool AddValue(T item, out uint indexSet)
        {
            var hash = this.comparer.GetHashCode(item);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            if (this.freeBufferIndex == this.buffer.Length)
            {
                var expandPrime = HashHelpers.ExpandPrime((int)this.freeBufferIndex);

                Array.Resize(ref this.buffer, expandPrime);
            }

            //buckets value -1 means it's empty
            var bufferIndex = this.buckets[bucketIndex] - 1;

            if (bufferIndex == -1)
                //create the info node at the last position and fill it with the relevant information
                this.buffer[this.freeBufferIndex] = new Node(item, hash);
            else //collision or already exists
            {
                var currentValueIndex = bufferIndex;
                do
                {
                    //must check if the item already exists in the dictionary
                    ref var node = ref this.buffer[currentValueIndex];
                    if (node.Hash == hash && this.comparer.Equals(node.Value, item))
                    {
                        //the item already exists, simply return
                        indexSet = (uint)currentValueIndex;
                        return false;
                    }

                    currentValueIndex = node.Previous;
                } while (currentValueIndex != -1); //-1 means no more item with the same hash

                //oops collision!
                this.collisions++;
                //create a new node which previous index points to node currently pointed in the bucket
                this.buffer[this.freeBufferIndex] = new Node(item, hash, bufferIndex);
                //update the next of the existing cell to point to the new one
                //old one -> new one | old one <- next one
                this.buffer[bufferIndex].Next = (int)this.freeBufferIndex;
                //Important: the new node is always the one that will be pointed by the bucket cell
                //so I can assume that the one pointed by the bucket is always the last item added
                //(next = -1)
            }

            //item with this bucketIndex will point to the last value created
            //TODO: if instead I assume that the original one is the one in the bucket
            //I wouldn't need to update the bucket here. Small optimization but important
            this.buckets[bucketIndex] = (int)(this.freeBufferIndex + 1);

            indexSet = this.freeBufferIndex;

            this.freeBufferIndex++;

            //too many collisions?
            if (this.collisions > this.buckets.Length)
            {
                //we need more space and less collisions
                this.buckets = new int[HashHelpers.ExpandPrime((int)this.collisions)];
                this.collisions = 0;

                //we need to get all the hash code of all the items stored so far and spread them over the new bucket
                //length
                for (var newBufferIndex = 0; newBufferIndex < this.freeBufferIndex; newBufferIndex++)
                {
                    //get the original hash code and find the new bucketIndex due to the new length
                    ref var node = ref this.buffer[newBufferIndex];
                    bucketIndex = Reduce((uint)node.Hash, (uint)this.buckets.Length);
                    //bucketsIndex can be -1 or a next value. If it's -1 means no collisions. If there is collision,
                    //we create a new node which prev points to the old one. Old one next points to the new one.
                    //the bucket will now points to the new one
                    //In this way we can rebuild the linkedlist.
                    //get the current bufferIndex, it's -1 if no collision happens
                    var existingValueIndex = this.buckets[bucketIndex] - 1;
                    //update the bucket index to the index of the current item that share the bucketIndex
                    //(last found is always the one in the bucket)
                    this.buckets[bucketIndex] = newBufferIndex + 1;
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
                        this.buffer[existingValueIndex].Next = newBufferIndex;
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

        private bool AddValue(in T item, out uint indexSet)
        {
            var hash = this.comparer.GetHashCode(in item);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            if (this.freeBufferIndex == this.buffer.Length)
            {
                var expandPrime = HashHelpers.ExpandPrime((int)this.freeBufferIndex);

                Array.Resize(ref this.buffer, expandPrime);
            }

            //buckets value -1 means it's empty
            var bufferIndex = this.buckets[bucketIndex] - 1;

            if (bufferIndex == -1)
                //create the info node at the last position and fill it with the relevant information
                this.buffer[this.freeBufferIndex] = new Node(in item, hash);
            else //collision or already exists
            {
                var currentValueIndex = bufferIndex;
                do
                {
                    //must check if the item already exists in the dictionary
                    ref var node = ref this.buffer[currentValueIndex];
                    if (node.Hash == hash && this.comparer.Equals(in node.Value, in item))
                    {
                        //the item already exists, simply return
                        indexSet = (uint)currentValueIndex;
                        return false;
                    }

                    currentValueIndex = node.Previous;
                } while (currentValueIndex != -1); //-1 means no more item with the same hash

                //oops collision!
                this.collisions++;
                //create a new node which previous index points to node currently pointed in the bucket
                this.buffer[this.freeBufferIndex] = new Node(in item, hash, bufferIndex);
                //update the next of the existing cell to point to the new one
                //old one -> new one | old one <- next one
                this.buffer[bufferIndex].Next = (int)this.freeBufferIndex;
                //Important: the new node is always the one that will be pointed by the bucket cell
                //so I can assume that the one pointed by the bucket is always the last item added
                //(next = -1)
            }

            //item with this bucketIndex will point to the last value created
            //TODO: if instead I assume that the original one is the one in the bucket
            //I wouldn't need to update the bucket here. Small optimization but important
            this.buckets[bucketIndex] = (int)(this.freeBufferIndex + 1);

            indexSet = this.freeBufferIndex;

            this.freeBufferIndex++;

            //too many collisions?
            if (this.collisions > this.buckets.Length)
            {
                //we need more space and less collisions
                this.buckets = new int[HashHelpers.ExpandPrime((int)this.collisions)];
                this.collisions = 0;

                //we need to get all the hash code of all the items stored so far and spread them over the new bucket
                //length
                for (var newBufferIndex = 0; newBufferIndex < this.freeBufferIndex; newBufferIndex++)
                {
                    //get the original hash code and find the new bucketIndex due to the new length
                    ref var node = ref this.buffer[newBufferIndex];
                    bucketIndex = Reduce((uint)node.Hash, (uint)this.buckets.Length);
                    //bucketsIndex can be -1 or a next value. If it's -1 means no collisions. If there is collision,
                    //we create a new node which prev points to the old one. Old one next points to the new one.
                    //the bucket will now points to the new one
                    //In this way we can rebuild the linkedlist.
                    //get the current bufferIndex, it's -1 if no collision happens
                    var existingBufferIndex = this.buckets[bucketIndex] - 1;
                    //update the bucket index to the index of the current item that share the bucketIndex
                    //(last found is always the one in the bucket)
                    this.buckets[bucketIndex] = newBufferIndex + 1;
                    if (existingBufferIndex != -1)
                    {
                        //oops a item was already being pointed by this cell in the new bucket list,
                        //it means there is a collision, problem
                        this.collisions++;
                        //the bucket will point to this value, so
                        //the previous index will be used as previous for the new value.
                        node.Previous = existingBufferIndex;
                        node.Next = -1;
                        //and update the previous next index to the new one
                        this.buffer[existingBufferIndex].Next = newBufferIndex;
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

        public bool Remove(T item)
        {
            var hash = this.comparer.GetHashCode(item);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            //find the bucket
            var indexToValueToRemove = this.buckets[bucketIndex] - 1;

            //Part one: look for the actual item in the bucket list if found I update the bucket list so that it doesn't
            //point anymore to the cell to remove
            while (indexToValueToRemove != -1)
            {
                ref var node = ref this.buffer[indexToValueToRemove];
                if (node.Hash == hash && this.comparer.Equals(node.Value, item))
                {
                    //if the item is found and the bucket points directly to the node to remove
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

                    UpdateLinkedList(indexToValueToRemove, this.buffer);

                    break;
                }

                indexToValueToRemove = node.Previous;
            }

            if (indexToValueToRemove == -1)
                return false; //not found!

            this.freeBufferIndex--; //one less value to iterate

            //Part two:
            //At this point nodes pointers and buckets are updated, but the _values array
            //still has got the value to delete. Remember the goal of this dictionary is to be able
            //to iterate over the values like an array, so the values array must always be up to date

            //if the cell to remove is the last one in the list, we can perform less operations (no swapping needed)
            //otherwise we want to move the last value cell over the value to remove
            if (indexToValueToRemove != this.freeBufferIndex)
            {
                //we can move the last value of both arrays in place of the one to delete.
                //in order to do so, we need to be sure that the bucket pointer is updated.
                //first we find the index in the bucket list of the pointer that points to the cell
                //to move
                var movingBucketIndex =
                    Reduce((uint)this.buffer[this.freeBufferIndex].Hash, (uint)this.buckets.Length);

                //if the item is found and the bucket points directly to the node to remove
                //it must now point to the cell where it's going to be moved
                if (this.buckets[movingBucketIndex] - 1 == this.freeBufferIndex)
                    this.buckets[movingBucketIndex] = indexToValueToRemove + 1;

                //otherwise it means that there was more than one item with the same hash (collision), so
                //we need to update the linked list and its pointers
                var next = this.buffer[this.freeBufferIndex].Next;
                var previous = this.buffer[this.freeBufferIndex].Previous;

                //they now point to the cell where the last value is moved into
                if (next != -1)
                    this.buffer[next].Previous = indexToValueToRemove;
                if (previous != -1)
                    this.buffer[previous].Next = indexToValueToRemove;

                //finally, actually move the values
                this.buffer[indexToValueToRemove] = this.buffer[this.freeBufferIndex];
            }

            return true;
        }

        public bool Remove(in T item)
        {
            var hash = this.comparer.GetHashCode(in item);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);

            //find the bucket
            var indexToValueToRemove = this.buckets[bucketIndex] - 1;

            //Part one: look for the actual item in the bucket list if found I update the bucket list so that it doesn't
            //point anymore to the cell to remove
            while (indexToValueToRemove != -1)
            {
                ref var node = ref this.buffer[indexToValueToRemove];
                if (node.Hash == hash &&
                    this.comparer.Equals(node.Value, item))
                {
                    //if the item is found and the bucket points directly to the node to remove
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

                    UpdateLinkedList(indexToValueToRemove, this.buffer);

                    break;
                }

                indexToValueToRemove = node.Previous;
            }

            if (indexToValueToRemove == -1)
                return false; //not found!

            this.freeBufferIndex--; //one less value to iterate

            //Part two:
            //At this point nodes pointers and buckets are updated, but the _values array
            //still has got the value to delete. Remember the goal of this dictionary is to be able
            //to iterate over the values like an array, so the values array must always be up to date

            //if the cell to remove is the last one in the list, we can perform less operations (no swapping needed)
            //otherwise we want to move the last value cell over the value to remove
            if (indexToValueToRemove != this.freeBufferIndex)
            {
                //we can move the last value of both arrays in place of the one to delete.
                //in order to do so, we need to be sure that the bucket pointer is updated.
                //first we find the index in the bucket list of the pointer that points to the cell
                //to move
                var movingBucketIndex =
                    Reduce((uint)this.buffer[this.freeBufferIndex].Hash, (uint)this.buckets.Length);

                //if the item is found and the bucket points directly to the node to remove
                //it must now point to the cell where it's going to be moved
                if (this.buckets[movingBucketIndex] - 1 == this.freeBufferIndex)
                    this.buckets[movingBucketIndex] = indexToValueToRemove + 1;

                //otherwise it means that there was more than one item with the same hash (collision), so
                //we need to update the linked list and its pointers
                var next = this.buffer[this.freeBufferIndex].Next;
                var previous = this.buffer[this.freeBufferIndex].Previous;

                //they now point to the cell where the last value is moved into
                if (next != -1)
                    this.buffer[next].Previous = indexToValueToRemove;
                if (previous != -1)
                    this.buffer[previous].Next = indexToValueToRemove;

                //finally, actually move the values
                this.buffer[indexToValueToRemove] = this.buffer[this.freeBufferIndex];
            }

            return true;
        }

        //I store all the index with an offset + 1, so that in the bucket list 0 means actually not existing.
        //When read the offset must be offset by -1 again to be the real one. In this way
        //I avoid to initialize the array to -1
        public bool TryGetIndex(T item, out uint findIndex)
        {
            var hash = this.comparer.GetHashCode(item);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);
            var bufferIndex = this.buckets[bucketIndex] - 1;

            //even if we found an existing value we need to be sure it's the one we requested
            while (bufferIndex != -1)
            {
                //for some reason this is way faster than using Comparer<T>.default, should investigate
                ref var node = ref this.buffer[bufferIndex];

                if (node.Hash == hash && this.comparer.Equals(node.Value, item))
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
            var hash = this.comparer.GetHashCode(in item);
            var bucketIndex = Reduce((uint)hash, (uint)this.buckets.Length);
            var bufferIndex = this.buckets[bucketIndex] - 1;

            //even if we found an existing value we need to be sure it's the one we requested
            while (bufferIndex != -1)
            {
                //for some reason this is way faster than using Comparer<T>.default, should investigate
                ref var node = ref this.buffer[bufferIndex];

                if (node.Hash == hash && this.comparer.Equals(node.Value, item))
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

        private static void UpdateLinkedList(int index, Node[] valuesInfo)
        {
            var next = valuesInfo[index].Next;
            var previous = valuesInfo[index].Previous;

            if (next != -1)
                valuesInfo[next].Previous = previous;

            if (previous != -1)
                valuesInfo[previous].Next = next;
        }

        internal static readonly ArrayHashSet<T> Empty = new ArrayHashSet<T>();

        public struct Node
        {
            public readonly T Value;
            public readonly int Hash;
            public int Previous;
            public int Next;

            public Node(T value, int hash, int previousNode)
            {
                this.Value = value;
                this.Hash = hash;
                this.Previous = previousNode;
                this.Next = -1;
            }

            public Node(T value, int hash)
            {
                this.Value = value;
                this.Hash = hash;
                this.Previous = -1;
                this.Next = -1;
            }

            public Node(in T value, int hash, int previousNode)
            {
                this.Value = value;
                this.Hash = hash;
                this.Previous = previousNode;
                this.Next = -1;
            }

            public Node(in T value, int hash)
            {
                this.Value = value;
                this.Hash = hash;
                this.Previous = -1;
                this.Next = -1;
            }

            public static implicit operator T(in Node node)
                => node.Value;
        }

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

    public class ArrayHashSetException : Exception
    {
        public ArrayHashSetException(string itemAlreadyExisting) : base(itemAlreadyExisting) { }
    }
}