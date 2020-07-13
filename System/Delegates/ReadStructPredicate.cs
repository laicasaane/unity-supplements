namespace System
{
    public delegate bool ReadStructPredicate<T>(in T value)
        where T : struct;

    public delegate bool ReadStructPredicate<T1, T2>(in T1 value1, in T2 value2)
        where T1 : struct
        where T2 : struct;

    public delegate bool ReadStructPredicate<T1, T2, T3>(in T1 value1, in T2 value2, in T3 value3)
        where T1 : struct
        where T2 : struct
        where T3 : struct;

    public delegate bool ReadStructPredicate<T1, T2, T3, T4>(in T1 value1, in T2 value2, in T3 value3, in T4 value4)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct;
}