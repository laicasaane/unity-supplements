using System.Collections.Generic;

namespace System.Collections.Pooling
{
    public interface IGetOnlyPool<T>
    {
        T Get();
    }

    public interface IReturnOnlyPool<T>
    {
        void Return(T item);

        void Return(params T[] items);

        void Return(IEnumerable<T> items);
    }

    public interface IPool<T> : IReturnOnlyPool<T>, IGetOnlyPool<T>
    {
    }
}