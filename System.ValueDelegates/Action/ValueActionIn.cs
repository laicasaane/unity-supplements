using System.Delegates;

namespace System.ValueDelegates
{
    public readonly struct ValueActionIn<TAction, TClosure> : IAction
        where TAction : struct, IActionIn<TClosure>
    {
        private readonly TAction action;
        private readonly TClosure closure;

        public ValueActionIn(in TClosure closure)
        {
            this.action = new TAction();
            this.closure = closure;
        }

        public ValueActionIn(TAction action, in TClosure closure)
        {
            this.action = action;
            this.closure = closure;
        }

        public ValueActionIn(in TAction action, in TClosure closure)
        {
            this.action = action;
            this.closure = closure;
        }

        public void Invoke()
            => this.action.Invoke(in this.closure);
    }
}