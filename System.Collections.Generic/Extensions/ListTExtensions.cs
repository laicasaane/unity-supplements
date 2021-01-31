namespace System.Collections.Generic
{
    public static class ListTExtensions
    {
        public static bool ValidateIndex<T>(this List<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(this IList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(this IReadOnlyList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static ReadList<T> AsReadList<T>(this List<T> self)
            => self;

        public static void Add<T>(this List<T> self, T item, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null ||
                (!allowNull && item == null) ||
                (!allowDuplicate && self.Contains(item)))
                return;

            self.Add(item);
        }

        public static void Add<T>(this List<T> self, object item)
            => self.Add(item, true);

        public static void Add<T>(this List<T> self, object item, bool allowDuplicate)
        {
            if (self == null ||
                !(item is T itemT) ||
                (!allowDuplicate && self.Contains(itemT)))
                return;

            self.Add(itemT);
        }

        public static void AddRange<T>(this List<T> self, IEnumerable<T> collection, bool allowDuplicate, bool allowNull = false)
            => self.AddRange(collection?.GetEnumerator(), allowDuplicate, allowNull);

        public static void AddRange<T>(this List<T> self, IEnumerable<object> collection)
            => self.AddRange(collection, true);

        public static void AddRange<T>(this List<T> self, IEnumerable<object> collection, bool allowDuplicate)
            => self.AddRange(collection?.GetEnumerator(), allowDuplicate);

        public static void AddRange<T>(this List<T> self, IEnumerator<T> enumerator)
            => self.AddRange(enumerator, true);

        public static void AddRange<T>(this List<T> self, IEnumerator<T> enumerator, bool allowDuplicate, bool allowNull = false)
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

        public static void AddRange<T>(this List<T> self, IEnumerator<object> enumerator)
            => self.AddRange(enumerator, true);

        public static void AddRange<T>(this List<T> self, IEnumerator<object> enumerator, bool allowDuplicate)
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

        public static void AddRange<T>(this List<T> self, params T[] items)
            => self.AddRange(true, items);

        public static void AddRange<T>(this List<T> self, bool allowDuplicate, params T[] items)
            => self.AddRange(allowDuplicate, false, items);

        public static void AddRange<T>(this List<T> self, bool allowDuplicate, bool allowNull, params T[] items)
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

        public static void AddRange<T>(this List<T> self, params object[] items)
            => self.AddRange(true, items);

        public static void AddRange<T>(this List<T> self, bool allowDuplicate, params object[] items)
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

        public static void GetRange<T>(this List<T> self, in IntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || self[i] != null)
                        output.Add(self[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || self[i] != null) && !output.Contains(self[i]))
                    output.Add(self[i]);
            }
        }

        public static void GetRange<T>(this List<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this List<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    if (allowNull || self[i] != null)
                        output.Add(self[i]);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                if ((allowNull || self[i] != null) && !output.Contains(self[i]))
                    output.Add(self[i]);
            }
        }

        public static void GetRange<T>(this IList<T> self, in IntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || self[i] != null)
                        output.Add(self[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || self[i] != null) && !output.Contains(self[i]))
                    output.Add(self[i]);
            }
        }

        public static void GetRange<T>(this IList<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IList<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    if (allowNull || self[i] != null)
                        output.Add(self[i]);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                if ((allowNull || self[i] != null) && !output.Contains(self[i]))
                    output.Add(self[i]);
            }
        }

        public static void GetRange<T>(this IReadOnlyList<T> self, in IntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || self[i] != null)
                        output.Add(self[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || self[i] != null) && !output.Contains(self[i]))
                    output.Add(self[i]);
            }
        }

        public static void GetRange<T>(this IReadOnlyList<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IReadOnlyList<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    if (allowNull || self[i] != null)
                        output.Add(self[i]);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                if ((allowNull || self[i] != null) && !output.Contains(self[i]))
                    output.Add(self[i]);
            }
        }

        private static void Validate(int listCount, ref int offset, ref int count)
        {
            offset = Math.Max(offset, 0);

            if (offset > listCount)
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0)
                count = listCount - offset;
            else
                count += offset;

            if (count > listCount)
                throw new ArgumentOutOfRangeException(nameof(count));
        }
    }
}
