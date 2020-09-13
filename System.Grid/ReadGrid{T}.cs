using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Grid
{
    public readonly struct ReadGrid<T> : IReadOnlyGrid<T>,
                                         IReadOnlyDictionary<GridIndex, T>,
                                         IReadOnlyCollection<KeyValuePair<GridIndex, T>>
    {
        public int Count => GetSource().Count;

        public IEnumerable<GridIndex> Indices => GetSource().Indices;

        public IEnumerable<T> Values => GetSource().Values;

        public T this[in GridIndex key] => GetSource()[in key];

        private readonly Grid<T> source;
        private readonly bool hasSource;

        public ReadGrid(Grid<T> source)
        {
            this.source = source;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Grid<T> GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

        public bool ContainsIndex(in GridIndex index)
            => GetSource().ContainsIndex(index);

        public bool TryGetValue(in GridIndex index, out T value)
            => GetSource().TryGetValue(index, out value);

        public IEnumerator<KeyValuePair<GridIndex, T>> GetEnumerator()
            => GetSource().GetEnumerator();

        public void GetIndices(in GridIndex pivot, int extend, ICollection<GridIndex> output)
            => GetSource().GetIndices(pivot, extend, output);

        public void GetIndices(in GridIndex pivot, in GridIndex extend, ICollection<GridIndex> output)
            => GetSource().GetIndices(pivot, extend, output);

        public void GetIndices(in GridIndex pivot, bool byRow, ICollection<GridIndex> output)
            => GetSource().GetIndices(pivot, byRow, output);

        public void GetIndices(in ReadRange<GridIndex> range, ICollection<GridIndex> output)
            => GetSource().GetIndices(range, output);

        public IEnumerable<GridIndex> GetIndices(in GridIndex pivot, int extend)
            => GetSource().GetIndices(pivot, extend);

        public IEnumerable<GridIndex> GetIndices(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndices(pivot, extend);

        public IEnumerable<GridIndex> GetIndices(in GridIndex pivot, bool byRow)
            => GetSource().GetIndices(pivot, byRow);

        public IEnumerable<GridIndex> GetIndices(ReadRange<GridIndex> range)
            => GetSource().GetIndices(range);

        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetSource().GetValues(pivot, byRow, output);

        public void GetValues(in ReadRange<GridIndex> range, ICollection<T> output)
            => GetSource().GetValues(range, output);

        public IEnumerable<T> GetValues(in GridIndex pivot, int extend)
            => GetSource().GetValues(pivot, extend);

        public IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetValues(pivot, extend);

        public IEnumerable<T> GetValues(in GridIndex pivot, bool byRow)
            => GetSource().GetValues(pivot, byRow);

        public IEnumerable<T> GetValues(ReadRange<GridIndex> range)
            => GetSource().GetValues(range);

        public void GetIndexedValues(ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(output);

        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, byRow, output);

        public void GetIndexedValues(in ReadRange<GridIndex> range, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(range, output);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValues(pivot, extend);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValues(pivot, extend);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValues(pivot, byRow);

        public IEnumerable<GridValue<T>> GetIndexedValues(ReadRange<GridIndex> range)
            => GetSource().GetIndexedValues(range);

        T IReadOnlyDictionary<GridIndex, T>.this[GridIndex key] => GetSource()[in key];

        IEnumerable<GridIndex> IReadOnlyDictionary<GridIndex, T>.Keys => GetSource().Indices;

        bool IReadOnlyDictionary<GridIndex, T>.ContainsKey(GridIndex key)
            => GetSource().ContainsIndex(key);

        IEnumerator IEnumerable.GetEnumerator()
            => GetSource().GetEnumerator();

        bool IReadOnlyDictionary<GridIndex, T>.TryGetValue(GridIndex key, out T value)
            => GetSource().TryGetValue(key, out value);

        private static Grid<T> _empty { get; } = new Grid<T>();

        public static ReadGrid<T> Empty { get; } = new ReadGrid<T>(_empty);
    }
}