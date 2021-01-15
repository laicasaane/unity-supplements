using System.Delegates;

namespace System.ValueDelegates
{
    public struct ValueFuncRef<TFunc, TClosure, TResult> : IFunc<TResult>
        where TFunc : struct, IFuncRef<TClosure, TResult>
    {
        private readonly TFunc func;
        private TClosure closure;

        public ValueFuncRef(ref TClosure closure)
        {
            this.func = new TFunc();
            this.closure = closure;
        }

        public ValueFuncRef(TFunc action, ref TClosure closure)
        {
            this.func = action;
            this.closure = closure;
        }

        public ValueFuncRef(in TFunc action, ref TClosure closure)
        {
            this.func = action;
            this.closure = closure;
        }

        public TResult Invoke()
            => this.func.Invoke(ref this.closure);
    }
}