namespace System.Collections.Generic
{
    public abstract class EnumValues<T> where T : Enum
    {
        private static T[] _values;
        private static int _valueCount;

        public static Segment<T> Values
        {
            get
            {
                EnsureInitialization();
                return _values;
            }
        }

        public static int ValueCount
        {
            get
            {
                EnsureInitialization();
                return _valueCount;
            }
        }

        private static void EnsureInitialization()
        {
            if (_values != null)
                return;

            _values = (T[])Enum.GetValues(typeof(T));

            var index = 0;

            foreach (var e in _values)
            {
                var val = (int)(object)e;

                if (index < val)
                    index = val;
            }

            _valueCount = index + 1;
        }
    }
}