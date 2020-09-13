using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Grid
{
    [Serializable]
    public class Grid<T> : IReadOnlyGrid<T>,
                           IReadOnlyDictionary<GridIndex, T>,
                           ISerializable
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

        public ReadRange<GridIndex> ClampIndexRange(in GridIndex start, in GridIndex end)
            => GridIndexRange.Get(
                ClampIndex(start),
                ClampIndex(end)
            );

        public ReadRange<GridIndex> ClampIndexRange(in ReadRange<GridIndex> range)
            => GridIndexRange.Get(
                ClampIndex(range.Start),
                ClampIndex(range.End)
            );

        public ReadRange<GridIndex> IndexRange(in GridIndex pivot, int extend)
            => IndexRange(pivot, GridIndex.One * extend);

        public ReadRange<GridIndex> IndexRange(in GridIndex pivot, in GridIndex extend)
            => GridIndexRange.Get(
                ClampIndex(pivot - extend),
                ClampIndex(pivot + extend)
            );

        public ReadRange<GridIndex> IndexRange(in GridIndex pivot, bool row)
            => GridIndexRange.Get(
                new GridIndex(row ? pivot.Row : 0, row ? 0 : pivot.Column),
                new GridIndex(row ? pivot.Row : this.Size.Row - 1, row ? this.Size.Column - 1 : pivot.Column)
            );

        public bool ContainsIndex(in GridIndex index)
            => this.data.ContainsKey(index);

        public bool TryGetValue(in GridIndex index, out T value)
            => this.data.TryGetValue(index, out value);

        public IEnumerator<KeyValuePair<GridIndex, T>> GetEnumerator()
            => this.data.GetEnumerator();

        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetValues(IndexRange(pivot, extend), output);

        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetValues(IndexRange(pivot, extend), output);

        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetValues(IndexRange(pivot, byRow), output);

        public void GetValues(in ReadRange<GridIndex> range, ICollection<T> output)
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

        public IEnumerable<T> GetValues(ReadRange<GridIndex> range)
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

        public void GetIndexedValues(in ReadRange<GridIndex> range, ICollection<GridValue<T>> output)
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

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(IndexRange(pivot, extend));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(IndexRange(pivot, extend));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(IndexRange(pivot, byRow));

        public IEnumerable<GridValue<T>> GetIndexedValues(ReadRange<GridIndex> range)
        {
            foreach (var index in ClampIndexRange(range))
            {
                if (this.data.TryGetValue(index, out var value))
                    yield return new GridValue<T>(index, value);
            }
        }

        T IReadOnlyDictionary<GridIndex, T>.this[GridIndex key] => this.data[key];

        IEnumerable<GridIndex> IReadOnlyDictionary<GridIndex, T>.Keys => this.data.Keys;

        bool IReadOnlyDictionary<GridIndex, T>.ContainsKey(GridIndex key)
            => this.data.ContainsKey(key);

        IEnumerator IEnumerable.GetEnumerator()
            => this.data.GetEnumerator();

        bool IReadOnlyDictionary<GridIndex, T>.TryGetValue(GridIndex key, out T value)
            => this.data.TryGetValue(key, out value);

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

            try
            {
                this.data = (Dictionary<GridIndex, T>)info.GetValue(nameof(this.data), typeof(Dictionary<GridIndex, T>));
            }
            catch
            {
                this.data = new Dictionary<GridIndex, T>();
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this.Size), this.Size);
            info.AddValue(nameof(this.data), this.data);
        }
    }
}