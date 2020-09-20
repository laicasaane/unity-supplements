namespace System.Collections.Generic
{
    public static class QueuePool<T>
    {
        private static readonly Pool<Queue<T>> _pool = new Pool<Queue<T>>();

        public static Queue<T> Get()
            => _pool.Get();

        public static void Return(Queue<T> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params Queue<T>[] items)
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

        public static void Return(IEnumerable<Queue<T>> items)
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