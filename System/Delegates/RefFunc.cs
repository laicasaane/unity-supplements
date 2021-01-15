namespace System
{
    public delegate TResult RefFunc<T, TResult>(ref T value);

    public delegate TResult RefFunc<T1, T2, TResult>(ref T1 value1, ref T2 value2);

    public delegate TResult RefFunc<T1, T2, T3, TResult>(ref T1 value1, ref T2 value2, ref T3 value3);

    public delegate TResult RefFunc<T1, T2, T3, T4, TResult>(ref T1 value1, ref T2 value2, ref T3 value3, ref T4 value4);
}