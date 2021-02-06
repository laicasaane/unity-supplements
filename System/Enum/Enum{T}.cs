namespace System
{
    public static class Enum<T> where T : unmanaged, Enum
    {
        public static readonly Type Type;
        public static readonly Type UnderlyingType;
        public static readonly ReadArray1<T> Values;
        public static readonly ReadArray1<string> Names;

        static Enum()
        {
            Type = typeof(T);
            UnderlyingType = Enum.GetUnderlyingType(Type);
            Values = (T[])Enum.GetValues(Type);
            Names = Enum.GetNames(Type);
        }

        public static T From(byte value)
        {
            try { return (T)(object)value; }
            catch { return default; }
        }

        public static T From(sbyte value)
        {
            try { return (T)(object)value; }
            catch { return default; }
        }

        public static T From(short value)
        {
            try { return (T)(object)value; }
            catch { return default; }
        }

        public static T From(ushort value)
        {
            try { return (T)(object)value; }
            catch { return default; }
        }

        public static T From(int value)
        {
            try { return (T)(object)value; }
            catch { return default; }
        }

        public static T From(uint value)
        {
            try { return (T)(object)value; }
            catch { return default; }
        }

        public static T From(long value)
        {
            try { return (T)(object)value; }
            catch { return default; }
        }

        public static T From(ulong value)
        {
            try { return (T)(object)value; }
            catch { return default; }
        }

        public static T From(object value)
        {
            try { return (T)Enum.ToObject(Type, value); }
            catch { return default; }
        }

        public static byte ToByte(T value)
        {
            try { return (byte)(object)value; }
            catch { return default; }
        }

        public static sbyte ToSByte(T value)
        {
            try { return (sbyte)(object)value; }
            catch { return default; }
        }

        public static short ToShort(T value)
        {
            try { return (short)(object)value; }
            catch { return default; }
        }

        public static ushort ToUShort(T value)
        {
            try { return (ushort)(object)value; }
            catch { return default; }
        }

        public static int ToInt(T value)
        {
            try { return (int)(object)value; }
            catch { return default; }
        }

        public static uint ToUInt(T value)
        {
            try { return (uint)(object)value; }
            catch { return default; }
        }

        public static long ToLong(T value)
        {
            try { return (long)(object)value; }
            catch { return default; }
        }

        public static ulong ToULong(T value)
        {
            try { return (ulong)(object)value; }
            catch { return default; }
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
    }
}