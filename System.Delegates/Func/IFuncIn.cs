namespace System.Delegates
{
    public interface IFuncIn<TClosure, TResult>
    {
        TResult Invoke(in TClosure closure);
    }

    public interface IFuncIn<TClosure, T, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(T arg);
    }

    public interface IFuncIn<TClosure, T1, T2, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IFuncIn<TClosure, T1, T2, T3, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IFuncIn<TClosure, T1, T2, T3, T4, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IFuncIn<TClosure, T1, T2, T3, T4, T5, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IFuncInArgIn<TClosure, T, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(in T arg);
    }

    public interface IFuncInArgIn<TClosure, T1, T2, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2);
    }

    public interface IFuncInArgIn<TClosure, T1, T2, T3, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3);
    }

    public interface IFuncInArgIn<TClosure, T1, T2, T3, T4, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4);
    }

    public interface IFuncInArgIn<TClosure, T1, T2, T3, T4, T5, TResult> : IFuncIn<TClosure, TResult>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5);
    }
}