using System.Collections;
using System.Collections.Generic;

namespace System.Grid
{
    public partial class Grid<T>
    {
        public readonly struct GridValues : IGridValues<T>
        {
            private readonly Grid<T> source;
            private readonly IEnumerator<GridIndex> enumerator;

            public GridValues(Grid<T> grid, IEnumerator<GridIndex> enumerator = null)
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
                private readonly ReadDictionary<GridIndex, T> source;
                private readonly IEnumerator<GridIndex> enumerator;
                private readonly bool hasSource;

                public Enumerator(Grid<T> grid, IEnumerator<GridIndex> enumerator = null)
                {
                    this.source = grid?.data ?? ReadDictionary<GridIndex, T>.Empty;
                    this.enumerator = enumerator ?? this.source.Keys.GetEnumerator();
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