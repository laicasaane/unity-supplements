namespace System
{
    public delegate TResult ReadStructFunc<T, out TResult>(in T value)
        where T : struct;

    public delegate TResult ReadStructFunc<T1, T2, out TResult>(in T1 value1, in T2 value2)
        where T1 : struct
        where T2 : struct;

    public delegate TResult ReadStructFunc<T1, T2, T3, out TResult>(in T1 value1, in T2 value2, in T3 value3)
        where T1 : struct
        where T2 : struct
        where T3 : struct;

    public delegate TResult ReadStructFunc<T1, T2, T3, T4, out TResult>(in T1 value1, in T2 value2, in T3 value3, in T4 value4)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct;
}