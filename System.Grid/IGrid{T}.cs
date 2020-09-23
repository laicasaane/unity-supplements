using System.Collections.Generic;

namespace System.Grid
{
    public interface IGrid<T> : IReadOnlyGrid<T>
    {
        void Initialize(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data);

        void Initialize(in GridSize size, IEnumerable<GridValue<T>> data);

        void Initialize(in GridSize size, IEnumerator<KeyValuePair<GridIndex, T>> data);

        void Initialize(in GridSize size, IEnumerator<GridValue<T>> data);

        void Initialize(Grid<T> data);

        void Clear();
    }
}