namespace System.Runtime.Serialization
{
    public static class SerializationInfoExtensions
    {
        public static bool GetBooleanOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetBoolean(name);
            }
            catch
            {
                return default;
            }
        }

        public static byte GetByteOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetByte(name);
            }
            catch
            {
                return default;
            }
        }

        public static char GetCharOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetChar(name);
            }
            catch
            {
                return default;
            }
        }

        public static DateTime GetDateTimeOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetDateTime(name);
            }
            catch
            {
                return default;
            }
        }

        public static decimal GetDecimalOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetDecimal(name);
            }
            catch
            {
                return default;
            }
        }

        public static double GetDoubleOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetDouble(name);
            }
            catch
            {
                return default;
            }
        }

        public static short GetInt16OrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetInt16(name);
            }
            catch
            {
                return default;
            }
        }

        public static int GetInt32OrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetInt32(name);
            }
            catch
            {
                return default;
            }
        }

        public static long GetInt64OrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetInt64(name);
            }
            catch
            {
                return default;
            }
        }

        public static sbyte GetSByteOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetSByte(name);
            }
            catch
            {
                return default;
            }
        }

        public static float GetSingleOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetSingle(name);
            }
            catch
            {
                return default;
            }
        }

        public static string GetStringOrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetString(name);
            }
            catch
            {
                return default;
            }
        }

        public static ushort GetUInt16OrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetUInt16(name);
            }
            catch
            {
                return default;
            }
        }

        public static uint GetUInt32OrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetUInt32(name);
            }
            catch
            {
                return default;
            }
        }

        public static ulong GetUInt64OrDefault(this SerializationInfo self, string name)
        {
            try
            {
                return self.GetUInt64(name);
            }
            catch
            {
                return default;
            }
        }

        public static object GetValueOrDefault(this SerializationInfo self, string name, Type type)
        {
            try
            {
                return self.GetValue(name, type);
            }
            catch
            {
                return default;
            }
        }

        public static object GetValueOrDefault(this SerializationInfo self, string name, Type type, object @default)
        {
            try
            {
                return self.GetValue(name, type);
            }
            catch
            {
                return @default;
            }
        }

        public static T GetValueOrDefault<T>(this SerializationInfo self, string name)
        {
            try
            {
                if (self.GetValue(name, typeof(T)) is T value)
                    return value;

                return default;
            }
            catch
            {
                return default;
            }
        }

        public static T GetValueOrDefault<T>(this SerializationInfo self, string name, T @default)
        {
            try
            {
                if (self.GetValue(name, typeof(T)) is T value)
                    return value;

                return @default;
            }
            catch
            {
                return @default;
            }
        }
    }
}