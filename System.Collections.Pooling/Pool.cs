namespace System.Collections.Pooling
{
    public static partial class Pool
    {
        public static IPoolProvider Provider
            => _provider ?? _defaultProvider;

        private static readonly DefaultProvider _defaultProvider;
        private static IPoolProvider _provider;

        static Pool()
        {
            _provider = _defaultProvider = new DefaultProvider();
        }

        public static void Set(IPoolProvider provider)
            => _provider = provider ?? _defaultProvider;

        public static void Set<T>() where T : IPoolProvider, new()
            => _provider = new T();
    }
}