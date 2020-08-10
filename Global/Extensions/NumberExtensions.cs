public static class NumberExtensions
{
    public static double Percent(this double value)
        => value * 0.01;

    public static double Percent(this long value)
        => value * 0.01;

    public static double Percent(this ulong value)
        => value * 0.01;

    public static float Percent(this float value)
        => value * 0.01f;

    public static float Percent(this int value)
        => value * 0.01f;

    public static float Percent(this uint value)
        => value * 0.01f;

    public static float Percent(this short value)
        => value * 0.01f;

    public static float Percent(this ushort value)
        => value * 0.01f;

    public static float Percent(this byte value)
        => value * 0.01f;

    public static float Percent(this sbyte value)
        => value * 0.01f;
}