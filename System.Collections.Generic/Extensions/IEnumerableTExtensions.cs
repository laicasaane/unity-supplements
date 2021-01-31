namespace System.Collections.Generic
{
    public static class IEnumerableTExtensions
    {
        public static void GetRange<T>(this IEnumerable<T> self, in IntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this IEnumerable<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IEnumerable<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
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
    }
}
