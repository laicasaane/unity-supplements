using System.Collections.Generic;

namespace System
{
    public static class EnumIndex
    {
        private static readonly int? _none = null;

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

        public static Index2 ToIndex<TA, TB>(in this (TA a, TB b) value)
            where TA : struct, Enum
            where TB : struct, Enum
            => new Index2(value.a.ToIndex(), value.b.ToIndex());

        public static Index3 ToIndex<TA, TB, TC>(in this (TA a, TB b, TC c) value)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            => new Index3(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex());

        public static Index4 ToIndex<TA, TB, TC, TD>(in this (TA a, TB b, TC c, TD d) value)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            => new Index4(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex(), value.d.ToIndex());

        public static Index5 ToIndex<TA, TB, TC, TD, TE>(in this (TA a, TB b, TC c, TD d, TE e) value)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            where TE : struct, Enum
            => new Index5(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex(), value.d.ToIndex(), value.e.ToIndex());

        public static int ToIndex1<TA, TB>(in this (TA a, TB b) value)
            where TA : struct, Enum
            where TB : struct, Enum
            => new Index2(value.a.ToIndex(), value.b.ToIndex())
               .ToIndex1(EnumLength.Get<TA>());

        public static int ToIndex1<TA, TB, TC>(in this (TA a, TB b, TC c) value)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            => new Index3(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex())
               .ToIndex1(EnumLength.Get<TA, TB>());

        public static int ToIndex1<TA, TB, TC, TD>(in this (TA a, TB b, TC c, TD d) value)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            => new Index4(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex(), value.d.ToIndex())
               .ToIndex1(EnumLength.Get<TA, TB, TC>());

        public static int ToIndex1<TA, TB, TC, TD, TE>(in this (TA a, TB b, TC c, TD d, TE e) value)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            where TE : struct, Enum
            => new Index5(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex(), value.d.ToIndex(), value.e.ToIndex())
               .ToIndex1(EnumLength.Get<TA, TB, TC, TD>());

        public static int ToIndex1<TA, TB>(in this (TA a, TB b) value, int lengthTA)
            where TA : struct, Enum
            where TB : struct, Enum
            => new Index2(value.a.ToIndex(), value.b.ToIndex())
               .ToIndex1(lengthTA);

        public static int ToIndex1<TA, TB>(in this (TA a, TB b) value, in Length2 length)
            where TA : struct, Enum
            where TB : struct, Enum
            => new Index2(value.a.ToIndex(), value.b.ToIndex())
               .ToIndex1(length);

        public static int ToIndex1<TA, TB, TC>(in this (TA a, TB b, TC c) value, in Length2 length)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            => new Index3(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex())
               .ToIndex1(length);

        public static int ToIndex1<TA, TB, TC, TD>(in this (TA a, TB b, TC c, TD d) value, in Length3 length)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            => new Index4(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex(), value.d.ToIndex()).ToIndex1(length);

        public static int ToIndex1<TA, TB, TC, TD, TE>(in this (TA a, TB b, TC c, TD d, TE e) value, in Length4 length)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            where TE : struct, Enum
            => new Index5(value.a.ToIndex(), value.b.ToIndex(), value.c.ToIndex(), value.d.ToIndex(), value.e.ToIndex())
               .ToIndex1(length);

        public static Index2 ToIndex<TA, TB>(TA a, TB b)
            where TA : struct, Enum
            where TB : struct, Enum
            => new Index2(a.ToIndex(), b.ToIndex());

        public static Index3 ToIndex<TA, TB, TC>(TA a, TB b, TC c)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            => new Index3(a.ToIndex(), b.ToIndex(), c.ToIndex());

        public static Index4 ToIndex<TA, TB, TC, TD>(TA a, TB b, TC c, TD d)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            => new Index4(a.ToIndex(), b.ToIndex(), c.ToIndex(), d.ToIndex());

        public static Index5 ToIndex<TA, TB, TC, TD, TE>(TA a, TB b, TC c, TD d, TE e)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            where TE : struct, Enum
            => new Index5(a.ToIndex(), b.ToIndex(), c.ToIndex(), d.ToIndex(), e.ToIndex());

        public static int ToIndex1<TA, TB>(TA a, TB b)
            where TA : struct, Enum
            where TB : struct, Enum
            => new Index2(a.ToIndex(), b.ToIndex()).ToIndex1(EnumLength.Get<TA>());

        public static int ToIndex1<TA, TB, TC>(TA a, TB b, TC c)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            => new Index3(a.ToIndex(), b.ToIndex(), c.ToIndex())
               .ToIndex1(EnumLength.Get<TA, TB>());

        public static int ToIndex1<TA, TB, TC, TD>(TA a, TB b, TC c, TD d)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            => new Index4(a.ToIndex(), b.ToIndex(), c.ToIndex(), d.ToIndex())
               .ToIndex1(EnumLength.Get<TA, TB, TC>());

        public static int ToIndex1<TA, TB, TC, TD, TE>(TA a, TB b, TC c, TD d, TE e)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            where TE : struct, Enum
            => new Index5(a.ToIndex(), b.ToIndex(), c.ToIndex(), d.ToIndex(), e.ToIndex())
               .ToIndex1(EnumLength.Get<TA, TB, TC, TD>());

        public static int ToIndex1<TA, TB>(TA a, TB b, int lengthTA)
            where TA : struct, Enum
            where TB : struct, Enum
            => new Index2(a.ToIndex(), b.ToIndex())
               .ToIndex1(lengthTA);

        public static int ToIndex1<TA, TB>(TA a, TB b, in Length2 length)
            where TA : struct, Enum
            where TB : struct, Enum
            => new Index2(a.ToIndex(), b.ToIndex())
               .ToIndex1(length);

        public static int ToIndex1<TA, TB, TC>(TA a, TB b, TC c, in Length2 length)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            => new Index3(a.ToIndex(), b.ToIndex(), c.ToIndex())
               .ToIndex1(length);

        public static int ToIndex1<TA, TB, TC, TD>(TA a, TB b, TC c, TD d, in Length3 length)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            => new Index4(a.ToIndex(), b.ToIndex(), c.ToIndex(), d.ToIndex())
               .ToIndex1(length);

        public static int ToIndex1<TA, TB, TC, TD, TE>(TA a, TB b, TC c, TD d, TE e, in Length4 length)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            where TE : struct, Enum
            => new Index5(a.ToIndex(), b.ToIndex(), c.ToIndex(), d.ToIndex(), e.ToIndex())
               .ToIndex1(length);

        private static int? ToIndex<T>(in this T? self) where T : struct, Enum
            => self.HasValue ? self.Value.ToIndex() : _none;

        public static Index2 With<TA, TB>(in this Index2 self, TA? A = null, TB? B = null)
            where TA : struct, Enum
            where TB : struct, Enum
              => new Index2(
                  A.ToIndex() ?? self.A,
                  B.ToIndex() ?? self.B
              );

        public static Index3 With<TA, TB, TC>(in this Index3 self, TA? A = null, TB? B = null, TC? C = null)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            => new Index3(
                A.ToIndex() ?? self.A,
                B.ToIndex() ?? self.B,
                C.ToIndex() ?? self.C
            );

        public static Index4 With<TA, TB, TC, TD>(in this Index4 self, TA? A = null, TB? B = null, TC? C = null, TD? D = null)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            => new Index4(
                A.ToIndex() ?? self.A,
                B.ToIndex() ?? self.B,
                C.ToIndex() ?? self.C,
                D.ToIndex() ?? self.D
            );

        public static Index5 With<TA, TB, TC, TD, TE>(in this Index5 self, TA? A = null, TB? B = null, TC? C = null, TD? D = null,
                                                      TE? E = null)
            where TA : struct, Enum
            where TB : struct, Enum
            where TC : struct, Enum
            where TD : struct, Enum
            where TE : struct, Enum
            => new Index5(
                A.ToIndex() ?? self.A,
                B.ToIndex() ?? self.B,
                C.ToIndex() ?? self.C,
                D.ToIndex() ?? self.D,
                E.ToIndex() ?? self.E
            );
    }
}