namespace System.Delegates
{
    public interface IActionRef<TClosure>
    {
        void Invoke(ref TClosure closure);
    }

    public interface IActionRef<TClosure, T> : IActionRef<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IActionRef<TClosure, T1, T2> : IActionRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IActionRef<TClosure, T1, T2, T3> : IActionRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IActionRef<TClosure, T1, T2, T3, T4> : IActionRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IActionRef<TClosure, T1, T2, T3, T4, T5> : IActionRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}