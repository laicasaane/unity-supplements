namespace System.Collections.Pooling.Concurrent
{
    public static partial class ConcurrentPool
    {
        public static IConcurrentPoolProvider Provider
            => _provider ?? _defaultProvider;

        private static readonly DefaultProvider _defaultProvider;
        private static IConcurrentPoolProvider _provider;

        static ConcurrentPool()
        {
            _provider = _defaultProvider = new DefaultProvider();
        }

        public static void Set(IConcurrentPoolProvider provider)
            => _provider = provider ?? _defaultProvider;

        public static void Set<T>() where T : IConcurrentPoolProvider, new()
            => _provider = new T();
    }
}