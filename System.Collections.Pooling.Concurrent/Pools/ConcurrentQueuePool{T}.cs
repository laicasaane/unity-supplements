using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class ConcurrentQueuePool<T>
    {
        private static readonly ConcurrentPool<ConcurrentQueue<T>> _pool = new ConcurrentPool<ConcurrentQueue<T>>();

        public static ConcurrentQueue<T> Get()
            => _pool.Get();

        public static void Return(ConcurrentQueue<T> item)
        {
            if (item == null)
                return;

            while (item.TryDequeue(out _))
            {
            }

            _pool.Return(item);
        }

        public static void Return(params ConcurrentQueue<T>[] items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                Return(item);
            }
        }

        public static void Return(IEnumerable<ConcurrentQueue<T>> items)
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