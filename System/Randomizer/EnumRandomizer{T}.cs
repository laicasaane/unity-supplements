using System.Collections.Generic;

namespace System
{
    using ConcurrentPool = Collections.Pooling.Concurrent.ConcurrentPool;

    public class EnumRandomizer<T> where T : unmanaged, Enum
    {
        private static readonly Random _rand = new Random();

        private readonly List<T> primaryValues = new List<T>();
        private readonly List<T> values = new List<T>();
        private readonly Random rand = new Random();

        public EnumRandomizer() : this(Enum<T>.Values)
        {
        }

        public EnumRandomizer(params T[] values) : this(values.AsReadArray())
        {
        }

        public EnumRandomizer(in Segment<T> values)
        {
            if (values.Count <= 0)
                throw new ArgumentOutOfRangeException(nameof(values), "Must be greater than 0");

            foreach (var value in values)
            {
                if (!this.primaryValues.Contains(value))
                    this.primaryValues.Add(value);
            }
        }

        public void Initialize(int sequenceAmount, bool divideByEnumCount = false)
        {
            var cache = ConcurrentPool.Provider.List<T>();
            var innerCache = ConcurrentPool.Provider.List<T>();
            var count = sequenceAmount;
            var enumCount = this.primaryValues.Count;
            var redundant = 0;

            if (divideByEnumCount && sequenceAmount > enumCount)
            {
                count = sequenceAmount / enumCount;
                redundant = sequenceAmount % enumCount;
            }

            for (var i = 0; i < count; i++)
            {
                cache.Clear();
                cache.AddRange(this.primaryValues);
                Randomize(this.rand, cache, innerCache);
                this.values.AddRange(innerCache);
            }

            if (redundant > 0)
            {
                cache.Clear();
                cache.AddRange(this.primaryValues);
                Randomize(this.rand, cache, innerCache, redundant);
                this.values.AddRange(innerCache);
            }

            cache.Clear();
            cache.AddRange(this.values);
            Randomize(this.rand, cache, this.values);

            ConcurrentPool.Provider.Return(cache, innerCache);
        }

        public T Random()
            => this.values.Count <= 0 ? RandomInternal(this.primaryValues) : RandomizeValue(this.rand, this.values);

        public static T Random(params T[] enumValues)
            => RandomInternal(enumValues);

        public static T Random(in Segment<T> enumValues)
            => RandomInternal(enumValues);

        private static T RandomInternal(in Segment<T> enumValues)
        {
            if (!enumValues.HasSource || enumValues.Count <= 0)
                return default;

            var cache = ConcurrentPool.Provider.List<T>();
            cache.AddRange(enumValues);

            var values = ConcurrentPool.Provider.List<T>();
            Randomize(_rand, cache, values);
            var value = RandomizeValue(_rand, values);

            ConcurrentPool.Provider.Return(cache, values);

            return value;
        }

        private static void Randomize(Random rand, List<T> cache, List<T> output, int? max = null)
        {
            output.Clear();

            var count = max ?? cache.Count;

            for (var i = 0; i < count; i++)
            {
                var index = rand.Next(0, cache.Count);
                output.Add(cache[index]);
                cache.RemoveAt(index);
            }
        }

        private static T RandomizeValue(Random rand, List<T> values)
        {
            var index = rand.Next(0, values.Count);
            var value = values[index];
            values.RemoveAt(index);

            return value;
        }
    }
}