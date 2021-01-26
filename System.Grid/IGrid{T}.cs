using System.Collections.Generic;

namespace System.Grid
{
    public interface IGrid<T> : IReadOnlyGrid<T>
    {
        void Initialize(in GridSize size, IEnumerable<KeyValuePair<GridIndex, T>> data);

        void Initialize(in GridSize size, IEnumerable<GridValue<T>> data);

        void Initialize(in GridSize size, IEnumerator<KeyValuePair<GridIndex, T>> data);

        void Initialize(in GridSize size, IEnumerator<GridValue<T>> data);

        void Initialize(IGrid<T> source);

        void Set(in GridIndex index, T value);

        void Set(in GridIndex index, in T value);

        bool TrySet(in GridIndex index, T value);

        bool TrySet(in GridIndex index, in T value);

        void Clear();
    }
}