namespace System.Fluent
{
    public static class ObjectExtensions
    {
        public static T Action<T>(this T self, Action action)
        {
            action();
            return self;
        }

        public static T Action<T>(this T self, Action<T> action)
        {
            action(self);
            return self;
        }

        public static T Action<T>(this T self, ReadOnlyStructAction<T> action)
            where T : struct
        {
            action(self);
            return self;
        }

        public static T Func<T, TResult>(this T self, Func<TResult> func)
        {
            func();
            return self;
        }

        public static T Func<T, TResult>(this T self, Func<T, TResult> func)
        {
            func(self);
            return self;
        }

        public static T Func<T, TResult>(this T self, ReadOnlyStructFunc<T, TResult> func)
            where T : struct
        {
            func(self);
            return self;
        }

        public static T Func<T, TResult>(this T self, out TResult result, Func<TResult> func)
        {
            result = func();
            return self;
        }

        public static T Func<T, TResult>(this T self, out TResult result, Func<T, TResult> func)
        {
            result = func(self);
            return self;
        }

        public static T Func<T, TResult>(this T self, out TResult result, ReadOnlyStructFunc<T, TResult> func)
            where T : struct
        {
            result = func(self);
            return self;
        }

        public static T Predicate<T>(this T self, Predicate<T> predicate)
        {
            predicate(self);
            return self;
        }

        public static T Predicate<T>(this T self, out bool result, Predicate<T> predicate)
        {
            result = predicate(self);
            return self;
        }
    }
}