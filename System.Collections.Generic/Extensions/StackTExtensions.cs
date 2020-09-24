namespace System.Collections.Generic
{
    public static class StackTExtensions
    {
        public static bool ValidateIndex<T>(this Stack<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static void Push<T>(this Stack<T> self, T item, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null ||
                (!allowNull && item == null) ||
                (!allowDuplicate && self.Contains(item)))
                return;

            self.Push(item);
        }

        public static void Push<T>(this Stack<T> self, object item)
            => self.Push(item, true);

        public static void Push<T>(this Stack<T> self, object item, bool allowDuplicate)
        {
            if (self == null ||
                !(item is T itemT) ||
                (!allowDuplicate && self.Contains(itemT)))
                return;

            self.Push(itemT);
        }

        public static void PushRange<T>(this Stack<T> self, IEnumerable<T> collection)
            => self.PushRange(collection, false);

        public static void PushRange<T>(this Stack<T> self, IEnumerable<T> collection, bool allowDuplicate, bool allowNull = false)
            => self.PushRange(collection?.GetEnumerator(), allowDuplicate, allowNull);

        public static void PushRange<T>(this Stack<T> self, IEnumerable<object> collection)
            => self.PushRange(collection, true);

        public static void PushRange<T>(this Stack<T> self, IEnumerable<object> collection, bool allowDuplicate)
            => self.PushRange(collection?.GetEnumerator(), allowDuplicate);

        public static void PushRange<T>(this Stack<T> self, IEnumerator<T> enumerator)
            => self.PushRange(enumerator, true);

        public static void PushRange<T>(this Stack<T> self, IEnumerator<T> enumerator, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null || enumerator == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    T item = enumerator.Current;

                    if (allowNull || item != null)
                        self.Push(item);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;

                if ((allowNull || item != null) && !self.Contains(item))
                    self.Push(item);
            }
        }

        public static void PushRange<T>(this Stack<T> self, IEnumerator<object> enumerator)
            => self.PushRange(enumerator, true);

        public static void PushRange<T>(this Stack<T> self, IEnumerator<object> enumerator, bool allowDuplicate)
        {
            if (self == null || enumerator == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is T itemT)
                        self.Push(itemT);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current is T itemT && !self.Contains(itemT))
                    self.Push(itemT);
            }
        }

        public static void PushRange<T>(this Stack<T> self, params T[] items)
            => self.PushRange(true, items);

        public static void PushRange<T>(this Stack<T> self, bool allowDuplicate, params T[] items)
            => self.PushRange(allowDuplicate, false, items);

        public static void PushRange<T>(this Stack<T> self, bool allowDuplicate, bool allowNull, params T[] items)
        {
            if (self == null || items == null)
                return;

            if (allowDuplicate)
            {
                foreach (var item in items)
                {
                    if (allowNull || item != null)
                        self.Push(item);
                }

                return;
            }

            foreach (var item in items)
            {
                if ((allowNull || item != null) && !self.Contains(item))
                    self.Push(item);
            }
        }

        public static void PushRange<T>(this Stack<T> self, params object[] items)
            => self.PushRange(true, items);

        public static void PushRange<T>(this Stack<T> self, bool allowDuplicate, params object[] items)
        {
            if (self == null || items == null)
                return;

            if (allowDuplicate)
            {
                foreach (var item in items)
                {
                    if (item is T itemT)
                        self.Push(itemT);
                }

                return;
            }

            foreach (var item in items)
            {
                if (item is T itemT && !self.Contains(itemT))
                    self.Push(itemT);
            }
        }

        public static void PopRange<T>(this Stack<T> self, in ReadRange<int> range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.PopRange(start + 1, end - start, output, allowDuplicate, allowNull);
        }

        public static void PopRange<T>(this Stack<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.PopRange(offset, -1, output, allowDuplicate, allowNull);

        public static void PopRange<T>(this Stack<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
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

            var cache = StackPool<T>.Get();

            if (allowDuplicate)
            {
                PopRangeWithDuplicate(self, offset, count, allowNull, output, cache);
            }
            else
            {
                PopRangeNoDuplicate(self, offset, count, allowNull, output, cache);
            }

            while (cache.Count > 0)
            {
                self.Push(cache.Pop());
            }

            StackPool<T>.Return(cache);
        }

        private static void PopRangeWithDuplicate<T>(Stack<T> self, int offset, int count, bool allowNull,
                                                     ICollection<T> output, Stack<T> cache)
        {
            var o = 0;
            var c = 0;

            while (self.Count > 0)
            {
                var item = self.Pop();

                if (o < offset)
                {
                    cache.Push(item);
                    o += 1;
                    continue;
                }

                if (c >= count)
                {
                    cache.Push(item);
                    continue;
                }

                if (allowNull || item != null)
                    output.Add(item);
                else
                    cache.Push(item);

                c += 1;
            }
        }

        private static void PopRangeNoDuplicate<T>(Stack<T> self, int offset, int count, bool allowNull,
                                                   ICollection<T> output, Stack<T> cache)
        {
            var o = 0;
            var c = 0;

            while (self.Count > 0)
            {
                var item = self.Pop();

                if (o < offset)
                {
                    cache.Push(item);
                    o += 1;
                    continue;
                }

                if (c >= count)
                {
                    cache.Push(item);
                    continue;
                }

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);
                else
                    cache.Push(item);

                c += 1;
            }
        }
    }
}
