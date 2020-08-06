using System.Collections.Generic;

namespace System
{
    public sealed partial class PseudoProbability
    {
        private readonly Dictionary<int, float> cValues;
        private readonly IMath math;
        private readonly IRandom rand;

        public PseudoProbability() : this(PMath.Default, PRandom.Default) { }

        public PseudoProbability(IMath math) : this(math, PRandom.Default) { }

        public PseudoProbability(IRandom rand) : this(PMath.Default, rand) { }

        public PseudoProbability(IMath math, IRandom rand)
        {
            this.math = math ?? throw new ArgumentNullException(nameof(rand));
            this.rand = rand ?? throw new ArgumentNullException(nameof(rand));
            this.cValues = new Dictionary<int, float>();
            Generate();
        }

        public void Generate()
        {
            this.cValues.Clear();

            for (var p = 1; p < 1000; p++)
            {
                var c = PRD.GetCFromP(p / 1000f, this.math);
                this.cValues.Add(p, c * 100f);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chance">In the range of [0.0, 1.0]</param>
        /// <returns></returns>
        public bool DoIHaveLuck(float chance)
        {
            var r = this.rand.Value;
            return r <= chance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chance">In the range of [1.0, 100.0]</param>
        /// <returns></returns>
        public bool DoIHaveLuckInHundred(float chance)
        {
            var r = this.rand.Range(1f, 101f);
            return r <= chance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chance">In the range of [1, 100]</param>
        /// <returns></returns>
        public bool DoIHaveLuckInHundred(int chance)
        {
            var r = this.rand.Range(1, 101);
            return r <= chance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chance">In the range of [0.0, 1.0]</param>
        /// <param name="n"></param>
        /// <param name="n_0">Number of failed attempts</param>
        /// <param name="n_1">n_0 if succeeded or n+1 if failed</param>
        /// <returns></returns>
        public bool DoIHaveLuck(float chance, int n, int n_0, out int n_1)
        {
            var thousand = this.math.RoundToInt(this.math.Clamp(chance, 0f, 1f) * 1000);

            if (thousand <= 0)
                thousand = 1;

            return DoIHaveLuck_Internal(thousand, n, n_0, out n_1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chance">In the range of [1, 100]</param>
        /// <param name="n"></param>
        /// <param name="n_0">Number of failed attempts</param>
        /// <param name="n_1">n_0 if succeeded or n+1 if failed</param>
        /// <returns></returns>
        public bool DoIHaveLuckInHundred(int chance, int n, int n_0, out int n_1)
        {
            var thousand = this.math.Clamp(chance, 1, 100) * 10;
            return DoIHaveLuck_Internal(thousand, n, n_0, out n_1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chance">In the range of [1.0, 100.0]</param>
        /// <param name="n"></param>
        /// <param name="n_0">Number of failed attempts</param>
        /// <param name="n_1">n_0 if succeeded or n+1 if failed</param>
        /// <returns></returns>
        public bool DoIHaveLuckInHundred(float chance, int n, int n_0, out int n_1)
        {
            var thousand = this.math.RoundToInt(this.math.Clamp(chance, 1f, 100f) * 10);
            return DoIHaveLuck_Internal(thousand, n, n_0, out n_1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chance">In the range of [1, 1000]</param>
        /// <param name="n"></param>
        /// <param name="n_0">Number of failed attempts</param>
        /// <param name="n_1">n_0 if succeeded or n+1 if failed</param>
        /// <returns></returns>
        public bool DoIHaveLuckInThousand(int chance, int n, int n_0, out int n_1)
        {
            var thousand = this.math.Clamp(chance, 1, 1000);
            return DoIHaveLuck_Internal(thousand, n, n_0, out n_1);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="chance">In the range of [1.0, 1000.0]</param>
        /// <param name="n"></param>
        /// <param name="n_0">Number of failed attempts</param>
        /// <param name="n_1">n_0 if succeeded or n+1 if failed</param>
        /// <returns></returns>
        public bool DoIHaveLuckInThousand(float chance, int n, int n_0, out int n_1)
        {
            var thousand = this.math.RoundToInt(this.math.Clamp(chance, 1f, 1000f));
            return DoIHaveLuck_Internal(thousand, n, n_0, out n_1);
        }

        private bool DoIHaveLuck_Internal(int thousand, int n, int n_0, out int n_1)
        {
            if (thousand < 1000)
            {
                var c = this.cValues[thousand];
                var p = c * n;
                var r = this.rand.Value * 100f;

                if (r > p)
                {
                    n_1 = n + 1;
                    return false;
                }
            }

            n_1 = n_0;
            return true;
        }
    }
}