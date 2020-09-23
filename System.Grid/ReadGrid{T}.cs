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

        public GridRange ClampIndexRange(in GridIndex start, in GridIndex end)
            => GetSource().ClampIndexRange(start, end);

        public GridRange ClampIndexRange(in GridIndexRange range)
            => GetSource().ClampIndexRange(range);

        public GridRange ClampIndexRange(in GridRange range)
            => GetSource().ClampIndexRange(range);

        public GridRange IndexRange(in GridIndex pivot, int extend)
            => GetSource().IndexRange(pivot, extend);

        public GridRange IndexRange(in GridIndex pivot, in GridIndex extend)
            => GetSource().IndexRange(pivot, extend);

        public GridRange IndexRange(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().IndexRange(pivot, lowerExtend, upperExtend);

        public GridRange IndexRange(in GridIndex pivot, bool row)
            => GetSource().IndexRange(pivot, row);

        public GridRange IndexRange()
            => GetSource().IndexRange();

        public void GetValues(ICollection<T> output)
            => GetSource().GetValues(output);

        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output);

        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetSource().GetValues(pivot, byRow, output);

        public void GetValues(in GridIndexRange range, ICollection<T> output)
            => GetSource().GetValues(range, output);

        public void GetValues(in GridRange range, ICollection<T> output)
            => GetSource().GetValues(range, output);

        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output)
            => GetSource().GetValues(indices, output);

        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output)
            => GetSource().GetValues(enumerator, output);

        public IEnumerable<T> GetValues(in GridIndex pivot, int extend)
            => GetSource().GetValues(pivot, extend);

        public IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetValues(pivot, extend);

        public IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        public IEnumerable<T> GetValues(in GridIndex pivot, bool byRow)
            => GetSource().GetValues(pivot, byRow);

        public IEnumerable<T> GetValues(GridIndexRange range)
            => GetSource().GetValues(range);

        public IEnumerable<T> GetValues(GridRange range)
            => GetSource().GetValues(range);

        public IEnumerable<T> GetValues(IEnumerable<GridIndex> indices)
            => GetSource().GetValues(indices);

        public IEnumerable<T> GetValues(IEnumerator<GridIndex> enumrator)
            => GetSource().GetValues(enumrator);

        public void GetIndexedValues(ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(output);

        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend, output);

        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, byRow, output);

        public void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(range, output);

        public void GetIndexedValues(in GridRange range, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(range, output);

        public void GetIndexedValues(IEnumerable<GridIndex> indices, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(indices, output);

        public void GetIndexedValues(IEnumerator<GridIndex> enumerator, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(enumerator, output);

        public IEnumerable<GridValue<T>> GetIndexedValues()
            => GetSource().GetIndexedValues();

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValues(pivot, extend);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValues(pivot, extend);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        public IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValues(pivot, byRow);

        public IEnumerable<GridValue<T>> GetIndexedValues(GridIndexRange range)
            => GetSource().GetIndexedValues(range);

        public IEnumerable<GridValue<T>> GetIndexedValues(GridRange range)
            => GetSource().GetIndexedValues(range);

        public IEnumerable<GridValue<T>> GetIndexedValues(IEnumerable<GridIndex> indices)
            => GetSource().GetIndexedValues(indices);

        public IEnumerable<GridValue<T>> GetIndexedValues(IEnumerator<GridIndex> enumerator)
            => GetSource().GetIndexedValues(enumerator);

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