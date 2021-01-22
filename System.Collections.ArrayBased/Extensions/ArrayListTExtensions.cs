using System.Collections.Generic;

namespace System.Collections.ArrayBased
{
    public static class ArrayListTExtensions
    {
        public static bool ValidateIndex<T>(this ArrayList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static ReadArrayList<T> AsReadArrayList<T>(this ArrayList<T> self)
            => self;

        public static void Add<T>(this ArrayList<T> self, T item, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null ||
                (!allowNull && item == null) ||
                (!allowDuplicate && self.Contains(item)))
                return;

            self.Add(item);
        }

        public static void Add<T>(this ArrayList<T> self, in T item, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null ||
                (!allowNull && item == null) ||
                (!allowDuplicate && self.Contains(in item)))
                return;

            self.Add(in item);
        }

        public static void Add<T>(this ArrayList<T> self, object item)
            => self.Add(item, true);

        public static void Add<T>(this ArrayList<T> self, object item, bool allowDuplicate)
        {
            if (self == null ||
                !(item is T itemT) ||
                (!allowDuplicate && self.Contains(itemT)))
                return;

            self.Add(itemT);
        }

        public static void AddIn<T>(this ArrayList<T> self, object item)
            => self.AddIn(item, true);

        public static void AddIn<T>(this ArrayList<T> self, object item, bool allowDuplicate)
        {
            if (self == null ||
                !(item is T itemT) ||
                (!allowDuplicate && self.Contains(in itemT)))
                return;

            self.Add(in itemT);
        }

        public static void AddRange<T>(this ArrayList<T> self, ArrayList<T> source)
            => self.AddRange(source, true);

        public static void AddRange<T>(this ArrayList<T> self, ArrayList<T> source, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null || source == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    ref var item = ref source[i];

                    if (allowNull || item != null)
                        self.Add(item);
                }

                return;
            }

            for (var i = 0u; i < source.Count; i++)
            {
                ref var item = ref source[i];

                if ((allowNull || item != null) && !self.Contains(item))
                    self.Add(item);
            }
        }

        public static void AddRangeIn<T>(this ArrayList<T> self, ArrayList<T> source)
            => self.AddRangeIn(source, true);

        public static void AddRangeIn<T>(this ArrayList<T> self, ArrayList<T> source, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null || source == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    ref var item = ref source[i];

                    if (allowNull || item != null)
                        self.Add(in item);
                }

                return;
            }

            for (var i = 0u; i < source.Count; i++)
            {
                ref var item = ref source[i];

                if ((allowNull || item != null) && !self.Contains(in item))
                    self.Add(in item);
            }
        }

        public static void AddRange<T>(this ArrayList<T> self, IEnumerable<T> collection, bool allowDuplicate, bool allowNull = false)
            => self.AddRange(collection?.GetEnumerator(), allowDuplicate, allowNull);

        public static void AddRange<T>(this ArrayList<T> self, IEnumerator<T> enumerator)
            => self.AddRange(enumerator, true);

        public static void AddRange<T>(this ArrayList<T> self, IEnumerator<T> enumerator, bool allowDuplicate, bool allowNull = false)
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

        public static void AddRangeIn<T>(this ArrayList<T> self, IEnumerable<T> collection, bool allowDuplicate, bool allowNull = false)
            => self.AddRangeIn(collection?.GetEnumerator(), allowDuplicate, allowNull);

        public static void AddRangeIn<T>(this ArrayList<T> self, IEnumerator<T> enumerator)
            => self.AddRangeIn(enumerator, true);

        public static void AddRangeIn<T>(this ArrayList<T> self, IEnumerator<T> enumerator, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null || enumerator == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    T item = enumerator.Current;

                    if (allowNull || item != null)
                        self.Add(in item);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;

                if ((allowNull || item != null) && !self.Contains(in item))
                    self.Add(in item);
            }
        }

        public static void AddRange<T>(this ArrayList<T> self, IEnumerable<object> collection)
            => self.AddRange(collection, true);

        public static void AddRange<T>(this ArrayList<T> self, IEnumerable<object> collection, bool allowDuplicate)
            => self.AddRange(collection?.GetEnumerator(), allowDuplicate);

        public static void AddRange<T>(this ArrayList<T> self, IEnumerator<object> enumerator)
            => self.AddRange(enumerator, true);

        public static void AddRange<T>(this ArrayList<T> self, IEnumerator<object> enumerator, bool allowDuplicate)
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

        public static void AddRangeIn<T>(this ArrayList<T> self, IEnumerable<object> collection)
            => self.AddRangeIn(collection, true);

        public static void AddRangeIn<T>(this ArrayList<T> self, IEnumerable<object> collection, bool allowDuplicate)
            => self.AddRangeIn(collection?.GetEnumerator(), allowDuplicate);

        public static void AddRangeIn<T>(this ArrayList<T> self, IEnumerator<object> enumerator)
            => self.AddRangeIn(enumerator, true);

        public static void AddRangeIn<T>(this ArrayList<T> self, IEnumerator<object> enumerator, bool allowDuplicate)
        {
            if (self == null || enumerator == null)
                return;

            if (allowDuplicate)
            {
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is T itemT)
                        self.Add(in itemT);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current is T itemT && !self.Contains(in itemT))
                    self.Add(in itemT);
            }
        }

        public static void AddRange<T>(this ArrayList<T> self, params T[] items)
            => self.AddRange(true, items);

        public static void AddRange<T>(this ArrayList<T> self, bool allowDuplicate, params T[] items)
            => self.AddRange(allowDuplicate, false, items);

        public static void AddRange<T>(this ArrayList<T> self, bool allowDuplicate, bool allowNull, params T[] items)
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

        public static void AddRangeIn<T>(this ArrayList<T> self, params T[] items)
            => self.AddRangeIn(true, items);

        public static void AddRangeIn<T>(this ArrayList<T> self, bool allowDuplicate, params T[] items)
            => self.AddRangeIn(allowDuplicate, false, items);

        public static void AddRangeIn<T>(this ArrayList<T> self, bool allowDuplicate, bool allowNull, params T[] items)
        {
            if (self == null || items == null)
                return;

            if (allowDuplicate)
            {
                foreach (var item in items)
                {
                    if (allowNull || item != null)
                        self.Add(in item);
                }

                return;
            }

            foreach (var item in items)
            {
                if ((allowNull || item != null) && !self.Contains(in item))
                    self.Add(in item);
            }
        }

        public static void AddRange<T>(this ArrayList<T> self, params object[] items)
            => self.AddRange(true, items);

        public static void AddRange<T>(this ArrayList<T> self, bool allowDuplicate, params object[] items)
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

        public static void AddRangeIn<T>(this ArrayList<T> self, params object[] items)
            => self.AddRangeIn(true, items);

        public static void AddRangeIn<T>(this ArrayList<T> self, bool allowDuplicate, params object[] items)
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
                if (item is T itemT && !self.Contains(in itemT))
                    self.Add(in itemT);
            }
        }

        public static void GetRange<T>(this ArrayList<T> self, in ReadRange<uint> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this ArrayList<T> self, long offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this ArrayList<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = (uint)offset; i < count; i++)
                {
                    if (allowNull || self[i] != null)
                        output.Add(self[i]);
                }

                return;
            }

            for (var i = (uint)offset; i < count; i++)
            {
                if ((allowNull || self[i] != null) && !output.Contains(self[i]))
                    output.Add(self[i]);
            }
        }

        public static void GetRangeIn<T>(this ArrayList<T> self, in ReadRange<uint> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRangeIn(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(this ArrayList<T> self, long offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this ArrayList<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = (uint)offset; i < count; i++)
                {
                    if (allowNull || self[i] != null)
                        output.Add(in self[i]);
                }

                return;
            }

            for (var i = (uint)offset; i < count; i++)
            {
                if ((allowNull || self[i] != null) && !output.Contains(in self[i]))
                    output.Add(in self[i]);
            }
        }

        public static void GetRange<T>(this ArrayList<T> self, in ReadRange<uint> range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this ArrayList<T> self, long offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this ArrayList<T> self, long offset, long count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = (uint)offset; i < count; i++)
                {
                    if (allowNull || self[i] != null)
                        output.Add(self[i]);
                }

                return;
            }

            for (var i = (uint)offset; i < count; i++)
            {
                if ((allowNull || self[i] != null) && !output.Contains(self[i]))
                    output.Add(self[i]);
            }
        }

        public static void GetRange<T>(this List<T> self, in ReadRange<int> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this List<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this List<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(this IList<T> self, in ReadRange<int> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this IList<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IList<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(this IReadOnlyList<T> self, in ReadRange<int> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this IReadOnlyList<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IReadOnlyList<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRangeIn<T>(this List<T> self, in ReadRange<int> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRangeIn(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(this List<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this List<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    var item = self[i];

                    if (allowNull || item != null)
                        output.Add(in item);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                var item = self[i];

                if ((allowNull || item != null) && !output.Contains(in item))
                    output.Add(in item);
            }
        }

        public static void GetRangeIn<T>(this IList<T> self, in ReadRange<int> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRangeIn(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(this IList<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this IList<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    var item = self[i];

                    if (allowNull || item != null)
                        output.Add(in item);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                var item = self[i];

                if ((allowNull || item != null) && !output.Contains(in item))
                    output.Add(in item);
            }
        }

        public static void GetRangeIn<T>(this IReadOnlyList<T> self, in ReadRange<int> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRangeIn(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(this IReadOnlyList<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this IReadOnlyList<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    var item = self[i];

                    if (allowNull || item != null)
                        output.Add(in item);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                var item = self[i];

                if ((allowNull || item != null) && !output.Contains(in item))
                    output.Add(in item);
            }
        }

        private static void Validate(int listCount, ref int offset, ref int count)
        {
            offset = Math.Max(offset, 0);

            if (offset > listCount)
                throw new IndexOutOfRangeException(nameof(offset));

            if (count < 0)
                count = listCount - offset;
            else
                count += offset;

            if (count > listCount)
                throw new IndexOutOfRangeException(nameof(count));
        }

        private static void Validate(uint listCount, ref long offset, ref long count)
        {
            offset = Math.Max(offset, 0);

            if (offset > listCount)
                throw new IndexOutOfRangeException(nameof(offset));

            if (count < 0)
                count = listCount - offset;
            else
                count += offset;

            if (count > listCount)
                throw new IndexOutOfRangeException(nameof(count));
        }
    }
}
