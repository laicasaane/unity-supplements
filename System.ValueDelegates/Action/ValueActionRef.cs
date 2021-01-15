using System.Delegates;

namespace System.ValueDelegates
{
    public struct ValueActionRef<TAction, TClosure> : IAction
        where TAction : struct, IActionRef<TClosure>
    {
        private readonly TAction action;
        private TClosure closure;

        public ValueActionRef(ref TClosure closure)
        {
            this.action = new TAction();
            this.closure = closure;
        }

        public ValueActionRef(TAction action, ref TClosure closure)
        {
            this.action = action;
            this.closure = closure;
        }

        public ValueActionRef(in TAction action, ref TClosure closure)
        {
            this.action = action;
            this.closure = closure;
        }

        public void Invoke()
            => this.action.Invoke(ref this.closure);
    }
}