﻿namespace System.Delegates
{
    public interface IPredicateIn<TClosure>
    {
        bool Invoke(in TClosure closure);
    }

    public interface IPredicateIn<TClosure, in T> : IPredicateIn<TClosure>
    {
        void SetArguments(T arg);
    }

    public interface IPredicateIn<TClosure, in T1, in T2> : IPredicateIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2);
    }

    public interface IPredicateIn<TClosure, in T1, in T2, in T3> : IPredicateIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3);
    }

    public interface IPredicateIn<TClosure, in T1, in T2, in T3, in T4> : IPredicateIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    }

    public interface IPredicateIn<TClosure, in T1, in T2, in T3, in T4, in T5> : IPredicateIn<TClosure>
    {
        void SetArguments(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    }

    public interface IPredicateInArgIn<TClosure, T> : IPredicateIn<TClosure>
    {
        void SetArguments(in T arg);
    }

    public interface IPredicateInArgIn<TClosure, T1, T2> : IPredicateIn<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2);
    }

    public interface IPredicateInArgIn<TClosure, T1, T2, T3> : IPredicateIn<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3);
    }

    public interface IPredicateInArgIn<TClosure, T1, T2, T3, T4> : IPredicateIn<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4);
    }

    public interface IPredicateInArgIn<TClosure, T1, T2, T3, T4, T5> : IPredicateIn<TClosure>
    {
        void SetArguments(in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5);
    }
}