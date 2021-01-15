using System.Delegates;
using System.Runtime.CompilerServices;

namespace System.ValueDelegates
{
    public static partial class ValuePredicate
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Invoke<TPredicate, TClosure>(in TClosure closure)
            where TPredicate : struct, IPredicateIn<TClosure>
            => new TPredicate().Invoke(in closure);

        public static bool Invoke<TPredicate, TClosure, T>(in TClosure closure, T arg)
            where TPredicate : struct, IPredicateIn<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2>(in TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4, T5>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T>(this TPredicate predicate, in TClosure closure, T arg)
            where TPredicate : struct, IPredicateIn<TClosure, T>
        {
            predicate.SetArguments(arg);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2>(this TPredicate predicate, in TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2>
        {
            predicate.SetArguments(arg1, arg2);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3>(this TPredicate predicate, in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3>
        {
            predicate.SetArguments(arg1, arg2, arg3);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4>(this TPredicate predicate, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4>
        {
            predicate.SetArguments(arg1, arg2, arg3, arg4);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TPredicate predicate, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4, T5>
        {
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T>(in TPredicate predicate, in TClosure closure, T arg)
            where TPredicate : struct, IPredicateIn<TClosure, T>
        {
            predicate.SetArguments(arg);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2>(in TPredicate predicate, in TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2>
        {
            predicate.SetArguments(arg1, arg2);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3>(in TPredicate predicate, in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3>
        {
            predicate.SetArguments(arg1, arg2, arg3);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4>(in TPredicate predicate, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4>
        {
            predicate.SetArguments(arg1, arg2, arg3, arg4);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4, T5>(in TPredicate predicate, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4, T5>
        {
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return predicate.Invoke(in closure);
        }
    }
}