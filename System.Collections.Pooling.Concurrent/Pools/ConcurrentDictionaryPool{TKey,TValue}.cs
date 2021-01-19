using System.Collections.Concurrent;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class ConcurrentDictionaryPool<TKey, TValue>
    {
        private static readonly ConcurrentPool<ConcurrentDictionary<TKey, TValue>> _pool = new ConcurrentPool<ConcurrentDictionary<TKey, TValue>>();

        public static ConcurrentDictionary<TKey, TValue> Get()
            => _pool.Get();

        public static void Return(ConcurrentDictionary<TKey, TValue> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params ConcurrentDictionary<TKey, TValue>[] items)
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

        public static void Return(IEnumerable<ConcurrentDictionary<TKey, TValue>> items)
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