using System.Delegates;

namespace System.ValueDelegates
{
    public readonly struct ValueFunc<TFunc, TClosure, TResult> : IFunc<TResult>
        where TFunc : struct, IFunc<TClosure, TResult>
    {
        private readonly TFunc func;
        private readonly TClosure closure;

        public ValueFunc(TClosure closure)
        {
            this.func = new TFunc();
            this.closure = closure;
        }

        public ValueFunc(TFunc action, TClosure closure)
        {
            this.func = action;
            this.closure = closure;
        }

        public ValueFunc(in TFunc action, TClosure closure)
        {
            this.func = action;
            this.closure = closure;
        }

        public ValueFunc(TFunc action, in TClosure closure)
        {
            this.func = action;
            this.closure = closure;
        }

        public TResult Invoke()
            => this.func.Invoke(this.closure);
    }
}