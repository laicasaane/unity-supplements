using System.Collections.Generic;

namespace System.Collections.ArrayBased
{
    public static class ReadArrayListTExtensions
    {
        public static bool ValidateIndex<T>(in this ReadArrayList<T> self, int index)
            => index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(in this ReadArrayList<T> self, uint index)
            => index >= 0 && index < self.Count;

        public static void GetRange<T>(in this ReadArrayList<T> self, in ReadRange<uint> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(in this ReadArrayList<T> self, long offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArrayList<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null || count == 0)
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

        public static void GetRangeIn<T>(in this ReadArrayList<T> self, in ReadRange<uint> range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRangeIn(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRangeIn<T>(in this ReadArrayList<T> self, long offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArrayList<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null || count == 0)
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

        public static void GetRange<T>(in this ReadArrayList<T> self, in ReadRange<uint> range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var start = Math.Min(range.Start, range.End);
            var end = Math.Max(range.Start, range.End);

            self.GetRange(start, end - start + 1, output, allowDuplicate, allowNull);
        }

        public static void GetRange<T>(in this ReadArrayList<T> self, long offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArrayList<T> self, long offset, long count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null || count == 0)
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
