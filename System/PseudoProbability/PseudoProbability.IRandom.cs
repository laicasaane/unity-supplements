namespace System
{
    public partial class PseudoProbability
    {
        public interface IRandom
        {
            /// <summary>
            /// Returns a random number between 0.0 [inclusive] and 1.0 [inclusive]
            /// </summary>
            float Value { get; }

            /// <summary>
            /// Return a random float number between min [inclusive] and max [inclusive]
            /// </summary>
            /// <param name="min"></param>
            /// <param name="max"></param>
            /// <returns></returns>
            float Range(float min, float max);
        }

        private readonly struct PRandom : IRandom
        {
            public float Value => (float)_rand.NextDouble();

            public float Range(float min, float max)
                => (float)(_rand.NextDouble() * (max - min) + min);

            private static readonly Random _rand = new Random();

            public static PRandom Default { get; } = new PRandom();
        }
    }
}