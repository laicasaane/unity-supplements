using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Grid
{
    public readonly struct ReadGrid<T> : IReadOnlyGrid<T>, IEquatableReadOnlyStruct<ReadGrid<T>>
    {
        public GridIndex Size => GetSource().Size;

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

        public bool ContainsValue(T value)
            => GetSource().ContainsValue(value);

        public bool TryGetValue(in GridIndex index, out T value)
            => GetSource().TryGetValue(index, out value);

        public IEnumerator<KeyValuePair<GridIndex, T>> GetEnumerator()
            => GetSource().GetEnumerator();

        public bool ValidateIndex(in GridIndex value)
            => GetSource().ValidateIndex(value);

        public GridIndex LastIndex()
            => GetSource().LastIndex();

        public GridIndex ClampIndex(in GridIndex value)
            => GetSource().ClampIndex(value);

        public GridIndexRange ClampIndexRange(in GridIndex start, in GridIndex end)
            => GetSource().ClampIndexRange(start, end);

        public GridIndexRange ClampIndexRange(in GridIndexRange range)
            => GetSource().ClampIndexRange(range);

        public GridIndexRange IndexRange(in GridIndex pivot, int extend)
            => GetSource().IndexRange(pivot, extend);

        public GridIndexRange IndexRange(in GridIndex pivot, in GridIndex extend)
            => GetSource().IndexRange(pivot, extend);

        public GridIndexRange IndexRange(in GridIndex pivot, bool row)
            => GetSource().IndexRange(pivot, row);

        public GridIndexRange IndexRange()
            => GetSource().IndexRange();

        public void GetValues(ICollection<T> output)
            => GetSource().GetValues(output);

        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetSource().GetValues(pivot, byRow, output);

        public void GetValues(in GridIndexRange range, ICollection<T> output)
            => GetSource().GetValues(range, output);

        public IEnumerable<T> GetValues(in GridIndex pivot, int extend)
            => GetSource().GetValues(pivot, extend);

        public IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetValues(pivot, extend);

        public IEnumerable<T> GetValues(in GridIndex pivot, bool byRow)
            => GetSource().GetValues(pivot, byRow);

        public IEnumerable<T> GetValues(GridIndexRange range)
            => GetSource().GetValues(range);

        public void GetIndexedValues(ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(output);

        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, byRow, output);

        public void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(range, output);

        public IEnumerable<GridValue<T>> GetIndexedValues()
            => GetSource().GetIndexedValues();

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValues(pivot, extend);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValues(pivot, extend);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValues(pivot, byRow);

        public IEnumerable<GridValue<T>> GetIndexedValues(GridIndexRange range)
            => GetSource().GetIndexedValues(range);

        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadGrid<T> other && Equals(in other);

        public bool Equals(ReadGrid<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Equals(in ReadGrid<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        private static Grid<T> _empty { get; } = new Grid<T>();

        public static ReadGrid<T> Empty { get; } = new ReadGrid<T>(_empty);

        public static implicit operator ReadGrid<T>(Grid<T> source)
            => source == null ? Empty : new ReadGrid<T>(source);

        public static bool operator ==(in ReadGrid<T> a, in ReadGrid<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadGrid<T> a, in ReadGrid<T> b)
            => !a.Equals(in b);
    }
}