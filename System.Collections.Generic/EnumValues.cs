namespace System.Collections.Generic
{
    public abstract class EnumValues<T> where T : struct, Enum
    {
        public static ReadArray1<T> Values => Enum<T>.Values;

        /// <summary>
        /// Total count of the underlying values
        /// </summary>
        public static int UnderlyingValueCount { get; }

        static EnumValues()
        {
            var index = 0;

            foreach (var e in Values)
            {
                var val = (int)(object)e;

                if (index < val)
                    index = val;
            }

            UnderlyingValueCount = index + 1;
        }
    }
}