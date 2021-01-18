using System.Collections.ArrayBased;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid.ArrayBased
{
    [Serializable]
    public partial class ArrayGrid<T> : IGrid<T>, ISerializable
    {
        public GridSize Size { get; private set; }

        public uint Count => this.data.Count;

        public ReadHashSet<GridIndex> Indices => this.indexCache;

        IEnumerable<GridIndex> IReadOnlyGrid<T>.Indices => this.indexCache;

        public ReadArray1<T> Values => this.data.Values;

        IEnumerable<T> IReadOnlyGrid<T>.Values => this.data.Values;

        public T this[in GridIndex key]
        {
            get
            {
                if (!this.data.TryGetValue(in key, out var value))
                    throw new ArgumentOutOfRangeException(nameof(key));

                return value;
            }

            set
            {
                if (!this.data.ContainsKey(in key))
                    throw new ArgumentOutOfRangeException(nameof(key));

                this.data[in key] = value;
            }
        }

        private readonly ArrayDictionary<GridIndex, T> data;
        private readonly HashSet<GridIndex> indexCache;

        public ArrayGrid()
        {
            this.data = new ArrayDictionary<GridIndex, T>();
            this.indexCache = new HashSet<GridIndex>();
        }

        public ArrayGrid(in GridSize size, ArrayDictionary<GridIndex, T> data)
            : this()
        {
            Initialize(size, data);
        }

        public ArrayGrid(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data)
            : this()
        {
            Initialize(size, data);
        }

        public ArrayGrid(in GridSize size, IEnumerable<GridValue<T>> data)
            : this()
        {
            Initialize(size, data);
        }

        public ArrayGrid(in GridSize size, IEnumerator<KeyValuePair<GridIndex, T>> data)
            : this()
        {
            Initialize(size, data);
        }

        public ArrayGrid(in GridSize size, IEnumerator<GridValue<T>> data)
            : this()
        {
            Initialize(size, data);
        }

        public ArrayGrid(ArrayGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.Size = source.Size;
            this.data = new ArrayDictionary<GridIndex, T>(source.data);

            RefreshIndexCache();
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

            RefreshIndexCache();
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Size), this.Size);

            foreach (var kv in this.data)
            {
                info.AddValue(kv.Key.ToString(), kv.Value);
            }
        }

        private void RefreshIndexCache()
        {
            this.indexCache.Clear();

            foreach (var node in this.data.Keys)
            {
                this.indexCache.Add(node.Key);
            }
        }

        public void Initialize(in GridSize size, ArrayDictionary<GridIndex, T> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            foreach (var (index, value) in data)
            {
                if (this.Size.ValidateIndex(index))
                    this.data.Add(in index, value);
            }

            RefreshIndexCache();
        }

        public void Initialize(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            foreach (var (index, value) in data)
            {
                if (this.Size.ValidateIndex(index))
                    this.data.Add(in index, value);
            }

            RefreshIndexCache();
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
                    this.data.Add(in kv.Index, kv.Value);
            }

            RefreshIndexCache();
        }

        public void Initialize(in GridSize size, IEnumerator<KeyValuePair<GridIndex, T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            while (data.MoveNext())
            {
                var (index, value) = data.Current;

                if (this.Size.ValidateIndex(index))
                    this.data.Add(in index, value);
            }

            RefreshIndexCache();
        }

        public void Initialize(in GridSize size, IEnumerator<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));


            Clear();
            this.Size = size;

            while (data.MoveNext())
            {
                var (index, value) = data.Current;

                if (this.Size.ValidateIndex(index))
                    this.data.Add(in index, value);
            }

            RefreshIndexCache();
        }

        public void Initialize(IGrid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Clear();

            this.Size = source.Size;

            foreach (var (index, value) in source.GetIndexedValues())
            {
                this.data.Add(in index, value);
            }

            RefreshIndexCache();
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

        public void FastClear()
        {
            this.Size = GridSize.Zero;
            this.data.FastClear();
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

        public void GetValues(ICollection<T> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var value in this.data.Values)
            {
                if (!output.Contains(value))
                    output.Add(value);
            }
        }

        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, extend), output);

        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, extend), output);

        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, byRow), output);

        public void GetValues(in GridIndexRange range, ICollection<T> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public void GetValues(in GridRange range, ICollection<T> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in indices)
            {
                if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            while (enumerator.MoveNext())
            {
                var index = enumerator.Current;

                if (this.data.TryGetValue(in index, out var value) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public GridValues GetValues()
        {
            RefreshIndexCache();

            return new GridValues(this, this.indexCache.GetEnumerator());
        }

        public GridValues GetValues(in GridIndex pivot, int extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        public GridValues GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        public GridValues GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        public GridValues GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        public GridValues GetValues(in GridIndex pivot, bool byRow)
            => GetValues(this.Size.IndexRange(pivot, byRow));

        public GridValues GetValues(in GridIndexRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridValues(this, enumerator);
        }

        public GridValues GetValues(in GridRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridValues(this, enumerator);
        }

        public GridValues GetValues(IEnumerable<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            return new GridValues(this, indices.GetEnumerator());
        }

        public GridValues GetValues(IEnumerator<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            return new GridValues(this, indices);
        }

        IGridValues<T> IReadOnlyGrid<T>.GetValues()
            => GetValues();

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, bool byRow)
            => GetValues(this.Size.IndexRange(pivot, byRow));

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndexRange range)
            => GetValues(range);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridRange range)
            => GetValues(range);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(IEnumerable<GridIndex> indices)
            => GetValues(indices);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(IEnumerator<GridIndex> indices)
            => GetValues(indices);

        public void GetIndexedValues(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var (index, value) in this.data)
            {
                var data = new GridValue<T>(index, value);

                if (!output.Contains(data))
                    output.Add(data);
            }
        }

        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

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

                var data = new GridValue<T>(index, value);

                if (!output.Contains(data))
                    output.Add(data);
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

                var data = new GridValue<T>(index, value);

                if (!output.Contains(data))
                    output.Add(data);
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

                var data = new GridValue<T>(index, value);

                if (!output.Contains(data))
                    output.Add(data);
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

                var data = new GridValue<T>(index, value);

                if (!output.Contains(data))
                    output.Add(data);
            }
        }

        public GridIndexedValues GetIndexedValues()
        {
            RefreshIndexCache();

            return new GridIndexedValues(this, this.indexCache.GetEnumerator());
        }

        public GridIndexedValues GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend));

        public GridIndexedValues GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        public GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend));

        public GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        public GridIndexedValues GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(this.Size.IndexRange(pivot, byRow));

        public GridIndexedValues GetIndexedValues(in GridIndexRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridIndexedValues(this, enumerator);
        }

        public GridIndexedValues GetIndexedValues(in GridRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new GridIndexedValues(this, enumerator);
        }

        public GridIndexedValues GetIndexedValues(IEnumerable<GridIndex> indices)
            => new GridIndexedValues(this, indices?.GetEnumerator());

        public GridIndexedValues GetIndexedValues(IEnumerator<GridIndex> indices)
            => new GridIndexedValues(this, indices);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues()
            => GetIndexedValues();

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(pivot, extend);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValues(pivot, lowerExtend, upperExtend);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(pivot, extend);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValues(pivot, lowerExtend, upperExtend);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(pivot, byRow);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndexRange range)
            => GetIndexedValues(range);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridRange range)
            => GetIndexedValues(range);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(IEnumerable<GridIndex> indices)
            => GetIndexedValues(indices);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(IEnumerator<GridIndex> indices)
            => GetIndexedValues(indices);
    }
}