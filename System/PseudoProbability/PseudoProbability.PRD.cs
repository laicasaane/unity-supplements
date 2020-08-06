namespace System
{
    public partial class PseudoProbability
    {
        /// <summary>
        /// Pseudo Random Distribution
        /// </summary>
        public static class PRD
        {
            public static float GetCFromP(float p, IMath math)
            {
                var upperC = p;
                var lowerC = 0f;
                var p2 = 1f;
                float midC;
                float p1;

                while (true)
                {
                    midC = (upperC + lowerC) / 2f;
                    p1 = GetPFromC(midC, math);

                    if (math.Abs(p1 - p2) <= 0f)
                        break;

                    if (p1 > p)
                        upperC = midC;
                    else
                        lowerC = midC;

                    p2 = p1;
                }

                return midC;
            }

            public static float GetPFromC(float c, IMath math)
            {
                var pProcByN = 0f;
                var sumNpProcOnN = 0f;

                var maxFails = math.CeilToInt(1f / c);

                for (var N = 1; N <= maxFails; ++N)
                {
                    var pProcOnN = math.Min(1f, N * c) * (1f - pProcByN);
                    pProcByN += pProcOnN;
                    sumNpProcOnN += N * pProcOnN;
                }

                return 1f / sumNpProcOnN;
            }
        }
    }
}