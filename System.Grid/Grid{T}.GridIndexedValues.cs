using System.Collections;
using System.Collections.Generic;

namespace System.Grid
{
    public partial class Grid<T>
    {
        public readonly struct GridIndexedValues : IGridIndexedValues<T>
        {
            private readonly Grid<T> source;
            private readonly IEnumerator<GridIndex> enumerator;

            public GridIndexedValues(Grid<T> grid, IEnumerator<GridIndex> enumerator)
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

            public readonly struct Enumerator : IGridIndexedValueEnumerator<T>
            {
                private readonly ReadDictionary<GridIndex, T> source;
                private readonly IEnumerator<GridIndex> enumerator;
                private readonly bool hasSource;

                public Enumerator(Grid<T> grid, IEnumerator<GridIndex> enumerator)
                {
                    this.source = grid?.data ?? ReadDictionary<GridIndex, T>.Empty;
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
                        this.source.TryGetValue(key, out var value);
                        return new GridValue<T>(in key, value);
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