﻿using System.Delegates;
using System.Runtime.CompilerServices;

namespace System.ValueDelegates
{
    public static partial class ValueAction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeRef<TAction, TClosure>(ref TClosure closure)
            where TAction : struct, IActionRef<TClosure>
            => new TAction().Invoke(ref closure);

        public static void InvokeRef<TAction, TClosure, T>(ref TClosure closure, T arg)
            where TAction : struct, IActionRef<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(arg);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2>(ref TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionRef<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3>(ref TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3, T4>(ref TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3, T4, T5>(ref TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T>(this TAction action, ref TClosure closure, T arg)
            where TAction : struct, IActionRef<TClosure, T>
        {
            action.SetArguments(arg);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2>(this TAction action, ref TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionRef<TClosure, T1, T2>
        {
            action.SetArguments(arg1, arg2);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3>(this TAction action, ref TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3>
        {
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3, T4>(this TAction action, ref TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3, T4>
        {
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3, T4, T5>(this TAction action, ref TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3, T4, T5>
        {
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T>(in TAction action, ref TClosure closure, T arg)
            where TAction : struct, IActionRef<TClosure, T>
        {
            action.SetArguments(arg);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2>(in TAction action, ref TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionRef<TClosure, T1, T2>
        {
            action.SetArguments(arg1, arg2);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3>(in TAction action, ref TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3>
        {
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3, T4>(in TAction action, ref TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3, T4>
        {
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(ref closure);
        }

        public static void InvokeRef<TAction, TClosure, T1, T2, T3, T4, T5>(in TAction action, ref TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3, T4, T5>
        {
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(ref closure);
        }
    }
}