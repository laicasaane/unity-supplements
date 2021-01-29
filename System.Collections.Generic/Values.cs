namespace System.Collections.Generic
{
    public readonly struct Values
    {
        public static IEnumerable<T> Range<T>(T v)
        {
            yield return v;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2)
        {
            yield return v1;
            yield return v2;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3)
        {
            yield return v1;
            yield return v2;
            yield return v3;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3, T v4)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3, T v4, T v5)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3, T v4, T v5, T v6)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3, T v4, T v5, T v6, T v7)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
            yield return v8;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
            yield return v8;
            yield return v9;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, T v10)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
            yield return v8;
            yield return v9;
            yield return v10;
        }

        public static IEnumerable<T> Range<T>(T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, T v10, params T[] rest)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
            yield return v8;
            yield return v9;
            yield return v10;

            if (rest != null)
            {
                for (var i = 0u; i < rest.LongLength; i++)
                {
                    yield return rest[i];
                }
            }
        }
    }

    public static class ValuesExtensions
    {
        public static IEnumerable<T> Range<T>(this Values _, T v)
        {
            yield return v;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2)
        {
            yield return v1;
            yield return v2;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3)
        {
            yield return v1;
            yield return v2;
            yield return v3;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3, T v4)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3, T v4, T v5)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3, T v4, T v5, T v6)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3, T v4, T v5, T v6, T v7)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
            yield return v8;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
            yield return v8;
            yield return v9;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, T v10)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
            yield return v8;
            yield return v9;
            yield return v10;
        }

        public static IEnumerable<T> Range<T>(this Values _, T v1, T v2, T v3, T v4, T v5, T v6, T v7, T v8, T v9, T v10, params T[] rest)
        {
            yield return v1;
            yield return v2;
            yield return v3;
            yield return v4;
            yield return v5;
            yield return v6;
            yield return v7;
            yield return v8;
            yield return v9;
            yield return v10;

            if (rest != null)
            {
                for (var i = 0u; i < rest.LongLength; i++)
                {
                    yield return rest[i];
                }
            }
        }
    }
}