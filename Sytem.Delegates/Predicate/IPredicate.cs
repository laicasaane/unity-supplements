namespace System.Delegates
{
    public interface IPredicate
    {
        bool Invoke();
    }

    public interface IPredicate<TClosure>
    {
        bool Invoke(TClosure closure);
    }

    public interface IPredicate<TClosure, T> : IPredicate<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IPredicate<TClosure, T1, T2> : IPredicate<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IPredicate<TClosure, T1, T2, T3> : IPredicate<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IPredicate<TClosure, T1, T2, T3, T4> : IPredicate<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IPredicate<TClosure, T1, T2, T3, T4, T5> : IPredicate<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}