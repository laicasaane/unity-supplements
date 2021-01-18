namespace System.Delegates
{
    public interface IFuncRef<TClosure, TResult>
    {
        TResult Invoke(ref TClosure closure);
    }

    public interface IFuncRef<TClosure, T, TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T arg);
    }

    public interface IFuncRef<TClosure, T1, T2, TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IFuncRef<TClosure, T1, T2, T3, TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IFuncRef<TClosure, T1, T2, T3, T4, TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IFuncRef<TClosure, T1, T2, T3, T4, T5, TResult> : IFuncRef<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}