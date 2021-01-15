namespace System.Delegates
{
    public interface IFunc<TResult>
    {
        TResult Invoke();
    }

    public interface IFunc<TClosure, TResult>
    {
        TResult Invoke(TClosure closure);
    }

    public interface IFunc<TClosure, T, TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T arg);
    }

    public interface IFunc<TClosure, T1, T2, TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IFunc<TClosure, T1, T2, T3, TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IFunc<TClosure, T1, T2, T3, T4, TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IFunc<TClosure, T1, T2, T3, T4, T5, TResult> : IFunc<TClosure, TResult>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}