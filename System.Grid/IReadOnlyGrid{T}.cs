using System.Collections.Generic;

namespace System.Grid
{
    public interface IReadOnlyGrid<T> : IReadOnlyCollection<KeyValuePair<GridIndex, T>>
    {
        T this[in GridIndex key] { get; }

        IEnumerable<GridIndex> Indices { get; }

        bool ContainsIndex(in GridIndex index);

        void GetIndices(in GridIndex pivot, int extend, ICollection<GridIndex> output);

        void GetIndices(in GridIndex pivot, in GridIndex extend, ICollection<GridIndex> output);

        void GetIndices(in GridIndex pivot, bool byRow, ICollection<GridIndex> output);

        void GetIndices(in ReadRange<GridIndex> range, ICollection<GridIndex> output);

        IEnumerable<GridIndex> GetIndices(in GridIndex pivot, int extend);

        IEnumerable<GridIndex> GetIndices(in GridIndex pivot, in GridIndex extend);

        IEnumerable<GridIndex> GetIndices(in GridIndex pivot, bool byRow);

        IEnumerable<GridIndex> GetIndices(ReadRange<GridIndex> range);

        void GetValues(in GridIndex pivot, int extend, ICollection<T> output);

        void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output);

        void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output);

        void GetValues(in ReadRange<GridIndex> range, ICollection<T> output);

        IEnumerable<T> GetValues(in GridIndex pivot, int extend);

        IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend);

        IEnumerable<T> GetValues(in GridIndex pivot, bool byRow);

        IEnumerable<T> GetValues(ReadRange<GridIndex> range);

        void GetIndexedValues(ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output);

        void GetIndexedValues(in ReadRange<GridIndex> range, ICollection<GridValue<T>> output);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow);

        IEnumerable<GridValue<T>> GetIndexedValues(ReadRange<GridIndex> range);
    }
}