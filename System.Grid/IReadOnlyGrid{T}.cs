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

        GridRange ClampIndexRange(in GridIndex start, in GridIndex end);

        GridRange ClampIndexRange(in GridIndexRange range);

        GridRange ClampIndexRange(in GridRange range);

        GridRange IndexRange(in GridIndex pivot, int extend);

        GridRange IndexRange(in GridIndex pivot, in GridIndex extend);

        GridRange IndexRange(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend);

        GridRange IndexRange(in GridIndex pivot, bool row);

        GridRange IndexRange();

        bool ContainsIndex(in GridIndex index);

        bool ContainsValue(T value);

        void GetValues(ICollection<T> output);

        void GetValues(in GridIndex pivot, int extend, ICollection<T> output);

        void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output);

        void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output);

        void GetValues(in GridIndexRange range, ICollection<T> output);

        void GetValues(in GridRange range, ICollection<T> output);

        void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output);

        void GetValues(IEnumerator<GridIndex> indices, ICollection<T> output);

        IEnumerable<T> GetValues(in GridIndex pivot, int extend);

        IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend);

        IEnumerable<T> GetValues(in GridIndex pivot, bool byRow);

        IEnumerable<T> GetValues(GridIndexRange range);

        IEnumerable<T> GetValues(GridRange range);

        IEnumerable<T> GetValues(IEnumerable<GridIndex> indices);

        IEnumerable<T> GetValues(IEnumerator<GridIndex> indices);

        void GetIndexedValues(ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridRange range, ICollection<GridValue<T>> output);

        void GetIndexedValues(IEnumerable<GridIndex> indices, ICollection<GridValue<T>> output);

        void GetIndexedValues(IEnumerator<GridIndex> indices, ICollection<GridValue<T>> output);

        IEnumerable<GridValue<T>> GetIndexedValues();

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow);

        IEnumerable<GridValue<T>> GetIndexedValues(GridIndexRange range);

        IEnumerable<GridValue<T>> GetIndexedValues(GridRange range);

        IEnumerable<GridValue<T>> GetIndexedValues(IEnumerable<GridIndex> indices);

        IEnumerable<GridValue<T>> GetIndexedValues(IEnumerator<GridIndex> indices);
    }
}