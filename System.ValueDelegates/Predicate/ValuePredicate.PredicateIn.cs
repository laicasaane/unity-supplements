using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValuePredicate
    {
        public static bool InvokeIn<TPredicate, TClosure>(this TClosure closure)
            where TPredicate : struct, IPredicateIn<TClosure>
            => new TPredicate().Invoke(in closure);

        public static bool InvokeIn<TPredicate, TClosure, T>(this TClosure closure, T arg)
            where TPredicate : struct, IPredicateIn<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);
            return predicate.Invoke(in closure);
        }

        public static bool InvokeIn<TPredicate, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);
            return predicate.Invoke(in closure);
        }

        public static bool InvokeIn<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);
            return predicate.Invoke(in closure);
        }

        public static bool InvokeIn<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);
            return predicate.Invoke(in closure);
        }

        public static bool InvokeIn<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
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

        public static bool InvokeIn<TPredicate, TClosure, T>(this TClosure closure, in T arg)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg);
            return predicate.Invoke(in closure);
        }

        public static bool InvokeIn<TPredicate, TClosure, T1, T2>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2);
            return predicate.Invoke(in closure);
        }

        public static bool InvokeIn<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3);
            return predicate.Invoke(in closure);
        }

        public static bool InvokeIn<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4);
            return predicate.Invoke(in closure);
        }

        public static bool InvokeIn<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T>(this TPredicate predicate, in TClosure closure, in T arg)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T>
        {
            predicate.SetArguments(in arg);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2>(this TPredicate predicate, in TClosure closure, in T1 arg1, in T2 arg2)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2>
        {
            predicate.SetArguments(in arg1, in arg2);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3>(this TPredicate predicate, in TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3>
        {
            predicate.SetArguments(in arg1, in arg2, in arg3);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4>(this TPredicate predicate, in TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3, T4>
        {
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4);
            return predicate.Invoke(in closure);
        }

        public static bool Invoke<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TPredicate predicate, in TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);
            return predicate.Invoke(in closure);
        }
    }
}