using System.Collections.Concurrent;

namespace System.Collections.Generic
{
    public static class DictionaryPool<TKey, TValue>
    {
        private static readonly ConcurrentQueue<Dictionary<TKey, TValue>> _pool = new ConcurrentQueue<Dictionary<TKey, TValue>>();

        public static Dictionary<TKey, TValue> Get()
        {
            if (_pool.TryDequeue(out var item))
                return item;

            return new Dictionary<TKey, TValue>();
        }

        public static void Return(Dictionary<TKey, TValue> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Enqueue(item);
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
                _pool.Enqueue(item);
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
                _pool.Enqueue(item);
            }
        }
    }
}