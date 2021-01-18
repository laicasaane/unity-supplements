using System.Collections;
using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Grid.ArrayBased
{
    public partial class ArrayGrid<T>
    {
        public readonly struct GridValues : IGridValues<T>
        {
            private readonly ArrayGrid<T> source;
            private readonly IEnumerator<GridIndex> enumerator;

            public GridValues(ArrayGrid<T> grid, IEnumerator<GridIndex> enumerator)
            {
                this.source = grid;
                this.enumerator = enumerator;
            }

            public Enumerator GetEnumerator()
                => new Enumerator(this.source, this.enumerator);

            IGridValueEnumerator<T> IGridValues<T>.GetEnumerator()
                => GetEnumerator();

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
                => GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();

            public struct Enumerator : IGridValueEnumerator<T>
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

                public T Current
                {
                    get
                    {
                        var key = this.enumerator.Current;
                        this.source.TryGetValue(key, out var value);
                        return value;
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