using System.Collections.Generic;

namespace System.Collections.Pooling
{
    public class Pool<T> : IPool<T> where T : class, new()
    {
        private readonly Queue<T> pool = new Queue<T>();

        public void Prepool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                this.pool.Enqueue(new T());
            }
        }

        public T Get()
            => this.pool.Count > 0 ? this.pool.Dequeue() : new T();

        public void Return(T item)
        {
            if (item == null || this.pool.Contains(item))
                return;

            this.pool.Enqueue(item);
        }

        public void Return(params T[] items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                Return(item);
            }
        }

        public void Return(IEnumerable<T> items)
        {
            if (items == null)
                return;

            foreach (var item in items)
            {
                Return(item);
            }
        }

        public static Pool<T> Default { get; } = new Pool<T>();
    }
}
