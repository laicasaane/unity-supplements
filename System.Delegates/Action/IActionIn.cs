namespace System.Delegates
{
    public interface IActionIn<TClosure>
    {
        void Invoke(in TClosure closure);
    }

    public interface IActionIn<TClosure, in T> : IActionIn<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IActionIn<TClosure, in T1, in T2> : IActionIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IActionIn<TClosure, in T1, in T2, in T3> : IActionIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IActionIn<TClosure, in T1, in T2, in T3, in T4> : IActionIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IActionIn<TClosure, in T1, in T2, in T3, in T4, in T5> : IActionIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IActionInArgIn<TClosure, T> : IActionIn<TClosure>
    {
        void SetArguments(in T arg);
    }

    public interface IActionInArgIn<TClosure, T1, T2> : IActionIn<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2);
    }

    public interface IActionInArgIn<TClosure, T1, T2, T3> : IActionIn<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3);
    }

    public interface IActionInArgIn<TClosure, T1, T2, T3, T4> : IActionIn<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4);
    }

    public interface IActionInArgIn<TClosure, T1, T2, T3, T4, T5> : IActionIn<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5);
    }
}