using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Collections.Pooling.Concurrent
{
    public static class ArrayListConcurrentPool<T>
    {
        private static readonly ConcurrentPool<ArrayList<T>> _pool = new ConcurrentPool<ArrayList<T>>();

        public static ArrayList<T> Get()
            => _pool.Get();

        public static void Return(ArrayList<T> item)
            => Return(false, item);

        public static void Return(bool shallowClear, ArrayList<T> item)
        {
            if (item == null)
                return;

            if (shallowClear)
                item.ShallowClear();
            else
                item.Clear();

            _pool.Return(item);
        }

        public static void Return(params ArrayList<T>[] items)
            => Return(false, items);

        public static void Return(bool shallowClear, params ArrayList<T>[] items)
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

        public static void Return(IEnumerable<ArrayList<T>> items)
            => Return(false, items);

        public static void Return(bool shallowClear, IEnumerable<ArrayList<T>> items)
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