using System.Collections.Generic;

namespace System
{
    public interface IRange<out TValue, out TEnumerator> : IRange<TValue>
        where TValue : struct
        where TEnumerator : IEnumerator<TValue>
    {
        new TEnumerator Range();
    }
}