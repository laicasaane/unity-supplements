using System.Collections.Generic;

namespace System.Grid
{
    public interface IGrid<T> : IReadOnlyGrid<T>
    {
        void Initialize(in GridIndex size, IEnumerable<KeyValuePair<GridIndex, T>> data);

        void Initialize(in GridIndex size, IEnumerable<GridValue<T>> data);

        void Initialize(in GridIndex size, IEnumerator<KeyValuePair<GridIndex, T>> data);

        void Initialize(in GridIndex size, IEnumerator<GridValue<T>> data);

        void Initialize(Grid<T> data);

        void Clear();
    }
}