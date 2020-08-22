namespace System.Collections.Generic
{
    public static class CollectionTExtensions
    {
        public static bool ValidateIndex<T>(this ICollection<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static ReadCollection<T> AsReadCollection<T>(this ICollection<T> self)
            => new ReadCollection<T>(self);

        public static void Add<T>(this ICollection<T> self, T item, bool allowDuplicate)
        {
            if (self == null || item == null || (!allowDuplicate && self.Contains(item)))
                return;

            self.Add(item);
        }

        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> collection)
            => self.AddRange(collection, true);

        public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> collection, bool allowDuplicate)
        {
            if (self == null || collection == null)
                return;

            foreach (var item in collection)
            {
                if (item == null || (!allowDuplicate && self.Contains(item)))
                    continue;

                self.Add(item);
            }
        }

        public static void AddRange<T>(this ICollection<T> self, params object[] items)
            => self.AddRange(true, items);

        public static void AddRange<T>(this ICollection<T> self, bool allowDuplicate, params object[] items)
        {
            if (self == null || items == null)
                return;

            foreach (var item in items)
            {
                if (item is T itemT)
                {
                    if (!allowDuplicate && self.Contains(itemT))
                        continue;

                    self.Add(itemT);
                }
            }
        }
    }
}
