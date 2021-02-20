using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueAction
    {
        public static void InvokeIn<TAction, TClosure>(this TClosure closure)
            where TAction : struct, IActionIn<TClosure>
            => new TAction().Invoke(in closure);

        public static void InvokeIn<TAction, TClosure, T>(this TClosure closure, T arg)
            where TAction : struct, IActionIn<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(arg);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionIn<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
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

        public static void Invoke<TAction, TClosure, T>(this TAction action, in TClosure closure, in T arg)
            where TAction : struct, IActionInArgIn<TClosure, T>
        {
            action.SetArguments(in arg);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2>(this TAction action, in TClosure closure, in T1 arg1, in T2 arg2)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2>
        {
            action.SetArguments(in arg1, in arg2);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3>(this TAction action, in TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3>
        {
            action.SetArguments(in arg1, in arg2, in arg3);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4>(this TAction action, in TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3, T4>
        {
            action.SetArguments(in arg1, in arg2, in arg3, in arg4);
            action.Invoke(in closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4, T5>(this TAction action, in TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            action.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T>(this TClosure closure, in T arg)
            where TAction : struct, IActionInArgIn<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(in arg);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T1, T2>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T1, T2, T3>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4);
            action.Invoke(in closure);
        }

        public static void InvokeIn<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);
            action.Invoke(in closure);
        }
    }
}