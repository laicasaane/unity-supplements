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

        private sealed class PRandom : IRandom
        {
            private readonly Random rand = new Random();

            public float Value => (float)this.rand.NextDouble();

            public float Range(float min, float max)
                => (float)(this.rand.NextDouble() * (max - min) + min);

            public static PRandom Default { get; } = new PRandom();
        }
    }
}