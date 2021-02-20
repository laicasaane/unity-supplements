using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueFunc
    {
        public static TResult Invoke<TFunc, TClosure, T, TResult>(this TFunc func, TClosure closure, T arg)
            where TFunc : struct, IFunc<TClosure, T, TResult>
        {
            func.SetArguments(arg);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(this TFunc func, TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFunc<TClosure, T1, T2, TResult>
        {
            func.SetArguments(arg1, arg2);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(this TFunc func, TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, TResult>
        {
            func.SetArguments(arg1, arg2, arg3);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TFunc func, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, T4, TResult>
        {
            func.SetArguments(arg1, arg2, arg3, arg4);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TFunc func, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T, TResult>(in TFunc func, TClosure closure, T arg)
            where TFunc : struct, IFunc<TClosure, T, TResult>
        {
            func.SetArguments(arg);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(in TFunc func, TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFunc<TClosure, T1, T2, TResult>
        {
            func.SetArguments(arg1, arg2);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(in TFunc func, TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, TResult>
        {
            func.SetArguments(arg1, arg2, arg3);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(in TFunc func, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, T4, TResult>
        {
            func.SetArguments(arg1, arg2, arg3, arg4);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(in TFunc func, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, TResult>(this TClosure closure)
            where TFunc : struct, IFunc<TClosure, TResult>
            => new TFunc().Invoke(closure);

        public static TResult Invoke<TFunc, TClosure, T, TResult>(this TClosure closure, T arg)
            where TFunc : struct, IFunc<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(this TClosure closure, T1 arg1, T2 arg2)
            where TFunc : struct, IFunc<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TFunc : struct, IFunc<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(arg1, arg2, arg3, arg4, arg5);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T, TResult>(this TFunc func, TClosure closure, in T arg)
            where TFunc : struct, IFuncArgIn<TClosure, T, TResult>
        {
            func.SetArguments(in arg);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(this TFunc func, TClosure closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, TResult>
        {
            func.SetArguments(in arg1, in arg2);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(this TFunc func, TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, TResult>
        {
            func.SetArguments(in arg1, in arg2, in arg3);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TFunc func, TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, T4, TResult>
        {
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TFunc func, TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T, TResult>(in TFunc func, TClosure closure, in T arg)
            where TFunc : struct, IFuncArgIn<TClosure, T, TResult>
        {
            func.SetArguments(in arg);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(in TFunc func, TClosure closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, TResult>
        {
            func.SetArguments(in arg1, in arg2);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(in TFunc func, TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, TResult>
        {
            func.SetArguments(in arg1, in arg2, in arg3);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(in TFunc func, TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, T4, TResult>
        {
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(in TFunc func, TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T, TResult>(this TClosure closure, in T arg)
            where TFunc : struct, IFuncArgIn<TClosure, T, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, TResult>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, T4, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4);
            return func.Invoke(closure);
        }

        public static TResult Invoke<TFunc, TClosure, T1, T2, T3, T4, T5, TResult>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TFunc : struct, IFuncArgIn<TClosure, T1, T2, T3, T4, T5, TResult>
        {
            var func = new TFunc();
            func.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);
            return func.Invoke(closure);
        }
    }
}