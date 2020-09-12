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
    }
}