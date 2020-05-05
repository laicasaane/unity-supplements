namespace System
{
    public delegate TResult ReadOnlyStructFunc<T, TResult>(in T value)
        where T : struct;

    public delegate TResult ReadOnlyStructFunc<T1, T2, TResult>(in T1 value1, in T2 value2)
        where T1 : struct
        where T2 : struct;

    public delegate TResult ReadOnlyStructFunc<T1, T2, T3, TResult>(in T1 value1, in T2 value2, in T3 value3)
        where T1 : struct
        where T2 : struct
        where T3 : struct;

    public delegate TResult ReadOnlyStructFunc<T1, T2, T3, T4, TResult>(in T1 value1, in T2 value2, in T3 value3, in T4 value4)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct;
}