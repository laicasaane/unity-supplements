namespace UnityEngine
{
    public static class ResolutionExtensions
    {
        public static void Deconstruct(in this Resolution self, out int width, out int height)
        {
            width = self.width;
            height = self.height;
        }

        public static void Deconstruct(in this Resolution self, out int width, out int height, out int refreshRate)
        {
            width = self.width;
            height = self.height;
            refreshRate = self.refreshRate;
        }
    }
}