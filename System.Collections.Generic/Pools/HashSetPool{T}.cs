namespace System.Collections.Generic
{
    public static class HashSetPool<T>
    {
        private static readonly Pool<HashSet<T>> _pool = new Pool<HashSet<T>>();

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