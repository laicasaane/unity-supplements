namespace System.Delegates
{
    public interface IAction
    {
        void Invoke();
    }

    public interface IAction<in TClosure>
    {
        void Invoke(TClosure closure);
    }

    public interface IAction<in TClosure, in T> : IAction<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IAction<in TClosure, in T1, in T2> : IAction<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IAction<in TClosure, in T1, in T2, in T3> : IAction<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IAction<in TClosure, in T1, in T2, in T3, in T4> : IAction<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IAction<in TClosure, in T1, in T2, in T3, in T4, in T5> : IAction<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IActionArgIn<in TClosure, T> : IAction<TClosure>
    {
        void SetArguments(in T arg);
    }

    public interface IActionArgIn<in TClosure, T1, T2> : IAction<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2);
    }

    public interface IActionArgIn<in TClosure, T1, T2, T3> : IAction<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3);
    }

    public interface IActionArgIn<in TClosure, T1, T2, T3, T4> : IAction<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4);
    }

    public interface IActionArgIn<in TClosure, T1, T2, T3, T4, T5> : IAction<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5);
    }
}