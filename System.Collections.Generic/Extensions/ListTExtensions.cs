namespace System.Collections.Generic
{
    public static class ListTExtensions
    {
        public static bool ValidateIndex<T>(this List<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(this IList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static bool ValidateIndex<T>(this IReadOnlyList<T> self, int index)
            => self != null && index >= 0 && index < self.Count;

        public static ReadList<T> AsReadList<T>(this List<T> self)
            => self;

        public static void Add<T>(this List<T> self, T item, bool allowDuplicate)
        {
            if (self == null || item == null || (!allowDuplicate && self.Contains(item)))
                return;

            self.Add(item);
        }

        public static void AddRange<T>(this List<T> self, IEnumerable<T> collection, bool allowDuplicate)
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

        public static void AddRange<T>(this List<T> self, params object[] items)
            => self.AddRange(true, items);

        public static void AddRange<T>(this List<T> self, bool allowDuplicate, params object[] items)
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
