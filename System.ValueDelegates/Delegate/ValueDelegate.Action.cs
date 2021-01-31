using System.Delegates;

namespace System.ValueDelegates
{
    public static partial class ValueDelegate
    {
        public static ValueAction<TAction, object> ValueAction<TAction>(this object closure)
            where TAction : struct, IAction<object>
            => new ValueAction<TAction, object>(new TAction(), closure);

        public static ValueAction<TAction, object> ValueAction<TAction, T>(this object closure, T arg)
            where TAction : struct, IAction<object, T>
        {
            var action = new TAction();
            action.SetArguments(arg);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueAction<TAction, object> ValueAction<TAction, T1, T2>(this object closure, T1 arg1, T2 arg2)
            where TAction : struct, IAction<object, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueAction<TAction, object> ValueAction<TAction, T1, T2, T3>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IAction<object, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueAction<TAction, object> ValueAction<TAction, T1, T2, T3, T4>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IAction<object, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueAction<TAction, object>(in action, closure);
        }

        public static ValueAction<TAction, object> ValueAction<TAction, T1, T2, T3, T4, T5>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IAction<object, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueAction<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction>(this object closure)
            where TAction : struct, IActionIn<object>
            => new ValueActionIn<TAction, object>(new TAction(), closure);

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T>(this object closure, T arg)
            where TAction : struct, IActionIn<object, T>
        {
            var action = new TAction();
            action.SetArguments(arg);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T1, T2>(this object closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionIn<object, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T1, T2, T3>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionIn<object, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T1, T2, T3, T4>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionIn<object, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionIn<TAction, object> ValueActionIn<TAction, T1, T2, T3, T4, T5>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionIn<object, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueActionIn<TAction, object>(in action, in closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction>(this object closure)
            where TAction : struct, IActionRef<object>
            => new ValueActionRef<TAction, object>(new TAction(), ref closure);

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T>(this object closure, T arg)
            where TAction : struct, IActionRef<object, T>
        {
            var action = new TAction();
            action.SetArguments(arg);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T1, T2>(this object closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionRef<object, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T1, T2, T3>(this object closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionRef<object, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T1, T2, T3, T4>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionRef<object, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueActionRef<TAction, object> ValueActionRef<TAction, T1, T2, T3, T4, T5>(this object closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionRef<object, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueActionRef<TAction, object>(in action, ref closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure>(this TClosure closure)
            where TAction : struct, IAction<TClosure>
            => new ValueAction<TAction, TClosure>(new TAction(), closure);

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T>(this TClosure closure, T arg)
            where TAction : struct, IAction<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(arg);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IAction<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IAction<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IAction<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueAction<TAction, TClosure>(in action, closure);
        }

        public static ValueAction<TAction, TClosure> ValueAction<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IAction<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueAction<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure>(this TClosure closure)
            where TAction : struct, IActionIn<TClosure>
            => new ValueActionIn<TAction, TClosure>(new TAction(), in closure);

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T>(this TClosure closure, T arg)
            where TAction : struct, IActionIn<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(arg);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionIn<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionIn<TAction, TClosure> ValueActionIn<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionIn<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueActionIn<TAction, TClosure>(in action, in closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure>(this TClosure closure)
            where TAction : struct, IActionRef<TClosure>
            => new ValueActionRef<TAction, TClosure>(new TAction(), ref closure);

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T>(this TClosure closure, T arg)
            where TAction : struct, IActionRef<TClosure, T>
        {
            var action = new TAction();
            action.SetArguments(arg);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T1, T2>(this TClosure closure, T1 arg1, T2 arg2)
            where TAction : struct, IActionRef<TClosure, T1, T2>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T1, T2, T3>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T1, T2, T3, T4>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3, T4>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }

        public static ValueActionRef<TAction, TClosure> ValueActionRef<TAction, TClosure, T1, T2, T3, T4, T5>(this TClosure closure, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            where TAction : struct, IActionRef<TClosure, T1, T2, T3, T4, T5>
        {
            var action = new TAction();
            action.SetArguments(arg1, arg2, arg3, arg4, arg5);

            return new ValueActionRef<TAction, TClosure>(in action, ref closure);
        }
    }
}