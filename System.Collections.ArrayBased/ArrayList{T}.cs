// Based on
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterList.cs
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterListEnumerator.cs

using System.Collections.Generic;
using System.Diagnostics;
using System.Helpers;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public partial class ArrayList<T> : IEnumerable<T>
    {
        private readonly IEqualityComparerIn<T> comparer;

        private T[] buffer;
        private uint count;

        public IEqualityComparerIn<T> Comparer => this.comparer;

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.count;
        }

        public uint Capacity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (uint)this.buffer.Length;
        }

        public T[] UnsafeBuffer
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.buffer;
        }

        public ArrayList()
            : this(EqualityComparerIn<T>.Default)
        {
        }

        public ArrayList(IEqualityComparerIn<T> comparer)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.count = 0;
            this.buffer = new T[0];
        }

        public ArrayList(int capacity)
            : this((uint)capacity)
        { }

        public ArrayList(uint capacity)
            : this(capacity, EqualityComparerIn<T>.Default)
        {
        }

        public ArrayList(int capacity, IEqualityComparerIn<T> comparer)
            : this((uint)capacity, comparer)
        {
        }

        public ArrayList(uint capacity, IEqualityComparerIn<T> comparer)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.count = 0;
            this.buffer = new T[capacity];
        }

        public ArrayList(params T[] source)
            : this(EqualityComparerIn<T>.Default, source)
        {
        }

        public ArrayList(IEqualityComparerIn<T> comparer, params T[] source)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new T[source.Length];
            Array.Copy(source, this.buffer, source.Length);

            this.count = (uint)source.Length;
        }

        public ArrayList(T[] source, uint actualSize, IEqualityComparerIn<T> comparer = null)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new T[actualSize];
            Array.Copy(source, this.buffer, actualSize);

            this.count = actualSize;
        }

        public ArrayList(ICollection<T> source, IEqualityComparerIn<T> comparer = null)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new T[source.Count];

            source.CopyTo(this.buffer, 0);

            this.count = (uint)source.Count;
        }

        public ArrayList(ICollection<T> source, int extraCapacity, IEqualityComparerIn<T> comparer = null)
            : this(source, (uint)extraCapacity, comparer)
        { }

        public ArrayList(ICollection<T> source, uint extraCapacity, IEqualityComparerIn<T> comparer = null)
        {
            this.comparer = comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new T[(uint)source.Count + extraCapacity];
            source.CopyTo(this.buffer, 0);

            this.count = (uint)source.Count;
        }

        public ArrayList(ArrayList<T> source, IEqualityComparerIn<T> comparer = null)
            : this(source, 0, comparer)
        { }

        public ArrayList(ArrayList<T> source, int extraCapacity, IEqualityComparerIn<T> comparer = null)
            : this(source, (uint)extraCapacity, comparer)
        { }

        public ArrayList(ArrayList<T> source, uint extraCapacity, IEqualityComparerIn<T> comparer = null)
        {
            this.comparer = comparer ?? source.comparer ?? EqualityComparerIn<T>.Default;
            this.buffer = new T[source.count + extraCapacity];
            source.CopyTo(this.buffer, 0);

            this.count = source.count;
        }

        public ArrayList(in ReadArrayList<T> source, IEqualityComparerIn<T> comparer = null)
            : this(source, 0, comparer)
        { }

        public ArrayList(in ReadArrayList<T> source, int extraCapacity, IEqualityComparerIn<T> comparer = null)
            : this(source, (uint)extraCapacity, comparer)
        { }

        public ArrayList(in ReadArrayList<T> source, uint extraCapacity, IEqualityComparerIn<T> comparer = null)
            : this(source.GetSource(), extraCapacity, comparer)
        { }

        public ref T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if ((uint)index >= this.count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.buffer[(uint)index];
            }
        }

        public ref T this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (index >= this.count)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return ref this.buffer[index];
            }
        }

        public void Add(T item)
        {
            if (this.count == this.buffer.Length)
                AllocateMore();

            this.buffer[this.count++] = item;
        }

        public void Add(in T item)
        {
            if (this.count == this.buffer.Length)
                AllocateMore();

            this.buffer[this.count++] = item;
        }

        public void AddAt(uint location, T item)
        {
            ExpandTo(location + 1);

            this.buffer[location] = item;
        }

        public void AddAt(uint location, in T item)
        {
            ExpandTo(location + 1);

            this.buffer[location] = item;
        }

        public void AddRange(ArrayList<T> items)
            => AddRange(items.buffer, items.count);

        public void AddRange(in ReadArrayList<T> items)
            => AddRange(items.GetSource());

        public void AddRange(params T[] items)
            => AddRange(items, (uint)items.Length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(T[] items, uint count)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (count == 0)
                return;

            if (this.count + count > this.buffer.Length)
                AllocateMore(this.count + count);

            Array.Copy(items, 0, this.buffer, this.count, count);
            this.count += count;
        }

        public bool Contains(T item)
        {
            for (var index = 0u; index < this.count; index++)
                if (this.comparer.Equals(this.buffer[index], item))
                    return true;

            return false;
        }

        public bool Contains(in T item)
        {
            for (var index = 0u; index < this.count; index++)
                if (this.comparer.Equals(in this.buffer[index], in item))
                    return true;

            return false;
        }

        public uint? IndexOf(T item)
        {
            for (var index = 0u; index < this.count; index++)
                if (this.comparer.Equals(this.buffer[index], item))
                    return index;

            return null;
        }

        public uint? IndexOf(in T item)
        {
            for (var index = 0u; index < this.count; index++)
                if (this.comparer.Equals(in this.buffer[index], in item))
                    return index;

            return null;
        }

        /// <summary>
        /// This method does not actually clear the list, thus objects held by this list won't be garbage collected.
        /// Use <see cref="ResetToReuse"/> or <see cref="Clear"/> to clear the entire list completely.
        /// </summary>
        public void ShallowClear()
        {
#if DEBUG
            if (TypeCache<T>.Type.IsClass)
                Debug.Print("Warning: objects held by this list won't be garbage collected. Use ResetToReuse or Clear to avoid this warning");
#endif
            this.count = 0;
        }

        public void ResetToReuse()
        {
            this.count = 0;
        }

        public bool ReuseOneSlot<U>(out U result) where U : T
        {
            if (this.count >= this.buffer.Length)
            {
                result = default;
                return false;
            }

            if (default(U) == null)
            {
                result = (U)this.buffer[this.count];

                if (result != null)
                {
                    this.count++;
                    return true;
                }

                return false;
            }

            this.count++;

            result = default;
            return true;
        }

        public bool ReuseOneSlot<U>() where U : T
        {
            if (this.count >= this.buffer.Length)
                return false;

            this.count++;

            return true;
        }

        public bool ReuseOneSlot()
        {
            if (this.count >= this.buffer.Length)
                return false;

            this.count++;

            return true;
        }

        public void Clear()
        {
            Array.Clear(this.buffer, 0, this.buffer.Length);

            this.count = 0;
        }

        public void Insert(int index, T item)
        {
            if ((uint)index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == this.buffer.Length) AllocateMore();

            Array.Copy(this.buffer, index, this.buffer, index + 1, this.count - index);
            ++this.count;

            this.buffer[index] = item;
        }

        public void Insert(int index, in T item)
        {
            if ((uint)index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == this.buffer.Length) AllocateMore();

            Array.Copy(this.buffer, index, this.buffer, index + 1, this.count - index);
            ++this.count;

            this.buffer[index] = item;
        }

        public void Insert(uint index, T item)
        {
            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == this.buffer.Length) AllocateMore();

            Array.Copy(this.buffer, index, this.buffer, index + 1, this.count - index);
            ++this.count;

            this.buffer[index] = item;
        }

        public void Insert(uint index, in T item)
        {
            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == this.buffer.Length) AllocateMore();

            Array.Copy(this.buffer, index, this.buffer, index + 1, this.count - index);
            ++this.count;

            this.buffer[index] = item;
        }

        public bool Remove(T item)
        {
            var indexTemp = IndexOf(item);

            if (!indexTemp.HasValue)
                return false;

            var index = indexTemp.Value;

            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (index == --this.count)
                return false;

            Array.Copy(this.buffer, index + 1, this.buffer, index, this.count - index);

            this.buffer[this.count] = default;
            return true;
        }

        public bool Remove(in T item)
        {
            var indexTemp = IndexOf(in item);

            if (!indexTemp.HasValue)
                return false;

            var index = indexTemp.Value;

            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (index == --this.count)
                return false;

            Array.Copy(this.buffer, index + 1, this.buffer, index, this.count - index);

            this.buffer[this.count] = default;
            return true;
        }

        public void RemoveAt(int index)
        {
            if ((uint)index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (index == --this.count)
                return;

            Array.Copy(this.buffer, index + 1, this.buffer, index, this.count - index);

            this.buffer[this.count] = default;
        }

        public void RemoveAt(uint index)
        {
            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (index == --this.count)
                return;

            Array.Copy(this.buffer, index + 1, this.buffer, index, this.count - index);

            this.buffer[this.count] = default;
        }

        public void Resize(uint newSize)
        {
            if (newSize == this.buffer.Length) return;

            Array.Resize(ref this.buffer, (int)newSize);
        }

        public T[] ToArray()
        {
            var destinationArray = new T[this.count];

            Array.Copy(this.buffer, 0, destinationArray, 0, this.count);

            return destinationArray;
        }

        /// <summary>
        /// Note: The length of the returned array cannot be used. Use the count argument instead.
        /// </summary>
        public T[] GetBufferArray(out uint count)
        {
            count = this.count;
            return this.buffer;
        }

        public bool UnorderedRemoveAt(int index)
        {
            if ((uint)index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (index == --this.count)
            {
                this.buffer[this.count] = default;
                return false;
            }

            this.buffer[(uint)index] = this.buffer[this.count];
            this.buffer[this.count] = default;

            return true;
        }

        public bool UnorderedRemoveAt(uint index)
        {
            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (index == --this.count)
            {
                this.buffer[this.count] = default;
                return false;
            }

            this.buffer[index] = this.buffer[this.count];
            this.buffer[this.count] = default;

            return true;
        }

        public void Trim()
        {
            if (this.count < this.buffer.Length)
                Resize(this.count);
        }

        public void TrimCount(uint newCount)
        {
            if (newCount > this.count)
                throw new ArgumentException($"{nameof(newCount)} must be lesser than the list count");

            this.count = newCount;
        }

        public void ExpandBy(uint increment)
        {
            var count = this.count + increment;

            if (this.buffer.Length < count)
                AllocateMore(count);

            this.count = count;
        }

        public void ExpandTo(uint newSize)
        {
            if (this.buffer.Length < newSize)
                AllocateMore(newSize);

            if (this.count < newSize)
                this.count = newSize;
        }

        public void EnsureCapacity(uint newSize)
        {
            if (this.buffer.Length < newSize)
                AllocateMore(newSize);
        }

        public uint Push(in T item)
        {
            AddAt(this.count, item);
            return this.count - 1;
        }

        public ref readonly T Pop()
        {
            --this.count;
            return ref this.buffer[this.count];
        }

        public ref readonly T Peek()
        {
            if (this.count == 0)
                throw ThrowHelper.GetArgumentOutOfRange_CountException();

            return ref this.buffer[this.count - 1];
        }

        public void CopyTo(T[] array, int arrayIndex)
            => Array.Copy(this.buffer, 0, array, arrayIndex, this.count);

        public void CopyTo(T[] array, uint arrayIndex)
            => Array.Copy(this.buffer, 0, array, arrayIndex, this.count);

        private void AllocateMore()
        {
            var newLength = (uint)((this.buffer.Length + 1) * 1.5f);
            var newList = new T[newLength];
            if (this.count > 0) Array.Copy(this.buffer, newList, this.count);
            this.buffer = newList;
        }

        private void AllocateMore(uint newSize)
        {
            if (newSize <= this.buffer.Length)
                return;

            var newLength = (uint)(newSize * 1.5f);
            var newList = new T[newLength];
            if (this.count > 0) Array.Copy(this.buffer, newList, this.count);
            this.buffer = newList;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this.buffer, this.count);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        internal static readonly ArrayList<T> Empty = new ArrayList<T>();

        public static explicit operator ArrayList<T>(T[] array)
            => new ArrayList<T>(array);

        public struct Enumerator : IEnumerator<T>
        {
            private readonly T[] buffer;
            private readonly uint count;

            private uint index;

            public T Current => this.buffer[this.index - 1];

            object IEnumerator.Current => this.Current;

            public Enumerator(T[] buffer, uint count)
            {
                this.buffer = buffer;
                this.count = count;
                this.index = 0;
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
