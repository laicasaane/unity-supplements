namespace System
{
    public static partial class Enum<T> where T : unmanaged, Enum
    {
        public static Type Type { get; }

        public static Type UnderlyingType { get; }

        public static ReadArray1<T> Values { get; }

        public static ReadArray1<string> Names { get; }

        static Enum()
        {
            Type = typeof(T);
            UnderlyingType = Enum.GetUnderlyingType(Type);
            Values = (T[])Enum.GetValues(Type);
            Names = Enum.GetNames(Type);
        }

        public static T Parse(string value)
            => (T)Enum.Parse(Type, value);

        public static T Parse(string value, bool ignoreCase)
            => (T)Enum.Parse(Type, value, ignoreCase);

        public static bool TryParse(string value, out T result)
            => Enum.TryParse(value, out result);

        public static bool TryParse(string value, bool ignoreCase, out T result)
            => Enum.TryParse(value, ignoreCase, out result);

        public static bool IsDefined(object value)
            => Enum.IsDefined(Type, value);

        public static string Format(object value, string format)
            => Enum.Format(Type, value, format);

        public static T From(ulong value)
            => (T)Enum.ToObject(Type, value);

        public static T From(uint value)
            => (T)Enum.ToObject(Type, value);

        public static T From(ushort value)
            => (T)Enum.ToObject(Type, value);

        public static T From(sbyte value)
            => (T)Enum.ToObject(Type, value);

        public static T From(long value)
            => (T)Enum.ToObject(Type, value);

        public static T From(int value)
            => (T)Enum.ToObject(Type, value);

        public static T From(byte value)
            => (T)Enum.ToObject(Type, value);

        public static T From(short value)
            => (T)Enum.ToObject(Type, value);

        public static T From(object value)
            => (T)Enum.ToObject(Type, value);
    }
}