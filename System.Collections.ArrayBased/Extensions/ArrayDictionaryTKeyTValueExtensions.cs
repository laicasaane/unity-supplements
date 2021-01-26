using System.Collections.Generic;

namespace System.Collections.ArrayBased
{
    public static class ArrayDictionaryTKeyTValueExtensions
    {
        public static ReadArrayDictionary<TKey, TValue> AsReadArrayDictionary<TKey, TValue>(this ArrayDictionary<TKey, TValue> self)
            => self;

        public static void Add<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, TKey key, TValue value, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || key == null ||
                (!allowNullValue && value == null) ||
                (!allowOverwrite && self.ContainsKey(key)))
                return;

            self.Set(key, value);
        }

        public static void Add<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in TKey key, TValue value, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || key == null ||
                (!allowNullValue && value == null) ||
                (!allowOverwrite && self.ContainsKey(in key)))
                return;

            self.Set(in key, value);
        }

        public static void Add<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, TKey key, in TValue value, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || key == null ||
                (!allowNullValue && value == null) ||
                (!allowOverwrite && self.ContainsKey(key)))
                return;

            self.Set(key, in value);
        }

        public static void Add<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in TKey key, in TValue value, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || key == null ||
                (!allowNullValue && value == null) ||
                (!allowOverwrite && self.ContainsKey(in key)))
                return;

            self.Set(in key, in value);
        }

        public static void Add<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in KeyValuePair<TKey, TValue> kvp, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || kvp.Key == null ||
                (!allowNullValue && kvp.Value == null) ||
                (!allowOverwrite && self.ContainsKey(kvp.Key)))
                return;

            self[kvp.Key] = kvp.Value;
        }

        public static void Add<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, object item, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || !(item is KeyValuePair<TKey, TValue> kvp))
                return;

            if ((!allowNullValue && kvp.Value == null) ||
                (!allowOverwrite && self.ContainsKey(kvp.Key)))
                return;

            self.Set(kvp.Key, kvp.Value);
        }

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayDictionary<TKey, TValue> source)
            => self.AddRange(source, true);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayDictionary<TKey, TValue> source, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || source == null)
                return;

            if (allowOverwrite)
            {
                foreach (var kv in source)
                {
                    if (allowNullValue || kv.Value != null)
                        self.Set(kv.Key, kv.Value);
                }

                return;
            }

            foreach (var kv in source)
            {
                if ((allowNullValue || kv.Value != null) && !self.ContainsKey(kv.Key))
                    self.Set(kv.Key, kv.Value);
            }
        }

        public static void AddRangeInValue<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayDictionary<TKey, TValue> source)
            => self.AddRangeInValue(source, true);

        public static void AddRangeInValue<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayDictionary<TKey, TValue> source, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || source == null)
                return;

            if (allowOverwrite)
            {
                foreach (var kv in source)
                {
                    if (allowNullValue || kv.Value != null)
                        self.Set(kv.Key, in kv.Value);
                }

                return;
            }

            foreach (var kv in source)
            {
                if ((allowNullValue || kv.Value != null) && !self.ContainsKey(kv.Key))
                    self.Set(kv.Key, in kv.Value);
            }
        }

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<KeyValuePair<TKey, TValue>> collection)
            => self.AddRange(collection, true);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<KeyValuePair<TKey, TValue>> collection, bool allowOverwrite, bool allowNullValue = false)
            => self.AddRange(collection?.GetEnumerator(), allowOverwrite, allowNullValue);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerator<KeyValuePair<TKey, TValue>> enumerator)
            => self.AddRange(enumerator, true);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerator<KeyValuePair<TKey, TValue>> enumerator, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || enumerator == null)
                return;

            if (allowOverwrite)
            {
                while (enumerator.MoveNext())
                {
                    var kv = enumerator.Current;

                    if (allowNullValue || kv.Value != null)
                        self.Set(kv.Key, kv.Value);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                var kv = enumerator.Current;

                if ((allowNullValue || kv.Value != null) && !self.ContainsKey(kv.Key))
                    self.Set(kv.Key, kv.Value);
            }
        }

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<object> collection)
           => self.AddRange(collection, true);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<object> collection, bool allowOverwrite, bool allowNullValue = false)
            => self.AddRange(collection?.GetEnumerator(), allowOverwrite, allowNullValue);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerator<object> enumerator)
            => self.AddRange(enumerator, true);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerator<object> enumerator, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || enumerator == null)
                return;

            if (allowOverwrite)
            {
                while (enumerator.MoveNext())
                {
                    if (!(enumerator.Current is KeyValuePair<TKey, TValue> kv))
                        continue;

                    if (allowNullValue || kv.Value != null)
                        self.Set(kv.Key, kv.Value);
                }

                return;
            }

            while (enumerator.MoveNext())
            {
                if (!(enumerator.Current is KeyValuePair<TKey, TValue> kv))
                    continue;

                if ((allowNullValue || kv.Value != null) && !self.ContainsKey(kv.Key))
                    self.Set(kv.Key, kv.Value);
            }
        }

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, params KeyValuePair<TKey, TValue>[] items)
            => self.AddRange(true, items);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, bool allowNullValue, params KeyValuePair<TKey, TValue>[] items)
            => self.AddRange(true, allowNullValue, items);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, bool allowOverwrite, bool allowNullValue, params KeyValuePair<TKey, TValue>[] items)
        {
            if (self == null || items == null)
                return;

            if (allowOverwrite)
            {
                foreach (var kv in items)
                {
                    if (allowNullValue || kv.Value != null)
                        self.Set(kv.Key, kv.Value);
                }

                return;
            }

            foreach (var kv in items)
            {
                if ((allowNullValue || kv.Value != null) && !self.ContainsKey(kv.Key))
                    self.Set(kv.Key, kv.Value);
            }
        }

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, params object[] items)
            => self.AddRange(true, items);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, bool allowNullValue, params object[] items)
            => self.AddRange(true, allowNullValue, items);

        public static void AddRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, bool allowOverwrite, bool allowNullValue, params object[] items)
        {
            if (self == null || items == null)
                return;

            if (allowOverwrite)
            {
                foreach (var item in items)
                {
                    if (!(item is KeyValuePair<TKey, TValue> kv))
                        continue;

                    if (allowNullValue || kv.Value != null)
                        self.Set(kv.Key, kv.Value);
                }

                return;
            }

            foreach (var item in items)
            {
                if (!(item is KeyValuePair<TKey, TValue> kv))
                    continue;

                if ((allowNullValue || kv.Value != null) && !self.ContainsKey(kv.Key))
                    self.Set(kv.Key, kv.Value);
            }
        }

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, Dictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Set(key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Set(key, value);
            }
        }

        public static void GetRangeIn<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Set(in key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Set(in key, in value);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Set(in key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Set(in key, value);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Set(key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Set(key, in value);
            }
        }

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, IDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ICollection<TValue> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicate)
            {
                foreach (var key in keys)
                {
                    if (!self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (!self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.Contains(value))
                    output.Add(value);
            }
        }

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ICollection<KeyValuePair<TKey, TValue>> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicate)
            {
                foreach (var key in keys)
                {
                    if (!self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(new KeyValuePair<TKey, TValue>(key, value));
                }

                return;
            }

            foreach (var key in keys)
            {
                if (!self.TryGetValue(key, out var value))
                    continue;

                var kvp = new KeyValuePair<TKey, TValue>(key, value);
                if ((allowNull || value != null) && !output.Contains(kvp))
                    output.Add(kvp);
            }
        }

        public static void GetRange<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Add(key, value);
            }
        }

        public static void GetRange<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(key, value);
            }
        }

        public static void GetRangeIn<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(in key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Add(in key, in value);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(in key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Add(in key, value);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Add(key, in value);
            }
        }

        public static void GetRangeIn<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(in key, in value);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(in key, value);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(key, in value);
            }
        }

        public static void GetRangeIn<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(in key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Add(in key, in value);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(in key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Add(in key, value);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Add(key, in value);
            }
        }

        public static void GetRangeIn<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(in key, in value);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(in key, value);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(this IDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(key, in value);
            }
        }

        public static void GetRangeIn<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(in key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Add(in key, in value);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(in key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Add(in key, value);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(key) ||
                        !self.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Add(key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Add(key, in value);
            }
        }

        public static void GetRangeIn<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(in key, in value);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(in key, value);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || keys == null || output == null)
                return;

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !self.TryGetValue(key, out var value))
                    continue;

                if (allowNull || value != null)
                    output.Add(key, in value);
            }
        }
    }
}
