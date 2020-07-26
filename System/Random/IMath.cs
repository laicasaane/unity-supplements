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
    }
}