namespace System.Collections.Generic
{
    public static class ReadDictionaryTKeyTValueExtensions
    {
        public static void GetRange<TKey, TValue>(in this ReadDictionary<TKey, TValue> self, IEnumerable<TKey> keys, Dictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (keys == null || output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !source.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !source.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(in this ReadDictionary<TKey, TValue> self, IEnumerable<TKey> keys, IDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (keys == null || output == null)
                return;

            var source = self.GetSource();

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !source.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(in this ReadDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ICollection<TValue> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (keys == null || output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var key in keys)
                {
                    if (!source.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (!source.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public static void GetRange<TKey, TValue>(in this ReadDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ICollection<KeyValuePair<TKey, TValue>> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (keys == null || output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var key in keys)
                {
                    if (!source.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(new KeyValuePair<TKey, TValue>(key, value));
                }

                return;
            }

            foreach (var key in keys)
            {
                if (!source.TryGetValue(key, out var value))
                    continue;

                var kvp = new KeyValuePair<TKey, TValue>(key, value);
                if ((allowNull || value != null) && !output.Contains(kvp))
                    output.Add(kvp);
            }
        }
    }
}
