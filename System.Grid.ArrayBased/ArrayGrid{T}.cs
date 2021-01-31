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
        }

        protected ArrayGrid(SerializationInfo info, StreamingContext context)
        {
            this.Size = info.GetValueOrDefault<GridSize>(nameof(this.Size));
            this.data = new ArrayDictionary<GridIndex, T>();

            foreach (var index in GridIndexRange.FromSize(this.Size))
            {
                try
                {
                    var value = (T)info.GetValue(index.ToString(), typeof(T));
                    this.data[index] = value;
                }
                catch
                {
                }
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Size), this.Size);

            foreach (var kv in this.data)
            {
                info.AddValue(kv.Key.ToString(), kv.Value);
            }
        }

        public void Initialize(ArrayGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Clear();

            this.Size = source.Size;

            foreach (var kv in source.data)
            {
                this.data.Set(kv.Key, kv.Value);
            }
        }

        /// <summary>
        /// Initialize without making multiple copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(ArrayGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Clear();

            this.Size = source.Size;

            foreach (var kv in source.data)
            {
                this.data.Set(kv.Key, in kv.Value);
            }
        }

        public void Initialize(in GridSize size, ArrayDictionary<GridIndex, T> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Key))
                    this.data.Set(kv.Key, kv.Value);
            }
        }

        /// <summary>
        /// Initialize without making multiple copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(in GridSize size, ArrayDictionary<GridIndex, T> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Key))
                    this.data.Set(kv.Key, in kv.Value);
            }
        }

        public void Initialize(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Key))
                    this.data.Set(kv.Key, kv.Value);
            }
        }

        public void Initialize(in GridSize size, IEnumerable<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));


            Clear();
            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Index))
                    this.data.Set(in kv.Index, kv.Value);
            }
        }

        /// <summary>
        /// Initialize without making multiple copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(in GridSize size, IEnumerable<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));


            Clear();
            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Index))
                    this.data.Set(in kv.Index, in kv.Value);
            }
        }

        public void Initialize(in GridSize size, IEnumerator<KeyValuePair<GridIndex, T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            while (data.MoveNext())
            {
                var kv = data.Current;

                if (this.Size.ValidateIndex(kv.Key))
                    this.data.Set(kv.Key, kv.Value);
            }
        }

        public void Initialize(in GridSize size, IEnumerator<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));


            Clear();
            this.Size = size;

            while (data.MoveNext())
            {
                var kv = data.Current;

                if (this.Size.ValidateIndex(kv.Index))
                    this.data.Set(in kv.Index, kv.Value);
            }
        }

        /// <summary>
        /// Initialize without making multiple copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(in GridSize size, IEnumerator<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));


            Clear();
            this.Size = size;

            while (data.MoveNext())
            {
                var kv = data.Current;

                if (this.Size.ValidateIndex(kv.Index))
                    this.data.Set(in kv.Index, in kv.Value);
            }
        }

        public void Initialize(IGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Clear();

            this.Size = source.Size;

            foreach (var kv in source.GetIndexedValues())
            {
                this.data.Set(in kv.Index, kv.Value);
            }
        }

        /// <summary>
        /// Initialize without making multiple copies of <see cref="T"/> value.
        /// </summary>
        public void InitializeIn(IGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Clear();

            this.Size = source.Size;

            foreach (var kv in source.GetIndexedValues())
            {
                this.data.Set(in kv.Index, in kv.Value);
            }
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

        public void CopyTo(ArrayGrid<T> dest)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

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

        public void GetValues(ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var value in this.data.Values)
                {
                    output.Add(value);
                }
            }
            else
            {
                foreach (var value in this.data.Values)
                {
                    if (!output.Contains(value))
                        output.Add(value);
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

        public void GetValuesIn(ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var value in this.data.Values)
                {
                    output.Add(in value);
                }
            }
            else
            {
                foreach (var value in this.data.Values)
                {
                    if (!output.Contains(in value))
                        output.Add(in value);
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

        public void GetValues(ICollection<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var value in this.data.Values)
                {
                    output.Add(value);
                }
            }
            else
            {
                foreach (var value in this.data.Values)
                {
                    if (!output.Contains(value))
                        output.Add(value);
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

        public void GetIndexedValues(ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var kv in this.data)
            {
                output.Add(new GridValue<T>(kv.Key, kv.Value));
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

        public void GetIndexedValues(in GridIndexRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, value));
            }
        }

        public void GetIndexedValues(in GridRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, value));
            }
        }

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

                output.Add(new GridValue<T>(index, value));
            }
        }

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

                output.Add(new GridValue<T>(index, value));
            }
        }

        public void GetIndexedValues(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var kv in this.data)
            {
                output.Add(new GridValue<T>(kv.Key, kv.Value));
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

        public void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, value));
            }
        }

        public void GetIndexedValues(in GridRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, value));
            }
        }

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

                output.Add(new GridValue<T>(index, value));
            }
        }

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

                output.Add(new GridValue<T>(index, value));
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

        public void GetIndexedValuesIn(ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var kv in this.data)
            {
                output.Add(new GridValue<T>(kv.Key, in kv.Value));
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

        public void GetIndexedValuesIn(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var kv in this.data)
            {
                output.Add(new GridValue<T>(kv.Key, in kv.Value));
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
            => this.data.GetKeyValueAt(index, out key, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetAt(uint index, out GridIndex key, out T value)
            => this.data.TryGetKeyValueAt(index, out key, out value);
    }
}