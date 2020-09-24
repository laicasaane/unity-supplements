using System;

namespace UnityEngine
{
    public static class RangeExtensions
    {
        public static void Deconstruct(in this RangeInt self, out int start, out int length)
        {
            start = self.start;
            length = self.length;
        }

        public static RangeInt With(in this RangeInt self, int? start = null, int? length = null)
            => new RangeInt(
                start ?? self.start,
                length ?? self.length
            );

        public static IntRange FromStart(in this RangeInt self)
            => new IntRange(self.start, self.end, false);

        public static IntRange FromEnd(in this RangeInt self)
            => new IntRange(self.start, self.end, true);

        public static IntRange.Enumerator GetEnumerator(in this RangeInt self)
            => new IntRange(self.start, self.end).GetEnumerator();

        public static IntRange.Enumerator Range(in this RangeInt self)
            => new IntRange(self.start, self.end).Range();
    }
}