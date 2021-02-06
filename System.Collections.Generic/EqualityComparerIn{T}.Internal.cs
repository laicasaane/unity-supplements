using System.Diagnostics.Contracts;

namespace System.Collections.Generic
{
    public partial class EqualityComparerIn<T>
    {
        private static EqualityComparerIn<T> CreateComparer()
        {
            var type = typeof(T);

            if (type.IsEnum)
            {
                return (EqualityComparerIn<T>)Activator.CreateInstance(typeof(EnumEqualityComparerIn<>).MakeGenericType(type));
            }

            var typecode = Type.GetTypeCode(type);

            switch (typecode)
            {
                case TypeCode.Boolean:
                    return (EqualityComparerIn<T>)(object)new BoolEqualityComparerIn();

                case TypeCode.Byte:
                    return (EqualityComparerIn<T>)(object)new ByteEqualityComparerIn();

                case TypeCode.Char:
                    return (EqualityComparerIn<T>)(object)new CharEqualityComparerIn();

                case TypeCode.DateTime:
                    return (EqualityComparerIn<T>)(object)new DateTimeEqualityComparerIn();

                case TypeCode.Decimal:
                    return (EqualityComparerIn<T>)(object)new DecimalEqualityComparerIn();

                case TypeCode.Double:
                    return (EqualityComparerIn<T>)(object)new DoubleEqualityComparerIn();

                case TypeCode.Int16:
                    return (EqualityComparerIn<T>)(object)new ShortEqualityComparerIn();

                case TypeCode.Int32:
                    return (EqualityComparerIn<T>)(object)new IntEqualityComparerIn();

                case TypeCode.Int64:
                    return (EqualityComparerIn<T>)(object)new LongEqualityComparerIn();

                case TypeCode.SByte:
                    return (EqualityComparerIn<T>)(object)new SByteEqualityComparerIn();

                case TypeCode.Single:
                    return (EqualityComparerIn<T>)(object)new FloatEqualityComparerIn();

                case TypeCode.String:
                    return (EqualityComparerIn<T>)(object)new StringEqualityComparerIn();

                case TypeCode.UInt16:
                    return (EqualityComparerIn<T>)(object)new UShortEqualityComparerIn();

                case TypeCode.UInt32:
                    return (EqualityComparerIn<T>)(object)new UIntEqualityComparerIn();

                case TypeCode.UInt64:
                    return (EqualityComparerIn<T>)(object)new ULongEqualityComparerIn();
            }

            if (typeof(IEquatableIn<T>).IsAssignableFrom(type))
            {
                return (EqualityComparerIn<T>)Activator.CreateInstance(typeof(EquatableInEqualityComparerIn<>).MakeGenericType(type));
            }

            if (typeof(IEquatable<T>).IsAssignableFrom(type))
            {
                return (EqualityComparerIn<T>)Activator.CreateInstance(typeof(EquatableEqualityComparerIn<>).MakeGenericType(type));
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var u = type.GetGenericArguments()[0];

                if (u.IsEnum)
                {
                    return (EqualityComparerIn<T>)Activator.CreateInstance(typeof(NullableEnumEqualityComparerIn<>).MakeGenericType(u));
                }

                if (typeof(IEquatable<>).MakeGenericType(u).IsAssignableFrom(u))
                {
                    return (EqualityComparerIn<T>)Activator.CreateInstance(typeof(NullableEquatableStructEqualityComparerIn<>).MakeGenericType(u));
                }

                if (u.IsValueType)
                {
                    return (EqualityComparerIn<T>)Activator.CreateInstance(typeof(NullableStructEqualityComparerIn<>).MakeGenericType(u));
                }
            }

            if (type.IsValueType)
            {
                return (EqualityComparerIn<T>)Activator.CreateInstance(typeof(StructEqualityComparerIn<>).MakeGenericType(type));
            }

            return new ObjectEqualityComparerIn<T>();
        }
    }

    internal sealed class BoolEqualityComparerIn : EqualityComparerIn<bool>
    {
        [Pure]
        public override bool Equals(in bool x, in bool y)
            => x == y;

        [Pure]
        public override bool Equals(bool x, bool y)
            => x == y;

        [Pure]
        public override int GetHashCode(in bool b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(bool b)
            => b.GetHashCode();
    }

    internal sealed class ByteEqualityComparerIn : EqualityComparerIn<byte>
    {
        [Pure]
        public override bool Equals(in byte x, in byte y)
            => x == y;

        [Pure]
        public override bool Equals(byte x, byte y)
            => x == y;

        [Pure]
        public override int GetHashCode(in byte b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(byte b)
            => b.GetHashCode();
    }

    internal sealed class SByteEqualityComparerIn : EqualityComparerIn<sbyte>
    {
        [Pure]
        public override bool Equals(in sbyte x, in sbyte y)
            => x == y;

        [Pure]
        public override bool Equals(sbyte x, sbyte y)
            => x == y;

        [Pure]
        public override int GetHashCode(in sbyte b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(sbyte b)
            => b.GetHashCode();
    }

    internal sealed class CharEqualityComparerIn : EqualityComparerIn<char>
    {
        [Pure]
        public override bool Equals(in char x, in char y)
            => x == y;

        [Pure]
        public override bool Equals(char x, char y)
            => x == y;

        [Pure]
        public override int GetHashCode(in char b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(char b)
            => b.GetHashCode();
    }

    internal sealed class DecimalEqualityComparerIn : EqualityComparerIn<decimal>
    {
        [Pure]
        public override bool Equals(in decimal x, in decimal y)
            => x == y;

        [Pure]
        public override bool Equals(decimal x, decimal y)
            => x == y;

        [Pure]
        public override int GetHashCode(in decimal b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(decimal b)
            => b.GetHashCode();
    }

    internal sealed class DoubleEqualityComparerIn : EqualityComparerIn<double>
    {
        [Pure]
        public override bool Equals(in double x, in double y)
            => x == y;

        [Pure]
        public override bool Equals(double x, double y)
            => x == y;

        [Pure]
        public override int GetHashCode(in double b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(double b)
            => b.GetHashCode();
    }

    internal sealed class FloatEqualityComparerIn : EqualityComparerIn<float>
    {
        [Pure]
        public override bool Equals(in float x, in float y)
            => x == y;

        [Pure]
        public override bool Equals(float x, float y)
            => x == y;

        [Pure]
        public override int GetHashCode(in float b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(float b)
            => b.GetHashCode();
    }

    internal sealed class IntEqualityComparerIn : EqualityComparerIn<int>
    {
        [Pure]
        public override bool Equals(in int x, in int y)
            => x == y;

        [Pure]
        public override bool Equals(int x, int y)
            => x == y;

        [Pure]
        public override int GetHashCode(in int b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(int b)
            => b.GetHashCode();
    }

    internal sealed class UIntEqualityComparerIn : EqualityComparerIn<uint>
    {
        [Pure]
        public override bool Equals(in uint x, in uint y)
            => x == y;

        [Pure]
        public override bool Equals(uint x, uint y)
            => x == y;

        [Pure]
        public override int GetHashCode(in uint b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(uint b)
            => b.GetHashCode();
    }

    internal sealed class LongEqualityComparerIn : EqualityComparerIn<long>
    {
        [Pure]
        public override bool Equals(in long x, in long y)
            => x == y;

        [Pure]
        public override bool Equals(long x, long y)
            => x == y;

        [Pure]
        public override int GetHashCode(in long b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(long b)
            => b.GetHashCode();
    }

    internal sealed class ULongEqualityComparerIn : EqualityComparerIn<ulong>
    {
        [Pure]
        public override bool Equals(in ulong x, in ulong y)
            => x == y;

        [Pure]
        public override bool Equals(ulong x, ulong y)
            => x == y;

        [Pure]
        public override int GetHashCode(in ulong b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(ulong b)
            => b.GetHashCode();
    }

    internal sealed class ShortEqualityComparerIn : EqualityComparerIn<short>
    {
        [Pure]
        public override bool Equals(in short x, in short y)
            => x == y;

        [Pure]
        public override bool Equals(short x, short y)
            => x == y;

        [Pure]
        public override int GetHashCode(in short b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(short b)
            => b.GetHashCode();
    }

    internal sealed class UShortEqualityComparerIn : EqualityComparerIn<ushort>
    {
        [Pure]
        public override bool Equals(in ushort x, in ushort y)
            => x == y;

        [Pure]
        public override bool Equals(ushort x, ushort y)
            => x == y;

        [Pure]
        public override int GetHashCode(in ushort b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(ushort b)
            => b.GetHashCode();
    }

    internal class EnumEqualityComparerIn<T> : EqualityComparerIn<T> where T : unmanaged, Enum
    {
        [Pure]
        public override bool Equals(in T x, in T y)
            => x.GetHashCode() == y.GetHashCode();

        [Pure]
        public override bool Equals(T x, T y)
            => x.GetHashCode() == y.GetHashCode();

        [Pure]
        public override int GetHashCode(in T obj)
            => obj.GetHashCode();

        [Pure]
        public override int GetHashCode(T obj)
            => obj.GetHashCode();
    }

    internal sealed class StringEqualityComparerIn : EqualityComparerIn<string>
    {
        [Pure]
        public override bool Equals(in string x, in string y)
            => string.Equals(x, y);

        [Pure]
        public override bool Equals(string x, string y)
            => string.Equals(x, y);

        [Pure]
        public override int GetHashCode(in string b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(string b)
            => b.GetHashCode();
    }

    internal sealed class DateTimeEqualityComparerIn : EqualityComparerIn<DateTime>
    {
        [Pure]
        public override bool Equals(in DateTime x, in DateTime y)
            => x == y;

        [Pure]
        public override bool Equals(DateTime x, DateTime y)
            => x == y;

        [Pure]
        public override int GetHashCode(in DateTime b)
            => b.GetHashCode();

        [Pure]
        public override int GetHashCode(DateTime b)
            => b.GetHashCode();
    }

    internal sealed class EquatableInEqualityComparerIn<T> : EqualityComparerIn<T> where T : IEquatableIn<T>
    {
        public override bool Equals(in T x, in T y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(in y);
        }

        public override bool Equals(T x, T y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(in y);
        }

        public override int GetHashCode(in T obj)
            => obj.GetHashCode();

        public override int GetHashCode(T obj)
            => obj.GetHashCode();
    }

    internal sealed class EquatableEqualityComparerIn<T> : EqualityComparerIn<T> where T : IEquatable<T>
    {
        public override bool Equals(in T x, in T y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public override bool Equals(T x, T y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public override int GetHashCode(in T obj)
            => obj.GetHashCode();

        public override int GetHashCode(T obj)
            => obj.GetHashCode();
    }

    internal class NullableEquatableStructEqualityComparerIn<T> : EqualityComparerIn<T?> where T : struct, IEquatable<T>
    {
        [Pure]
        public override bool Equals(in T? x, in T? y)
        {
            if (x.HasValue)
            {
                if (y.HasValue)
                    return x.Value.Equals(y.Value);
                return false;
            }
            if (y.HasValue) return false;
            return true;
        }

        [Pure]
        public override bool Equals(T? x, T? y)
        {
            if (x.HasValue)
            {
                if (y.HasValue)
                    return x.Value.Equals(y.Value);
                return false;
            }
            if (y.HasValue) return false;
            return true;
        }

        [Pure]
        public override int GetHashCode(in T? obj)
            => obj.GetHashCode();

        [Pure]
        public override int GetHashCode(T? obj)
            => obj.GetHashCode();
    }

    internal class NullableEnumEqualityComparerIn<T> : EqualityComparerIn<T?> where T : unmanaged, Enum
    {
        [Pure]
        public override bool Equals(in T? x, in T? y)
        {
            if (x.HasValue)
            {
                if (y.HasValue)
                    return x.Value.GetHashCode() == y.Value.GetHashCode();
                return false;
            }
            if (y.HasValue) return false;
            return true;
        }

        [Pure]
        public override bool Equals(T? x, T? y)
        {
            if (x.HasValue)
            {
                if (y.HasValue)
                    return x.Value.GetHashCode() == y.Value.GetHashCode();
                return false;
            }
            if (y.HasValue) return false;
            return true;
        }

        [Pure]
        public override int GetHashCode(in T? obj)
            => obj.GetHashCode();

        [Pure]
        public override int GetHashCode(T? obj)
            => obj.GetHashCode();
    }

    internal class NullableStructEqualityComparerIn<T> : EqualityComparerIn<T?> where T : struct
    {
        [Pure]
        public override bool Equals(in T? x, in T? y)
        {
            if (x.HasValue)
            {
                if (y.HasValue)
                    return x.Value.Equals(y.Value);
                return false;
            }
            if (y.HasValue) return false;
            return true;
        }

        [Pure]
        public override bool Equals(T? x, T? y)
        {
            if (x.HasValue)
            {
                if (y.HasValue)
                    return x.Value.Equals(y.Value);
                return false;
            }
            if (y.HasValue) return false;
            return true;
        }

        [Pure]
        public override int GetHashCode(in T? obj)
            => obj.GetHashCode();

        [Pure]
        public override int GetHashCode(T? obj)
            => obj.GetHashCode();
    }

    internal sealed class StructEqualityComparerIn<T> : EqualityComparerIn<T> where T : struct
    {
        public override bool Equals(in T x, in T y)
            => x.Equals(y);

        public override bool Equals(T x, T y)
            => x.Equals(y);

        public override int GetHashCode(in T obj)
            => obj.GetHashCode();

        public override int GetHashCode(T obj)
            => obj.GetHashCode();
    }

    internal sealed class ObjectEqualityComparerIn<T> : EqualityComparerIn<T>
    {
        public override bool Equals(in T x, in T y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public override bool Equals(T x, T y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Equals(y);
        }

        public override int GetHashCode(in T obj)
            => obj.GetHashCode();

        public override int GetHashCode(T obj)
            => obj.GetHashCode();
    }
}
