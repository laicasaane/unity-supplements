namespace System.Collections.Generic
{
    public static class StackPool<T>
    {
        private static readonly Pool<Stack<T>> _pool = new Pool<Stack<T>>();

        public static Stack<T> Get()
            => _pool.Get();

        public static void Return(Stack<T> item)
        {
            if (item == null)
                return;

            item.Clear();
            _pool.Return(item);
        }

        public static void Return(params Stack<T>[] items)
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

        public static void Return(IEnumerable<Stack<T>> items)
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