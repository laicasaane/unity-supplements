using System.Collections.ArrayBased;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Grid.ArrayBased
{
    public readonly struct ReadArrayGrid<T> : IEquatableReadOnlyStruct<ReadArrayGrid<T>>
    {
        public GridSize Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Size;
        }

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Count;
        }

        public ReadArray1<ArrayDictionary<GridIndex, T>.Node> Indices
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Indices;
        }

        public ReadArray1<T> Values
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource().Values;
        }

        public T this[in GridIndex key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource()[in key];
        }

        public KeyValuePair<GridIndex, T> this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => GetSource()[index];
        }

        private readonly ArrayGrid<T> source;
        private readonly bool hasSource;

        public ReadArrayGrid(ArrayGrid<T> source)
        {
            this.source = source ?? _empty;
            this.hasSource = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(ArrayGrid<T> dest, bool inValue = false)
            => GetSource().CopyTo(dest, inValue);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsIndex(in GridIndex index)
            => GetSource().ContainsIndex(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsValue(T value)
            => GetSource().ContainsValue(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsValue(in T value)
            => GetSource().ContainsValue(in value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(in GridIndex index, out T value)
            => GetSource().TryGetValue(index, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayDictionary<GridIndex, T>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ArrayList<T> output)
            => GetSource().GetValues(output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, extend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, extend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, byRow, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(range, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(range, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(indices, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(enumerator, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(ArrayList<T> output)
            => GetSource().GetValuesIn(output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, int extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(pivot, extend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(pivot, lowerExtend, upperExtend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, in GridIndex extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(pivot, extend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(pivot, lowerExtend, upperExtend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, bool byRow, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(pivot, byRow, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndexRange range, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(range, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridRange range, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(range, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(IEnumerable<GridIndex> indices, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(indices, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(IEnumerator<GridIndex> enumerator, ArrayList<T> output, bool allowDuplicate = false)
            => GetSource().GetValuesIn(enumerator, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, extend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, extend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(pivot, byRow, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(range, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(range, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(indices, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output, bool allowDuplicate = false)
            => GetSource().GetValues(enumerator, output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, int extend)
            => GetSource().GetValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, bool byRow)
            => GetSource().GetValues(pivot, byRow);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndexRange range)
            => GetSource().GetValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridRange range)
            => GetSource().GetValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(IEnumerable<GridIndex> indices)
            => GetSource().GetValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(IEnumerator<GridIndex> enumrator)
            => GetSource().GetValues(enumrator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int extend, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, bool byRow, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(pivot, byRow, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndexRange range, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridRange range, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerable<GridIndex> indices, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(indices, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerator<GridIndex> enumerator, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValues(enumerator, output);

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
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValues(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetIndexedValues(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValues(pivot, byRow);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndexRange range)
            => GetSource().GetIndexedValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridRange range)
            => GetSource().GetIndexedValues(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(IEnumerable<GridIndex> indices)
            => GetSource().GetIndexedValues(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(IEnumerator<GridIndex> enumerator)
            => GetSource().GetIndexedValues(enumerator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int extend, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, bool byRow, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, byRow, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndexRange range, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridRange range, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerable<GridIndex> indices, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(indices, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerator<GridIndex> enumerator, ArrayList<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(enumerator, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, extend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, lowerExtend, upperExtend, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(pivot, byRow, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndexRange range, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridRange range, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(range, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerable<GridIndex> indices, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(indices, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerator<GridIndex> enumerator, ICollection<GridValue<T>> output)
            => GetSource().GetIndexedValuesIn(enumerator, output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, int extend)
            => GetSource().GetIndexedValuesIn(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetSource().GetIndexedValuesIn(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend)
            => GetSource().GetIndexedValuesIn(pivot, extend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetSource().GetIndexedValuesIn(pivot, lowerExtend, upperExtend);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, bool byRow)
            => GetSource().GetIndexedValuesIn(pivot, byRow);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndexRange range)
            => GetSource().GetIndexedValuesIn(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridRange range)
            => GetSource().GetIndexedValuesIn(range);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(IEnumerable<GridIndex> indices)
            => GetSource().GetIndexedValuesIn(indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(IEnumerator<GridIndex> enumerator)
            => GetSource().GetIndexedValuesIn(enumerator);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetAt(uint index, out GridIndex key, out T value)
            => GetSource().GetAt(index, out key, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetAt(uint index, out GridIndex key, out T value)
            => GetSource().TryGetAt(index, out key, out value);

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