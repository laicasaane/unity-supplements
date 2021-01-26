using System.Collections.Generic;

namespace System.Grid
{
    public interface IReadOnlyGrid<T>
    {
        GridSize Size { get; }

        T this[in GridIndex key] { get; }

        IEnumerable<GridIndex> Indices { get; }

        IEnumerable<T> Values { get; }

        bool ContainsIndex(in GridIndex index);

        bool ContainsValue(T value);

        void GetValues(ICollection<T> output);

        void GetValues(in GridIndex pivot, int extend, ICollection<T> output);

        void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output);

        void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output);

        void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output);

        void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output);

        void GetValues(in GridIndexRange range, ICollection<T> output);

        void GetValues(in GridRange range, ICollection<T> output);

        void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output);

        void GetValues(IEnumerator<GridIndex> indices, ICollection<T> output);

        void GetValues(ICollection<T> output, bool allowDuplicate);

        void GetValues(in GridIndex pivot, int extend, ICollection<T> output, bool allowDuplicate);

        void GetValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<T> output, bool allowDuplicate);

        void GetValues(in GridIndex pivot, in GridIndex extend, ICollection<T> output, bool allowDuplicate);

        void GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<T> output, bool allowDuplicate);

        void GetValues(in GridIndex pivot, bool byRow, ICollection<T> output, bool allowDuplicate);

        void GetValues(in GridIndexRange range, ICollection<T> output, bool allowDuplicate);

        void GetValues(in GridRange range, ICollection<T> output, bool allowDuplicate);

        void GetValues(IEnumerable<GridIndex> indices, ICollection<T> output, bool allowDuplicate);

        void GetValues(IEnumerator<GridIndex> indices, ICollection<T> output, bool allowDuplicate);

        IGridValues<T> GetValues();

        IGridValues<T> GetValues(in GridIndex pivot, int extend);

        IGridValues<T> GetValues(in GridIndex pivot, int lowerExtend, int upperExtend);

        IGridValues<T> GetValues(in GridIndex pivot, in GridIndex extend);

        IGridValues<T> GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend);

        IGridValues<T> GetValues(in GridIndex pivot, bool byRow);

        IGridValues<T> GetValues(in GridIndexRange range);

        IGridValues<T> GetValues(in GridRange range);

        IGridValues<T> GetValues(IEnumerable<GridIndex> indices);

        IGridValues<T> GetValues(IEnumerator<GridIndex> indices);

        void GetIndexedValues(ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, int extend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, in GridIndex extend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndex pivot, bool byRow, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridIndexRange range, ICollection<GridValue<T>> output);

        void GetIndexedValues(in GridRange range, ICollection<GridValue<T>> output);

        void GetIndexedValues(IEnumerable<GridIndex> indices, ICollection<GridValue<T>> output);

        void GetIndexedValues(IEnumerator<GridIndex> indices, ICollection<GridValue<T>> output);

        IGridIndexedValues<T> GetIndexedValues();

        IGridIndexedValues<T> GetIndexedValues(in GridIndex pivot, int extend);

        IGridIndexedValues<T> GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend);

        IGridIndexedValues<T> GetIndexedValues(in GridIndex pivot, in GridIndex extend);

        IGridIndexedValues<T> GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend);

        IGridIndexedValues<T> GetIndexedValues(in GridIndex pivot, bool byRow);

        IGridIndexedValues<T> GetIndexedValues(in GridIndexRange range);

        IGridIndexedValues<T> GetIndexedValues(in GridRange range);

        IGridIndexedValues<T> GetIndexedValues(IEnumerable<GridIndex> indices);

        IGridIndexedValues<T> GetIndexedValues(IEnumerator<GridIndex> indices);
    }
}