using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public static class ArrayDictionaryTKeyTValueExtensions
    {
        public static bool ValidateIndex<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, int index)
            => index >= 0 && index < self.Count;

        public static bool ValidateIndex<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, uint index)
            => index >= 0 && index < self.Count;

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
                for (var i = 0u; i < source.Count; i++)
                {
                    if (allowNullValue || source.UnsafeValues[i] != null)
                        self.Set(source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
                }

                return;
            }

            for (var i = 0u; i < source.Count; i++)
            {
                if ((allowNullValue || source.UnsafeValues[i] != null) && !self.ContainsKey(source.UnsafeKeys[i].Key))
                    self.Set(source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
            }
        }

        public static void AddRangeIn<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayDictionary<TKey, TValue> source)
            => self.AddRangeIn(source, true);

        public static void AddRangeIn<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayDictionary<TKey, TValue> source, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || source == null)
                return;

            if (allowOverwrite)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    if (allowNullValue || source.UnsafeValues[i] != null)
                        self.Set(in source.UnsafeKeys[i].Key, in source.UnsafeValues[i]);
                }

                return;
            }

            for (var i = 0u; i < source.Count; i++)
            {
                if ((allowNullValue || source.UnsafeValues[i] != null) && !self.ContainsKey(in source.UnsafeKeys[i].Key))
                    self.Set(in source.UnsafeKeys[i].Key, in source.UnsafeValues[i]);
            }
        }

        public static void AddRangeInKey<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayDictionary<TKey, TValue> source)
            => self.AddRangeInKey(source, true);

        public static void AddRangeInKey<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayDictionary<TKey, TValue> source, bool allowOverwrite, bool allowNullValue = false)
        {
            if (self == null || source == null)
                return;

            if (allowOverwrite)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    if (allowNullValue || source.UnsafeValues[i] != null)
                        self.Set(in source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
                }

                return;
            }

            for (var i = 0u; i < source.Count; i++)
            {
                if ((allowNullValue || source.UnsafeValues[i] != null) && !self.ContainsKey(in source.UnsafeKeys[i].Key))
                    self.Set(in source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
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
                for (var i = 0u; i < source.Count; i++)
                {
                    if (allowNullValue || source.UnsafeValues[i] != null)
                        self.Set(source.UnsafeKeys[i].Key, in source.UnsafeValues[i]);
                }

                return;
            }

            for (var i = 0u; i < source.Count; i++)
            {
                if ((allowNullValue || source.UnsafeValues[i] != null) && !self.ContainsKey(source.UnsafeKeys[i].Key))
                    self.Set(source.UnsafeKeys[i].Key, in source.UnsafeValues[i]);
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

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, Dictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(self.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || self.UnsafeValues[i] != null)
                        output.Add(self.UnsafeKeys[i].Key, self.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(self.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || self.UnsafeValues[i] != null) && !output.ContainsValue(self.UnsafeValues[i]))
                    output.Add(self.UnsafeKeys[i].Key, self.UnsafeValues[i]);
            }
        }

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(self.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || self.UnsafeValues[i] != null)
                        output.Add(self.UnsafeKeys[i].Key, self.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(self.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || self.UnsafeValues[i] != null) && !output.ContainsValue(self.UnsafeValues[i]))
                    output.Add(self.UnsafeKeys[i].Key, self.UnsafeValues[i]);
            }
        }

        public static void GetRangeIn<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(in self.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || self.UnsafeValues[i] != null)
                        output.Add(in self.UnsafeKeys[i].Key, in self.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(in self.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || self.UnsafeValues[i] != null) && !output.ContainsValue(in self.UnsafeValues[i]))
                    output.Add(in self.UnsafeKeys[i].Key, in self.UnsafeValues[i]);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(in self.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || self.UnsafeValues[i] != null)
                        output.Add(in self.UnsafeKeys[i].Key, self.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(in self.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || self.UnsafeValues[i] != null) && !output.ContainsValue(self.UnsafeValues[i]))
                    output.Add(in self.UnsafeKeys[i].Key, self.UnsafeValues[i]);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(self.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || self.UnsafeValues[i] != null)
                        output.Add(self.UnsafeKeys[i].Key, in self.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(self.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || self.UnsafeValues[i] != null) && !output.ContainsValue(in self.UnsafeValues[i]))
                    output.Add(self.UnsafeKeys[i].Key, in self.UnsafeValues[i]);
            }
        }

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, IDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            foreach (var i in range)
            {
                if (output.ContainsKey(self.UnsafeKeys[i].Key))
                    continue;

                if (allowNull || self.UnsafeValues[i] != null)
                    output.Add(self.UnsafeKeys[i].Key, self.UnsafeValues[i]);
            }
        }

        public static void GetRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ICollection<KeyValuePair<TKey, TValue>> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || self.UnsafeValues[i] != null)
                        output.Add(new KeyValuePair<TKey, TValue>(self.UnsafeKeys[i].Key, self.UnsafeValues[i]));
                }

                return;
            }

            foreach (var i in range)
            {
                var kvp = new KeyValuePair<TKey, TValue>(self.UnsafeKeys[i].Key, self.UnsafeValues[i]);

                if ((allowNull || self.UnsafeValues[i] != null) && !output.Contains(kvp))
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

        public static void GetKeys<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayList<TKey> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < self.Count; i++)
                {
                    output.Add(self.UnsafeKeys[i].Key);
                }
            }
            else
            {
                for (var i = 0u; i < self.Count; i++)
                {
                    if (output.Contains(self.UnsafeKeys[i].Key))
                        output.Add(self.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetKeysIn<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayList<TKey> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < self.Count; i++)
                {
                    output.Add(in self.UnsafeKeys[i].Key);
                }
            }
            else
            {
                for (var i = 0u; i < self.Count; i++)
                {
                    if (output.Contains(in self.UnsafeKeys[i].Key))
                        output.Add(in self.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetKeys<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ICollection<TKey> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            if (allowDuplicate)
            {
                for (var i = 0u; i < self.Count; i++)
                {
                    output.Add(self.UnsafeKeys[i].Key);
                }
            }
            else
            {
                for (var i = 0u; i < self.Count; i++)
                {
                    if (output.Contains(self.UnsafeKeys[i].Key))
                        output.Add(self.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetValues<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, ArrayList<TValue> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            if (allowDuplicate)
                output.AddRange(self.UnsafeValues, self.Count);
            else
            {
                for (var i = 0u; i < self.Count; i++)
                {
                    if (output.Contains(self.UnsafeValues[i]))
                        output.Add(self.UnsafeValues[i]);
                }
            }
        }

        public static void GetKeyRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayList<TKey> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(self.UnsafeKeys[i].Key);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(self.UnsafeKeys[i].Key))
                        output.Add(self.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetKeyRangeIn<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayList<TKey> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(in self.UnsafeKeys[i].Key);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(in self.UnsafeKeys[i].Key))
                        output.Add(in self.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetKeyRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ICollection<TKey> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(self.UnsafeKeys[i].Key);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(self.UnsafeKeys[i].Key))
                        output.Add(self.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetValueRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayList<TValue> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(self.UnsafeValues[i]);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(self.UnsafeValues[i]))
                        output.Add(self.UnsafeValues[i]);
                }
            }
        }

        public static void GetValueRangeIn<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayList<TValue> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(in self.UnsafeValues[i]);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(in self.UnsafeValues[i]))
                        output.Add(in self.UnsafeValues[i]);
                }
            }
        }

        public static void GetValueRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range, ICollection<TValue> output, bool allowDuplicate = false)
        {
            if (self == null || output == null)
                return;

            self.Validate(range);

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(self.UnsafeValues[i]);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(self.UnsafeValues[i]))
                        output.Add(self.UnsafeValues[i]);
                }
            }
        }

        public static void GetValueRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayList<TValue> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetValueRangeIn<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayList<TValue> output, bool allowDuplicate = true, bool allowNull = false)
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
                        output.Add(in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (!self.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.Contains(in value))
                    output.Add(in value);
            }
        }

        public static void GetValueRange<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ICollection<TValue> output, bool allowDuplicate = true, bool allowNull = false)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Validate<TKey, TValue>(this ArrayDictionary<TKey, TValue> self, in UIntRange range)
        {
            if (range.Start >= self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));
        }
    }
}
