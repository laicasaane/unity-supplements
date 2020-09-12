namespace UnityEngine
{
    public static class ColorExtensions
    {
        public static void Deconstruct(in this Color self, out float r, out float g, out float b)
        {
            r = self.r;
            g = self.g;
            b = self.b;
        }

        public static void Deconstruct(in this Color self, out float r, out float g, out float b, out float a)
        {
            r = self.r;
            g = self.g;
            b = self.b;
            a = self.a;
        }

        public static void Deconstruct(in this Color32 self, out byte r, out byte g, out byte b)
        {
            r = self.r;
            g = self.g;
            b = self.b;
        }

        public static void Deconstruct(in this Color32 self, out byte r, out byte g, out byte b, out byte a)
        {
            r = self.r;
            g = self.g;
            b = self.b;
            a = self.a;
        }

        public static Color With(in this Color self, float? r = null, float? g = null, float? b = null, float? a = null)
            => new Color(
                r ?? self.r,
                g ?? self.g,
                b ?? self.b,
                a ?? self.a
            );

        public static Color32 With(in this Color32 self, byte? r = null, byte? g = null, byte? b = null, byte? a = null)
            => new Color32(
                r ?? self.r,
                g ?? self.g,
                b ?? self.b,
                a ?? self.a
            );
    }
}