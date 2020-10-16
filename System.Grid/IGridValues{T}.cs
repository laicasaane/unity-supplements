using System.Collections.Generic;

namespace System.Grid
{
    public interface IGridValues<T> : IEnumerable<T>
    {
        new IGridValueEnumerator<T> GetEnumerator();
    }
}