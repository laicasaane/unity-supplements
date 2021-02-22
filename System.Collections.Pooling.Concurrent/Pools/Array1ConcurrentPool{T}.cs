using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class Array1ConcurrentPool<T>
    {
        private static readonly PoolMap _poolMap = new PoolMap();

        public static T[] Get(int size)
            => Get((long)size);

        public static T[] Get(long size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size), "Must be a positive number.");

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
            Return(item.LongLength, item);
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
                Return(item.LongLength, item);
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
                Return(item.LongLength, item);
            }
        }

        private static void Return(long size, T[] item)
        {
            if (!_poolMap.TryGetValue(size, out var pool))
            {
                _poolMap.TryAdd(size, pool = new ConcurrentQueue<T[]>());
            }

            pool.Enqueue(item);
        }

        public static void Clear()
        {
            foreach (var kv in _poolMap)
            {
                while (kv.Value.Count > 0)
                {
                    kv.Value.TryDequeue(out _);
                }
            }

            _poolMap.Clear();
        }

        private class PoolMap : ConcurrentDictionary<long, ConcurrentQueue<T[]>> { }
    }
}