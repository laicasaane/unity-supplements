// Based on
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterList.cs
// https://github.com/sebas77/Svelto.Common/blob/master/DataStructures/Arrays/FasterListEnumerator.cs

using System.Collections.Generic;
using System.Diagnostics;
using System.Helpers;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Collections.ArrayBased
{
    [Serializable]
    public class ArrayList<T> : ISerializable
    {
        private T[] buffer;
        private uint count;

        public uint Count => this.count;

        public uint Capacity => (uint)this.buffer.Length;

        public ArrayList()
        {
            this.count = 0;
            this.buffer = new T[0];
        }

        public ArrayList(uint initialSize)
        {
            this.count = 0;
            this.buffer = new T[initialSize];
        }

        public ArrayList(int initialSize) : this((uint)initialSize)
        { }

        public ArrayList(params T[] collection)
        {
            this.buffer = new T[collection.Length];
            Array.Copy(collection, this.buffer, collection.Length);

            this.count = (uint)collection.Length;
        }

        public ArrayList(T[] collection, uint actualSize)
        {
            this.buffer = new T[actualSize];
            Array.Copy(collection, this.buffer, actualSize);

            this.count = actualSize;
        }

        public ArrayList(ICollection<T> collection)
        {
            this.buffer = new T[collection.Count];

            collection.CopyTo(this.buffer, 0);

            this.count = (uint)collection.Count;
        }

        public ArrayList(ICollection<T> collection, int extraSize)
        {
            this.buffer = new T[(uint)collection.Count + (uint)extraSize];
            collection.CopyTo(this.buffer, 0);

            this.count = (uint)collection.Count;
        }

        public ArrayList(in ArrayList<T> source)
        {
            this.buffer = new T[source.count];
            source.CopyTo(this.buffer, 0);

            this.count = source.count;
        }

        public ArrayList(in ReadArrayList<T> source)
        {
            this.buffer = new T[source.Count];
            source.CopyTo(this.buffer, 0);

            this.count = source.Count;
        }

        protected ArrayList(SerializationInfo info, StreamingContext context)
        {
            var buffer = info.GetValueOrDefault<T[]>(nameof(this.buffer));

            if (buffer == null)
            {
                this.count = 0;
                this.buffer = new T[0];
                return;
            }

            this.count = (uint)buffer.Length;
            this.buffer = buffer;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.buffer), this.buffer);
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayList<T> Add(T item)
        {
            if (this.count == this.buffer.Length)
                AllocateMore();

            this.buffer[this.count++] = item;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayList<T> Add(in T item)
        {
            if (this.count == this.buffer.Length)
                AllocateMore();

            this.buffer[this.count++] = item;

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddAt(uint location, T item)
        {
            ExpandTo(location + 1);

            this.buffer[location] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddAt(uint location, in T item)
        {
            ExpandTo(location + 1);

            this.buffer[location] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayList<T> AddRange(in ArrayList<T> items)
        {
            AddRange(items.buffer, items.count);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayList<T> AddRange(in ReadArrayList<T> items)
        {
            AddRange(items.GetSource().buffer, items.Count);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(T[] items, uint count)
        {
            if (count == 0) return;

            if (this.count + count > this.buffer.Length)
                AllocateMore(this.count + count);

            Array.Copy(items, 0, this.buffer, this.count, count);
            this.count += count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddRange(T[] items)
        {
            AddRange(items, (uint)items.Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
        {
            var comp = EqualityComparer<T>.Default;

            for (uint index = 0; index < this.count; index++)
                if (comp.Equals(this.buffer[index], item))
                    return true;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(in T item)
        {
            var comp = EqualityComparer<T>.Default;

            for (uint index = 0; index < this.count; index++)
                if (comp.Equals(this.buffer[index], item))
                    return true;

            return false;
        }

        /// <summary>
        /// This method does not actually clear the list, thus objects held by this list won't be garbage collected.
        /// Use <see cref="ResetToReuse"/> or <see cref="Clear"/> to clear the entire list completely.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FastClear()
        {
#if DEBUG
            if (TypeCache<T>.Type.IsClass)
                Debug.Print("Warning: objects held by this list won't be garbage collected. Use ResetToReuse or Clear to avoid this warning");
#endif
            this.count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            Array.Clear(this.buffer, 0, this.buffer.Length);

            this.count = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(int index, T item)
        {
            if ((uint)index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == this.buffer.Length) AllocateMore();

            Array.Copy(this.buffer, index, this.buffer, index + 1, this.count - index);
            ++this.count;

            this.buffer[index] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(int index, in T item)
        {
            if ((uint)index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == this.buffer.Length) AllocateMore();

            Array.Copy(this.buffer, index, this.buffer, index + 1, this.count - index);
            ++this.count;

            this.buffer[index] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(uint index, T item)
        {
            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == this.buffer.Length) AllocateMore();

            Array.Copy(this.buffer, index, this.buffer, index + 1, this.count - index);
            ++this.count;

            this.buffer[index] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(uint index, in T item)
        {
            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (this.count == this.buffer.Length) AllocateMore();

            Array.Copy(this.buffer, index, this.buffer, index + 1, this.count - index);
            ++this.count;

            this.buffer[index] = item;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(int index)
        {
            if ((uint)index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (index == --this.count)
                return;

            Array.Copy(this.buffer, index + 1, this.buffer, index, this.count - index);

            this.buffer[this.count] = default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(uint index)
        {
            if (index >= this.count)
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();

            if (index == --this.count)
                return;

            Array.Copy(this.buffer, index + 1, this.buffer, index, this.count - index);

            this.buffer[this.count] = default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Resize(uint newSize)
        {
            if (newSize == this.buffer.Length) return;

            Array.Resize(ref this.buffer, (int)newSize);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] ToArray()
        {
            var destinationArray = new T[this.count];

            Array.Copy(this.buffer, 0, destinationArray, 0, this.count);

            return destinationArray;
        }

        /// <summary>
        /// Note: The length of the returned array cannot be used. Use the count argument instead.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T[] GetBufferArray(out uint count)
        {
            count = this.count;
            return this.buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Trim()
        {
            if (this.count < this.buffer.Length)
                Resize(this.count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void TrimCount(uint newCount)
        {
            if (newCount > this.count)
                throw new ArgumentException($"{nameof(newCount)} must be lesser than the list count");

            this.count = newCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ExpandBy(uint increment)
        {
            var count = this.count + increment;

            if (this.buffer.Length < count)
                AllocateMore(count);

            this.count = count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Push(in T item)
        {
            AddAt(this.count, item);
            return this.count - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly T Pop()
        {
            --this.count;
            return ref this.buffer[this.count];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref readonly T Peek()
            => ref this.buffer[this.count - 1];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex)
            => Array.Copy(this.buffer, 0, array, arrayIndex, this.count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AllocateMore()
        {
            var newLength = (int)((this.buffer.Length + 1) * 1.5f);
            var newList = new T[newLength];
            if (this.count > 0) Array.Copy(this.buffer, newList, this.count);
            this.buffer = newList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AllocateMore(uint newSize)
        {
            if (newSize <= this.buffer.Length)
                return;

            var newLength = (int)(newSize * 1.5f);
            var newList = new T[newLength];
            if (this.count > 0) Array.Copy(this.buffer, newList, this.count);
            this.buffer = newList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
            => new Enumerator(this.buffer, this.count);

        internal static readonly ArrayList<T> Empty = new ArrayList<T>();

        public static explicit operator ArrayList<T>(T[] array)
            => new ArrayList<T>(array);

        public struct Enumerator
        {
            private readonly T[] buffer;
            private readonly uint count;

            private uint index;

            public T Current => this.buffer[this.index - 1];

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
        }
    }
}
