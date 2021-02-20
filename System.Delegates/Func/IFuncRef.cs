namespace System.Delegates
{
    public interface IFuncRef<TClosure, out TResult>
    {
        TResult Invoke(ref TClosure closure);
    }

    public interface IFuncRef<TClosure, in T, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T arg);
    }

    public interface IFuncRef<TClosure, in T1, in T2, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IFuncRef<TClosure, in T1, in T2, in T3, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IFuncRef<TClosure, in T1, in T2, in T3, in T4, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IFuncRef<TClosure, in T1, in T2, in T3, in T4, in T5, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IFuncRefArgIn<TClosure, T, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(in T arg);
    }

    public interface IFuncRefArgIn<TClosure, T1, T2, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2);
    }

    public interface IFuncRefArgIn<TClosure, T1, T2, T3, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3);
    }

    public interface IFuncRefArgIn<TClosure, T1, T2, T3, T4, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4);
    }

    public interface IFuncRefArgIn<TClosure, T1, T2, T3, T4, T5, out TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5);
    }
}