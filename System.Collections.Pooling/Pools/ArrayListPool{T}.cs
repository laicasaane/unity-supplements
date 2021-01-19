using System.Collections.ArrayBased;
using System.Collections.Generic;

namespace System.Collections.Pooling
{
    public static class ArrayListPool<T>
    {
        private static readonly Pool<ArrayList<T>> _pool = new Pool<ArrayList<T>>();

        public static ArrayList<T> Get()
            => _pool.Get();

        public static void Return(ArrayList<T> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params ArrayList<T>[] items)
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

        public static void Return(IEnumerable<ArrayList<T>> items)
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