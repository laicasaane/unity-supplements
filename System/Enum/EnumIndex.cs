using System.Collections.Generic;

namespace System
{
    public static class EnumIndex
    {
        public static int ToIndex<T>(this T value) where T : struct, Enum
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        public static Index2 ToIndex<T1, T2>(in this (T1 t1, T2 t2) value)
            where T1 : struct, Enum
            where T2 : struct, Enum
            => new Index2(value.t1.ToIndex(), value.t2.ToIndex());

        public static Index3 ToIndex<T1, T2, T3>(in this (T1 t1, T2 t2, T3 t3) value)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            => new Index3(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex());

        public static Index4 ToIndex<T1, T2, T3, T4>(in this (T1 t1, T2 t2, T3 t3, T4 t4) value)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            => new Index4(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex(), value.t4.ToIndex());

        public static Index5 ToIndex<T1, T2, T3, T4, T5>(in this (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) value)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            where T5 : struct, Enum
            => new Index5(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex(), value.t4.ToIndex(), value.t5.ToIndex());

        public static int ToIndex1<T1, T2>(in this (T1 t1, T2 t2) value)
            where T1 : struct, Enum
            where T2 : struct, Enum
            => new Index2(value.t1.ToIndex(), value.t2.ToIndex()).ToIndex1(EnumValues<T1>.UnderlyingValueCount);

        public static int ToIndex1<T1, T2, T3>(in this (T1 t1, T2 t2, T3 t3) value)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            => new Index3(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex())
               .ToIndex1(EnumValues<T1>.UnderlyingValueCount, EnumValues<T2>.UnderlyingValueCount);

        public static int ToIndex1<T1, T2, T3, T4>(in this (T1 t1, T2 t2, T3 t3, T4 t4) value)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            => new Index4(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex(), value.t4.ToIndex())
               .ToIndex1(EnumValues<T1>.UnderlyingValueCount, EnumValues<T2>.UnderlyingValueCount,
                         EnumValues<T3>.UnderlyingValueCount);

        public static int ToIndex1<T1, T2, T3, T4, T5>(in this (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) value)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            where T5 : struct, Enum
            => new Index5(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex(), value.t4.ToIndex(), value.t5.ToIndex())
               .ToIndex1(EnumValues<T1>.UnderlyingValueCount, EnumValues<T2>.UnderlyingValueCount,
                         EnumValues<T3>.UnderlyingValueCount, EnumValues<T4>.UnderlyingValueCount);

        public static int ToIndex1<T1, T2>(in this (T1 t1, T2 t2) value, int lengthT1)
            where T1 : struct, Enum
            where T2 : struct, Enum
            => new Index2(value.t1.ToIndex(), value.t2.ToIndex()).ToIndex1(lengthT1);

        public static int ToIndex1<T1, T2>(in this (T1 t1, T2 t2) value, in Length2 length)
            where T1 : struct, Enum
            where T2 : struct, Enum
            => new Index2(value.t1.ToIndex(), value.t2.ToIndex()).ToIndex1(length);

        public static int ToIndex1<T1, T2, T3>(in this (T1 t1, T2 t2, T3 t3) value, in Length2 length)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            => new Index3(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex()).ToIndex1(length);

        public static int ToIndex1<T1, T2, T3, T4>(in this (T1 t1, T2 t2, T3 t3, T4 t4) value, in Length3 length)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            => new Index4(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex(), value.t4.ToIndex()).ToIndex1(length);

        public static int ToIndex1<T1, T2, T3, T4, T5>(in this (T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) value, in Length4 length)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            where T5 : struct, Enum
            => new Index5(value.t1.ToIndex(), value.t2.ToIndex(), value.t3.ToIndex(), value.t4.ToIndex(), value.t5.ToIndex())
               .ToIndex1(length);

        public static Index2 ToIndex<T1, T2>(T1 t1, T2 t2)
            where T1 : struct, Enum
            where T2 : struct, Enum
            => new Index2(t1.ToIndex(), t2.ToIndex());

        public static Index3 ToIndex<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            => new Index3(t1.ToIndex(), t2.ToIndex(), t3.ToIndex());

        public static Index4 ToIndex<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            => new Index4(t1.ToIndex(), t2.ToIndex(), t3.ToIndex(), t4.ToIndex());

        public static Index5 ToIndex<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            where T5 : struct, Enum
            => new Index5(t1.ToIndex(), t2.ToIndex(), t3.ToIndex(), t4.ToIndex(), t5.ToIndex());

        public static int ToIndex1<T1, T2>(T1 t1, T2 t2)
            where T1 : struct, Enum
            where T2 : struct, Enum
            => new Index2(t1.ToIndex(), t2.ToIndex()).ToIndex1(EnumValues<T1>.UnderlyingValueCount);

        public static int ToIndex1<T1, T2, T3>(T1 t1, T2 t2, T3 t3)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            => new Index3(t1.ToIndex(), t2.ToIndex(), t3.ToIndex())
               .ToIndex1(EnumValues<T1>.UnderlyingValueCount, EnumValues<T2>.UnderlyingValueCount);

        public static int ToIndex1<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            => new Index4(t1.ToIndex(), t2.ToIndex(), t3.ToIndex(), t4.ToIndex())
               .ToIndex1(EnumValues<T1>.UnderlyingValueCount, EnumValues<T2>.UnderlyingValueCount,
                         EnumValues<T3>.UnderlyingValueCount);

        public static int ToIndex1<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            where T5 : struct, Enum
            => new Index5(t1.ToIndex(), t2.ToIndex(), t3.ToIndex(), t4.ToIndex(), t5.ToIndex())
               .ToIndex1(EnumValues<T1>.UnderlyingValueCount, EnumValues<T2>.UnderlyingValueCount,
                         EnumValues<T3>.UnderlyingValueCount, EnumValues<T4>.UnderlyingValueCount);

        public static int ToIndex1<T1, T2>(T1 t1, T2 t2, int lengthT1)
            where T1 : struct, Enum
            where T2 : struct, Enum
            => new Index2(t1.ToIndex(), t2.ToIndex()).ToIndex1(lengthT1);

        public static int ToIndex1<T1, T2>(T1 t1, T2 t2, in Length2 length)
            where T1 : struct, Enum
            where T2 : struct, Enum
            => new Index2(t1.ToIndex(), t2.ToIndex()).ToIndex1(length);

        public static int ToIndex1<T1, T2, T3>(T1 t1, T2 t2, T3 t3, in Length2 length)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            => new Index3(t1.ToIndex(), t2.ToIndex(), t3.ToIndex()).ToIndex1(length);

        public static int ToIndex1<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4, in Length3 length)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            => new Index4(t1.ToIndex(), t2.ToIndex(), t3.ToIndex(), t4.ToIndex()).ToIndex1(length);

        public static int ToIndex1<T1, T2, T3, T4, T5>(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, in Length4 length)
            where T1 : struct, Enum
            where T2 : struct, Enum
            where T3 : struct, Enum
            where T4 : struct, Enum
            where T5 : struct, Enum
            => new Index5(t1.ToIndex(), t2.ToIndex(), t3.ToIndex(), t4.ToIndex(), t5.ToIndex()).ToIndex1(length);
    }
}