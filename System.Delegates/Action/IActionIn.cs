namespace System.Delegates
{
    public interface IActionIn<TClosure>
    {
        void Invoke(in TClosure closure);
    }

    public interface IActionIn<TClosure, T> : IActionIn<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IActionIn<TClosure, T1, T2> : IActionIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IActionIn<TClosure, T1, T2, T3> : IActionIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IActionIn<TClosure, T1, T2, T3, T4> : IActionIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IActionIn<TClosure, T1, T2, T3, T4, T5> : IActionIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}