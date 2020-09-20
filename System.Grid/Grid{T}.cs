using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public class Grid<T> : IGrid<T>, ISerializable
    {
        public GridIndex Size { get; private set; }

        public int Count => this.data.Count;

        public IEnumerable<GridIndex> Indices => this.data.Keys;

        public IEnumerable<T> Values => this.data.Values;

        public T this[in GridIndex key] => this.data[key];

        private readonly Dictionary<GridIndex, T> data;

        public Grid()
        {
            this.data = new Dictionary<GridIndex, T>();
        }

        public Grid(in GridIndex size, IEnumerable<KeyValuePair<GridIndex, T>> data)
        {
            this.data = new Dictionary<GridIndex, T>();
            Initialize(size, data);
        }

        public Grid(in GridIndex size, IEnumerable<GridValue<T>> data)
        {
            this.data = new Dictionary<GridIndex, T>();
            Initialize(size, data);
        }

        public void Initialize(in GridIndex size, IEnumerable<KeyValuePair<GridIndex, T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Clear();
            this.Size = size;

            foreach (var kv in data)
            {
                if (ValidateIndex(kv.Key))
                    this.data[kv.Key] = kv.Value;
            }
        }

        public void Initialize(in GridIndex size, IEnumerable<GridValue<T>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));


            Clear();
            this.Size = size;

            foreach (var kv in data)
            {
                if (ValidateIndex(kv.Index))
                    this.data[kv.Index] = kv.Value;
            }
        }

        public void Clear()
        {
            this.Size = GridIndex.Zero;
            this.data.Clear();
        }

        public bool ValidateIndex(in GridIndex value)
            => value.Row < this.Size.Row && value.Column < this.Size.Column;

        public GridIndex LastIndex()
            => this.Size - GridIndex.One;

        public GridIndex ClampIndex(in GridIndex value)
            => new GridIndex(
                value.Row >= this.Size.Row ? this.Size.Row - 1 : value.Row,
                value.Column >= this.Size.Column ? this.Size.Column - 1 : value.Column
            );

        public GridIndexRange ClampIndexRange(in GridIndex start, in GridIndex end)
            => new GridIndexRange(
                ClampIndex(start),
                ClampIndex(end)
            );

        public GridIndexRange ClampIndexRange(in GridIndexRange range)
            => new GridIndexRange(
                ClampIndex(range.Start),
                ClampIndex(range.End)
            );

        public GridIndexRange IndexRange(in GridIndex pivot, int extend)
            => IndexRange(pivot, GridIndex.One * extend);

        public GridIndexRange IndexRange(in GridIndex pivot, in GridIndex extend)
            => new GridIndexRange(
                ClampIndex(pivot - extend),
                ClampIndex(pivot + extend)
            );

        public GridIndexRange IndexRange(in GridIndex pivot, bool row)
            => new GridIndexRange(
                new GridIndex(row ? pivot.Row : 0, row ? 0 : pivot.Column),
                new GridIndex(row ? pivot.Row : this.Size.Row - 1, row ? this.Size.Column - 1 : pivot.Column)
            );

        public GridIndexRange IndexRange()
            => new GridIndexRange(
                GridIndex.Zero,
                this.Size - GridIndex.One
            );

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
            => GetValues(IndexRange(pivot, extend), output);

        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetValues(IndexRange(pivot, extend), output);

        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetValues(IndexRange(pivot, byRow), output);

        public void GetValues(in GridIndexRange range, ICollection<T> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public IEnumerable<T> GetValues(in GridIndex pivot, int extend)
            => GetValues(IndexRange(pivot, extend));

        public IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(IndexRange(pivot, extend));

        public IEnumerable<T> GetValues(in GridIndex pivot, bool byRow)
            => GetValues(IndexRange(pivot, byRow));

        public IEnumerable<T> GetValues(GridIndexRange range)
        {
            foreach (var index in ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value))
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
            => GetIndexedValues(IndexRange(pivot, extend), output);

        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(IndexRange(pivot, extend), output);

        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetIndexedValues(IndexRange(pivot, byRow), output);

        public void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            foreach (var index in ClampIndexRange(range))
            {
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
            => GetIndexedValues(IndexRange(pivot, extend));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(IndexRange(pivot, extend));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(IndexRange(pivot, byRow));

        public IEnumerable<GridValue<T>> GetIndexedValues(GridIndexRange range)
        {
            foreach (var index in ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value))
                    yield return new GridValue<T>(index, value);
            }
        }

        protected Grid(SerializationInfo info, StreamingContext context)
        {
            try
            {
                this.Size = (GridIndex)info.GetValue(nameof(this.Size), typeof(GridIndex));
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
    }
}