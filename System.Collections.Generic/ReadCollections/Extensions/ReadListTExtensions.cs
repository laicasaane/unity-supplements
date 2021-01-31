namespace System.Collections.Generic
{
    public static class ReadListTExtensions
    {
        public static bool ValidateIndex<T>(in this ReadList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static void GetRange<T>(in this ReadList<T> self, in IntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var source = self.GetSource();

            if (range.Start >= source.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= source.Count)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || source[i] != null)
                        output.Add(source[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || source[i] != null) && !output.Contains(source[i]))
                    output.Add(source[i]);
            }
        }

        public static void GetRange<T>(in this ReadList<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadList<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null || count == 0)
                return;

            var source = self.GetSource();

            offset = Math.Max(offset, 0);

            if (offset > source.Count)
                throw new IndexOutOfRangeException(nameof(offset));

            if (count < 0)
                count = source.Count - offset;
            else
                count += offset;

            if (count > source.Count)
                throw new IndexOutOfRangeException(nameof(count));

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    if (allowNull || source[i] != null)
                        output.Add(source[i]);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                if ((allowNull || source[i] != null) && !output.Contains(source[i]))
                    output.Add(source[i]);
            }
        }
    }
}
