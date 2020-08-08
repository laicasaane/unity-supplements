namespace System.Collections.Generic
{
    public abstract class EnumValues<T> where T : struct, Enum
    {
        public static ReadArray<T> Values { get; }

        [Obsolete("This property has been deprecated. Use UnderlyingValueCount instead.")]
        public static int ValueCount => UnderlyingValueCount;

        /// <summary>
        /// Total count of the underlying values
        /// </summary>
        public static int UnderlyingValueCount { get; }

        static EnumValues()
        {
            Values = Enum<T>.Values;

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