namespace UnityEngine
{
    public partial struct ScreenResolution
    {
        public static class Predefined
        {
            public static ScreenResolution[] ByAspectRatio4x3 { get; } = new[] {
                new ScreenResolution(640, 480),
                new ScreenResolution(800, 600),
                new ScreenResolution(1024, 768),
                new ScreenResolution(1152, 864),
                new ScreenResolution(1600, 1200)
            };

            public static ScreenResolution[] ByAspectRatio16x9 { get; } = new[] {
                new ScreenResolution(640, 360),
                new ScreenResolution(854, 450),
                new ScreenResolution(1280, 720),
                new ScreenResolution(1366, 768),
                new ScreenResolution(1600, 900),
                new ScreenResolution(1920, 1080),
                new ScreenResolution(2048, 1152),
                new ScreenResolution(2560, 1440),
                new ScreenResolution(3840, 2160),
                new ScreenResolution(7680, 4320),
            };

            public static ScreenResolution[] ByAspectRatio16x10 { get; } = new[] {
                new ScreenResolution(1280, 800),
                new ScreenResolution(1440, 900),
                new ScreenResolution(1680, 1050),
                new ScreenResolution(1920, 1200)
            };
        }
    }
}