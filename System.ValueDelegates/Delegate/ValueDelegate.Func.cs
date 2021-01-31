using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueDelegate
    {
        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, TResult>(this object closure)
            where TFunc : struct, IFunc<object, TResult>
            => new ValueFunc<TFunc, object, TResult>(new TFunc(), closure);

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T, TResult>(this object closure, T arg)
            where TFunc : struct, IFunc<object, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T1, T2, TResult>(this object closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFunc<object, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T1, T2, T3, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFunc<object, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc,  T1, T2, T3, T4, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFunc<object, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, object, TResult> ValueFunc<TFunc, T1, T2, T3, T4, T5, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFunc<object, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueFunc<TFunc, object, TResult>(in func, closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, TResult>(this object closure)
            where TFunc : struct, IFuncIn<object, TResult>
            => new ValueFuncIn<TFunc, object, TResult>(new TFunc(), in closure);

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T, TResult>(this object closure, T arg)
            where TFunc : struct, IFuncIn<object, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T1, T2, TResult>(this object closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFuncIn<object, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T1, T2, T3, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFuncIn<object, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T1, T2, T3, T4, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFuncIn<object, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, object, TResult> ValueFuncIn<TFunc, T1, T2, T3, T4, T5, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFuncIn<object, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueFuncIn<TFunc, object, TResult>(in func, in closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, TResult>(this object closure)
            where TFunc : struct, IFuncRef<object, TResult>
            => new ValueFuncRef<TFunc, object, TResult>(new TFunc(), ref closure);

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T, TResult>(this object closure, T arg)
            where TFunc : struct, IFuncRef<object, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T1, T2, TResult>(this object closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFuncRef<object, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T1, T2, T3, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFuncRef<object, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T1, T2, T3, T4, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFuncRef<object, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, object, TResult> ValueFuncRef<TFunc, T1, T2, T3, T4, T5, TResult>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFuncRef<object, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueFuncRef<TFunc, object, TResult>(in func, ref closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, TResult>(this TClosure closure)
            where TFunc : struct, IFunc<TClosure, TResult>
            => new ValueFunc<TFunc, TClosure, TResult>(new TFunc(), closure);

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T, TResult>(this TClosure closure, T arg)
            where TFunc : struct, IFunc<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T1, T2, TResult>(this TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFunc<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T1, T2, T3, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFunc<TFunc, TClosure, TResult> ValueFunc<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueFunc<TFunc, TClosure, TResult>(in func, closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, TResult>(this TClosure closure)
            where TFunc : struct, IFuncIn<TClosure, TResult>
            => new ValueFuncIn<TFunc, TClosure, TResult>(new TFunc(), in closure);

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T, TResult>(this TClosure closure, T arg)
            where TFunc : struct, IFuncIn<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T1, T2, TResult>(this TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T1, T2, T3, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncIn<TFunc, TClosure, TResult> ValueFuncIn<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueFuncIn<TFunc, TClosure, TResult>(in func, in closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, TResult>(this TClosure closure)
            where TFunc : struct, IFuncRef<TClosure, TResult>
            => new ValueFuncRef<TFunc, TClosure, TResult>(new TFunc(), ref closure);

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T, TResult>(this TClosure closure, T arg)
            where TFunc : struct, IFuncRef<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T1, T2, TResult>(this TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFuncRef<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T1, T2, T3, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFuncRef<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFuncRef<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }

        public static ValueFuncRef<TFunc, TClosure, TResult> ValueFuncRef<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFuncRef<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueFuncRef<TFunc, TClosure, TResult>(in func, ref closure);
        }
    }
}