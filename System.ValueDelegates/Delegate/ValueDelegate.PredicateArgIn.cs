using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueDelegate
    {
        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T>(this object closure, in T arg)
            where TPredicate : struct, IPredicateArgIn<object, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T1, T2>(this object closure, in T1 arg1, in T2 arg2)
            where TPredicate : struct, IPredicateArgIn<object, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T1, T2, T3>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TPredicate : struct, IPredicateArgIn<object, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T1, T2, T3, T4>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TPredicate : struct, IPredicateArgIn<object, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T1, T2, T3, T4, T5>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TPredicate : struct, IPredicateArgIn<object, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T>(this object closure, in T arg)
            where TPredicate : struct, IPredicateInArgIn<object, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T1, T2>(this object closure, in T1 arg1, in T2 arg2)
            where TPredicate : struct, IPredicateInArgIn<object, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T1, T2, T3>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TPredicate : struct, IPredicateInArgIn<object, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T1, T2, T3, T4>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TPredicate : struct, IPredicateInArgIn<object, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T1, T2, T3, T4, T5>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TPredicate : struct, IPredicateInArgIn<object, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T>(this object closure, in T arg)
            where TPredicate : struct, IPredicateRefArgIn<object, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T1, T2>(this object closure, in T1 arg1, in T2 arg2)
            where TPredicate : struct, IPredicateRefArgIn<object, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T1, T2, T3>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TPredicate : struct, IPredicateRefArgIn<object, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T1, T2, T3, T4>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TPredicate : struct, IPredicateRefArgIn<object, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T1, T2, T3, T4, T5>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TPredicate : struct, IPredicateRefArgIn<object, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T>(this TClosure closure, in T arg)
            where TPredicate : struct, IPredicateArgIn<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T1, T2>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TPredicate : struct, IPredicateArgIn<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TPredicate : struct, IPredicateArgIn<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TPredicate : struct, IPredicateArgIn<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TPredicate : struct, IPredicateArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T>(this TClosure closure, in T arg)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T1, T2>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TPredicate : struct, IPredicateInArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T>(this TClosure closure, in T arg)
            where TPredicate : struct, IPredicateRefArgIn<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T1, T2>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TPredicate : struct, IPredicateRefArgIn<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TPredicate : struct, IPredicateRefArgIn<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TPredicate : struct, IPredicateRefArgIn<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TPredicate : struct, IPredicateRefArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }
    }
}