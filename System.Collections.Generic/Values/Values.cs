namespace System.Collections.Generic
{
    public readonly struct Values
    {
        public static Values<T> Range<T>(params T[] values)
            => new Values<T>(values);
    }

    public static class ValuesExtensions
    {
        public static Values<T> Range<T>(this Values _, params T[] values)
            => new Values<T>(values);
    }
}