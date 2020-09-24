using System.Collections.Generic;

namespace System
{
    public static class ReadArray1TExtensions
    {
        public static bool ValidateIndex<T>(in this ReadArray1<T> self, int index)
           => self != null && index >= 0 && index < self.Length;

        public static ReadArray1<T> Get<T, TResult>(in this ReadArray1<T> self, int index, out TResult value)
        {
            value = default;

            if (self != null && self.Length > index && self[index] is TResult val)
                value = val;

            return self;
        }

        public static ReadArray1<T> GetThenMoveNext<T, TResult>(in this ReadArray1<T> self, ref int index, out TResult value)
        {
            value = default;

            if (self != null)
            {
                if (self.Length > index && self[index] is TResult val)
                    value = val;

                index += 1;
            }

            return self;
        }

        public static void GetRange<T>(in this ReadArray1<T> self, in ReadRange<int> range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start + 1, end - start, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(in this ReadArray1<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            offset = Math.Max(offset, 0);

            if (offset > self.Length)
                throw new IndexOutOfRangeException(nameof(offset));

            if (count < 0)
                count = self.Length - offset;
            else
                count += offset;

            if (count > self.Length)
                throw new IndexOutOfRangeException(nameof(count));

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    ref var item = ref self[i];

                    if (allowNull || item != null)
                        output.Add(item);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                ref var item = ref self[i];

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);
            }
        }
    }
}