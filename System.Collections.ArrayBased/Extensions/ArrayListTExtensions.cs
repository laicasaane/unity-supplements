﻿using System.Collections.Generic;

namespace System.Collections.ArrayBased
{
    public static class ArrayListTExtensions
    {
        public static bool ValidateIndex<T>(this ArrayList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(this ArrayList<T> self, uint index)
            => self != null && index < self.Count;

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

        public static void AddRange<T>(this ArrayList<T> self, in ReadArrayList<T> source, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    var item = source[i];

                    if (allowNull || item != null)
                        self.Add(item);
                }

                return;
            }

            for (var i = 0u; i < source.Count; i++)
            {
                var item = source[i];

                if ((allowNull || item != null) && !self.Contains(item))
                    self.Add(item);
            }
        }

        public static void AddRangeIn<T>(this ArrayList<T> self, in ReadArrayList<T> source, bool allowDuplicate, bool allowNull = false)
        {
            if (self == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    var item = source[i];

                    if (allowNull || item != null)
                        self.Add(in item);
                }

                return;
            }

            for (var i = 0u; i < source.Count; i++)
            {
                var item = source[i];

                if ((allowNull || item != null) && !self.Contains(in item))
                    self.Add(in item);
            }
        }

        public static void AddRange<T>(this ArrayList<T> self, IEnumerable<T> collection)
            => self.AddRange(collection?.GetEnumerator(), true);

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

        public static void AddRangeIn<T>(this ArrayList<T> self, IEnumerable<T> collection)
            => self.AddRangeIn(collection?.GetEnumerator(), true);

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

        public static void GetRange<T>(this ArrayList<T> self, in UIntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (range.Start >= self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= self.Count)
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

        public static void GetRange<T>(this ArrayList<T> self, uint offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this ArrayList<T> self, uint offset, uint count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, (long)count, output, allowDuplicate, allowNull);

        private static void GetRange<T>(this ArrayList<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRangeIn<T>(this ArrayList<T> self, in UIntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (range.Start >= self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || self[i] != null)
                        output.Add(in self[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || self[i] != null) && !output.Contains(in self[i]))
                    output.Add(in self[i]);
            }
        }

        public static void GetRangeIn<T>(this ArrayList<T> self, uint offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this ArrayList<T> self, uint offset, uint count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, (long)count, output, allowDuplicate, allowNull);

        private static void GetRangeIn<T>(this ArrayList<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(this ArrayList<T> self, in UIntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (range.Start >= self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= self.Count)
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

        public static void GetRange<T>(this ArrayList<T> self, uint offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this ArrayList<T> self, uint offset, uint count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, (long)count, output, allowDuplicate, allowNull);

        private static void GetRange<T>(this ArrayList<T> self, long offset, long count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(this T[] self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= (uint)self.Length)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)self.Length)
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

        public static void GetRange<T>(this T[] self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this T[] self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Length, ref offset, ref count);

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

        public static void GetRangeIn<T>(this T[] self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= (uint)self.Length)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)self.Length)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || self[i] != null)
                        output.Add(in self[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || self[i] != null) && !output.Contains(in self[i]))
                    output.Add(in self[i]);
            }
        }

        public static void GetRangeIn<T>(this T[] self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this T[] self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Length, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    if (allowNull || self[i] != null)
                        output.Add(in self[i]);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                if ((allowNull || self[i] != null) && !output.Contains(in self[i]))
                    output.Add(in self[i]);
            }
        }

        public static void GetRange<T>(this T[] self, in LongRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= self.LongLength)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= self.LongLength)
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

        public static void GetRange<T>(this T[] self, long offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this T[] self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.LongLength, ref offset, ref count);

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

        public static void GetRangeIn<T>(this T[] self, in LongRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= self.LongLength)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= self.LongLength)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || self[i] != null)
                        output.Add(in self[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || self[i] != null) && !output.Contains(in self[i]))
                    output.Add(in self[i]);
            }
        }

        public static void GetRangeIn<T>(this T[] self, long offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this T[] self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.LongLength, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    if (allowNull || self[i] != null)
                        output.Add(in self[i]);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                if ((allowNull || self[i] != null) && !output.Contains(in self[i]))
                    output.Add(in self[i]);
            }
        }

        public static void GetRange<T>(this T[] self, in UIntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (range.Start >= self.LongLength)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= self.LongLength)
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

        public static void GetRangeIn<T>(this T[] self, in UIntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (range.Start >= self.LongLength)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= self.LongLength)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || self[i] != null)
                        output.Add(in self[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || self[i] != null) && !output.Contains(in self[i]))
                    output.Add(in self[i]);
            }
        }

        public static void GetRange<T>(in this ReadArray1<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(range, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, count, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArray1<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(range, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArray1<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArray1<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(offset, count, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, in LongRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(range, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, long offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, count, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArray1<T> self, in LongRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(range, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArray1<T> self, long offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArray1<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(offset, count, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, in UIntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(range, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArray1<T> self, in UIntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(range, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this List<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(in this ReadList<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(range, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadList<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadList<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, count, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IList<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(this IReadOnlyList<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(this ICollection<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this ICollection<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this ICollection<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            var o = 0;
            var c = 0;

            if (allowDuplicate)
            {
                foreach (var item in self)
                {
                    if (o < offset)
                    {
                        o += 1;
                        continue;
                    }

                    if (c >= count)
                        break;

                    if (allowNull || item != null)
                        output.Add(item);

                    c += 1;
                }

                return;
            }

            foreach (var item in self)
            {
                if (o < offset)
                {
                    o += 1;
                    continue;
                }

                if (c >= count)
                    break;

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);

                c += 1;
            }
        }

        public static void GetRange<T>(in this ReadCollection<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(range, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadCollection<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadCollection<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, count, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IReadOnlyCollection<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this IReadOnlyCollection<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IReadOnlyCollection<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            var o = 0;
            var c = 0;

            if (allowDuplicate)
            {
                foreach (var item in self)
                {
                    if (o < offset)
                    {
                        o += 1;
                        continue;
                    }

                    if (c >= count)
                        break;

                    if (allowNull || item != null)
                        output.Add(item);

                    c += 1;
                }

                return;
            }

            foreach (var item in self)
            {
                if (o < offset)
                {
                    o += 1;
                    continue;
                }

                if (c >= count)
                    break;

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);

                c += 1;
            }
        }

        public static void GetRange<T>(this IEnumerable<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this IEnumerable<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IEnumerable<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            offset = Math.Max(offset, 0);

            var o = 0;

            if (count < 0)
            {
                foreach (var item in self)
                {
                    if (o < offset)
                    {
                        o += 1;
                        continue;
                    }

                    if ((allowNull || item != null) && (allowDuplicate || !output.Contains(item)))
                        output.Add(item);
                }

                return;
            }

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

                if ((allowNull || item != null) && (allowDuplicate || !output.Contains(item)))
                    output.Add(item);

                c += 1;
            }
        }

        public static void GetRange<T>(this IEnumerator<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this IEnumerator<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IEnumerator<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            offset = Math.Max(offset, 0);

            var o = 0;

            if (count < 0)
            {
                while (self.MoveNext())
                {
                    if (o < offset)
                    {
                        o += 1;
                        continue;
                    }

                    var item = self.Current;

                    if ((allowNull || item != null) && (allowDuplicate || !output.Contains(item)))
                        output.Add(item);
                }

                return;
            }

            var c = 0;

            while (self.MoveNext())
            {
                if (o < offset)
                {
                    o += 1;
                    continue;
                }

                if (c >= count)
                    break;

                var item = self.Current;

                if ((allowNull || item != null) && (allowDuplicate || !output.Contains(item)))
                    output.Add(item);

                c += 1;
            }
        }

        public static void GetRangeIn<T>(this List<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    var item = self[i];

                    if (allowNull || item != null)
                        output.Add(in item);
                }

                return;
            }

            foreach (var i in range)
            {
                var item = self[i];

                if ((allowNull || item != null) && !output.Contains(in item))
                    output.Add(in item);
            }
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

        public static void GetRangeIn<T>(in this ReadList<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(range, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadList<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadList<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRangeIn(offset, count, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this IList<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    var item = self[i];

                    if (allowNull || item != null)
                        output.Add(in item);
                }

                return;
            }

            foreach (var i in range)
            {
                var item = self[i];

                if ((allowNull || item != null) && !output.Contains(in item))
                    output.Add(in item);
            }
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

        public static void GetRangeIn<T>(this IReadOnlyList<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if ((uint)range.Start >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    var item = self[i];

                    if (allowNull || item != null)
                        output.Add(in item);
                }

                return;
            }

            foreach (var i in range)
            {
                var item = self[i];

                if ((allowNull || item != null) && !output.Contains(in item))
                    output.Add(in item);
            }
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

        public static void GetRangeIn<T>(this ICollection<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(this ICollection<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this ICollection<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            var o = 0;
            var c = 0;

            if (allowDuplicate)
            {
                foreach (var item in self)
                {
                    if (o < offset)
                    {
                        o += 1;
                        continue;
                    }

                    if (c >= count)
                        break;

                    if (allowNull || item != null)
                        output.Add(in item);

                    c += 1;
                }

                return;
            }

            foreach (var item in self)
            {
                if (o < offset)
                {
                    o += 1;
                    continue;
                }

                if (c >= count)
                    break;

                if ((allowNull || item != null) && !output.Contains(in item))
                    output.Add(in item);

                c += 1;
            }
        }

        public static void GetRangeIn<T>(in this ReadCollection<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(range, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadCollection<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadCollection<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetSource().GetRange(offset, count, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this IReadOnlyCollection<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(this IReadOnlyCollection<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this IReadOnlyCollection<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            Validate(self.Count, ref offset, ref count);

            var o = 0;
            var c = 0;

            if (allowDuplicate)
            {
                foreach (var item in self)
                {
                    if (o < offset)
                    {
                        o += 1;
                        continue;
                    }

                    if (c >= count)
                        break;

                    if (allowNull || item != null)
                        output.Add(in item);

                    c += 1;
                }

                return;
            }

            foreach (var item in self)
            {
                if (o < offset)
                {
                    o += 1;
                    continue;
                }

                if (c >= count)
                    break;

                if ((allowNull || item != null) && !output.Contains(in item))
                    output.Add(in item);

                c += 1;
            }
        }

        public static void GetRangeIn<T>(this IEnumerable<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(this IEnumerable<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this IEnumerable<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            offset = Math.Max(offset, 0);

            var o = 0;

            if (count < 0)
            {
                foreach (var item in self)
                {
                    if (o < offset)
                    {
                        o += 1;
                        continue;
                    }

                    if ((allowNull || item != null) && (allowDuplicate || !output.Contains(in item)))
                        output.Add(in item);
                }

                return;
            }

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

                if ((allowNull || item != null) && (allowDuplicate || !output.Contains(in item)))
                    output.Add(in item);

                c += 1;
            }
        }

        public static void GetRangeIn<T>(this IEnumerator<T> self, in IntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(this IEnumerator<T> self, int offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(this IEnumerator<T> self, int offset, int count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            offset = Math.Max(offset, 0);

            var o = 0;

            if (count < 0)
            {
                while (self.MoveNext())
                {
                    if (o < offset)
                    {
                        o += 1;
                        continue;
                    }

                    var item = self.Current;

                    if ((allowNull || item != null) && (allowDuplicate || !output.Contains(in item)))
                        output.Add(in item);
                }

                return;
            }

            var c = 0;

            while (self.MoveNext())
            {
                if (o < offset)
                {
                    o += 1;
                    continue;
                }

                if (c >= count)
                    break;

                var item = self.Current;

                if ((allowNull || item != null) && (allowDuplicate || !output.Contains(in item)))
                    output.Add(in item);

                c += 1;
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

        private static void Validate(uint listCount, ref long offset, ref long count)
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

        private static void Validate(long listCount, ref long offset, ref long count)
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
