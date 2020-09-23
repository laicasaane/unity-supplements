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

        IEnumerable<T> GetValues(in GridIndex pivot, int extend);

        IEnumerable<T> GetValues(in GridIndex pivot, int lowerExtend, int upperExtend);

        IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex extend);

        IEnumerable<T> GetValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend);

        IEnumerable<T> GetValues(in GridIndex pivot, bool byRow);

        IEnumerable<T> GetValues(GridIndexRange range);

        IEnumerable<T> GetValues(GridRange range);

        IEnumerable<T> GetValues(IEnumerable<GridIndex> indices);

        IEnumerable<T> GetValues(IEnumerator<GridIndex> indices);

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

        IEnumerable<GridValue<T>> GetIndexedValues();

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int extend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, int lowerExtend, int upperExtend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex extend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, in GridIndex lowerExtend, in GridIndex upperExtend);

        IEnumerable<GridValue<T>> GetIndexedValues(in GridIndex pivot, bool byRow);

        IEnumerable<GridValue<T>> GetIndexedValues(GridIndexRange range);

        IEnumerable<GridValue<T>> GetIndexedValues(GridRange range);

        IEnumerable<GridValue<T>> GetIndexedValues(IEnumerable<GridIndex> indices);

        IEnumerable<GridValue<T>> GetIndexedValues(IEnumerator<GridIndex> indices);
    }
}