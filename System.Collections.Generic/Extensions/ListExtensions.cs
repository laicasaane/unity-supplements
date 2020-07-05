namespace System.Collections.Generic
{
    public static class ListExtensions
    {
        public static bool ValidateIndex<T>(this List<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(this IList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(this IReadOnlyList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static ReadList<T> AsReadList<T>(this List<T> self)
            => self;
    }
}
