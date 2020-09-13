using System.Collections.Generic;

namespace System.Grid
{
    public interface IReadOnlyGrid<T> : IReadOnlyCollection<KeyValuePair<GridIndex, T>>
    {
        GridIndex Size { get; }

        T this[in GridIndex key] { get; }

        IEnumerable<GridIndex> Indices { get; }

        bool ValidateIndex(in GridIndex value);

        GridIndex LastIndex();

        GridIndex ClampIndex(in GridIndex value);

        ReadRange<GridIndex> ClampIndexRange(in GridIndex start, in GridIndex end);

        ReadRange<GridIndex> ClampIndexRange(in ReadRange<GridIndex> range);

        ReadRange<GridIndex> IndexRange(in GridIndex pivot, int extend);

        ReadRange<GridIndex> IndexRange(in GridIndex pivot, in GridIndex extend);

        ReadRange<GridIndex> IndexRange(in GridIndex pivot, bool row);

        bool ContainsIndex(in GridIndex index);

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