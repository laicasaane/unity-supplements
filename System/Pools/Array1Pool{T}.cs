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
                if (pool.Count > 0)
                    return pool.Dequeue();
            }
            else
            {
                _poolMap.Add(size, new Queue<T[]>());
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
                _poolMap.Add(size, pool = new Queue<T[]>());
            }

            pool.Enqueue(item);
        }

        private class PoolMap : Dictionary<int, Queue<T[]>> { }
    }
}