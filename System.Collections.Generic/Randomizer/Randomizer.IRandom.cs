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

        private sealed class PRandom : IRandom
        {
            private readonly Random rand = new Random();

            public int Range(int min, int max)
                => this.rand.Next(min, max);

            public static PRandom Default { get; } = new PRandom();
        }
    }
}