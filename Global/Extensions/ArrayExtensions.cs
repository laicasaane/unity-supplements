using System;

public static class ArrayExtensions
{
    public static bool ValidateIndex<T>(this T[] self, int index)
        => self != null && index >= 0 && index < self.Length;

    public static ReadArray<T> AsReadArray<T>(this T[] self)
        => self;
}