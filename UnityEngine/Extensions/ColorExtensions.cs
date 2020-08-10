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
    }
}