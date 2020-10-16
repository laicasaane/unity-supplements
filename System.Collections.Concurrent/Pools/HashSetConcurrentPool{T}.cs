using System.Collections.Generic;

namespace System.Collections.Concurrent
{
    public static class HashSetConcurrentPool<T>
    {
        private static readonly ConcurrentPool<HashSet<T>> _pool = new ConcurrentPool<HashSet<T>>();

        public static HashSet<T> Get()
            => _pool.Get();

        public static void Return(HashSet<T> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params HashSet<T>[] items)
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

        public static void Return(IEnumerable<HashSet<T>> items)
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