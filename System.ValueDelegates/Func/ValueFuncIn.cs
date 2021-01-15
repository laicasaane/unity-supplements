using System.Delegates;

namespace System.ValueDelegates
{
    public readonly struct ValueFuncIn<TFunc, TClosure, TResult> : IFunc<TResult>
        where TFunc : struct, IFuncIn<TClosure, TResult>
    {
        private readonly TFunc func;
        private readonly TClosure closure;

        public ValueFuncIn(in TClosure closure)
        {
            this.func = new TFunc();
            this.closure = closure;
        }

        public ValueFuncIn(TFunc action, in TClosure closure)
        {
            this.func = action;
            this.closure = closure;
        }

        public ValueFuncIn(in TFunc action, in TClosure closure)
        {
            this.func = action;
            this.closure = closure;
        }

        public TResult Invoke()
            => this.func.Invoke(in this.closure);
    }
}