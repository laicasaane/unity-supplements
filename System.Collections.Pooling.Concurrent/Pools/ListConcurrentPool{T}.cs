using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class ListConcurrentPool<T>
    {
        private static readonly ConcurrentPool<List<T>> _pool = new ConcurrentPool<List<T>>();

        public static List<T> Get()
            => _pool.Get();

        public static void Return(List<T> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params List<T>[] items)
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

        public static void Return(IEnumerable<List<T>> items)
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

        public static void Clear()
            => _pool.Clear();
    }
}