namespace System.Delegates
{
    public interface IAction
    {
        void Invoke();
    }

    public interface IAction<TClosure>
    {
        void Invoke(TClosure closure);
    }

    public interface IAction<TClosure, T> : IAction<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IAction<TClosure, T1, T2> : IAction<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IAction<TClosure, T1, T2, T3> : IAction<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IAction<TClosure, T1, T2, T3, T4> : IAction<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IAction<TClosure, T1, T2, T3, T4, T5> : IAction<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}