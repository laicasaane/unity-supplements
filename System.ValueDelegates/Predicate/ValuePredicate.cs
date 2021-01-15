using System.Delegates;

namespace System.ValueDelegates
{
    public readonly struct ValuePredicate<TPredicate, TClosure> : IPredicate
        where TPredicate : struct, IPredicate<TClosure>
    {
        private readonly TPredicate predicate;
        private readonly TClosure closure;

        public ValuePredicate(TClosure closure)
        {
            this.predicate = new TPredicate();
            this.closure = closure;
        }

        public ValuePredicate(in TClosure closure)
        {
            this.predicate = new TPredicate();
            this.closure = closure;
        }

        public ValuePredicate(TPredicate action, TClosure closure)
        {
            this.predicate = action;
            this.closure = closure;
        }

        public ValuePredicate(TPredicate action, in TClosure closure)
        {
            this.predicate = action;
            this.closure = closure;
        }

        public ValuePredicate(in TPredicate action, TClosure closure)
        {
            this.predicate = action;
            this.closure = closure;
        }

        public ValuePredicate(in TPredicate action, in TClosure closure)
        {
            this.predicate = action;
            this.closure = closure;
        }

        public bool Invoke()
            => this.predicate.Invoke(this.closure);
    }
}