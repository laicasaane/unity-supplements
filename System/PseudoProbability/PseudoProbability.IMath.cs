namespace System
{
    public partial class PseudoProbability
    {
        public interface IMath
        {
            float Abs(float f);

            int CeilToInt(float f);

            int RoundToInt(float f);

            float Min(float a, float b);

            float Clamp(float value, float min, float max);

            int Clamp(int value, int min, int max);
        }

        private readonly struct PMath : IMath
        {
            public float Abs(float f)
                => Math.Abs(f);

            public int CeilToInt(float f)
                => (int)Math.Ceiling(f);

            public float Clamp(float value, float min, float max)
            {
                if (value < min)
                    value = min;
                else if (value > max)
                    value = max;

                return value;
            }

            public int Clamp(int value, int min, int max)
            {
                if (value < min)
                    value = min;
                else if (value > max)
                    value = max;

                return value;
            }

            public float Min(float a, float b)
                => a < b ? a : b;

            public int RoundToInt(float f)
                => (int)Math.Round(f);

            public static PMath Default { get; } = new PMath();
        }
    }
}