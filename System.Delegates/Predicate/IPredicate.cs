namespace System.Delegates
{
    public interface IPredicate
    {
        bool Invoke();
    }

    public interface IPredicate<in TClosure>
    {
        bool Invoke(TClosure closure);
    }

    public interface IPredicate<in TClosure, in T> : IPredicate<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IPredicate<in TClosure, in T1, in T2> : IPredicate<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IPredicate<in TClosure, in T1, in T2, in T3> : IPredicate<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IPredicate<in TClosure, in T1, in T2, in T3, in T4> : IPredicate<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IPredicate<in TClosure, in T1, in T2, in T3, in T4, in T5> : IPredicate<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IPredicateArgIn<in TClosure, T> : IPredicate<TClosure>
    {
        void SetArguments(in T arg);
    }

    public interface IPredicateArgIn<in TClosure, T1, T2> : IPredicate<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2);
    }

    public interface IPredicateArgIn<in TClosure, T1, T2, T3> : IPredicate<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3);
    }

    public interface IPredicateArgIn<in TClosure, T1, T2, T3, T4> : IPredicate<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4);
    }

    public interface IPredicateArgIn<in TClosure, T1, T2, T3, T4, T5> : IPredicate<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5);
    }
}