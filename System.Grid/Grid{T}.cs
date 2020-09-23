using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public class Grid<T> : IGrid<T>, ISerializable
    {
        public GridSize Size { get; private set; }

        public int Count => this.data.Count;

        public IEnumerable<GridIndex> Indices => this.data.Keys;

        public IEnumerable<T> Values => this.data.Values;

        public T this[in GridIndex key] => this.data[key];

        private readonly Dictionary<GridIndex, T> data;

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

        public Grid(Grid<T> grid)
        {
            if (grid == null)
                throw new ArgumentNullException(nameof(grid));

            this.Size = grid.Size;
            this.data = new Dictionary<GridIndex, T>(grid.data);
        }

        protected Grid(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Size = (GridSize)info.GetValue(nameof(this.Size), typeof(GridSize));
            }
            catch
            {
                this.Size = default;
            }

            this.data = new Dictionary<GridIndex, T>();

            foreach (var index in GridIndexRange.Count(this.Size))
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
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

        public void Initialize(Grid<T> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = data.Size;
            this.data.AddRange(data.data);
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

        public IEnumerator<KeyValuePair<GridIndex, T>> GetEnumerator()
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
                if (this.data.TryGetValue(index, out var value) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public void GetValues(in GridRange range, ICollection<T> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value) && !output.Contains(value))
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
                if (this.data.TryGetValue(index, out var value) && !output.Contains(value))
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
                if (this.data.TryGetValue(enumerator.Current, out var value) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public IEnumerable<T> GetValues(in GridIndex pivot, int extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        public IEnumerable<T> GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        public IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        public IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        public IEnumerable<T> GetValues(in GridIndex pivot, bool byRow)
            => GetValues(this.Size.IndexRange(pivot, byRow));

        public IEnumerable<T> GetValues(GridIndexRange range)
        {
            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value))
                    yield return value;
            }
        }

        public IEnumerable<T> GetValues(GridRange range)
        {
            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value))
                    yield return value;
            }
        }

        public IEnumerable<T> GetValues(IEnumerable<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            foreach (var index in indices)
            {
                if (this.data.TryGetValue(index, out var value))
                    yield return value;
            }
        }

        public IEnumerable<T> GetValues(IEnumerator<GridIndex> enumerator)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            while (enumerator.MoveNext())
            {
                if (this.data.TryGetValue(enumerator.Current, out var value))
                    yield return value;
            }
        }

        public void GetIndexedValues(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var kv in this.data)
            {
                GridValue<T> data = kv;

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
                if (!this.data.TryGetValue(index, out var value))
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
                if (!this.data.TryGetValue(index, out var value))
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
                if (!this.data.TryGetValue(index, out var value))
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

                if (!this.data.TryGetValue(index, out var value))
                    continue;

                var data = new GridValue<T>(index, value);

                if (!output.Contains(data))
                    output.Add(data);
            }
        }

        public IEnumerable<GridValue<T>> GetIndexedValues()
        {
            foreach (var kv in this.data)
            {
                yield return kv;
            }
        }

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(this.Size.IndexRange(pivot, byRow));

        public IEnumerable<GridValue<T>> GetIndexedValues(GridIndexRange range)
        {
            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value))
                    yield return new GridValue<T>(index, value);
            }
        }

        public IEnumerable<GridValue<T>> GetIndexedValues(GridRange range)
        {
            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value))
                    yield return new GridValue<T>(index, value);
            }
        }

        public IEnumerable<GridValue<T>> GetIndexedValues(IEnumerable<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            foreach (var index in indices)
            {
                if (this.data.TryGetValue(index, out var value))
                    yield return new GridValue<T>(index, value);
            }
        }

        public IEnumerable<GridValue<T>> GetIndexedValues(IEnumerator<GridIndex> enumerator)
        {
            if (enumerator == null)
                throw new ArgumentNullException(nameof(enumerator));

            while (enumerator.MoveNext())
            {
                var index = enumerator.Current;

                if (this.data.TryGetValue(index, out var value))
                    yield return new GridValue<T>(index, value);
            }
        }
    }
}