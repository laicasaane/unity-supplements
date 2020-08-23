using System;

public static class Array1Extensions
{
    public static bool ValidateIndex<T>(this T[] self, int index)
        => self != null && index >= 0 && index < self.Length;

    public static ReadArray1<T> AsReadArray<T>(this T[] self)
        => self;

    public static T Get<T>(this object[] self, int index)
    {
        if (self == null)
            return default;

        return self.Length > index && self[index] is T val ? val : default;
    }

    public static object[] Get<T>(this object[] self, int index, out T value)
    {
        value = default;

        if (self != null && self.Length > index && self[index] is T val)
            value = val;

        return self;
    }

    public static object[] GetThenMoveNext<T>(this object[] self, ref int index, out T value)
    {
        value = default;

        if (self != null)
        {
            if (self.Length > index && self[index] is T val)
                value = val;

            index += 1;
        }

        return self;
    }

    public static T[] Get<T, TResult>(this T[] self, int index, out TResult value)
    {
        value = default;

        if (self != null && self.Length > index && self[index] is TResult val)
            value = val;

        return self;
    }

    public static T[] GetThenMoveNext<T, TResult>(this T[] self, ref int index, out TResult value)
    {
        value = default;

        if (self != null)
        {
            if (self.Length > index && self[index] is TResult val)
                value = val;

            index += 1;
        }

        return self;
    }
}