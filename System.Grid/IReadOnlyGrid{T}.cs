using System.Collections.Generic;

namespace System.Grid
{
    public interface IReadOnlyGrid<T>
    {
        GridIndex Size { get; }

        T this[in GridIndex key] { get; }

        IEnumerable<GridIndex> Indices { get; }

        IEnumerable<T> Values { get; }

        bool ValidateIndex(in GridIndex value);

        GridIndex LastIndex();

        GridIndex ClampIndex(in GridIndex value);

        GridIndexRange ClampIndexRange(in GridIndex start, in GridIndex end);

        GridIndexRange ClampIndexRange(in GridIndexRange range);

        GridIndexRange IndexRange(in GridIndex pivot, int extend);

        GridIndexRange IndexRange(in GridIndex pivot, in GridIndex extend);

        GridIndexRange IndexRange(in GridIndex pivot, bool row);

        GridIndexRange IndexRange();

        bool ContainsIndex(in GridIndex index);

        bool ContainsValue(T value);

        void GetValues(ICollection<T> output);

        void GetValues(in GridIndex pivot, int extend, ICollection<T> output);

        void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output);

        void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output);

        void GetValues(in GridIndexRange range, ICollection<T> output);

        IEnumerable<T> GetValues(in GridIndex pivot, int extend);

        IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend);

        IEnumerable<T> GetValues(in GridIndex pivot, bool byRow);

        IEnumerable<T> GetValues(GridIndexRange range);

        void GetIndexedValues(ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output);

        IEnumerable<GridValue<T>> GetIndexedValues();

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow);

        IEnumerable<GridValue<T>> GetIndexedValues(GridIndexRange range);
    }
}