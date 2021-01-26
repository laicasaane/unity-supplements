using System.Collections;
using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Grid.ArrayBased
{
    public partial class ArrayGrid<T>
    {
        public readonly struct GridIndexedValuesIn : IGridIndexedValues<T>
        {
            private readonly ArrayGrid<T> source;
            private readonly IEnumerator<GridIndex> enumerator;

            public GridIndexedValuesIn(ArrayGrid<T> grid, IEnumerator<GridIndex> enumerator)
            {
                this.source = grid;
                this.enumerator = enumerator;
            }

            public Enumerator GetEnumerator()
                => new Enumerator(this.source, this.enumerator);

            IGridIndexedValueEnumerator<T> IGridIndexedValues<T>.GetEnumerator()
                => GetEnumerator();

            IEnumerator<GridValue<T>> IEnumerable<GridValue<T>>.GetEnumerator()
                => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            public struct Enumerator : IGridIndexedValueEnumerator<T>
            {
                private readonly ReadArrayDictionary<GridIndex, T> source;
                private readonly IEnumerator<GridIndex> enumerator;
                private readonly bool hasSource;

                public Enumerator(ArrayGrid<T> grid, IEnumerator<GridIndex> enumerator)
                {
                    this.source = grid?.data ?? ReadArrayDictionary<GridIndex, T>.Empty;
                    this.enumerator = enumerator;
                    this.hasSource = true;
                }

                public bool ContainsCurrent()
                    => this.enumerator != null &&
                       this.source.ContainsKey(this.enumerator.Current);

                public GridValue<T> Current
                {
                    get
                    {
                        var key = this.enumerator.Current;
                        this.source.TryGetValue(in key, out var value);
                        return new GridValue<T>(key, in value);
                    }
                }

                public bool MoveNext()
                    => this.hasSource && this.enumerator.MoveNext();

                object IEnumerator.Current
                    => this.Current;

                void IEnumerator.Reset()
                    => this.enumerator?.Reset();

                public void Dispose()
                    => this.enumerator?.Dispose();
            }
        }
    }
}