using System.Delegates;

namespace System.ValueDelegates
{
    public struct ValuePredicateRef<TPredicate, TClosure> : IPredicate
        where TPredicate : struct, IPredicateRef<TClosure>
    {
        private readonly TPredicate predicate;
        private TClosure closure;

        public ValuePredicateRef(ref TClosure closure)
        {
            this.predicate = new TPredicate();
            this.closure = closure;
        }

        public ValuePredicateRef(TPredicate action, ref TClosure closure)
        {
            this.predicate = action;
            this.closure = closure;
        }

        public ValuePredicateRef(in TPredicate action, ref TClosure closure)
        {
            this.predicate = action;
            this.closure = closure;
        }

        public bool Invoke()
            => this.predicate.Invoke(ref this.closure);
    }
}