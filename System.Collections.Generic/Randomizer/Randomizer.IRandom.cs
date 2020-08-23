namespace System.Collections.Generic
{
    public static partial class Randomizer
    {
        public interface IRandom
        {
            /// <summary>
            /// Return a random float number between min [inclusive] and max [exclusive]
            /// </summary>
            /// <param name="min"></param>
            /// <param name="max"></param>
            /// <returns></returns>
            int Range(int min, int max);
        }

        private readonly struct DefaultRandom : IRandom
        {
            public int Range(int min, int max)
                => _rand.Next(min, max);

            private static readonly Random _rand = new Random();

            public static DefaultRandom Default { get; } = new DefaultRandom();
        }
    }
}