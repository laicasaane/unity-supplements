using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Grid
{
    public readonly struct ReadGrid<T> : IReadOnlyGrid<T>
    {
        public GridSize Size => this.size;

        public IEnumerable<GridIndex> Indices => GetSource().data.Keys;

        public IEnumerable<T> Values => GetSource().data.Values;

        public T this[in GridIndex key]
        {
            get
            {
                if (this.source == null || !this.source.data.TryGetValue(key, out var value))
                    throw new ArgumentOutOfRangeException(nameof(key));

                return value;
            }
        }

        public readonly int Count;

        private readonly Grid<T> source;
        private readonly GridSize size;

        public ReadGrid(Grid<T> source)
        {
            this.source = source ?? Grid<T>.Empty;
            this.size = this.source.Size;
            this.Count = this.source.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Grid<T> GetSource()
            => this.source ?? Grid<T>.Empty;

        public bool ContainsIndex(in GridIndex index)
            => GetSource().data.ContainsKey(index);

        public bool ContainsValue(T value)
            => GetSource().data.ContainsValue(value);

        public bool TryGetValue(in GridIndex index, out T value)
            => GetSource().data.TryGetValue(index, out value);

        public Dictionary<GridIndex, T>.Enumerator GetEnumerator()
            => GetSource().data.GetEnumerator();

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
                foreach (var value in this.source.data.Values)
                {
                    output.Add(value);
                }
            }
            else
            {
                foreach (var value in this.source.data.Values)
                {
                    if (!output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetValues(this.size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output)
            => GetValues(this.size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output)
            => GetValues(this.size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output)
            => GetValues(this.size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output)
            => GetValues(this.size.IndexRange(pivot, byRow), output);

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
            => GetValues(this.size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output, bool allowDuplicate)
            => GetValues(this.size.IndexRange(pivot, byRow), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ICollection<T> output, bool allowDuplicate)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in this.size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ICollection<T> output, bool allowDuplicate)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in this.size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(index, out var value) && !output.Contains(value))
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

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in indices)
                {
                    if (this.source.data.TryGetValue(index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in indices)
                {
                    if (this.source.data.TryGetValue(index, out var value) && !output.Contains(value))
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

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    if (this.source.data.TryGetValue(enumerator.Current, out var value))
                        output.Add(value);
                }
            }
            else
            {
                while (enumerator.MoveNext())
                {
                    if (this.source.data.TryGetValue(enumerator.Current, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues()
            => new Grid<T>.GridValues(this.source, this.source.data.Keys.GetEnumerator());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, int extend)
            => GetValues(this.size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetValues(this.size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(this.size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetValues(this.size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndex pivot, bool byRow)
            => GetValues(this.size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridIndexRange range)
        {
            var enumerator = this.size.ClampIndexRange(range).GetEnumerator();
            return new Grid<T>.GridValues(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(in GridRange range)
        {
            var enumerator = this.size.ClampIndexRange(range).GetEnumerator();
            return new Grid<T>.GridValues(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(IEnumerable<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            return new Grid<T>.GridValues(GetSource(), indices.GetEnumerator());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridValues GetValues(IEnumerator<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            return new Grid<T>.GridValues(GetSource(), indices);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues()
            => GetValues();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int extend)
            => GetValues(this.size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetValues(this.size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(this.size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetValues(this.size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, bool byRow)
            => GetValues(this.size.IndexRange(pivot, byRow));

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

            if (this.source == null)
                return;

            foreach (var (index, value) in this.source.data)
            {
                var data = new GridValue<T>(in index, value);

                if (!output.Contains(data))
                    output.Add(data);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(index, out var value))
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

            if (this.source == null)
                return;

            foreach (var index in indices)
            {
                if (!this.source.data.TryGetValue(index, out var value))
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

            if (this.source == null)
                return;

            while (enumerator.MoveNext())
            {
                var index = enumerator.Current;

                if (!this.source.data.TryGetValue(index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues()
            => new Grid<T>.GridIndexedValues(this.source, this.source.data.Keys.GetEnumerator());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(this.size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValues(this.size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(this.size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValues(this.size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(this.size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridIndexRange range)
        {
            var enumerator = this.size.ClampIndexRange(range).GetEnumerator();
            return new Grid<T>.GridIndexedValues(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(in GridRange range)
        {
            var enumerator = this.size.ClampIndexRange(range).GetEnumerator();
            return new Grid<T>.GridIndexedValues(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(IEnumerable<GridIndex> indices)
            => new Grid<T>.GridIndexedValues(GetSource(), indices?.GetEnumerator());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Grid<T>.GridIndexedValues GetIndexedValues(IEnumerator<GridIndex> indices)
            => new Grid<T>.GridIndexedValues(GetSource(), indices);

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

        public static ReadGrid<T> Empty { get; } = new ReadGrid<T>(Grid<T>.Empty);

        public static implicit operator ReadGrid<T>(Grid<T> source)
            => source == null ? Empty : new ReadGrid<T>(source);
    }
}