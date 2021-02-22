using System.Collections.Generic;
using System.Collections.Concurrent;

namespace System.Collections.Pooling.Concurrent
{
    public partial class ConcurrentPool<T> : IPool<T> where T : class, new()
    {
        private readonly ConcurrentQueue<T> pool = new ConcurrentQueue<T>();

        public void Prepool(int count)
        {
            for (var i = 0; i < count; i++)
            {
                this.pool.Enqueue(new T());
            }
        }

        public T Get()
        {
            if (this.pool.TryDequeue(out var item))
                return item;

            return new T();
        }

        public void Return(T item)
        {
            if (item == null)
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

        public void Clear()
        {
            while (this.pool.Count > 0)
            {
                this.pool.TryDequeue(out _);
            }
        }

        public static ConcurrentPool<T> Default { get; } = new ConcurrentPool<T>();
    }
}
