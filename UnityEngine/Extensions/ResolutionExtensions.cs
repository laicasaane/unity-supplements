namespace UnityEngine
{
    public static class ResolutionExtensions
    {
        public static void Deconstruct(this Resolution self, out int width, out int height)
        {
            width = self.width;
            height = self.height;
        }

        public static void Deconstruct(this Resolution self, out int width, out int height, out int refreshRate)
        {
            width = self.width;
            height = self.height;
            refreshRate = self.refreshRate;
        }

        public static Resolution With(this Resolution self, int? width = null, int? height = null, int? refreshRate = null)
            => new Resolution {
                width = width ?? self.width,
                height = height ?? self.height,
                refreshRate = refreshRate ?? self.refreshRate
            };
    }
}