namespace System.Delegates
{
    public interface IPredicateRef<TClosure>
    {
        bool Invoke(ref TClosure closure);
    }

    public interface IPredicateRef<TClosure, T> : IPredicateRef<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IPredicateRef<TClosure, T1, T2> : IPredicateRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IPredicateRef<TClosure, T1, T2, T3> : IPredicateRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IPredicateRef<TClosure, T1, T2, T3, T4> : IPredicateRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IPredicateRef<TClosure, T1, T2, T3, T4, T5> : IPredicateRef<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }
}