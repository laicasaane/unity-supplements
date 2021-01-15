using System.Delegates;

namespace System.ValueDelegates
{
    public readonly struct ValuePredicateIn<TPredicate, TClosure> : IPredicate
        where TPredicate : struct, IPredicateIn<TClosure>
    {
        private readonly TPredicate predicate;
        private readonly TClosure closure;

        public ValuePredicateIn(in TClosure closure)
        {
            this.predicate = new TPredicate();
            this.closure = closure;
        }

        public ValuePredicateIn(TPredicate action, in TClosure closure)
        {
            this.predicate = action;
            this.closure = closure;
        }

        public ValuePredicateIn(in TPredicate action, in TClosure closure)
        {
            this.predicate = action;
            this.closure = closure;
        }

        public bool Invoke()
            => this.predicate.Invoke(in this.closure);
    }
}