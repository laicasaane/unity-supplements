using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.ArrayBased
{
    public static class ReadArrayDictionaryTKeyTValueExtensions
    {
        public static bool ValidateIndex<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, int index)
            => index >= 0 && index < self.Count;

        public static bool ValidateIndex<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, uint index)
            => index >= 0 && index < self.Count;

        public static void GetRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
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
                        output.Set(key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !source.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Set(key, value);
            }
        }

        public static void GetRangeIn<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (keys == null || output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !source.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Set(in key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !source.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Set(in key, in value);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (keys == null || output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicateValue)
            {
                foreach (var key in keys)
                {
                    if (output.ContainsKey(in key) ||
                        !source.TryGetValue(key, out var value))
                        continue;

                    if (allowNull || value != null)
                        output.Set(in key, value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(in key) ||
                    !source.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(value))
                    output.Set(in key, value);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
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
                        output.Set(key, in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (output.ContainsKey(key) ||
                    !source.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.ContainsValue(in value))
                    output.Set(key, in value);
            }
        }

        public static void GetRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, IDictionary<TKey, TValue> output, bool allowNull = false)
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

        public static void GetRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ICollection<KeyValuePair<TKey, TValue>> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, Dictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(source.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || source.UnsafeValues[i] != null)
                        output.Add(source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(source.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || source.UnsafeValues[i] != null) && !output.ContainsValue(source.UnsafeValues[i]))
                    output.Add(source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
            }
        }

        public static void GetRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(source.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || source.UnsafeValues[i] != null)
                        output.Add(source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(source.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || source.UnsafeValues[i] != null) && !output.ContainsValue(source.UnsafeValues[i]))
                    output.Add(source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
            }
        }

        public static void GetRangeIn<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(in source.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || source.UnsafeValues[i] != null)
                        output.Add(in source.UnsafeKeys[i].Key, in source.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(in source.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || source.UnsafeValues[i] != null) && !output.ContainsValue(in source.UnsafeValues[i]))
                    output.Add(in source.UnsafeKeys[i].Key, in source.UnsafeValues[i]);
            }
        }

        public static void GetRangeInKey<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(in source.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || source.UnsafeValues[i] != null)
                        output.Add(in source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(in source.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || source.UnsafeValues[i] != null) && !output.ContainsValue(source.UnsafeValues[i]))
                    output.Add(in source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
            }
        }

        public static void GetRangeInValue<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayDictionary<TKey, TValue> output, bool allowDuplicateValue = false, bool allowNull = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicateValue)
            {
                foreach (var i in range)
                {
                    if (output.ContainsKey(source.UnsafeKeys[i].Key))
                        continue;

                    if (allowNull || source.UnsafeValues[i] != null)
                        output.Add(source.UnsafeKeys[i].Key, in source.UnsafeValues[i]);
                }

                return;
            }

            foreach (var i in range)
            {
                if (output.ContainsKey(source.UnsafeKeys[i].Key))
                    continue;

                if ((allowNull || source.UnsafeValues[i] != null) && !output.ContainsValue(in source.UnsafeValues[i]))
                    output.Add(source.UnsafeKeys[i].Key, in source.UnsafeValues[i]);
            }
        }

        public static void GetRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, IDictionary<TKey, TValue> output, bool allowNull = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            foreach (var i in range)
            {
                if (output.ContainsKey(source.UnsafeKeys[i].Key))
                    continue;

                if (allowNull || source.UnsafeValues[i] != null)
                    output.Add(source.UnsafeKeys[i].Key, source.UnsafeValues[i]);
            }
        }

        public static void GetRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ICollection<KeyValuePair<TKey, TValue>> output, bool allowDuplicate = true, bool allowNull = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    if (allowNull || source.UnsafeValues[i] != null)
                        output.Add(new KeyValuePair<TKey, TValue>(source.UnsafeKeys[i].Key, source.UnsafeValues[i]));
                }

                return;
            }

            foreach (var i in range)
            {
                var kvp = new KeyValuePair<TKey, TValue>(source.UnsafeKeys[i].Key, source.UnsafeValues[i]);

                if ((allowNull || source.UnsafeValues[i] != null) && !output.Contains(kvp))
                    output.Add(kvp);
            }
        }

        public static void GetKeys<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, ArrayList<TKey> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicate)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    output.Add(source.UnsafeKeys[i].Key);
                }
            }
            else
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    if (output.Contains(source.UnsafeKeys[i].Key))
                        output.Add(source.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetKeysIn<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, ArrayList<TKey> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicate)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    output.Add(in source.UnsafeKeys[i].Key);
                }
            }
            else
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    if (output.Contains(in source.UnsafeKeys[i].Key))
                        output.Add(in source.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetKeys<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, ICollection<TKey> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicate)
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    output.Add(source.UnsafeKeys[i].Key);
                }
            }
            else
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    if (output.Contains(source.UnsafeKeys[i].Key))
                        output.Add(source.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetValues<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, ArrayList<TValue> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            var source = self.GetSource();

            if (allowDuplicate)
                output.AddRange(source.UnsafeValues, source.Count);
            else
            {
                for (var i = 0u; i < source.Count; i++)
                {
                    if (output.Contains(source.UnsafeValues[i]))
                        output.Add(source.UnsafeValues[i]);
                }
            }
        }

        public static void GetKeyRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayList<TKey> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(source.UnsafeKeys[i].Key);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(source.UnsafeKeys[i].Key))
                        output.Add(source.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetKeyRangeIn<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayList<TKey> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(in source.UnsafeKeys[i].Key);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(in source.UnsafeKeys[i].Key))
                        output.Add(in source.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetKeyRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ICollection<TKey> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(source.UnsafeKeys[i].Key);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(source.UnsafeKeys[i].Key))
                        output.Add(source.UnsafeKeys[i].Key);
                }
            }
        }

        public static void GetValueRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayList<TValue> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(source.UnsafeValues[i]);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(source.UnsafeValues[i]))
                        output.Add(source.UnsafeValues[i]);
                }
            }
        }

        public static void GetValueRangeIn<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ArrayList<TValue> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(in source.UnsafeValues[i]);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(in source.UnsafeValues[i]))
                        output.Add(in source.UnsafeValues[i]);
                }
            }
        }

        public static void GetValueRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range, ICollection<TValue> output, bool allowDuplicate = false)
        {
            if (output == null)
                return;

            self.Validate(range);

            var source = self.GetSource();

            if (allowDuplicate)
            {
                foreach (var i in range)
                {
                    output.Add(source.UnsafeValues[i]);
                }
            }
            else
            {
                foreach (var i in range)
                {
                    if (output.Contains(source.UnsafeValues[i]))
                        output.Add(source.UnsafeValues[i]);
                }
            }
        }

        public static void GetValueRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayList<TValue> output, bool allowDuplicate = true, bool allowNull = false)
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

        public static void GetValueRangeIn<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ArrayList<TValue> output, bool allowDuplicate = true, bool allowNull = false)
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
                        output.Add(in value);
                }

                return;
            }

            foreach (var key in keys)
            {
                if (!source.TryGetValue(key, out var value))
                    continue;

                if ((allowNull || value != null) && !output.Contains(in value))
                    output.Add(in value);
            }
        }

        public static void GetValueRange<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, IEnumerable<TKey> keys, ICollection<TValue> output, bool allowDuplicate = true, bool allowNull = false)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Validate<TKey, TValue>(in this ReadArrayDictionary<TKey, TValue> self, in UIntRange range)
        {
            if (range.Start >= self.Count)
                throw new IndexOutOfRangeException(nameof(range.Start));

            if (range.End >= self.Count)
                throw new IndexOutOfRangeException(nameof(range.End));
        }
    }
}
