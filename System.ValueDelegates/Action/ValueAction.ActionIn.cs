using System.Delegates;
using System.Runtime.CompilerServices;

namespace System.ValueDelegates
{
    public static partial class ValueAction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Invoke<TAction, TClosure>(in TClosure closure)
            where TAction : struct, IActionIn<TClosure>
            => new TAction().Invoke(in closure);

        public static void Invoke<TAction, TClosure, T>(in TClosure closure, T arg)
            where TAction : struct, IActionIn<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(arg);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2>(in TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionIn<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4, T5>(in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T>(this TAction action, in TClosure closure, T arg)
            where TAction : struct, IActionIn<TClosure, T>
        {
            action.SetArguments(arg);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2>(this TAction action, in TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionIn<TClosure, T1, T2>
        {
            action.SetArguments(arg1, arg2);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3>(this TAction action, in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3>
        {
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4>(this TAction action, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4>
        {
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4, T5>(this TAction action, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4, T5>
        {
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T>(in TAction action, in TClosure closure, T arg)
            where TAction : struct, IActionIn<TClosure, T>
        {
            action.SetArguments(arg);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2>(in TAction action, in TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionIn<TClosure, T1, T2>
        {
            action.SetArguments(arg1, arg2);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3>(in TAction action, in TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3>
        {
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4>(in TAction action, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4>
        {
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4, T5>(in TAction action, in TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4, T5>
        {
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(in closure);
        }
    }
}