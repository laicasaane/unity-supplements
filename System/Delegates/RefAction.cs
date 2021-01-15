namespace System
{
    public delegate void RefAction<T>(ref T value);

    public delegate void RefAction<T1, T2>(ref T1 value1, ref T2 value2);

    public delegate void RefAction<T1, T2, T3>(ref T1 value1, ref T2 value2, ref T3 value3);

    public delegate void RefAction<T1, T2, T3, T4>(ref T1 value1, ref T2 value2, ref T3 value3, ref T4 value4);
}