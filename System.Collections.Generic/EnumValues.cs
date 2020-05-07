namespace System.Collections.Generic
{
    public abstract class EnumValues<T> where T : struct, Enum
    {
        private readonly static T[] _values;

        public static ArraySegment<T> Values
            => _values;

        public static int ValueCount { get; }

        static EnumValues()
        {
            _values = (T[])Enum.GetValues(typeof(T));

            var index = 0;

            foreach (var e in _values)
            {
                var val = (int)(object)e;

                if (index < val)
                    index = val;
            }

            ValueCount = index + 1;
        }
    }
}