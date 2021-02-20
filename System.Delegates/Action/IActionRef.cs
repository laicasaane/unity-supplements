namespace System.Delegates
{
    public interface IActionRef<TClosure>
    {
        void Invoke(ref TClosure closure);
    }

    public interface IActionRef<TClosure, in T> : IActionRef<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IActionRef<TClosure, in T1, in T2> : IActionRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IActionRef<TClosure, in T1, in T2, in T3> : IActionRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IActionRef<TClosure, in T1, in T2, in T3, in T4> : IActionRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IActionRef<TClosure, in T1, in T2, in T3, in T4, in T5> : IActionRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IActionRefArgIn<TClosure, T> : IActionRef<TClosure>
    {
        void SetArguments(in T arg);
    }

    public interface IActionRefArgIn<TClosure, T1, T2> : IActionRef<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2);
    }

    public interface IActionRefArgIn<TClosure, T1, T2, T3> : IActionRef<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3);
    }

    public interface IActionRefArgIn<TClosure, T1, T2, T3, T4> : IActionRef<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4);
    }

    public interface IActionRefArgIn<TClosure, T1, T2, T3, T4, T5> : IActionRef<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5);
    }
}