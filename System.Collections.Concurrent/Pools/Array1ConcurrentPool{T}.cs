using System.Collections.Generic;

namespace System.Collections.Concurrent
{
    public static class Array1ConcurrentPool<T>
    {
        private static readonly PoolMap _poolMap = new PoolMap();

        public static T[] Get(int size)
        {
            if (_poolMap.TryGetValue(size, out var pool))
            {
                if (pool.TryDequeue(out var item))
                    return item;
            }
            else
            {
                _poolMap.TryAdd(size, new ConcurrentQueue<T[]>());
            }

            return new T[size];
        }

        public static void Return(T[] item)
        {
            if (item == null)
                return;

            item.Clear();
            Return(item.Length, item);
        }

        public static void Return(params T[][] items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                item.Clear();
                Return(item.Length, item);
            }
        }

        public static void Return(IEnumerable<T[]> items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                if (item == null)
                    continue;

                item.Clear();
                Return(item.Length, item);
            }
        }

        private static void Return(int size, T[] item)
        {
            if (!_poolMap.TryGetValue(size, out var pool))
            {
                _poolMap.TryAdd(size, pool = new ConcurrentQueue<T[]>());
            }

            pool.Enqueue(item);
        }

        private class PoolMap : ConcurrentDictionary<int, ConcurrentQueue<T[]>> { }
    }
}