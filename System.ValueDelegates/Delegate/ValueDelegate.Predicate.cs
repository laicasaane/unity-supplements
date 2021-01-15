using System.Delegates;
using System.Runtime.CompilerServices;

namespace System.ValueDelegates
{
    public static partial class ValueDelegate
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate>(this object closure)
            where TPredicate : struct, IPredicate<object>
            => new ValuePredicate<TPredicate, object>(new TPredicate(), closure);

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T>(this object closure, T arg)
            where TPredicate : struct, IPredicate<object, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T1, T2>(this object closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicate<object, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T1, T2, T3>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicate<object, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T1, T2, T3, T4>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicate<object, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, object> ValuePredicate<TPredicate, T1, T2, T3, T4, T5>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicate<object, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValuePredicate<TPredicate, object>(in predicate, closure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate>(this object closure)
            where TPredicate : struct, IPredicateIn<object>
            => new ValuePredicateIn<TPredicate, object>(new TPredicate(), in closure);

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T>(this object closure, T arg)
            where TPredicate : struct, IPredicateIn<object, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T1, T2>(this object closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicateIn<object, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T1, T2, T3>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicateIn<object, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T1, T2, T3, T4>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicateIn<object, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, object> ValuePredicateIn<TPredicate, T1, T2, T3, T4, T5>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicateIn<object, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValuePredicateIn<TPredicate, object>(in predicate, in closure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate>(this object closure)
            where TPredicate : struct, IPredicateRef<object>
            => new ValuePredicateRef<TPredicate, object>(new TPredicate(), ref closure);

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T>(this object closure, T arg)
            where TPredicate : struct, IPredicateRef<object, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T1, T2>(this object closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicateRef<object, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T1, T2, T3>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicateRef<object, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T1, T2, T3, T4>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicateRef<object, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, object> ValuePredicateRef<TPredicate, T1, T2, T3, T4, T5>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicateRef<object, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValuePredicateRef<TPredicate, object>(in predicate, ref closure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure>(this TClosure closure)
            where TPredicate : struct, IPredicate<TClosure>
            => new ValuePredicate<TPredicate, TClosure>(new TPredicate(), closure);

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T>(this TClosure closure, T arg)
            where TPredicate : struct, IPredicate<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicate<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        public static ValuePredicate<TPredicate, TClosure> ValuePredicate<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicate<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValuePredicate<TPredicate, TClosure>(in predicate, closure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure>(this TClosure closure)
            where TPredicate : struct, IPredicateIn<TClosure>
            => new ValuePredicateIn<TPredicate, TClosure>(new TPredicate(), in closure);

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T>(this TClosure closure, T arg)
            where TPredicate : struct, IPredicateIn<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        public static ValuePredicateIn<TPredicate, TClosure> ValuePredicateIn<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicateIn<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValuePredicateIn<TPredicate, TClosure>(in predicate, in closure);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure>(this TClosure closure)
            where TPredicate : struct, IPredicateRef<TClosure>
            => new ValuePredicateRef<TPredicate, TClosure>(new TPredicate(), ref closure);

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T>(this TClosure closure, T arg)
            where TPredicate : struct, IPredicateRef<TClosure, T>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TPredicate : struct, IPredicateRef<TClosure, T1, T2>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TPredicate : struct, IPredicateRef<TClosure, T1, T2, T3>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TPredicate : struct, IPredicateRef<TClosure, T1, T2, T3, T4>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }

        public static ValuePredicateRef<TPredicate, TClosure> ValuePredicateRef<TPredicate, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TPredicate : struct, IPredicateRef<TClosure, T1, T2, T3, T4, T5>
        {
            var predicate = new TPredicate();
            predicate.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValuePredicateRef<TPredicate, TClosure>(in predicate, ref closure);
        }
    }
}