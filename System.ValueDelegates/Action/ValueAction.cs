using System.Delegates;

namespace System.ValueDelegates
{
    public readonly struct ValueAction<TAction, TClosure> : IAction
        where TAction : struct, IAction<TClosure>
    {
        private readonly TAction action;
        private readonly TClosure closure;

        public ValueAction(TClosure closure)
        {
            this.action = new TAction();
            this.closure = closure;
        }

        public ValueAction(in TClosure closure)
        {
            this.action = new TAction();
            this.closure = closure;
        }

        public ValueAction(TAction action, TClosure closure)
        {
            this.action = action;
            this.closure = closure;
        }

        public ValueAction(in TAction action, TClosure closure)
        {
            this.action = action;
            this.closure = closure;
        }

        public ValueAction(TAction action, in TClosure closure)
        {
            this.action = action;
            this.closure = closure;
        }

        public ValueAction(in TAction action, in TClosure closure)
        {
            this.action = action;
            this.closure = closure;
        }

        public void Invoke()
            => this.action.Invoke(this.closure);
    }
}