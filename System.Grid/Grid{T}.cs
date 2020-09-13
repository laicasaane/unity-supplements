using System.Collections;
using System.Collections.Generic;

namespace System.Grid
{
    public class Grid<T> : IReadOnlyGrid<T>,
                           IReadOnlyDictionary<GridIndex, T>,
                           IReadOnlyCollection<KeyValuePair<GridIndex, T>>
    {
        public GridIndex Size { get; private set; }

        private readonly Dictionary<GridIndex, T> data = new Dictionary<GridIndex, T>();

        public int Count => this.data.Count;

        public IEnumerable<GridIndex> Indices => this.data.Keys;

        public IEnumerable<T> Values => this.data.Values;

        public T this[in GridIndex key] => this.data[key];

        public Grid() { }

        public Grid(in GridIndex size, IEnumerable<KeyValuePair<GridIndex, T>> data)
        {
            Initialize(size, data);
        }

        public Grid(in GridIndex size, IEnumerable<GridValue<T>> data)
        {
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
                this.data[kv.Index] = kv.Value;
            }
        }

        public void Clear()
        {
            this.Size = GridIndex.Zero;
            this.data.Clear();
        }

        public bool ContainsIndex(in GridIndex index)
            => this.data.ContainsKey(index);

        public bool TryGetValue(in GridIndex index, out T value)
            => this.data.TryGetValue(index, out value);

        public IEnumerator<KeyValuePair<GridIndex, T>> GetEnumerator()
            => this.data.GetEnumerator();

        public void GetIndices(in GridIndex pivot, int extend, ICollection<GridIndex> output)
            => GetIndices(pivot, GridIndex.One * extend, output);

        public void GetIndices(in GridIndex pivot, in GridIndex extend, ICollection<GridIndex> output)
            => GetIndices(GridIndex.Range(pivot, extend, this.Size), output);

        public void GetIndices(in GridIndex pivot, bool byRow, ICollection<GridIndex> output)
            => GetIndices(GridIndex.Range(pivot, byRow, this.Size), output);

        public void GetIndices(in ReadRange<GridIndex> range, ICollection<GridIndex> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            for (var r = range.Start.Row; r <= range.End.Row; r++)
            {
                for (var c = range.Start.Column; c <= range.End.Column; c++)
                {
                    var index = new GridIndex(r, c);

                    if (!output.Contains(index))
                        output.Add(index);
                }
            }
        }

        public IEnumerable<GridIndex> GetIndices(in GridIndex pivot, int extend)
            => GetIndices(pivot, GridIndex.One * extend);

        public IEnumerable<GridIndex> GetIndices(in GridIndex pivot, in GridIndex extend)
            => GetIndices(GridIndex.Range(pivot, extend, this.Size));

        public IEnumerable<GridIndex> GetIndices(in GridIndex pivot, bool byRow)
            => GetIndices(GridIndex.Range(pivot, byRow, this.Size));

        public IEnumerable<GridIndex> GetIndices(ReadRange<GridIndex> range)
        {
            for (var r = range.Start.Row; r <= range.End.Row; r++)
            {
                for (var c = range.Start.Column; c <= range.End.Column; c++)
                {
                    var index = new GridIndex(r, c);

                    if (this.data.ContainsKey(index))
                        yield return index;
                }
            }
        }

        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetValues(pivot, GridIndex.One * extend, output);

        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetValues(GridIndex.Range(pivot, extend, this.Size), output);

        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetValues(GridIndex.Range(pivot, byRow, this.Size), output);

        public void GetValues(in ReadRange<GridIndex> range, ICollection<T> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            for (var r = range.Start.Row; r <= range.End.Row; r++)
            {
                for (var c = range.Start.Column; c <= range.End.Column; c++)
                {
                    var index = new GridIndex(r, c);

                    if (this.data.TryGetValue(index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        public IEnumerable<T> GetValues(in GridIndex pivot, int extend)
            => GetValues(pivot, GridIndex.One * extend);

        public IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(GridIndex.Range(pivot, extend, this.Size));

        public IEnumerable<T> GetValues(in GridIndex pivot, bool byRow)
            => GetValues(GridIndex.Range(pivot, byRow, this.Size));

        public IEnumerable<T> GetValues(ReadRange<GridIndex> range)
        {
            for (var r = range.Start.Row; r <= range.End.Row; r++)
            {
                for (var c = range.Start.Column; c <= range.End.Column; c++)
                {
                    var index = new GridIndex(r, c);

                    if (this.data.TryGetValue(index, out var value))
                        yield return value;
                }
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
            => GetIndexedValues(pivot, GridIndex.One * extend, output);

        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(GridIndex.Range(pivot, extend, this.Size), output);

        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetIndexedValues(GridIndex.Range(pivot, byRow, this.Size), output);

        public void GetIndexedValues(in ReadRange<GridIndex> range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            for (var r = range.Start.Row; r <= range.End.Row; r++)
            {
                for (var c = range.Start.Column; c <= range.End.Column; c++)
                {
                    var index = new GridIndex(r, c);

                    if (!this.data.TryGetValue(index, out var value))
                        continue;

                    var data = new GridValue<T>(index, value);

                    if (!output.Contains(data))
                        output.Add(data);
                }
            }
        }

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(pivot, GridIndex.One * extend);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(GridIndex.Range(pivot, extend, this.Size));

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(GridIndex.Range(pivot, byRow, this.Size));

        public IEnumerable<GridValue<T>> GetIndexedValues(ReadRange<GridIndex> range)
        {
            for (var r = range.Start.Row; r <= range.End.Row; r++)
            {
                for (var c = range.Start.Column; c <= range.End.Column; c++)
                {
                    var index = new GridIndex(r, c);

                    if (this.data.TryGetValue(index, out var value))
                        yield return new GridValue<T>(index, value);
                }
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
    }
}