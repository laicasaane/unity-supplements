using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public partial class Grid<T> : IGrid<T>, ISerializable
    {
        public GridSize Size { get; private set; }

        public int Count => this.data.Count;

        public IEnumerable<GridIndex> Indices => this.data.Keys;

        public IEnumerable<T> Values => this.data.Values;

        public T this[in GridIndex key]
        {
            get
            {
                if (!this.data.TryGetValue(key, out var value))
                    throw new ArgumentOutOfRangeException(nameof(key));

                return value;
            }

            set
            {
                if (!this.data.ContainsKey(key))
                    throw new ArgumentOutOfRangeException(nameof(key));

                this.data[key] = value;
            }
        }

        internal readonly Dictionary<GridIndex, T> data;

        public Grid()
        {
            this.data = new Dictionary<GridIndex, T>();
        }

        public Grid(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data)
        {
            this.data = new Dictionary<GridIndex, T>();
            Initialize(size, data);
        }

        public Grid(in GridSize size, IEnumerable<GridValue<T>> data)
        {
            this.data = new Dictionary<GridIndex, T>();
            Initialize(size, data);
        }

        public Grid(in GridSize size, IEnumerator<KeyValuePair<GridIndex, T>> data)
        {
            this.data = new Dictionary<GridIndex, T>();
            Initialize(size, data);
        }

        public Grid(in GridSize size, IEnumerator<GridValue<T>> data)
        {
            this.data = new Dictionary<GridIndex, T>();
            Initialize(size, data);
        }

        public Grid(Grid<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            this.Size = source.Size;
            this.data = new Dictionary<GridIndex, T>(source.data);
        }

        public Grid(in ReadGrid<T> source)
            : this(source.GetSource())
        { }

        protected Grid(SerializationInfo info, StreamingContext context)
        {
            this.Size = info.GetValueOrDefault<GridSize>(nameof(this.Size));
            this.data = new Dictionary<GridIndex, T>();

            foreach (var index in GridIndexRange.FromSize(this.Size))
            {
                try
                {
                    var value = (T)info.GetValue(index.ToString(), typeof(T));
                    this.data[index] = value;
                }
                catch
                {
                    // ignored
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

        public void Initialize(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            foreach (var kv in data)
            {
                if (this.Size.ValidateIndex(kv.Key))
                    this.data[kv.Key] = kv.Value;
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
                    this.data[kv.Index] = kv.Value;
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
                    this.data[kv.Key] = kv.Value;
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
                    this.data[kv.Index] = kv.Value;
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
                this.data[kv.Index] = kv.Value;
            }
        }

        public void Set(in GridIndex index, T value)
        {
            if (!this.data.ContainsKey(index))
                throw new ArgumentOutOfRangeException(nameof(index));

            this.data[index] = value;
        }

        public void Set(in GridIndex index, in T value)
        {
            if (!this.data.ContainsKey(index))
                throw new ArgumentOutOfRangeException(nameof(index));

            this.data[index] = value;
        }

        public bool TrySet(in GridIndex index, T value)
        {
            if (!this.data.ContainsKey(index))
                return false;

            this.data[index] = value;
            return true;
        }

        public bool TrySet(in GridIndex index, in T value)
        {
            if (!this.data.ContainsKey(index))
                return false;

            this.data[index] = value;
            return true;
        }

        public void CopyTo(Grid<T> dest)
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

        public bool ContainsIndex(in GridIndex index)
            => this.data.ContainsKey(index);

        public bool ContainsValue(T value)
            => this.data.ContainsValue(value);

        public bool TryGetValue(in GridIndex index, out T value)
            => this.data.TryGetValue(index, out value);

        public Dictionary<GridIndex, T>.Enumerator GetEnumerator()
            => this.data.GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ICollection<T> output)
            => GetValues(output, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ICollection<T> output, bool allowDuplicate)
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
        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetValues(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ICollection<T> output)
            => GetValues(range, output, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ICollection<T> output)
            => GetValues(range, output, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output)
            => GetValues(indices, output, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output)
            => GetValues(enumerator, output, false);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.Size.IndexRange(pivot, byRow), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ICollection<T> output, bool allowDuplicate)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ICollection<T> output, bool allowDuplicate)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.data.TryGetValue(index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output, bool allowDuplicate)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                foreach (var index in indices)
                {
                    if (this.data.TryGetValue(index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in indices)
                {
                    if (this.data.TryGetValue(index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output, bool allowDuplicate)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    if (this.data.TryGetValue(enumerator.Current, out var value))
                        output.Add(value);
                }
            }
            else
            {
                while (enumerator.MoveNext())
                {
                    if (this.data.TryGetValue(enumerator.Current, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridValues GetValues()
            => new GridValues(this, this.data.Keys.GetEnumerator());

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
        IGridValues<T> IReadOnlyGrid<T>.GetValues()
            => GetValues();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, bool byRow)
            => GetValues(this.Size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndexRange range)
            => GetValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridRange range)
            => GetValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(IEnumerable<GridIndex> indices)
            => GetValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(IEnumerator<GridIndex> indices)
            => GetValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var (index, value) in this.data)
            {
                var data = new GridValue<T>(in index, value);

                if (!output.Contains(data))
                    output.Add(data);
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
                if (!this.data.TryGetValue(index, out var value))
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
                if (!this.data.TryGetValue(index, out var value))
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
                if (!this.data.TryGetValue(index, out var value))
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

                if (!this.data.TryGetValue(index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GridIndexedValues GetIndexedValues()
            => new GridIndexedValues(this, this.data.Keys.GetEnumerator());

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
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues()
            => GetIndexedValues();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(pivot, byRow);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndexRange range)
            => GetIndexedValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridRange range)
            => GetIndexedValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(IEnumerable<GridIndex> indices)
            => GetIndexedValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(IEnumerator<GridIndex> indices)
            => GetIndexedValues(indices);

        internal static readonly Grid<T> Empty = new Grid<T>();
    }
}