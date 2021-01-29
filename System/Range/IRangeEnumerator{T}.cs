using System.Collections.Generic;

namespace System
{
    public interface IRangeEnumerator<T> where T : struct
    {
        IEnumerator<T> Enumerate(T start, T end, bool fromEnd);
    }
}