using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Grid
{
    public readonly struct ReadGrid<T> : IReadOnlyGrid<T>, IEquatableReadOnlyStruct<ReadGrid<T>>
    {
        public GridSize Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Size;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Count;
        }

        public IEnumerable<GridIndex> Indices
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Indices;
        }

        public IEnumerable<T> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Values;
        }

        public T this[in GridIndex key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource()[in key];
        }

        private readonly Grid<T> source;
        private readonly bool hasSource;

        public ReadGrid(Grid<T> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Grid<T> GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(Grid<T> dest)
            => GetSource().CopyTo(dest);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(in GridIndex index)
            => GetSource().ContainsIndex(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsValue(T value)
            => GetSource().ContainsValue(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(in GridIndex index, out T value)
            => GetSource().TryGetValue(index, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Dictionary<GridIndex, T>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ICollection<T> output)
            => GetSource().GetValues(output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetSource().GetValues(pivot, byRow, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ICollection<T> output)
            => GetSource().GetValues(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ICollection<T> output)
            => GetSource().GetValues(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output)
            => GetSource().GetValues(indices, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output)
            => GetSource().GetValues(enumerator, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(pivot, extend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(pivot, extend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(pivot, byRow, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(range, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(range, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(indices, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output, bool allowDuplicate)
            => GetSource().GetValues(enumerator, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues()
            => GetSource().GetValues();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, int extend)
            => GetSource().GetValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, bool byRow)
            => GetSource().GetValues(pivot, byRow);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndexRange range)
            => GetSource().GetValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridRange range)
            => GetSource().GetValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(IEnumerable<GridIndex> indices)
            => GetSource().GetValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(IEnumerator<GridIndex> enumrator)
            => GetSource().GetValues(enumrator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues()
            => GetSource().GetValues();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int extend)
            => GetSource().GetValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, bool byRow)
            => GetSource().GetValues(pivot, byRow);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndexRange range)
            => GetSource().GetValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridRange range)
            => GetSource().GetValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(IEnumerable<GridIndex> indices)
            => GetSource().GetValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(IEnumerator<GridIndex> enumrator)
            => GetSource().GetValues(enumrator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, byRow, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridRange range, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerable<GridIndex> indices, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(indices, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerator<GridIndex> enumerator, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(enumerator, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues()
            => GetSource().GetIndexedValues();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValues(pivot, byRow);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndexRange range)
            => GetSource().GetIndexedValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridRange range)
            => GetSource().GetIndexedValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(IEnumerable<GridIndex> indices)
            => GetSource().GetIndexedValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(IEnumerator<GridIndex> enumerator)
            => GetSource().GetIndexedValues(enumerator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues()
            => GetSource().GetIndexedValues();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValues(pivot, byRow);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndexRange range)
            => GetSource().GetIndexedValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridRange range)
            => GetSource().GetIndexedValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(IEnumerable<GridIndex> indices)
            => GetSource().GetIndexedValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(IEnumerator<GridIndex> enumerator)
            => GetSource().GetIndexedValues(enumerator);

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