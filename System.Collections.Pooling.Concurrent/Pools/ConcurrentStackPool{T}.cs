using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class ConcurrentStackPool<T>
    {
        private static readonly ConcurrentPool<ConcurrentStack<T>> _pool = new ConcurrentPool<ConcurrentStack<T>>();

        public static ConcurrentStack<T> Get()
            => _pool.Get();

        public static void Return(ConcurrentStack<T> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params ConcurrentStack<T>[] items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                item.Clear();
                _pool.Return(item);
            }
        }

        public static void Return(IEnumerable<ConcurrentStack<T>> items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                item.Clear();
                _pool.Return(item);
            }
        }
    }
}