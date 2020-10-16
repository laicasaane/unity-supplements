using System.Collections.Generic;

namespace System.Collections.Concurrent
{
    public static class DictionaryConcurrentPool<TKey, TValue>
    {
        private static readonly ConcurrentPool<Dictionary<TKey, TValue>> _pool = new ConcurrentPool<Dictionary<TKey, TValue>>();

        public static Dictionary<TKey, TValue> Get()
            => _pool.Get();

        public static void Return(Dictionary<TKey, TValue> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params Dictionary<TKey, TValue>[] items)
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

        public static void Return(IEnumerable<Dictionary<TKey, TValue>> items)
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