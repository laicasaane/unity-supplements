using System.Collections.Generic;

namespace System
{
    public static class EnumLength
    {
        public static int Get<T>() where T : unmanaged, Enum
            => EnumValues<T>.UnderlyingValueCount;

        public static Length2 Get<TA, TB>()
            where TA : unmanaged, Enum
            where TB : unmanaged, Enum
            => new Length2(EnumValues<TA>.UnderlyingValueCount, EnumValues<TB>.UnderlyingValueCount);

        public static Length3 Get<TA, TB, TC>()
            where TA : unmanaged, Enum
            where TB : unmanaged, Enum
            where TC : unmanaged, Enum
            => new Length3(EnumValues<TA>.UnderlyingValueCount, EnumValues<TB>.UnderlyingValueCount,
                           EnumValues<TC>.UnderlyingValueCount);

        public static Length4 Get<TA, TB, TC, TD>()
            where TA : unmanaged, Enum
            where TB : unmanaged, Enum
            where TC : unmanaged, Enum
            where TD : unmanaged, Enum
            => new Length4(EnumValues<TA>.UnderlyingValueCount, EnumValues<TB>.UnderlyingValueCount,
                           EnumValues<TC>.UnderlyingValueCount, EnumValues<TD>.UnderlyingValueCount);

        public static Length5 Get<TA, TB, TC, TD, TE>()
            where TA : unmanaged, Enum
            where TB : unmanaged, Enum
            where TC : unmanaged, Enum
            where TD : unmanaged, Enum
            where TE : unmanaged, Enum
            => new Length5(EnumValues<TA>.UnderlyingValueCount, EnumValues<TB>.UnderlyingValueCount,
                           EnumValues<TC>.UnderlyingValueCount, EnumValues<TD>.UnderlyingValueCount,
                           EnumValues<TE>.UnderlyingValueCount);

        public static Length2 With<TA, TB>(in this Length2 self, bool A = false, bool B = false)
            where TA : unmanaged, Enum
            where TB : unmanaged, Enum
              => new Length2(
                  A ? Get<TA>() : self.A,
                  B ? Get<TB>() : self.B
              );

        public static Length3 With<TA, TB, TC>(in this Length3 self, bool A = false, bool B = false, bool C = false)
            where TA : unmanaged, Enum
            where TB : unmanaged, Enum
            where TC : unmanaged, Enum
            => new Length3(
                A ? Get<TA>() : self.A,
                B ? Get<TB>() : self.B,
                C ? Get<TC>() : self.C
            );

        public static Length4 With<TA, TB, TC, TD>(in this Length4 self, bool A = false, bool B = false, bool C = false,
                                                   bool D = false)
            where TA : unmanaged, Enum
            where TB : unmanaged, Enum
            where TC : unmanaged, Enum
            where TD : unmanaged, Enum
            => new Length4(
                A ? Get<TA>() : self.A,
                B ? Get<TB>() : self.B,
                C ? Get<TC>() : self.C,
                D ? Get<TD>() : self.D
            );

        public static Length5 With<TA, TB, TC, TD, TE>(in this Length5 self, bool A = false, bool B = false, bool C = false,
                                                       bool D = false, bool E = false)
            where TA : unmanaged, Enum
            where TB : unmanaged, Enum
            where TC : unmanaged, Enum
            where TD : unmanaged, Enum
            where TE : unmanaged, Enum
            => new Length5(
                A ? Get<TA>() : self.A,
                B ? Get<TB>() : self.B,
                C ? Get<TC>() : self.C,
                D ? Get<TD>() : self.D,
                E ? Get<TE>() : self.E
            );
    }
}