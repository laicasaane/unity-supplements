using System.Collections.Generic;

namespace System.Collections.ArrayBased
{
    public static class ReadArrayListTExtensions
    {
        public static bool ValidateIndex<T>(in this ReadArrayList<T> self, int index)
            => index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(in this ReadArrayList<T> self, uint index)
            => index >= 0 && index < self.Count;

        public static void GetRange<T>(in this ReadArrayList<T> self, in UIntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(in this ReadArrayList<T> self, uint offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArrayList<T> self, uint offset, uint count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange((long)offset, (long)count, output, allowDuplicate, allowNull);

        private static void GetRange<T>(in this ReadArrayList<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null || count == 0)
                return;

            var source = self.GetSource();

            Validate(source.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = (uint)offset; i < count; i++)
                {
                    if (allowNull || source[i] != null)
                        output.Add(source[i]);
                }

                return;
            }

            for (var i = (uint)offset; i < count; i++)
            {
                if ((allowNull || source[i] != null) && !output.Contains(source[i]))
                    output.Add(source[i]);
            }
        }

        public static void GetRangeIn<T>(in this ReadArrayList<T> self, in UIntRange range, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
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
                        output.Add(in source[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if ((allowNull || source[i] != null) && !output.Contains(in source[i]))
                    output.Add(in source[i]);
            }
        }

        public static void GetRangeIn<T>(in this ReadArrayList<T> self, uint offset, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRangeIn<T>(in this ReadArrayList<T> self, uint offset, uint count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRangeIn((long)offset, (long)count, output, allowDuplicate, allowNull);

        private static void GetRangeIn<T>(in this ReadArrayList<T> self, long offset, long count, ArrayList<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null || count == 0)
                return;

            var source = self.GetSource();

            Validate(source.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = (uint)offset; i < count; i++)
                {
                    if (allowNull || source[i] != null)
                        output.Add(in source[i]);
                }

                return;
            }

            for (var i = (uint)offset; i < count; i++)
            {
                if ((allowNull || source[i] != null) && !output.Contains(in source[i]))
                    output.Add(in source[i]);
            }
        }

        public static void GetRange<T>(in this ReadArrayList<T> self, in UIntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<T>(in this ReadArrayList<T> self, uint offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArrayList<T> self, uint offset, uint count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange((long)offset, (long)count, output, allowDuplicate, allowNull);

        private static void GetRange<T>(in this ReadArrayList<T> self, long offset, long count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null || count == 0)
                return;

            var source = self.GetSource();

            Validate(source.Count, ref offset, ref count);

            if (allowDuplicate)
            {
                for (var i = (uint)offset; i < count; i++)
                {
                    if (allowNull || source[i] != null)
                        output.Add(source[i]);
                }

                return;
            }

            for (var i = (uint)offset; i < count; i++)
            {
                if ((allowNull || source[i] != null) && !output.Contains(source[i]))
                    output.Add(source[i]);
            }
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
    }
}
