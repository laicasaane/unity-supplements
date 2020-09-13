using System.Collections.Generic;

namespace System
{
    public interface IRangeEnumerator<T>
    {
        IEnumerator<T> Enumerate(T start, T end);
    }
}