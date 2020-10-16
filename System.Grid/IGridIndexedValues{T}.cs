using System.Collections.Generic;

namespace System.Grid
{
    public interface IGridIndexedValues<T> : IEnumerable<GridValue<T>>
    {
        new IGridIndexedValueEnumerator<T> GetEnumerator();
    }
}