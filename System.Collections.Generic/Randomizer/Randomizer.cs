namespace System.Collections.Generic
{
    public static partial class Randomizer
    {
        public static IReadOnlyList<T> Randomize<T>(this IEnumerable<T> collection)
            => Randomize(collection, PRandom.Default, PCache<T>.Default);

        public static IReadOnlyList<T> Randomize<T>(this IEnumerable<T> collection, IRandom rand)
            => Randomize(collection, rand, PCache<T>.Default);

        public static IReadOnlyList<T> Randomize<T>(this IEnumerable<T> collection, ICache<T> cache)
            => Randomize(collection, PRandom.Default, cache);

        public static IReadOnlyList<T> Randomize<T>(this IEnumerable<T> collection, IRandom rand, ICache<T> cache)
        {
            if (rand == null) throw new ArgumentNullException(nameof(rand));
            if (cache == null) throw new ArgumentNullException(nameof(cache));

            cache.Clear();

            if (collection != null)
                cache.Input.AddRange(collection);

            for (var i = 0; i < cache.Input.Count; i++)
            {
                var index = rand.Range(0, cache.Input.Count);
                cache.Output.Add(cache.Input[index]);
                cache.Input.RemoveAt(index);
            }

            return cache.Output;
        }

        public static IReadOnlyList<T> RandomizeAllocated<T>(this IEnumerable<T> collection)
            => Randomize(collection, PRandom.Default, new PCache<T>());

        public static IReadOnlyList<T> RandomizeAllocated<T>(this IEnumerable<T> collection, IRandom rand)
            => Randomize(collection, rand, new PCache<T>());
    }
}