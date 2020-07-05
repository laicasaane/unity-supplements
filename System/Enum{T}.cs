namespace System
{
    using CG = Collections.Generic;

    public static partial class Enum<T> where T : struct, Enum
    {
        public static Type UnderlyingType { get; }

        public static CG.ArraySegment<T> Values { get; }

        public static CG.ArraySegment<string> Names { get; }

        private static readonly Type _type;

        static Enum()
        {
            _type = typeof(T);
            UnderlyingType = Enum.GetUnderlyingType(_type);
            Values = (T[])Enum.GetValues(_type);
            Names = Enum.GetNames(_type);
        }
    }
}