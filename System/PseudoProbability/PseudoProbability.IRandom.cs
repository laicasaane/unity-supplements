namespace System
{
    public partial class PseudoProbability
    {
        public interface IRandom
        {
            float Value { get; }

            float Range(float min, float max);
        }
    }
}