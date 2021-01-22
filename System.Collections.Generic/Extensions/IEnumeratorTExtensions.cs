namespace System.Collections.Generic
{
    public static class IEnumeratorTExtensions
    {
        public static void GetRange<T>(this IEnumerator<T> self, in ReadRange<int> range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(this IEnumerator<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(this IEnumerator<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
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
    }
}
