using System.Collections.ArrayBased;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Grid.ArrayBased
{
    public readonly struct ReadArrayGrid<T>
    {
        public ReadArray1<ArrayDictionary<GridIndex, T>.Node> Indices => GetSource().Indices;

        public ReadArray1<T> Values => GetSource().Values;

        public T this[in GridIndex key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.source == null || !this.source.data.TryGetValue(in key, out var value))
                    throw new ArgumentOutOfRangeException(nameof(key));

                return value;
            }
        }

        public KeyValuePair<GridIndex, T> this[uint index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                if (this.source == null)
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();

                return this.source.data[index];
            }
        }

        public readonly uint Count;
        public readonly GridSize Size;

        private readonly ArrayGrid<T> source;

        public ReadArrayGrid(ArrayGrid<T> source)
        {
            this.source = source ?? ArrayGrid<T>.Empty;
            this.Size = this.source.Size;
            this.Count = this.source.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal ArrayGrid<T> GetSource()
            => this.source ?? ArrayGrid<T>.Empty;

        /// <param name="inValue">Should set to true when <see cref="T"/> is struct to avoid copying its value around.</param>
        public void CopyTo(ArrayGrid<T> dest, bool inValue = false)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));

            if (this.source == null)
                return;

            if (inValue)
                dest.InitializeIn(this);
            else
                dest.Initialize(this);
        }

        public bool ContainsIndex(in GridIndex index)
        {
            if (this.source == null)
                return false;

            return this.source.data.ContainsKey(in index);
        }

        public bool ContainsValue(T value)
        {
            if (this.source == null)
                return false;

            return this.source.data.ContainsValue(value);
        }

        public bool ContainsValue(in T value)
        {
            if (this.source == null)
                return false;

            return this.source.data.ContainsValue(in value);
        }

        public bool TryGetValue(in GridIndex index, out T value)
        {
            if (this.source == null)
            {
                value = default;
                return false;
            }

            return this.source.data.TryGetValue(in index, out value);
        }

        public ArrayDictionary<GridIndex, T>.Enumerator GetEnumerator()
            => GetSource().GetEnumerator();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < this.source.data.Count; i++)
                {
                    output.Add(this.source.data.UnsafeValues[i]);
                }
            }
            else
            {
                for (var i = 0u; i < this.source.data.Count; i++)
                {
                    if (!output.Contains(this.source.data.UnsafeValues[i]))
                        output.Add(this.source.data.UnsafeValues[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ArrayList<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, byRow), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ArrayList<T> output, bool allowDuplicate = false)
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
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in indices)
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ArrayList<T> output, bool allowDuplicate = false)
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
                    var index = enumerator.Current;

                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < this.source.data.Count; i++)
                {
                    output.Add(in this.source.data.UnsafeValues[i]);
                }
            }
            else
            {
                for (var i = 0u; i < this.source.data.Count; i++)
                {
                    if (!output.Contains(in this.source.data.UnsafeValues[i]))
                        output.Add(in this.source.data.UnsafeValues[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, int extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, in GridIndex extend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndex pivot, bool byRow, ArrayList<T> output, bool allowDuplicate = false)
            => GetValuesIn(this.Size.IndexRange(pivot, byRow), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridIndexRange range, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(in value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(in value))
                        output.Add(in value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(in GridRange range, ArrayList<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(in value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(in value))
                        output.Add(in value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(IEnumerable<GridIndex> indices, ArrayList<T> output, bool allowDuplicate = false)
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
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(in value);
                }
            }
            else
            {
                foreach (var index in indices)
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(in value))
                        output.Add(in value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValuesIn(IEnumerator<GridIndex> enumerator, ArrayList<T> output, bool allowDuplicate = false)
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
                    var index = enumerator.Current;

                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(in value);
                }
            }
            else
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(in value))
                        output.Add(in value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(ICollection<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < this.source.data.Count; i++)
                {
                    output.Add(this.source.data.UnsafeValues[i]);
                }
            }
            else
            {
                for (var i = 0u; i < this.source.data.Count; i++)
                {
                    if (!output.Contains(this.source.data.UnsafeValues[i]))
                        output.Add(this.source.data.UnsafeValues[i]);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int extend, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, extend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output, bool allowDuplicate = false)
            => GetValues(this.Size.IndexRange(pivot, byRow), output, allowDuplicate);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridIndexRange range, ICollection<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(in GridRange range, ICollection<T> output, bool allowDuplicate = false)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            if (allowDuplicate)
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in this.Size.ClampIndexRange(range))
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output, bool allowDuplicate = false)
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
                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                foreach (var index in indices)
                {
                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetValues(IEnumerator<GridIndex> enumerator, ICollection<T> output, bool allowDuplicate = false)
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
                    var index = enumerator.Current;

                    if (this.source.data.TryGetValue(in index, out var value))
                        output.Add(value);
                }
            }
            else
            {
                while (enumerator.MoveNext())
                {
                    var index = enumerator.Current;

                    if (this.source.data.TryGetValue(in index, out var value) && !output.Contains(value))
                        output.Add(value);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, int extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex extend)
            => GetValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndex pivot, bool byRow)
            => GetValues(this.Size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridIndexRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new ArrayGrid<T>.GridValues(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(in GridRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new ArrayGrid<T>.GridValues(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(IEnumerable<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            return new ArrayGrid<T>.GridValues(GetSource(), indices.GetEnumerator());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridValues GetValues(IEnumerator<GridIndex> indices)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            return new ArrayGrid<T>.GridValues(GetSource(), indices);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            for (var i = 0u; i < this.source.data.Count; i++)
            {
                output.Add(new GridValue<T>(in this.source.data.UnsafeKeys[i].Key, this.source.data.UnsafeValues[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int extend, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, bool byRow, ArrayList<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndexRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerable<GridIndex> indices, ArrayList<GridValue<T>> output)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in indices)
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(IEnumerator<GridIndex> enumerator, ArrayList<GridValue<T>> output)
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

                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            for (var i = 0u; i < this.source.data.Count; i++)
            {
                output.Add(new GridValue<T>(in this.source.data.UnsafeKeys[i].Key, this.source.data.UnsafeValues[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetIndexedValues(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(in index, out var value))
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

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(in index, out var value))
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
                if (!this.source.data.TryGetValue(in index, out var value))
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

                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(in index, value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int extend)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValues(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValues(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndex pivot, bool byRow)
            => GetIndexedValues(this.Size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridIndexRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new ArrayGrid<T>.GridIndexedValues(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(in GridRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new ArrayGrid<T>.GridIndexedValues(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(IEnumerable<GridIndex> indices)
            => new ArrayGrid<T>.GridIndexedValues(GetSource(), indices?.GetEnumerator());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValues GetIndexedValues(IEnumerator<GridIndex> indices)
            => new ArrayGrid<T>.GridIndexedValues(GetSource(), indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            for (var i = 0u; i < this.source.data.Count; i++)
            {
                output.Add(new GridValue<T>(this.source.data.UnsafeKeys[i].Key, in this.source.data.UnsafeValues[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int extend, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, bool byRow, ArrayList<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndexRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridRange range, ArrayList<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerable<GridIndex> indices, ArrayList<GridValue<T>> output)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in indices)
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerator<GridIndex> enumerator, ArrayList<GridValue<T>> output)
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

                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            for (var i = 0u; i < this.source.data.Count; i++)
            {
                output.Add(new GridValue<T>(this.source.data.UnsafeKeys[i].Key, in this.source.data.UnsafeValues[i]));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int extend, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, byRow), output);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridIndexRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(in GridRange range, ICollection<GridValue<T>> output)
        {
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in this.Size.ClampIndexRange(range))
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerable<GridIndex> indices, ICollection<GridValue<T>> output)
        {
            if (indices == null)
                throw new ArgumentNullException(nameof(indices));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (this.source == null)
                return;

            foreach (var index in indices)
            {
                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetIndexedValuesIn(IEnumerator<GridIndex> enumerator, ICollection<GridValue<T>> output)
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

                if (!this.source.data.TryGetValue(in index, out var value))
                    continue;

                output.Add(new GridValue<T>(index, in value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, int extend)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, int lowerExtend, int upperExtend)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, in GridIndex extend)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, extend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, lowerExtend, upperExtend));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndex pivot, bool byRow)
            => GetIndexedValuesIn(this.Size.IndexRange(pivot, byRow));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridIndexRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new ArrayGrid<T>.GridIndexedValuesIn(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(in GridRange range)
        {
            var enumerator = this.Size.ClampIndexRange(range).GetEnumerator();
            return new ArrayGrid<T>.GridIndexedValuesIn(GetSource(), enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(IEnumerable<GridIndex> indices)
            => new ArrayGrid<T>.GridIndexedValuesIn(GetSource(), indices?.GetEnumerator());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayGrid<T>.GridIndexedValuesIn GetIndexedValuesIn(IEnumerator<GridIndex> indices)
            => new ArrayGrid<T>.GridIndexedValuesIn(GetSource(), indices);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetAt(uint index, out GridIndex key, out T value)
            => GetSource().data.GetAt(index, out key, out value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetAt(uint index, out GridIndex key, out T value)
        {
            if (this.source == null)
            {
                key = default;
                value = default;
                return false;
            }

            return this.source.data.TryGetAt(index, out key, out value);
        }

        public static ReadArrayGrid<T> Empty { get; } = new ReadArrayGrid<T>(ArrayGrid<T>.Empty);

        public static implicit operator ReadArrayGrid<T>(ArrayGrid<T> source)
            => source == null ? Empty : new ReadArrayGrid<T>(source);
    }
}