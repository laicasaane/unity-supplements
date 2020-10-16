using System.Collections.Generic;

namespace System.Grid
{
    public interface IGridValueEnumerator<T> : IEnumerator<T>
    {
        bool ContainsCurrent();
    }
}