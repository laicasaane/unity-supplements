using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueFunc
    {
        public static TResult Invoke<TFunc, TClosure, TResult>(in TClosure closure)
            where TFunc : struct, IFuncIn<TClosure, TResult>
            => new TFunc().Invoke(in closure);

        public static TResult Invoke<TFunc, TClosure, T, TResult>(in TClosure closure, T arg)
            where TFunc : struct, IFuncIn<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(in TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T, TResult>(this TFunc func, in TClosure closure, T arg)
            where TFunc : struct, IFuncIn<TClosure, T, TResult>
        {
            func.SetArguments(arg);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(this TFunc func, in TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, TResult>
        {
            func.SetArguments(arg1, arg2);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(this TFunc func, in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, TResult>
        {
            func.SetArguments(arg1, arg2, arg3);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TFunc func, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, T4, TResult>
        {
            func.SetArguments(arg1, arg2, arg3, arg4);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TFunc func, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T, TResult>(in TFunc func, in TClosure closure, T arg)
            where TFunc : struct, IFuncIn<TClosure, T, TResult>
        {
            func.SetArguments(arg);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(in TFunc func, in TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, TResult>
        {
            func.SetArguments(arg1, arg2);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(in TFunc func, in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, TResult>
        {
            func.SetArguments(arg1, arg2, arg3);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(in TFunc func, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, T4, TResult>
        {
            func.SetArguments(arg1, arg2, arg3, arg4);
            return func.Invoke(in closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(in TFunc func, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFuncIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return func.Invoke(in closure);
        }
    }
}