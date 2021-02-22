using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class ConcurrentBagPool<T>
    {
        private static readonly ConcurrentPool<ConcurrentBag<T>> _pool = new ConcurrentPool<ConcurrentBag<T>>();

        public static ConcurrentBag<T> Get()
            => _pool.Get();

        public static void Return(ConcurrentBag<T> item)
        {
            if (item == null)
                return;

            while (item.TryTake(out _))
            {
            }

            _pool.Return(item);
        }

        public static void Return(params ConcurrentBag<T>[] items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                Return(item);
            }
        }

        public static void Return(IEnumerable<ConcurrentBag<T>> items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                Return(item);
            }
        }

        public static void Clear()
            => _pool.Clear();
    }
}