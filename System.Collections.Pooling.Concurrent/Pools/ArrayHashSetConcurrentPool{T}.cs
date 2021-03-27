using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class ArrayHashSetConcurrentPool<T>
    {
        private static readonly ConcurrentPool<ArrayHashSet<T>> _pool = new ConcurrentPool<ArrayHashSet<T>>();

        public static ArrayHashSet<T> Get()
            => _pool.Get();

        public static void Return(ArrayHashSet<T> item)
            => Return(false, item);

        public static void Return(bool shallowClear, ArrayHashSet<T> item)
        {
            if (item == null)
                return;

            if (shallowClear)
                item.ShallowClear();
            else
                item.Clear();

            _pool.Return(item);
        }

        public static void Return(params ArrayHashSet<T>[] items)
            => Return(false, items);

        public static void Return(bool shallowClear, params ArrayHashSet<T>[] items)
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

        public static void Return(IEnumerable<ArrayHashSet<T>> items)
            => Return(false, items);

        public static void Return(bool shallowClear, IEnumerable<ArrayHashSet<T>> items)
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