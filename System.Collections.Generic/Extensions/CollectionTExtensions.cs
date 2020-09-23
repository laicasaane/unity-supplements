namespace System.Collections.Generic
{
    public static class CollectionTExtensions
    {
        public static bool ValidateIndex<T>(this ICollection<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(this IReadOnlyCollection<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static ReadCollection<T> AsReadCollection<T>(this ICollection<T> self)
            => new ReadCollection<T>(self);

        public static void Add<T>(this ICollection<T> self, T item, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null ||
                (!allowNull && item == null) ||
                (!allowDuplicate && self.Contains(item)))
                return;

            self.Add(item);
        }

        public static void Add<T>(this ICollection<T> self, object item)
            => self.Add(item, true);

        public static void Add<T>(this ICollection<T> self, object item, bool allowDuplicate)
        {
            if (self == null ||
                !(item is T itemT) ||
                (!allowDuplicate && self.Contains(itemT)))
                return;

            self.Add(itemT);
        }

        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> collection, bool allowNull = false)
            => self.AddRange(collection, true, allowNull);

        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> collection, bool allowDuplicate, bool allowNull = false)
            => self.AddRange(collection?.GetEnumerator(), allowDuplicate, allowNull);

        public static void AddRange<T>(this ICollection<T> self, IEnumerable<object> collection)
            => self.AddRange(collection, true);

        public static void AddRange<T>(this ICollection<T> self, IEnumerable<object> collection, bool allowDuplicate)
            => self.AddRange(collection?.GetEnumerator(), allowDuplicate);

        public static void AddRange<T>(this ICollection<T> self, IEnumerator<T> enumerator)
            => self.AddRange(enumerator, true);

        public static void AddRange<T>(this ICollection<T> self, IEnumerator<T> enumerator, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null || enumerator == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    T item = enumerator.Current;

                    if (allowNull || item != null)
                        self.Add(item);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;

                if ((allowNull || item != null) && !self.Contains(item))
                    self.Add(item);
            }
        }

        public static void AddRange<T>(this ICollection<T> self, IEnumerator<object> enumerator)
            => self.AddRange(enumerator, true);

        public static void AddRange<T>(this ICollection<T> self, IEnumerator<object> enumerator, bool allowDuplicate)
        {
            if (self == null || enumerator == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is T itemT)
                        self.Add(itemT);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current is T itemT && !self.Contains(itemT))
                    self.Add(itemT);
            }
        }

        public static void AddRange<T>(this ICollection<T> self, params T[] items)
            => self.AddRange(true, items);

        public static void AddRange<T>(this ICollection<T> self, bool allowDuplicate, params T[] items)
            => self.AddRange(allowDuplicate, false, items);

        public static void AddRange<T>(this ICollection<T> self, bool allowDuplicate, bool allowNull, params T[] items)
        {
            if (self == null || items == null)
                return;

            if (allowDuplicate)
            {
                foreach (var item in items)
                {
                    if (allowNull || item != null)
                        self.Add(item);
                }

                return;
            }

            foreach (var item in items)
            {
                if ((allowNull || item != null) && !self.Contains(item))
                    self.Add(item);
            }
        }

        public static void AddRange<T>(this ICollection<T> self, params object[] items)
            => self.AddRange(true, items);

        public static void AddRange<T>(this ICollection<T> self, bool allowDuplicate, params object[] items)
        {
            if (self == null || items == null)
                return;

            if (allowDuplicate)
            {
                foreach (var item in items)
                {
                    if (item is T itemT)
                        self.Add(itemT);
                }

                return;
            }

            foreach (var item in items)
            {
                if (item is T itemT && !self.Contains(itemT))
                    self.Add(itemT);
            }
        }

        public static void GetRange<T>(this ICollection<T> self, in ReadRange<int> range, ICollection<T> output)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start + 1, end - start, output);
        }

        public static void GetRange<T>(this ICollection<T> self, int offset, ICollection<T> output)
            => self.GetRange(offset, -1, output);

        public static void GetRange<T>(this ICollection<T> self, int offset, int count, ICollection<T> output)
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

            var o = 0;
            var c = 0;

            foreach (var item in self)
            {
                if (o < offset)
                {
                    o += 1;
                    continue;
                }

                if (c >= count)
                    break;

                output.Add(item);
                c += 1;
            }
        }

        public static void GetRange<T>(this IReadOnlyCollection<T> self, in ReadRange<int> range, ICollection<T> output)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start + 1, end - start, output);
        }

        public static void GetRange<T>(this IReadOnlyCollection<T> self, int offset, ICollection<T> output)
            => self.GetRange(offset, -1, output);

        public static void GetRange<T>(this IReadOnlyCollection<T> self, int offset, int count, ICollection<T> output)
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

            var o = 0;
            var c = 0;

            foreach (var item in self)
            {
                if (o < offset)
                {
                    o += 1;
                    continue;
                }

                if (c >= count)
                    break;

                output.Add(item);
                c += 1;
            }
        }
    }
}
