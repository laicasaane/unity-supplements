using System.Collections.Generic;

namespace System
{
    public static class ReadArray1TExtensions
    {
        public static bool ValidateIndex<T>(in this ReadArray1<T> self, int index)
           => self != null && index >= 0 && index < self.Length;

        public static bool ValidateIndex<T>(in this ReadArray1<T> self, uint index)
           => self != null && index < self.LongLength;

        public static bool ValidateIndex<T>(in this ReadArray1<T> self, long index)
           => self != null && index >= 0 && index < self.LongLength;

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

        public static void GetRange<T>(in this ReadArray1<T> self, in IntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var source = self.GetSource();

            if ((uint)range.Start >= (uint)source.Length)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((uint)range.End >= (uint)source.Length)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    ref var item = ref source[i];

                    if (allowNull || item != null)
                        output.Add(item);
                }

                return;
            }

            foreach (var i in range)
            {
                ref var item = ref source[i];

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);
            }
        }

        public static void GetRange<T>(in this ReadArray1<T> self, int offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, int offset, int count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            var source = self.GetSource();

            offset = Math.Max(offset, 0);

            if (offset > source.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0)
                count = source.Length - offset;
            else
                count += offset;

            if (count > source.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    ref var item = ref source[i];

                    if (allowNull || item != null)
                        output.Add(item);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                ref var item = ref source[i];

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);
            }
        }

        public static void GetRange<T>(in this ReadArray1<T> self, in LongRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var source = self.GetSource();

            if ((ulong)range.Start >= (ulong)source.LongLength)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if ((ulong)range.End >= (ulong)source.LongLength)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    ref var item = ref source[i];

                    if (allowNull || item != null)
                        output.Add(item);
                }

                return;
            }

            foreach (var i in range)
            {
                ref var item = ref source[i];

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);
            }
        }

        public static void GetRange<T>(in this ReadArray1<T> self, long offset, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
            => self.GetRange(offset, -1, output, allowDuplicate, allowNull);

        public static void GetRange<T>(in this ReadArray1<T> self, long offset, long count, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null || count == 0)
                return;

            var source = self.GetSource();

            offset = Math.Max(offset, 0);

            if (offset > source.LongLength)
                throw new ArgumentOutOfRangeException(nameof(offset));

            if (count < 0)
                count = source.LongLength - offset;
            else
                count += offset;

            if (count > source.LongLength)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (allowDuplicate)
            {
                for (var i = offset; i < count; i++)
                {
                    ref var item = ref source[i];

                    if (allowNull || item != null)
                        output.Add(item);
                }

                return;
            }

            for (var i = offset; i < count; i++)
            {
                ref var item = ref source[i];

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);
            }
        }

        public static void GetRange<T>(in this ReadArray1<T> self, in UIntRange range, ICollection<T> output, bool allowDuplicate = true, bool allowNull = false)
        {
            var source = self.GetSource();

            if (range.Start >= source.LongLength)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= source.LongLength)
                throw new IndexOutOfRangeException(nameof(range.End));

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    ref var item = ref source[i];

                    if (allowNull || item != null)
                        output.Add(item);
                }

                return;
            }

            foreach (var i in range)
            {
                ref var item = ref source[i];

                if ((allowNull || item != null) && !output.Contains(item))
                    output.Add(item);
            }
        }
    }
}