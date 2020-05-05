public static class StringExtensions
{
    /// <summary>
    /// Return other string if this string is null or empty.
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static string Or(this string self, string other)
        => string.IsNullOrEmpty(self) ? other : self;

    public static bool ValidateIndex(this string self, int index)
        => self != null && index >= 0 && index < self.Length;
}