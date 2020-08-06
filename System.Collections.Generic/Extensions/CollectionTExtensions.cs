namespace System.Collections.Generic
{
    public static class CollectionTExtensions
    {
        public static bool ValidateIndex<T>(this ICollection<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static ReadCollection<T> AsReadCollection<T>(this ICollection<T> self)
            => new ReadCollection<T>(self);
    }
}
