using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueAction
    {
        public static void Invoke<TAction>()
            where TAction : struct, IAction
            => new TAction().Invoke();

        public static void Invoke<TAction, TClosure, T>(this TAction action, TClosure closure, T arg)
            where TAction : struct, IAction<TClosure, T>
        {
            action.SetArguments(arg);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2>(this TAction action, TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IAction<TClosure, T1, T2>
        {
            action.SetArguments(arg1, arg2);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3>(this TAction action, TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IAction<TClosure, T1, T2, T3>
        {
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4>(this TAction action, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IAction<TClosure, T1, T2, T3, T4>
        {
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4, T5>(this TAction action, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IAction<TClosure, T1, T2, T3, T4, T5>
        {
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T>(in TAction action, TClosure closure, T arg)
            where TAction : struct, IAction<TClosure, T>
        {
            action.SetArguments(arg);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2>(in TAction action, TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IAction<TClosure, T1, T2>
        {
            action.SetArguments(arg1, arg2);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3>(in TAction action, TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IAction<TClosure, T1, T2, T3>
        {
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4>(in TAction action, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IAction<TClosure, T1, T2, T3, T4>
        {
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4, T5>(in TAction action, TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IAction<TClosure, T1, T2, T3, T4, T5>
        {
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure>(this TClosure closure)
               where TAction : struct, IAction<TClosure>
               => new TAction().Invoke(closure);

        public static void Invoke<TAction, TClosure, T>(this TClosure closure, T arg)
            where TAction : struct, IAction<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(arg);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IAction<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IAction<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IAction<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);
            action.Invoke(closure);
        }

        public static void Invoke<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IAction<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);
            action.Invoke(closure);
        }
    }
}