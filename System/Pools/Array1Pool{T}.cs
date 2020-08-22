using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System
{
    public static class Array1Pool<T>
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

            Clear(item);
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

                Clear(item);
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

                Clear(item);
                Return(item.Length, item);
            }
        }

        private static void Return(int size, T[] item)
        {
            if (!_poolMap.TryGetValue(size, out var pool))
            {
                pool = new ConcurrentQueue<T[]>();

                if (!_poolMap.TryAdd(size, pool))
                    return;
            }

            pool.Enqueue(item);
        }

        private static void Clear(T[] self)
        {
            for (var i = 0; i < self.Length; i++)
            {
                self[i] = default;
            }
        }

        private class PoolMap : ConcurrentDictionary<int, ConcurrentQueue<T[]>> { }
    }
}