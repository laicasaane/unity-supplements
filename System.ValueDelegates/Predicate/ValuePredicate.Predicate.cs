using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValuePredicate
    {
        public static bool Invoke<TPredicate>()
            where TPredicate : struct, IPredicate
            => new TPredicate().Invoke();

        public static bool Invoke<TPredicate, TClosure, T>(this TPredicate predicate, TClosure closure, T arg)
            where TPredicate : struct, IPredicate<TClosure, T>
        {
            predicate.SetArguments(arg);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2>(this TPredicate predicate, TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicate<TClosure, T1, T2>
        {
            predicate.SetArguments(arg1, arg2);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3>(this TPredicate predicate, TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3>
        {
            predicate.SetArguments(arg1, arg2, arg3);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4>(this TPredicate predicate, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3, T4>
        {
            predicate.SetArguments(arg1, arg2, arg3, arg4);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TPredicate predicate, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3, T4, T5>
        {
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T>(in TPredicate predicate, TClosure closure, T arg)
            where TPredicate : struct, IPredicate<TClosure, T>
        {
            predicate.SetArguments(arg);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2>(in TPredicate predicate, TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicate<TClosure, T1, T2>
        {
            predicate.SetArguments(arg1, arg2);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3>(in TPredicate predicate, TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3>
        {
            predicate.SetArguments(arg1, arg2, arg3);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4>(in TPredicate predicate, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3, T4>
        {
            predicate.SetArguments(arg1, arg2, arg3, arg4);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4, T5>(in TPredicate predicate, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3, T4, T5>
        {
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure>(this TClosure closure)
            where TPredicate : struct, IPredicate<TClosure>
            => new TPredicate().Invoke(closure);

        public static bool Invoke<TPredicate, TClosure, T>(this TClosure closure, T arg)
            where TPredicate : struct, IPredicate<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicate<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);
            return predicate.Invoke(closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return predicate.Invoke(closure);
        }
    }
}