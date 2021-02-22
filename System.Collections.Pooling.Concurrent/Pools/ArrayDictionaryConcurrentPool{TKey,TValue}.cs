using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class ArrayDictionaryConcurrentPool<TKey, TValue>
    {
        private static readonly ConcurrentPool<ArrayDictionary<TKey, TValue>> _pool = new ConcurrentPool<ArrayDictionary<TKey, TValue>>();

        public static ArrayDictionary<TKey, TValue> Get()
            => _pool.Get();

        public static void Return(ArrayDictionary<TKey, TValue> item)
            => Return(false, item);

        public static void Return(bool shallowClear, ArrayDictionary<TKey, TValue> item)
        {
            if (item == null)
                return;

            if (shallowClear)
                item.ShallowClear();
            else
                item.Clear();

            _pool.Return(item);
        }

        public static void Return(params ArrayDictionary<TKey, TValue>[] items)
            => Return(false, items);

        public static void Return(bool shallowClear, params ArrayDictionary<TKey, TValue>[] items)
        {
            if (items == null)
                return;

            if (shallowClear)
            {
                foreach (var item in items)
                {
                    if (item == null)
                        continue;

                    item.ShallowClear();
                    _pool.Return(item);
                }
            }
            else
            {
                foreach (var item in items)
                {
                    if (item == null)
                        continue;

                    item.Clear();
                    _pool.Return(item);
                }
            }
        }

        public static void Return(IEnumerable<ArrayDictionary<TKey, TValue>> items)
            => Return(false, items);

        public static void Return(bool shallowClear, IEnumerable<ArrayDictionary<TKey, TValue>> items)
        {
            if (items == null)
                return;

            if (shallowClear)
            {
                foreach (var item in items)
                {
                    if (item == null)
                        continue;

                    item.ShallowClear();
                    _pool.Return(item);
                }
            }
            else
            {
                foreach (var item in items)
                {
                    if (item == null)
                        continue;

                    item.Clear();
                    _pool.Return(item);
                }
            }
        }

        public static void Clear()
            => _pool.Clear();
    }
}