namespace UnityEngine
{
    public static class RangeExtensions
    {
        public static void Deconstruct(in this RangeInt self, out int start, out int length)
        {
            start = self.start;
            length = self.length;
        }
    }
}