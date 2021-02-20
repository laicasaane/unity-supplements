using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueDelegate
    {
        public static ValueAction<TAction, object> ValueAction<TAction, T>(this object closure, in T arg)
            where TAction : struct, IActionArgIn<object, T>
        {
            var action = new TAction();
            action.SetArguments(in arg);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueAction<TAction, object> ValueAction<TAction, T1, T2>(this object closure, in T1 arg1, in T2 arg2)
            where TAction : struct, IActionArgIn<object, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueAction<TAction, object> ValueAction<TAction, T1, T2, T3>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TAction : struct, IActionArgIn<object, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueAction<TAction, object> ValueAction<TAction, T1, T2, T3, T4>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TAction : struct, IActionArgIn<object, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueAction<TAction, object> ValueAction<TAction, T1, T2, T3, T4, T5>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TAction : struct, IActionArgIn<object, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T>(this object closure, in T arg)
            where TAction : struct, IActionInArgIn<object, T>
        {
            var action = new TAction();
            action.SetArguments(in arg);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T1, T2>(this object closure, in T1 arg1, in T2 arg2)
            where TAction : struct, IActionInArgIn<object, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T1, T2, T3>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TAction : struct, IActionInArgIn<object, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T1, T2, T3, T4>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TAction : struct, IActionInArgIn<object, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T1, T2, T3, T4, T5>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TAction : struct, IActionInArgIn<object, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T>(this object closure, in T arg)
            where TAction : struct, IActionRefArgIn<object, T>
        {
            var action = new TAction();
            action.SetArguments(in arg);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T1, T2>(this object closure, in T1 arg1, in T2 arg2)
            where TAction : struct, IActionRefArgIn<object, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T1, T2, T3>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TAction : struct, IActionRefArgIn<object, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T1, T2, T3, T4>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TAction : struct, IActionRefArgIn<object, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T1, T2, T3, T4, T5>(this object closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TAction : struct, IActionRefArgIn<object, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T>(this TClosure closure, in T arg)
            where TAction : struct, IActionArgIn<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(in arg);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T1, T2>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TAction : struct, IActionArgIn<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T1, T2, T3>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TAction : struct, IActionArgIn<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TAction : struct, IActionArgIn<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TAction : struct, IActionArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T>(this TClosure closure, in T arg)
            where TAction : struct, IActionInArgIn<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(in arg);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T1, T2>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T1, T2, T3>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TAction : struct, IActionInArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T>(this TClosure closure, in T arg)
            where TAction : struct, IActionRefArgIn<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(in arg);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T1, T2>(this TClosure closure, in T1 arg1, in T2 arg2)
            where TAction : struct, IActionRefArgIn<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T1, T2, T3>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3)
            where TAction : struct, IActionRefArgIn<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
            where TAction : struct, IActionRefArgIn<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
            where TAction : struct, IActionRefArgIn<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(in arg1, in arg2, in arg3, in arg4, in arg5);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }
    }
}