namespace System.Delegates
{
    public interface IFunc<out TResult>
    {
        TResult Invoke();
    }

    public interface IFunc<in TClosure, out TResult>
    {
        TResult Invoke(TClosure closure);
    }

    public interface IFunc<in TClosure, in T, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T arg);
    }

    public interface IFunc<in TClosure, in T1, in T2, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IFunc<in TClosure, in T1, in T2, in T3, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IFunc<in TClosure, in T1, in T2, in T3, in T4, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IFunc<in TClosure, in T1, in T2, in T3, in T4, in T5, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IFuncArgIn<in TClosure, T, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(in T arg);
    }

    public interface IFuncArgIn<in TClosure, T1, T2, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2);
    }

    public interface IFuncArgIn<in TClosure, T1, T2, T3, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3);
    }

    public interface IFuncArgIn<in TClosure, T1, T2, T3, T4, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4);
    }

    public interface IFuncArgIn<in TClosure, T1, T2, T3, T4, T5, out TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5);
    }
}