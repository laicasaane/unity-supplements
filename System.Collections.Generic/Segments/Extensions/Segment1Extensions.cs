namespace System.Collections.Generic
{
    public static class Segment1Extensions
    {
        public static Segment1<T> AsSegment1<T>(this T item)
            => item;

        public static bool ValidateIndex<T>(in this Segment1<T> self, int index)
            => index >= 0 && index < self.Count;
    }
}