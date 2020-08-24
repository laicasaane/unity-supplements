namespace System.Collections.Generic
{
    public static class ReadSegment1Extensions
    {
        public static ReadSegment1<T> AsReadSegment1<T>(this T item)
            => item;

        public static bool ValidateIndex<T>(in this ReadSegment1<T> self, int index)
            => index >= 0 && index < self.Count;
    }
}