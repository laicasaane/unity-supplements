using System.Collections.Generic;

namespace System
{
    public interface IRange<out T> where T : unmanaged, IEquatable<T>
    {
        T Start { get; }

        T End { get; }

        bool IsFromEnd { get; }

        IRange<T> FromStart();

        IRange<T> FromEnd();

        IEnumerator<T> Range();
    }
}