namespace System.Collections.Generic
{
    public static class ReadCollectionTExtensions
    {
        public static bool ValidateIndex<T>(in this ReadCollection<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static void GetRange<T>(in this ReadCollection<T> self, in ReadRange<int> range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start + 1, end - start, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(in this ReadCollection<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadCollection<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null || count == 0)
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
    }
}
