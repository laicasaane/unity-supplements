using System.Collections.Generic;

namespace System.Grid
{
    public interface IGridIndexedValueEnumerator<T> : IEnumerator<GridValue<T>>
    {
        bool ContainsCurrent();
    }
}