namespace System.Collections.Generic
{
    using ConcurrentPool = System.Collections.Concurrent.ConcurrentPool;

    public static class QueueTExtensions
    {
        public static bool ValidateIndex<T>(this Queue<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static void Enqueue<T>(this Queue<T> self, T item, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null ||
                (!allowNull && item == null) ||
                (!allowDuplicate && self.Contains(item)))
                return;

            self.Enqueue(item);
        }

        public static void Enqueue<T>(this Queue<T> self, object item)
            => self.Enqueue(item, true);

        public static void Enqueue<T>(this Queue<T> self, object item, bool allowDuplicate)
        {
            if (self == null ||
                !(item is T itemT) ||
                (!allowDuplicate && self.Contains(itemT)))
                return;

            self.Enqueue(itemT);
        }

        public static void EnqueueRange<T>(this Queue<T> self, IEnumerable<T> collection)
            => self.EnqueueRange(collection, false);

        public static void EnqueueRange<T>(this Queue<T> self, IEnumerable<T> collection, bool allowDuplicate, bool allowNull = false)
            => self.EnqueueRange(collection?.GetEnumerator(), allowDuplicate, allowNull);

        public static void EnqueueRange<T>(this Queue<T> self, IEnumerable<object> collection)
            => self.EnqueueRange(collection, true);

        public static void EnqueueRange<T>(this Queue<T> self, IEnumerable<object> collection, bool allowDuplicate)
            => self.EnqueueRange(collection?.GetEnumerator(), allowDuplicate);

        public static void EnqueueRange<T>(this Queue<T> self, IEnumerator<T> enumerator)
            => self.EnqueueRange(enumerator, true);

        public static void EnqueueRange<T>(this Queue<T> self, IEnumerator<T> enumerator, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null || enumerator == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    T item = enumerator.Current;

                    if (allowNull || item != null)
                        self.Enqueue(item);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;

                if ((allowNull || item != null) && !self.Contains(item))
                    self.Enqueue(item);
            }
        }

        public static void EnqueueRange<T>(this Queue<T> self, IEnumerator<object> enumerator)
            => self.EnqueueRange(enumerator, true);

        public static void EnqueueRange<T>(this Queue<T> self, IEnumerator<object> enumerator, bool allowDuplicate)
        {
            if (self == null || enumerator == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is T itemT)
                        self.Enqueue(itemT);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current is T itemT && !self.Contains(itemT))
                    self.Enqueue(itemT);
            }
        }

        public static void EnqueueRange<T>(this Queue<T> self, params T[] items)
            => self.EnqueueRange(true, items);

        public static void EnqueueRange<T>(this Queue<T> self, bool allowDuplicate, params T[] items)
            => self.EnqueueRange(allowDuplicate, false, items);

        public static void EnqueueRange<T>(this Queue<T> self, bool allowDuplicate, bool allowNull, params T[] items)
        {
            if (self == null || items == null)
                return;

            if (allowDuplicate)
            {
                foreach (var item in items)
                {
                    if (allowNull || item != null)
                        self.Enqueue(item);
                }

                return;
            }

            foreach (var item in items)
            {
                if ((allowNull || item != null) && !self.Contains(item))
                    self.Enqueue(item);
            }
        }

        public static void EnqueueRange<T>(this Queue<T> self, params object[] items)
            => self.EnqueueRange(true, items);

        public static void EnqueueRange<T>(this Queue<T> self, bool allowDuplicate, params object[] items)
        {
            if (self == null || items == null)
                return;

            if (allowDuplicate)
            {
                foreach (var item in items)
                {
                    if (item is T itemT)
                        self.Enqueue(itemT);
                }

                return;
            }

            foreach (var item in items)
            {
                if (item is T itemT && !self.Contains(itemT))
                    self.Enqueue(itemT);
            }
        }

        public static void DequeueRange<T>(this Queue<T> self, in ReadRange<int> range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.DequeueRange(start + 1, end - start, output, allowDuplicate, allowNull);
        }

        public static void DequeueRange<T>(this Queue<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.DequeueRange(offset, -1, output, allowDuplicate, allowNull);

        public static void DequeueRange<T>(this Queue<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            offset = Math.Max(offset, 0);

            if (offset > self.Count)
                throw new IndexOutOfRangeException(nameof(offset));

            if (count < 0)
                count = self.Count - offset;
            else
                count += offset;

            if (count > self.Count)
                throw new IndexOutOfRangeException(nameof(count));

            var cache = ConcurrentPool.Provider.Queue<T>();

            if (allowDuplicate)
            {
                DequeueRangeWithDuplicate(self, offset, count, allowNull, output, cache);
            }
            else
            {
                DequeueRangeNoDuplicate(self, offset, count, allowNull, output, cache);
            }

            while (cache.Count > 0)
            {
                self.Enqueue(cache.Dequeue());
            }

            ConcurrentPool.Provider.Return(cache);
        }

        private static void DequeueRangeWithDuplicate<T>(Queue<T> self, int offset, int count, bool allowNull,
                                                         ICollection<T> output, Queue<T> cache)
        {
            var o = 0;
            var c = 0;

            while (self.Count > 0)
            {
                var item = self.Dequeue();

                if (o < offset)
                {
                    cache.Enqueue(item);
                    o += 1;
                    continue;
                }

                if (c >= count)
                {
                    cache.Enqueue(item);
                    continue;
                }

                if (allowNull || item != null)
                    output.Add(item);
                else
                    cache.Enqueue(item);

                c += 1;
            }
        }

        private static void DequeueRangeNoDuplicate<T>(Queue<T> self, int offset, int count, bool allowNull,
                                                       ICollection<T> output, Queue<T> cache)
        {
            var o = 0;
            var c = 0;

            while (self.Count > 0)
            {
                var item = self.Dequeue();

                if (o < offset)
                {
                    cache.Enqueue(item);
                    o += 1;
                    continue;
                }

                if (c >= count)
                {
                    cache.Enqueue(item);
                    continue;
                }

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);
                else
                    cache.Enqueue(item);

                c += 1;
            }
        }
    }
}
