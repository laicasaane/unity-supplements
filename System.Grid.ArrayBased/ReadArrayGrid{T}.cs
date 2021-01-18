using System.Collections.ArrayBased;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Grid.ArrayBased
{
    public readonly struct ReadArrayGrid<T> : IReadOnlyGrid<T>, IEquatableReadOnlyStruct<ReadArrayGrid<T>>
    {
        public GridSize Size => GetSource().Size;

        public uint Count => GetSource().Count;

        public ReadHashSet<GridIndex> Indices => GetSource().Indices;

        IEnumerable<GridIndex> IReadOnlyGrid<T>.Indices => GetSource().Indices;

        public ReadArray1<T> Values => GetSource().Values;

        IEnumerable<T> IReadOnlyGrid<T>.Values => GetSource().Values;

        public T this[in GridIndex key] => GetSource()[in key];

        private readonly ArrayGrid<T> source;
        private readonly bool hasSource;

        public ReadArrayGrid(ArrayGrid<T> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
        }

        public override int GetHashCode()
            => GetSource().GetHashCode();

        public override bool Equals(object obj)
            => obj is ReadArrayGrid<T> other && Equals(in other);

        public bool Equals(ReadArrayGrid<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        public bool Equals(in ReadArrayGrid<T> other)
        {
            var source = GetSource();
            var otherSource = other.GetSource();

            return source == otherSource;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ArrayGrid<T> GetSource()
            => this.hasSource ? (this.source ?? _empty) : _empty;

        public void CopyTo(ArrayGrid<T> dest)
            => GetSource().CopyTo(dest);

        public bool ContainsIndex(in GridIndex index)
            => GetSource().ContainsIndex(index);

        public bool ContainsValue(T value)
            => GetSource().ContainsValue(value);

        public bool TryGetValue(in GridIndex index, out T value)
            => GetSource().TryGetValue(index, out value);

        public ArrayDictionary<GridIndex, T>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        public void GetValues(ICollection<T> output)
            => GetSource().GetValues(output);

        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output)
            => GetSource().GetValues(pivot, extend, output);

        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output);

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

        public ArrayGrid<T>.GridValues GetValues()
            => GetSource().GetValues();

        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, int extend)
            => GetSource().GetValues(pivot, extend);

        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetValues(pivot, extend);

        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, bool byRow)
            => GetSource().GetValues(pivot, byRow);

        public ArrayGrid<T>.GridValues GetValues(in GridIndexRange range)
            => GetSource().GetValues(range);

        public ArrayGrid<T>.GridValues GetValues(in GridRange range)
            => GetSource().GetValues(range);

        public ArrayGrid<T>.GridValues GetValues(IEnumerable<GridIndex> indices)
            => GetSource().GetValues(indices);

        public ArrayGrid<T>.GridValues GetValues(IEnumerator<GridIndex> enumrator)
            => GetSource().GetValues(enumrator);

        IGridValues<T> IReadOnlyGrid<T>.GetValues()
            => GetSource().GetValues();

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int extend)
            => GetSource().GetValues(pivot, extend);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetValues(pivot, extend);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndex pivot, bool byRow)
            => GetSource().GetValues(pivot, byRow);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridIndexRange range)
            => GetSource().GetValues(range);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(in GridRange range)
            => GetSource().GetValues(range);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(IEnumerable<GridIndex> indices)
            => GetSource().GetValues(indices);

        IGridValues<T> IReadOnlyGrid<T>.GetValues(IEnumerator<GridIndex> enumrator)
            => GetSource().GetValues(enumrator);

        public void GetIndexedValues(ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(output);

        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend, output);

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

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues()
            => GetSource().GetIndexedValues();

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValues(pivot, extend);

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValues(pivot, extend);

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValues(pivot, byRow);

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndexRange range)
            => GetSource().GetIndexedValues(range);

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridRange range)
            => GetSource().GetIndexedValues(range);

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(IEnumerable<GridIndex> indices)
            => GetSource().GetIndexedValues(indices);

        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(IEnumerator<GridIndex> enumerator)
            => GetSource().GetIndexedValues(enumerator);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues()
            => GetSource().GetIndexedValues();

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValues(pivot, extend);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValues(pivot, extend);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValues(pivot, byRow);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridIndexRange range)
            => GetSource().GetIndexedValues(range);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(in GridRange range)
            => GetSource().GetIndexedValues(range);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(IEnumerable<GridIndex> indices)
            => GetSource().GetIndexedValues(indices);

        IGridIndexedValues<T> IReadOnlyGrid<T>.GetIndexedValues(IEnumerator<GridIndex> enumerator)
            => GetSource().GetIndexedValues(enumerator);

        private static ArrayGrid<T> _empty { get; } = new ArrayGrid<T>();

        public static ReadArrayGrid<T> Empty { get; } = new ReadArrayGrid<T>(_empty);

        public static implicit operator ReadArrayGrid<T>(ArrayGrid<T> source)
            => source == null ? Empty : new ReadArrayGrid<T>(source);

        public static bool operator ==(in ReadArrayGrid<T> a, in ReadArrayGrid<T> b)
            => a.Equals(in b);

        public static bool operator !=(in ReadArrayGrid<T> a, in ReadArrayGrid<T> b)
            => !a.Equals(in b);
    }
}