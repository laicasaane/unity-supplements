using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueDelegate
    {
        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T, TResult>(this object closure, in T arg)
            where TFunc : struct, IFuncArgIn<object, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T1, T2, TResult>(this object closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncArgIn<object, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T1, T2, T3, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncArgIn<object, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T1, T2, T3, T4, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncArgIn<object, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T1, T2, T3, T4, T5, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncArgIn<object, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T, TResult>(this object closure, in T arg)
            where TFunc : struct, IFuncInArgIn<object, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T1, T2, TResult>(this object closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncInArgIn<object, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T1, T2, T3, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncInArgIn<object, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T1, T2, T3, T4, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncInArgIn<object, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T1, T2, T3, T4, T5, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncInArgIn<object, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T, TResult>(this object closure, in T arg)
            where TFunc : struct, IFuncRefArgIn<object, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T1, T2, TResult>(this object closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncRefArgIn<object, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T1, T2, T3, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncRefArgIn<object, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T1, T2, T3, T4, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncRefArgIn<object, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T1, T2, T3, T4, T5, TResult>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncRefArgIn<object, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T, TResult>(this TClosure closure, in T arg)
            where TFunc : struct, IFuncArgIn<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T1, T2, TResult>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T1, T2, T3, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T, TResult>(this TClosure closure, in T arg)
            where TFunc : struct, IFuncInArgIn<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T1, T2, TResult>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncInArgIn<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T1, T2, T3, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncInArgIn<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncInArgIn<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncInArgIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T, TResult>(this TClosure closure, in T arg)
            where TFunc : struct, IFuncRefArgIn<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T1, T2, TResult>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncRefArgIn<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T1, T2, T3, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncRefArgIn<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncRefArgIn<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncRefArgIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }
    }
}