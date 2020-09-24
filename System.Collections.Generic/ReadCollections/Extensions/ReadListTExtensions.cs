﻿namespace System.Collections.Generic
{
    public static class ReadListTExtensions
    {
        public static bool ValidateIndex<T>(in this ReadList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static void GetRange<T>(in this ReadList<T> self, in ReadRange<int> range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start + 1, end - start, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(in this ReadList<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadList<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
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
    }
}