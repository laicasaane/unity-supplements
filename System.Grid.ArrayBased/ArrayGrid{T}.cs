using System.Collections.ArrayBased;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Grid.ArrayBased
{
    [Serializable]
    public partial class ArrayGrid<T> : ISerializable
    {
        public GridSize Size { get; private set; }

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.data.Count;
        }

        public ReadArray1<ArrayDictionary<GridIndex, T>.Node> Indices
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.data.Keys;
        }

        public ReadArray1<T> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.data.Values;
        }

        public ArrayDictionary<GridIndex, T>.Node[] UnsafeIndices
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.data.UnsafeKeys;
        }

        public T[] UnsafeValues
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.data.UnsafeValues;
        }

        public T this[in GridIndex key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (!this.data.TryGetValue(in key, out var value))
                    throw new ArgumentOutOfRangeException(nameof(key));

                return value;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (!this.data.ContainsKey(in key))
                    throw new ArgumentOutOfRangeException(nameof(key));

                this.data[in key] = value;
            }
        }

        public KeyValuePair<GridIndex, T> this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.data[index];
        }

        private readonly ArrayDictionary<GridIndex, T> data;
        private bool initialized;

        public ArrayGrid()
        {
            this.data = new ArrayDictionary<GridIndex, T>();
        }

        /// <param name="inValue">Should set to true when <see cref="T"/> is struct to avoid copying its value around.</param>
        public ArrayGrid(in GridSize size, ArrayDictionary<GridIndex, T> data, bool inValue = false)
            : this()
        {
            if (inValue)
                InitializeIn(size, data);
            else
                Initialize(size, data);
        }

        /// <param name="inValue">Should set to true when <see cref="T"/> is struct to avoid copying its value around.</param>
        public ArrayGrid(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data)
            : this()
        {
            Initialize(size, data);
        }

        /// <param name="inValue">Should set to true when <see cref="T"/> is struct to avoid copying its value around.</param>
        public ArrayGrid(in GridSize size, IEnumerable<GridValue<T>> data, bool inValue = false)
            : this()
        {
            if (inValue)
                InitializeIn(size, data);
            else
                Initialize(size, data);
        }

        /// <param name="inValue">Should set to true when <see cref="T"/> is struct to avoid copying its value around.</param>
        public ArrayGrid(in GridSize size, IEnumerator<KeyValuePair<GridIndex, T>> data)
            : this()
        {
            Initialize(size, data);
        }

        /// <param name="inValue">Should set to true when <see cref="T"/> is struct to avoid copying its value around.</param>
        public ArrayGrid(in GridSize size, IEnumerator<GridValue<T>> data, bool inValue = false)
            : this()
        {
            if (inValue)
                InitializeIn(size, data);
            else
                Initialize(size, data);
        }

        public ArrayGrid(ArrayGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.Size = source.Size;
            this.data = new ArrayDictionary<GridIndex, T>(source.data);
            this.initialized = true;
        }

        public ArrayGrid(in ReadArrayGrid<T> source)
            : this(source.GetSource())
        { }

        protected ArrayGrid(SerializationInfo info, StreamingContext context)
        {
            this.Size = info.GetValueOrDefault<GridSize>(nameof(this.Size));
            this.data = new ArrayDictionary<GridIndex, T>();

            foreach (var index in GridIndexRange.FromSize(this.Size))
            {
                try
                {
                    var value = (T)info.GetValue(index.ToString(), typeof(T));
                    this.data[in index] = value;
                }
                catch
                {
                    // ignore
                }
            }

            this.initialized = true;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Size), this.Size);

            for (var i = 0u; i < this.data.Count; i++)
            {
                info.AddValue(this.data.UnsafeKeys[i].Key.ToString(), this.data.UnsafeValues[i]);
            }
        }

        public void Initialize(in ReadArrayGrid<T> source)
            => Initialize(source.GetSource());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(ArrayGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (this.initialized)
                ShallowClear();

            this.Size = source.Size;
            var data = source.data;

            for (var i = 0u; i < data.Count; i++)
            {
                this.data.Set(in data.UnsafeKeys[i].Key, data.UnsafeValues[i]);
            }

            this.initialized = true;
        }

        /// <summary>
        /// Initialize without passing copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(in ReadArrayGrid<T> source)
            => InitializeIn(source.GetSource());

        /// <summary>
        /// Initialize without passing copies of <see cref="T"/> value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitializeIn(ArrayGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (this.initialized)
                ShallowClear();

            this.Size = source.Size;
            var data = source.data;

            for (var i = 0u; i < data.Count; i++)
            {
                this.data.Set(in data.UnsafeKeys[i].Key, in data.UnsafeValues[i]);
            }

            this.initialized = true;
        }

        public void Initialize(in GridSize size, in ReadArrayDictionary<GridIndex, T> data)
            => Initialize(size, data.GetSource());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Initialize(in GridSize size, ArrayDictionary<GridIndex, T> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (this.initialized)
                ShallowClear();

            this.Size = size;

            for (var i = 0u; i < data.Count; i++)
            {
                if (this.Size.ValidateIndex(data.UnsafeKeys[i].Key))
                    this.data.Set(in data.UnsafeKeys[i].Key, data.UnsafeValues[i]);
            }

            this.initialized = true;
        }

        /// <summary>
        /// Initialize without passing copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(in GridSize size, in ReadArrayDictionary<GridIndex, T> data)
            => InitializeIn(size, data.GetSource());

        /// <summary>
        /// Initialize without passing copies of <see cref="T"/> value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InitializeIn(in GridSize size, ArrayDictionary<GridIndex, T> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (this.initialized)
                ShallowClear();

            this.Size = size;

            for (var i = 0u; i < data.Count; i++)
            {
                if (this.Size.ValidateIndex(data.UnsafeKeys[i].Key))
                    this.data.Set(in data.UnsafeKeys[i].Key, in data.UnsafeValues[i]);
            }

            this.initialized = true;
        }

        public void Initialize(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (this.initialized)
                ShallowClear();

            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Key))
                    this.data.Set(kv.Key, kv.Value);
            }

            this.initialized = true;
        }

        public void Initialize(in GridSize size, IEnumerable<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (this.initialized)
                ShallowClear();

            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Index))
                    this.data.Set(in kv.Index, kv.Value);
            }

            this.initialized = true;
        }

        /// <summary>
        /// Initialize without passing copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(in GridSize size, IEnumerable<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (this.initialized)
                ShallowClear();

            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Index))
                    this.data.Set(in kv.Index, in kv.Value);
            }

            this.initialized = true;
        }

        public void Initialize(in GridSize size, IEnumerator<KeyValuePair<GridIndex, T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (this.initialized)
                ShallowClear();

            this.Size = size;

            while (data.MoveNext())
            {
                var kv = data.Current;

                if (this.Size.ValidateIndex(kv.Key))
                    this.data.Set(kv.Key, kv.Value);
            }

            this.initialized = true;
        }

        public void Initialize(in GridSize size, IEnumerator<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (this.initialized)
                ShallowClear();

            this.Size = size;

            while (data.MoveNext())
            {
                var kv = data.Current;

                if (this.Size.ValidateIndex(kv.Index))
                    this.data.Set(in kv.Index, kv.Value);
            }

            this.initialized = true;
        }

        /// <summary>
        /// Initialize without passing copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(in GridSize size, IEnumerator<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (this.initialized)
                ShallowClear();

            this.Size = size;

            while (data.MoveNext())
            {
                var kv = data.Current;

                if (this.Size.ValidateIndex(kv.Index))
                    this.data.Set(in kv.Index, in kv.Value);
            }

            this.initialized = true;
        }

        public void Initialize(IGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (this.initialized)
                ShallowClear();

            this.Size = source.Size;

            foreach (var kv in source.GetIndexedValues())
            {
                this.data.Set(in kv.Index, kv.Value);
            }

            this.initialized = true;
        }

        /// <summary>
        /// Initialize without passing copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(IGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (this.initialized)
                ShallowClear();

            this.Size = source.Size;

            foreach (var kv in source.GetIndexedValues())
            {
                this.data.Set(in kv.Index, in kv.Value);
            }

            this.initialized = true;
        }

        public void Set(in GridIndex index, T value)
        {
            if (!this.data.ContainsKey(in index))
                throw new ArgumentOutOfRangeException(nameof(index));

            this.data.Set(in index, value);
        }

        public void Set(in GridIndex index, in T value)
        {
            if (!this.data.ContainsKey(in index))
                throw new ArgumentOutOfRangeException(nameof(index));

            this.data.Set(in index, in value);
        }

        public bool TrySet(in GridIndex index, T value)
        {
            if (!this.data.ContainsKey(in index))
                return false;

            this.data.Set(in index, value);
            return true;
        }

        public bool TrySet(in GridIndex index, in T value)
        {
            if (!this.data.ContainsKey(in index))
                return false;

            this.data.Set(in index, in value);
            return true;
        }

        /// <param name="inValue">Should set to true when <see cref="T"/> is struct to avoid copying its value around.</param>
        public void CopyTo(ArrayGrid<T> dest, bool inValue = false)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            if (inValue)
                dest.InitializeIn(this);
            else
                dest.Initialize(this);
        }

        public void Clear()
        {
            this.Size = GridSize.Zero;
            this.data.Clear();
        }

        public void ShallowClear()
        {
            this.Size = GridSize.Zero;
            this.data.ShallowClear();
        }

        public bool ContainsIndex(in GridIndex index)
            => this.data.ContainsKey(in index);

        public bool ContainsValue(T value)
            => this.data.ContainsValue(value);

        public bool ContainsValue(in T value)
            => this.data.ContainsValue(in value);

        public bool TryGetValue(in GridIndex index, out T value)
            => this.data.TryGetValue(in index, out value);

        public ArrayDictionary<GridIndex, T>.Enumerator GetEnumerator()
            => this.data.GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                for (var i = 0u; i < this.data.Count; i++)
                {
                    output.Add(this.data.UnsafeValues[i]);
                }
            }
            else
            {
                for (var i = 0u; i < this.data.Count; i++)
                {
                    if (!output.Contains(this.data.UnsafeValues[i]))
                        output.Add(this.data.UnsafeValues[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, byRow), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in indices)
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in indices)
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                for (var i = 0u; i < this.data.Count; i++)
                {
                    output.Add(in this.data.UnsafeValues[i]);
                }
            }
            else
            {
                for (var i = 0u; i < this.data.Count; i++)
                {
                    if (!output.Contains(in this.data.UnsafeValues[i]))
                        output.Add(in this.data.UnsafeValues[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, int extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, in GridIndex extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, bool byRow, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, byRow), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndexRange range, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(in value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(in value))
                        output.Add(in value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridRange range, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(in value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(in value))
                        output.Add(in value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(IEnumerable<GridIndex> indices, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in indices)
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(in value);
                }
            }
            else
            {
                foreach (var index in indices)
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(in value))
                        output.Add(in value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(IEnumerator<GridIndex> enumerator, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(in value);
                }
            }
            else
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(in value))
                        output.Add(in value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ICollection<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                for (var i = 0u; i < this.data.Count; i++)
                {
                    output.Add(this.data.UnsafeValues[i]);
                }
            }
            else
            {
                for (var i = 0u; i < this.data.Count; i++)
                {
                    if (!output.Contains(this.data.UnsafeValues[i]))
                        output.Add(this.data.UnsafeValues[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, byRow), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ICollection<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ICollection<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output, bool allowDuplicate = false)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in indices)
                {
                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in indices)
                {
                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output, bool allowDuplicate = false)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(in GridIndex pivot, int extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(in GridIndex pivot, bool byRow)
            => GetValues(this.Size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(in GridIndexRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridValues(this, enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(in GridRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridValues(this, enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(IEnumerable<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            return new GridValues(this, indices.GetEnumerator());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues(IEnumerator<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            return new GridValues(this, indices);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            for (var i = 0u; i < this.data.Count; i++)
            {
                output.Add(new GridValue<T>(in this.data.UnsafeKeys[i].Key, this.data.UnsafeValues[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int extend, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, bool byRow, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndexRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerable<GridIndex> indices, ArrayList<GridValue<T>> output)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in indices)
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerator<GridIndex> enumerator, ArrayList<GridValue<T>> output)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            while (enumerator.MoveNext())
            {
                var index = enumerator.Current;

                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            for (var i = 0u; i < this.data.Count; i++)
            {
                output.Add(new GridValue<T>(in this.data.UnsafeKeys[i].Key, this.data.UnsafeValues[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerable<GridIndex> indices, ICollection<GridValue<T>> output)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in indices)
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerator<GridIndex> enumerator, ICollection<GridValue<T>> output)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            while (enumerator.MoveNext())
            {
                var index = enumerator.Current;

                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(this.Size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(in GridIndexRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridIndexedValues(this, enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(in GridRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridIndexedValues(this, enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(IEnumerable<GridIndex> indices)
            => new GridIndexedValues(this, indices?.GetEnumerator());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues(IEnumerator<GridIndex> indices)
            => new GridIndexedValues(this, indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            for (var i = 0u; i < this.data.Count; i++)
            {
                output.Add(new GridValue<T>(this.data.UnsafeKeys[i].Key, in this.data.UnsafeValues[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int extend, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, bool byRow, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndexRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerable<GridIndex> indices, ArrayList<GridValue<T>> output)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in indices)
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerator<GridIndex> enumerator, ArrayList<GridValue<T>> output)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            while (enumerator.MoveNext())
            {
                var index = enumerator.Current;

                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            for (var i = 0u; i < this.data.Count; i++)
            {
                output.Add(new GridValue<T>(this.data.UnsafeKeys[i].Key, in this.data.UnsafeValues[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndexRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerable<GridIndex> indices, ICollection<GridValue<T>> output)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in indices)
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerator<GridIndex> enumerator, ICollection<GridValue<T>> output)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            while (enumerator.MoveNext())
            {
                var index = enumerator.Current;

                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, int extend)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, bool byRow)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(in GridIndexRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridIndexedValuesIn(this, enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(in GridRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridIndexedValuesIn(this, enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(IEnumerable<GridIndex> indices)
            => new GridIndexedValuesIn(this, indices?.GetEnumerator());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValuesIn GetIndexedValuesIn(IEnumerator<GridIndex> indices)
            => new GridIndexedValuesIn(this, indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetAt(uint index, out GridIndex key, out T value)
            => this.data.GetAt(index, out key, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetAt(uint index, out GridIndex key, out T value)
            => this.data.TryGetAt(index, out key, out value);
    }
}